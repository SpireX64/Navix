using Moq;
using Spx.Navix.Abstractions;
using Spx.Navix.Commands;
using Spx.Navix.Internal;
using Spx.Navix.UnitTests.Stubs;
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

        [Fact]
        public void NavigationManager_UseMiddleware_MiddlewareInvoked()
        {
            // -- Arrange:
            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);
            var navigatorMock = new Mock<Navigator>();
            var middlewareMock = new Mock<INavigationMiddleware>();

            manager.SetNavigator(navigatorMock.Object);
            manager.SetMiddlewares(new[] {middlewareMock.Object});

            var screen = new ScreenStub1();
            INavCommand command = new ForwardNavCommand(screen, new ScreenResolverStub1());

            // -- Act:
            manager.SendCommands(new[] {command});

            // -- Assert:
            middlewareMock.Verify(
                e => e.BeforeApply(null, ref command), Times.Once);
            middlewareMock.Verify(
                e => e.AfterApply(screen, command));
        }

        [Fact]
        public void NavigationManager_ReplaceCommandByMiddleware_CommandReplaced()
        {
            // -- Arrange:
            var firstScreen = new ScreenStub1();
            var firstScreenResolver = new ScreenResolverStub1();
            var firstCommand = new ForwardNavCommand(firstScreen, firstScreenResolver);

            var secondScreen = new ScreenStub2();
            var secondScreenResolver = new ScreenResolverStub1();
            var secondCommand = new ForwardNavCommand(secondScreen, secondScreenResolver);
            var middleware = new ReplaceForwardCommandMiddleware(secondCommand);

            var registryStub = new Mock<IScreenRegistry>().Object;
            var navigatorMock = new Mock<Navigator>();
            var manager = new NavigationManager(registryStub);
            manager.SetNavigator(navigatorMock.Object);
            manager.SetMiddlewares(new[] {middleware});

            // -- Act:
            manager.SendCommands(new[] {firstCommand});

            // -- Assert:
            Assert.Equal(secondCommand, middleware.ExecutedCommand);
            navigatorMock.Verify(
                e => e.Forward(firstScreen, firstScreenResolver), Times.Never);
            navigatorMock.Verify(
                e => e.Forward(secondScreen, secondScreenResolver), Times.Once);
        }

        [Fact]
        public void NavigationManager_GetSpecificationWithoutNavigator_SpecAllFalse()
        {
            // -- Arrange:
            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);

            // -- Act:
            var spec = manager.Specification;

            // -- Assert:
            Assert.False(spec.ReplaceScreenSupported);
            Assert.False(spec.BackToScreenSupported);
            Assert.False(spec.BackToRootSupported);
        }

        [Fact]
        public void NavigationManager_GetSpecificationWithNavigator_ReturnsNavigatorSpecs()
        {
            // -- Arrange:
            var registryStub = new Mock<IScreenRegistry>().Object;
            var spec = new NavigatorSpecification();
            var navigatorMock = new Mock<Navigator>();
            navigatorMock
                .SetupGet(e => e.Specification)
                .Returns(spec);

            var manager = new NavigationManager(registryStub);

            // -- Act:
            var specFromManager = manager.Specification;

            // -- Assert:
            Assert.Equal(spec, specFromManager);
        }

        [Fact]
        public void NavigationManager_GetScreens_ReturnsEmptyEnumerableByDefault()
        {
            // -- Arrange:
            var registryStub = new Mock<IScreenRegistry>().Object;
            var manager = new NavigationManager(registryStub);

            // -- Act:
            var screens = manager.Screens;

            // -- Assert:
            Assert.NotNull(screens);
            Assert.Empty(screens);
        }
    }
}