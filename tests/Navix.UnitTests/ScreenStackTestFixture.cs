using System;
using Moq;
using Navix.UnitTests.Stubs;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class ScreenStackTestFixture
    {
        [Test]
        public void NewScreenStackIsEmpty()
        {
            // - Act
            var screenStack = new ScreenStack();

            // - Assert
            Assert.IsNull(screenStack.CurrentScreen);
            Assert.IsTrue(screenStack.IsRoot);
            Assert.IsEmpty(screenStack);
            Assert.AreEqual(0, screenStack.Count);
        }

        [Test]
        public void PushFirstScreenTest()
        {
            // - Arrange
            var screen = new ScreenStub();
            var screenStack = new ScreenStack();

            // - Act
            screenStack.Push(screen);
            
            // - Assert
            Assert.IsTrue(screenStack.IsRoot);
            Assert.AreEqual(1, screenStack.Count);
            Assert.AreSame(screen, screenStack.CurrentScreen);
        }

        [Test]
        public void PushManyScreensTest()
        {
            // - Arrange
            var screenA = new Mock<Screen>().Object;
            var screenB = new Mock<Screen>().Object;
            
            var screenStack = new ScreenStack();
            
            // - Act
            screenStack.Push(screenA, screenB);
            
            // - Assert
            Assert.IsFalse(screenStack.IsRoot);
            Assert.AreSame(screenB, screenStack.CurrentScreen);
            Assert.AreEqual(2, screenStack.Count);
            CollectionAssert.Contains(screenStack, screenA);
        }

        [Test]
        public void PopScreenTest()
        {
            // - Arrange
            
            var screenA = new Mock<Screen>().Object;
            var screenB = new Mock<Screen>().Object;
            
            var screenStack = new ScreenStack();
            screenStack.Push(screenA, screenB);

            // - Act
            var removedScreen = screenStack.Pop();

            // - Assert
            Assert.IsTrue(screenStack.IsRoot);
            Assert.AreSame(screenA, screenStack.CurrentScreen);
            Assert.AreSame(screenB, removedScreen);
            CollectionAssert.DoesNotContain(screenStack, screenB);
            Assert.AreEqual(1, screenStack.Count);
        }

        [Test]
        public void PopScreenWhenEmptyTest()
        {
            // - Arrange
            var screenStack = new ScreenStack();

            // - Act & Assert
            Assert.Throws<InvalidOperationException>(
                () => screenStack.Pop());
        }

        [Test]
        public void ClearStackTest()
        {
            // - Arrange
            var rootScreen = new Mock<Screen>().Object;
            var screen = new Mock<Screen>().Object;
            var screenStack = new ScreenStack();
            screenStack.Push(rootScreen, screen);

            // - Act
            screenStack.Clear();

            // - Assert
            Assert.IsTrue(screenStack.IsRoot);
            Assert.AreEqual(rootScreen, screenStack.CurrentScreen);
            Assert.AreEqual(1, screenStack.Count);
        }

        [Test]
        public void ClearStackWithOnlyRootScreen()
        {
            // - Arrange
            var rootScreen = new Mock<Screen>().Object;
            var screenStack = new ScreenStack();
            screenStack.Push(rootScreen);

            // - Act
            screenStack.Clear();

            // - Assert
            Assert.IsTrue(screenStack.IsRoot);
            Assert.AreEqual(rootScreen, screenStack.CurrentScreen);
            Assert.AreEqual(1, screenStack.Count);
        }

        [Test]
        public void ClearEmptyStackScreen()
        {
            // - Arrange
            var rootScreen = new Mock<Screen>().Object;
            var screenStack = new ScreenStack();

            // - Act
            screenStack.Clear();

            // - Assert
            Assert.IsTrue(screenStack.IsRoot);
            Assert.IsNull(screenStack.CurrentScreen);
            Assert.AreEqual(0, screenStack.Count);
        }
    }
}