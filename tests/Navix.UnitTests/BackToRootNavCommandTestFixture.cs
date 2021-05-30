using Moq;
using Navix.Commands;
using Navix.UnitTests.Stubs;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class BackToRootNavCommandTestFixture
    {
        [Test]
        public void ApplyBackToRootCommandTest()
        {
            // - Arrange
            var screenStack = new ScreenStack();
            var rootScreen = new ScreenStub();
            screenStack.Push(rootScreen, new ScreenStub());
            
            var navigatorMock = new Mock<Navigator>();

            var command = new BackToRootNavCommand();

            // - Act
            command.Apply(navigatorMock.Object, screenStack);

            // - Assert
            Assert.IsTrue(screenStack.IsRoot);
            Assert.AreEqual(1, screenStack.Count);
            Assert.AreEqual(rootScreen, screenStack.CurrentScreen);
            navigatorMock.Verify(it => it.BackToRoot(), Times.Once);
        }

        [Test]
        public void ApplyBackToRootOnRootScreenTest()
        {
            // - Arrange
            var screenStack = new ScreenStack();
            
            var navigatorMock = new Mock<Navigator>();

            var command = new BackToRootNavCommand();

            // - Act
            command.Apply(navigatorMock.Object, screenStack);

            // - Assert
            Assert.IsTrue(screenStack.IsRoot);
            navigatorMock.Verify(it => it.BackToRoot(), Times.Never);
        }
    }
}