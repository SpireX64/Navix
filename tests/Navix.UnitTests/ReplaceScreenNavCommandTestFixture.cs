using Moq;
using Navix.Commands;
using Navix.UnitTests.Stubs;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class ReplaceScreenNavCommandTestFixture
    {
        [Test]
        public void ApplyReplaceScreenNavCommandTest()
        {
            // - Arrange
            var screen = new ScreenStub();
            var resolverStub = new Mock<IScreenResolver>().Object;

            var screenStack = new ScreenStack();
            var oldScreen = new ScreenStub();
            screenStack.Push(oldScreen);

            var navigatorMock = new Mock<Navigator>();
            
            var command = new ReplaceScreenNavCommand(screen, resolverStub);

            // - Act
            command.Apply(navigatorMock.Object, screenStack);

            // - Assert
            Assert.AreNotEqual(oldScreen, screenStack.CurrentScreen);
            Assert.AreEqual(screen, screenStack.CurrentScreen);
            Assert.AreEqual(1, screenStack.Count);
            navigatorMock.Verify(it => it.Replace(screen, resolverStub), Times.Once);
        }
    }
}