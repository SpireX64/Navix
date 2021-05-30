using Moq;
using Navix.Commands;
using Navix.Exceptions;
using Navix.UnitTests.Stubs;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class BackToScreenNavCommandTestFixture
    {
        [Test]
        public void ApplyBackToScreenCommandTest()
        {
            // - Arrange
            var screenType = typeof(ScreenStub);
            var screenStack = new ScreenStack();

            var expectedScreen = new ScreenStub();
            var screen = new Mock<Screen>().Object;
            screenStack.Push(expectedScreen, screen, screen);

            var navigatorMock = new Mock<Navigator>();
            
            var command = new BackToScreenNavCommand(screenType);

            // - Act
            command.Apply(navigatorMock.Object, screenStack);

            // - Assert
            Assert.AreEqual(1, screenStack.Count);
            Assert.AreEqual(expectedScreen, screenStack.CurrentScreen);
            navigatorMock.Verify(it => it.BackToScreen(expectedScreen), Times.Once);
        }

        [Test]
        public void ApplyBackToScreenButScreenNotFoundTest()
        {
            // - Arrange
            var screenType = typeof(ScreenStub);
            var screenStack = new ScreenStack();

            var navigatorMock = new Mock<Navigator>();
            
            var command = new BackToScreenNavCommand(screenType);

            // - Act
            var exception = Assert.Throws<ScreenNotFoundException>(
                () => command.Apply(navigatorMock.Object, screenStack));
            Assert.AreEqual(screenType, exception!.ScreenClass.Type);
            navigatorMock.Verify(it => it.BackToScreen(It.IsAny<Screen>()), Times.Never);

        }
    }
}