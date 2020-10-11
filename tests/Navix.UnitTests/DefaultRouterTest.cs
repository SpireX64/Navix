using System;
using System.Collections.Generic;
using Moq;
using Spx.Navix.Abstractions;
using Spx.Navix.Internal.Defaults;
using Spx.Navix.UnitTests.Stubs;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class DefaultRouterTest
    {
        [Fact]
        public void DefaultRouter_CreateWithNullManager_ThrowsArgumentNull()
        {
            // -- Arrange
            var commandsFactoryStub = new Mock<ICommandsFactory>();

            // -- Act & Assert:
            Assert.Throws<ArgumentNullException>(
                () => new DefaultRouter(null!, commandsFactoryStub.Object));
        }

        [Fact]
        public void DefaultRouter_CreateWithNullCommandsFactory_ThrowsArgumentNull()
        {
            // -- Arrange
            var navManagerStub = new Mock<INavigationManager>();

            // -- Act & Assert:
            Assert.Throws<ArgumentNullException>(
                () => new DefaultRouter(navManagerStub.Object, null!));
        }

        [Fact]
        public void DefaultRouter_NavigateForward_SendForwardCommand()
        {
            // -- Arrange:
            var screen = new ScreenStub1();
            var commands = new INavCommand[] { };
            var managerMock = new Mock<INavigationManager>();

            var commandsFactoryMock = new Mock<ICommandsFactory>();
            commandsFactoryMock.Setup(e => e.Forward(screen)).Returns(commands);

            var router = new DefaultRouter(managerMock.Object, commandsFactoryMock.Object);

            // -- Act:
            router.Forward(screen);

            // -- Assert:
            commandsFactoryMock.Verify(e => e.Forward(screen), Times.Once);
            managerMock.Verify(e => e.SendCommands(commands), Times.Once);
        }

        [Fact]
        public void DefaultRouter_NavigateBack_SendBackCommand()
        {
            // -- Arrange:
            var commands = new INavCommand[] { };
            var managerMock = new Mock<INavigationManager>();
            var commandsFactoryMock = new Mock<ICommandsFactory>();
            commandsFactoryMock.Setup(e => e.Back()).Returns(commands);

            var router = new DefaultRouter(managerMock.Object, commandsFactoryMock.Object);

            // -- Act:
            router.Back();

            // -- Assert:
            commandsFactoryMock.Verify(e => e.Back());
            managerMock.Verify(e => e.SendCommands(commands), Times.Once);
        }

        [Fact]
        public void DefaultRouter_NavigateBackToScreen_SendBackToScreenCommand()
        {
            // -- Arrange:
            var commands = new INavCommand[] { };
            var screenType = typeof(ScreenStub1);
            var managerMock = new Mock<INavigationManager>();
            var spec = new NavigatorSpecification();

            var commandsFactoryMock = new Mock<ICommandsFactory>();
            commandsFactoryMock
                .Setup(e => e.BackToScreen(It.IsAny<IEnumerable<Screen>>(), spec, screenType))
                .Returns(commands);

            var router = new DefaultRouter(managerMock.Object, commandsFactoryMock.Object);

            // -- Act:
            router.BackToScreen(screenType);

            // -- Assert:
            commandsFactoryMock.Verify(e => e.BackToScreen(It.IsAny<IEnumerable<Screen>>(), spec, screenType));
            managerMock.Verify(e => e.SendCommands(commands), Times.Once);
        }

        [Fact]
        public void DefaultRouter_NavigateBackToRoot_SendBackToRootCommand()
        {
            // -- Arrange:
            var commands = new INavCommand[] { };
            var managerMock = new Mock<INavigationManager>();
            var spec = new NavigatorSpecification();

            var cmdFactoryMock = new Mock<ICommandsFactory>();
            cmdFactoryMock
                .Setup(e => e.BackToRoot(It.IsAny<IEnumerable<Screen>>(), spec))
                .Returns(commands);

            var router = new DefaultRouter(managerMock.Object, cmdFactoryMock.Object);

            // -- Act
            router.BackToRoot();

            // -- Assert
            cmdFactoryMock.Verify(e => e.BackToRoot(It.IsAny<IEnumerable<Screen>>(), spec));
            managerMock.Verify(e => e.SendCommands(commands), Times.Once);
        }

        [Fact]
        public void DefaultRouter_ReplaceScreen_SendReplaceScreenCommand()
        {
            // -- Arrange:
            var screen = new ScreenStub1();
            var commands = new INavCommand[] { };
            var managerMock = new Mock<INavigationManager>();
            var spec = new NavigatorSpecification();

            var cmdFactoryMock = new Mock<ICommandsFactory>();
            cmdFactoryMock
                .Setup(e => e.ReplaceScreen(It.IsAny<IEnumerable<Screen>>(), spec, screen))
                .Returns(commands);

            var router = new DefaultRouter(managerMock.Object, cmdFactoryMock.Object);

            // -- Act:
            router.Replace(screen);

            // -- Assert:
            cmdFactoryMock.Verify(e => e.ReplaceScreen(It.IsAny<IEnumerable<Screen>>(), spec, screen));
            managerMock.Verify(e => e.SendCommands(commands), Times.Once);
        }

        [Fact]
        public void DefaultRouter_GetScreens_ReturnsCurrentScreens()
        {
            // -- Arrange:
            var stack = new ScreenStack();
            var managerMock = new Mock<INavigationManager>();
            managerMock
                .SetupGet(e => e.Screens)
                .Returns(stack);

            var cmdFactoryStub = new Mock<ICommandsFactory>().Object;
            var router = new DefaultRouter(managerMock.Object, cmdFactoryStub);

            // -- Act:
            var screens = router.Screens;

            // -- Assert:
            Assert.NotNull(screens);
            Assert.Equal(stack, screens);
        }
    }
}