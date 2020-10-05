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
            var navigatorFake = new Mock<Navigator>().Object;
            var manager = new NavigationManager();
            
            // -- Act:
            manager.SetNavigator(navigatorFake);
            
            // -- Assert:
            Assert.NotNull(manager.Navigator);
            Assert.Equal(navigatorFake, manager.Navigator);
        }

        [Fact]
        public void NavigatorManager_RemoveNavigator_NavigatorRemoved()
        {
            // -- Arrange:
            var navigatorFake = new Mock<Navigator>().Object;
            var manager = new NavigationManager();
            manager.SetNavigator(navigatorFake);
            
            // -- Act:
            manager.RemoveNavigator();
            
            // -- Assert:
            Assert.Null(manager.Navigator);
        }

        [Fact]
        public void NavigatorManager_CreateInstance_NoPendingCommandsAndNavigator()
        {
            // -- Arrange:
            var manager = new NavigationManager();
            
            // -- Assert:
            Assert.Null(manager.Navigator);
            Assert.False(manager.HasPendingCommands);
        }

        [Fact]
        public void NavigatorManager_SendCommandsWithoutNavigator()
        {
            // -- Arrange:
            var commandMock = new Mock<INavCommand>();
            var commands = new[] { commandMock.Object };
            var manager = new NavigationManager();
            
            // -- Act:
            manager.SendCommands(commands);
            
            // -- Assert
            Assert.True(manager.HasPendingCommands);
            commandMock.Verify(
                e => e.Apply(It.IsAny<Navigator>()), Times.Never);
        }

        [Fact]
        public void NavigationManager_SendCommandsWithNavigator()
        {
            // -- Arrange:
            var commandMock = new Mock<INavCommand>();
            var commands = new[] { commandMock.Object };
            var navigatorStub = new Mock<Navigator>().Object;
            
            var manager = new NavigationManager();
            manager.SetNavigator(navigatorStub);
            
            // -- Act:
            manager.SendCommands(commands);
            
            // -- Arrange:
            Assert.False(manager.HasPendingCommands);
            commandMock.Verify(
                e => e.Apply(navigatorStub), Times.Once);
        }

        [Fact]
        public void NavigationManager_SetNavigatorAfterSendCommands_ApplyPendingCommands()
        {
            // -- Arrange:
            var commandMock = new Mock<INavCommand>();
            var commands = new[] { commandMock.Object };
            var navigatorStub = new Mock<Navigator>().Object;
            
            var manager = new NavigationManager();
            
            // -- Act:
            manager.SendCommands(commands);
            manager.SetNavigator(navigatorStub);
            
            // -- Assert:
            Assert.False(manager.HasPendingCommands);
            commandMock.Verify(
                e => e.Apply(navigatorStub), Times.Once);
        }

        [Fact]
        public void NavigationManager_RemoveNavigatorOnSendCommands_PendingCommand()
        {
            // -- Arrange:
            var manager = new NavigationManager();
            
            var pendingCommandMock = new Mock<INavCommand>();
            var removeNavigatorCommandMock = new Mock<INavCommand>();
            removeNavigatorCommandMock.Setup(e => e.Apply(It.IsAny<Navigator>()))
                .Callback<Navigator>((navigator) => { manager.RemoveNavigator(); });
            
            var commands = new[] {removeNavigatorCommandMock.Object, pendingCommandMock.Object};
            var navigatorStub = new Mock<Navigator>().Object;
            manager.SetNavigator(navigatorStub);

            // -- Act:
            manager.SendCommands(commands);
            
            // -- Assert:
            Assert.Null(manager.Navigator);
            Assert.True(manager.HasPendingCommands);
            removeNavigatorCommandMock.Verify(
                e => e.Apply(It.IsAny<Navigator>()), Times.Once);
            pendingCommandMock.Verify(
                e => e.Apply(It.IsAny<Navigator>()), Times.Never);
        }

        [Fact]
        public void NavigationManager_RemoveNavigatorOnApplyPendingCommand_BreakApplying()
        {
            // -- Arrange:
            var manager = new NavigationManager();
            
            var removeNavigatorCommandMock = new Mock<INavCommand>();
            var pendingCommandMock = new Mock<INavCommand>();
            removeNavigatorCommandMock.Setup(e => e.Apply(It.IsAny<Navigator>()))
                .Callback<Navigator>((navigator) => { manager.RemoveNavigator(); });
            var commands = new[] {removeNavigatorCommandMock.Object, pendingCommandMock.Object};
            
            var navigatorStub = new Mock<Navigator>().Object;
            
            manager.SendCommands(commands);
            
            // -- Act:
            manager.SetNavigator(navigatorStub);
            
            // -- Assert:
            Assert.Null(manager.Navigator);
            Assert.True(manager.HasPendingCommands);
            removeNavigatorCommandMock.Verify(
                e => e.Apply(It.IsAny<Navigator>()), Times.Once);
            pendingCommandMock.Verify(
                e => e.Apply(It.IsAny<Navigator>()), Times.Never);
        }
    }
}