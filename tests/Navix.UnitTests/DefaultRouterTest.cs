using System;
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
            var commandsFactoryMock = new Mock<ICommandsFactory>();
            commandsFactoryMock
                .Setup(e => e.BackToScreen(screenType))
                .Returns(commands);

            var router = new DefaultRouter(managerMock.Object, commandsFactoryMock.Object);
            
            // -- Act:
            router.BackToScreen(screenType);
            
            // -- Assert:
            commandsFactoryMock.Verify(e => e.BackToScreen(screenType));
            managerMock.Verify(e => e.SendCommands(commands), Times.Once);
        }

        [Fact]
        public void DefaultRouter_NavigateBackToRoot_SendBackToRootCommand()
        {
            // -- Arrange:
            var commands = new INavCommand[] { };
            var managerMock = new Mock<INavigationManager>();
            var cmdFactoryMock = new Mock<ICommandsFactory>();
            cmdFactoryMock
                .Setup(e => e.BackToRoot())
                .Returns(commands);
            
            var router = new DefaultRouter(managerMock.Object, cmdFactoryMock.Object);
            
            // -- Act
            router.BackToRoot();
            
            // -- Assert
            cmdFactoryMock.Verify(e => e.BackToRoot());
            managerMock.Verify(e => e.SendCommands(commands), Times.Once);
        }
    }
}