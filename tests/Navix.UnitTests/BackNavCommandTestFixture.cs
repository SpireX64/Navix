using Moq;
using Navix.Commands;
using Navix.UnitTests.Stubs;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class BackNavCommandTestFixture
    {
        [Test]
        public void ApplyBackNavCommandTest()
        {
            // - Arrange
            var screenStack = new ScreenStack();
            screenStack.Push(new ScreenStub());
            var navigatorMock = new Mock<Navigator>();
            
            var command = new BackNavCommand();

            // - Act
            command.Apply(navigatorMock.Object, screenStack);

            // - Assert
            Assert.AreEqual(0, screenStack.Count);
            navigatorMock.Verify(it => it.Back(), Times.Once);
        }
    }
}