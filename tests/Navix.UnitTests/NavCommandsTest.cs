using Moq;
using Spx.Navix.Commands;
using Spx.Navix.Platform;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class NavCommandsTest
    {
        [Fact]
        public void ForwardNavCommand_Apply_InvokeForward()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();

            var screenFake = new Mock<Screen>().Object;
            var resolverFake = new Mock<IScreenResolver>().Object;
            var command = new ForwardNavCommand(screenFake, resolverFake);
            
            // -- Act:
            command.Apply(navigatorMock.Object);
            
            // -- Assert
            navigatorMock.Verify(
                e => e.Forward(screenFake, resolverFake), Times.Once);
        }
        
        [Fact]
        public void BackNavCommand_Apply_InvokeBack()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();

            var command = new BackNavCommand();
            
            // -- Act:
            command.Apply(navigatorMock.Object);
            
            // -- Assert
            navigatorMock.Verify(
                e => e.Back(), Times.Once);
        }
        
        [Fact]
        public void BackToNavCommand_Apply_InvokeBackTo()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();

            var screenFake = new Mock<Screen>().Object;
            var command = new BackToNavCommand(screenFake);
            
            // -- Act:
            command.Apply(navigatorMock.Object);
            
            // -- Assert
            navigatorMock.Verify(
                e => e.BackTo(screenFake), Times.Once);
        }
        
        [Fact]
        public void BackToRootNavCommand_Apply_InvokeBackToRoot()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();

            var command = new BackToRootNavCommand();
            
            // -- Act:
            command.Apply(navigatorMock.Object);
            
            // -- Assert
            navigatorMock.Verify(
                e => e.BackToRoot(), Times.Once);
        }
        
        [Fact]
        public void UpdateNavCommand_Apply_InvokeUpdate()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();

            var screenFake = new Mock<Screen>().Object;
            var command = new UpdateNavCommand(screenFake);
            
            // -- Act:
            command.Apply(navigatorMock.Object);
            
            // -- Assert
            navigatorMock.Verify(
                e => e.Update(screenFake), Times.Once);
        }
    }
}