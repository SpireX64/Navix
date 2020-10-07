using System;
using Spx.Navix.UnitTests.Stubs;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class ScreenStackTest
    {
        [Fact]
        public void ScreenStack_CreateScreenStack_StackIsEmpty()
        {
            // -- Arrange:
            var stack = new ScreenStack();

            // -- Assert:
            Assert.True(stack.IsRoot);
            Assert.Equal(0, stack.Count);
            Assert.Null(stack.CurrentScreen);
        }

        [Fact]
        public void ScreenStack_PushScreen_ScreenAddedToStack()
        {
            // -- Arrange:
            var screen = new ScreenStub1();
            var stack = new ScreenStack();

            // -- Act:
            stack.Push(screen);

            // -- Assert:
            Assert.False(stack.IsRoot);
            Assert.Equal(1, stack.Count);
            Assert.Equal(screen, stack.CurrentScreen);
        }

        [Fact]
        public void ScreenStack_PushTwoScreens_ScreensAddedToStack()
        {
            // -- Arrange:
            var screen1 = new ScreenStub1();
            var screen2 = new ScreenStub2();
            var stack = new ScreenStack();

            // -- Act:
            stack.Push(screen1, screen2);

            // -- Assert:
            Assert.False(stack.IsRoot);
            Assert.Equal(2, stack.Count);
            Assert.Equal(screen2, stack.CurrentScreen);
        }

        [Fact]
        public void ScreenStack_PopAtRoot_ThrowsInvalidOperation()
        {
            // -- Arrange:
            var stack = new ScreenStack();

            // -- Act & Assert:
            Assert.Throws<InvalidOperationException>(
                () => stack.Pop());
        }

        [Fact]
        public void ScreenStack_PopScreen_PopAndReturnScreen()
        {
            // -- Arrange:
            var screen = new ScreenStub1();
            var stack = new ScreenStack();
            stack.Push(screen);

            // -- Act:
            var prevScreen = stack.Pop();

            // -- Assert:
            Assert.True(stack.IsRoot);
            Assert.Equal(0, stack.Count);
            Assert.Equal(screen, prevScreen);
        }
    }
}