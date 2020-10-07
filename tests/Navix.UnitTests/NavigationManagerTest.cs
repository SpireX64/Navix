using Moq;
using Spx.Navix.Abstractions;
using Spx.Navix.Internal;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class NavigationManagerTest
    {
        [Fact]
        public void NavigationManager_SetNavigator_NavigatorApplied()
        {
            // -- Arrange:
            var navigatorStub = new Mock<Navigator>().Object;
            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);

            // -- Act:
            manager.SetNavigator(navigatorStub);

            // -- Assert:
            Assert.NotNull(manager.Navigator);
            Assert.Equal(navigatorStub, manager.Navigator);
        }

        [Fact]
        public void NavigationManager_RemoveNavigator_NavigatorRemoved()
        {
            // -- Arrange:
            var navigatorStub = new Mock<Navigator>().Object;
            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);
            manager.SetNavigator(navigatorStub);

            // -- Act:
            manager.RemoveNavigator();

            // -- Assert:
            Assert.Null(manager.Navigator);
        }

        [Fact]
        public void NavigationManager_CreateInstance_NoPendingCommandsAndNavigator()
        {
            // -- Arrange:
            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);

            // -- Assert:
            Assert.Null(manager.Navigator);
            Assert.False(manager.HasPendingCommands);
        }

        [Fact]
        public void NavigationManager_SendCommandsWithoutNavigator()
        {
            // -- Arrange:
            var commandMock = new Mock<INavCommand>();
            var commands = new[] {commandMock.Object};
            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);

            // -- Act:
            manager.SendCommands(commands);

            // -- Assert
            Assert.True(manager.HasPendingCommands);
            commandMock.Verify(
                e => e.Apply(It.IsAny<Navigator>(), It.IsAny<ScreenStack>()), Times.Never);
        }

        [Fact]
        public void NavigationManager_SendCommandsWithNavigator()
        {
            // -- Arrange:
            var commandMock = new Mock<INavCommand>();
            var commands = new[] {commandMock.Object};
            var navigatorStub = new Mock<Navigator>().Object;

            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);
            manager.SetNavigator(navigatorStub);

            // -- Act:
            manager.SendCommands(commands);

            // -- Arrange:
            Assert.False(manager.HasPendingCommands);
            commandMock.Verify(
                e => e.Apply(navigatorStub, It.IsAny<ScreenStack>()), Times.Once);
        }

        [Fact]
        public void NavigationManager_SetNavigatorAfterSendCommands_ApplyPendingCommands()
        {
            // -- Arrange:
            var commandMock = new Mock<INavCommand>();
            var commands = new[] {commandMock.Object};
            var navigatorStub = new Mock<Navigator>().Object;

            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);

            // -- Act:
            manager.SendCommands(commands);
            manager.SetNavigator(navigatorStub);

            // -- Assert:
            Assert.False(manager.HasPendingCommands);
            commandMock.Verify(
                e => e.Apply(navigatorStub, It.IsAny<ScreenStack>()), Times.Once);
        }

        [Fact]
        public void NavigationManager_RemoveNavigatorOnSendCommands_PendingCommand()
        {
            // -- Arrange:
            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);

            var pendingCommandMock = new Mock<INavCommand>();
            var removeNavigatorCommandMock = new Mock<INavCommand>();
            removeNavigatorCommandMock
                .Setup(e => e.Apply(It.IsAny<Navigator>(), It.IsAny<ScreenStack>()))
                .Callback(() => { manager.RemoveNavigator(); });

            var commands = new[] {removeNavigatorCommandMock.Object, pendingCommandMock.Object};
            var navigatorStub = new Mock<Navigator>().Object;
            manager.SetNavigator(navigatorStub);

            // -- Act:
            manager.SendCommands(commands);

            // -- Assert:
            Assert.Null(manager.Navigator);
            Assert.True(manager.HasPendingCommands);
            removeNavigatorCommandMock.Verify(
                e => e.Apply(It.IsAny<Navigator>(), It.IsAny<ScreenStack>()), Times.Once);
            pendingCommandMock.Verify(
                e => e.Apply(It.IsAny<Navigator>(), It.IsAny<ScreenStack>()), Times.Never);
        }

        [Fact]
        public void NavigationManager_RemoveNavigatorOnApplyPendingCommand_BreakApplying()
        {
            // -- Arrange:
            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);

            var removeNavigatorCommandMock = new Mock<INavCommand>();
            var pendingCommandMock = new Mock<INavCommand>();
            removeNavigatorCommandMock
                .Setup(e => e.Apply(It.IsAny<Navigator>(), It.IsAny<ScreenStack>()))
                .Callback(() => { manager.RemoveNavigator(); });
            var commands = new[] {removeNavigatorCommandMock.Object, pendingCommandMock.Object};

            var navigatorStub = new Mock<Navigator>().Object;

            manager.SendCommands(commands);

            // -- Act:
            manager.SetNavigator(navigatorStub);

            // -- Assert:
            Assert.Null(manager.Navigator);
            Assert.True(manager.HasPendingCommands);
            removeNavigatorCommandMock.Verify(
                e => e.Apply(It.IsAny<Navigator>(), It.IsAny<ScreenStack>()), Times.Once);
            pendingCommandMock.Verify(
                e => e.Apply(It.IsAny<Navigator>(), It.IsAny<ScreenStack>()), Times.Never);
        }
    }
}