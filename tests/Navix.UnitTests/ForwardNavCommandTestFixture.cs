using Moq;
using Navix.Commands;
using Navix.UnitTests.Stubs;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class ForwardNavCommandTestFixture
    {
        [Test]
        public void ApplyForwardCommandTest()
        {
            // - Arrange
            var screen = new ScreenStub();
            var screenStack = new ScreenStack();
            
            var screenResorverStub = new Mock<IScreenResolver>().Object;
            var navigatorMock = new Mock<Navigator>();

            var command = new ForwardNavCommand(screen, screenResorverStub);

            // - Act
            command.Apply(navigatorMock.Object, screenStack);

            // - Assert
            CollectionAssert.Contains(screenStack, screen);
            navigatorMock.Verify(
                it => it.Forward(screen, screenResorverStub), 
                Times.Once);
        }
    }
}