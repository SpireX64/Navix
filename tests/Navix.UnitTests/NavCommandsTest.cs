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
    }
}