﻿using System;
using Moq;
using Navix.Commands;
using Navix.Exceptions;
using Navix.UnitTests.Stubs;
using Xunit;

namespace Navix.UnitTests
{
    public class NavCommandsTest
    {
        [Fact]
        public void ForwardNavCommand_Apply_InvokeForward()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();
            var screenStack = new ScreenStack();

            var screenFake = new Mock<Screen>().Object;
            var resolverFake = new Mock<IScreenResolver>().Object;
            var command = new ForwardNavCommand(screenFake, resolverFake);

            // -- Act:
            command.Apply(navigatorMock.Object, screenStack);

            // -- Assert
            Assert.Equal(screenFake, screenStack.CurrentScreen);
            navigatorMock.Verify(
                e => e.Forward(screenFake, resolverFake), Times.Once);
        }

        [Fact]
        public void BackNavCommand_Apply_InvokeBack()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();
            var screenStack = new ScreenStack();
            screenStack.Push(new ScreenStub1());

            var command = new BackNavCommand();

            // -- Act:
            command.Apply(navigatorMock.Object, screenStack);

            // -- Assert
            Assert.True(screenStack.IsRoot);
            Assert.Equal(0, screenStack.Count);
            navigatorMock.Verify(
                e => e.Back(), Times.Once);
        }

        [Fact]
        public void BackToScreenCommand_Apply_InvokeBackToScreen()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();
            var screen = new ScreenStub1();
            var screenType = typeof(ScreenStub1);
            var screenStack = new ScreenStack();

            var currentScreen = new ScreenStub2();
            screenStack.Push(screen);
            screenStack.Push(currentScreen);

            var command = new BackToScreenNavCommand(screenType);

            // -- Act:
            command.Apply(navigatorMock.Object, screenStack);

            // -- Assert:
            Assert.Equal(screen, screenStack.CurrentScreen);
            navigatorMock.Verify(
                e => e.BackToScreen(screen), Times.Once);
        }

        [Fact]
        public void BackToScreenCommand_ApplyWhenScreenNotInStack_ThrowsException()
        {
            // -- Arrange:
            var navigatorStub = new Mock<Navigator>().Object;
            var stack = new ScreenStack();
            var screenType = typeof(ScreenStub1);
            var command = new BackToScreenNavCommand(screenType);

            // -- Act & Assert:
            var exception = Assert.Throws<ScreenNotFoundException>(
                () => command.Apply(navigatorStub, stack));

            Assert.Equal(screenType, exception.ScreenClass.Type);
        }

        [Fact]
        public void BackToRootCommand_Apply_InvokeBackToRoot()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();
            var stack = new ScreenStack();
            stack.Push(new ScreenStub1());
            var command = new BackToRootNavCommand();

            // -- Act
            command.Apply(navigatorMock.Object, stack);

            // -- Assert:
            Assert.True(stack.IsRoot);
            Assert.Equal(0, stack.Count);
            navigatorMock.Verify(e => e.BackToRoot(), Times.Once);
        }

        [Fact]
        public void BackToRootCommand_ApplyOnRoot_Ignored()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();
            var stack = new ScreenStack();
            var command = new BackToRootNavCommand();

            // -- Act
            command.Apply(navigatorMock.Object, stack);

            // -- Assert:
            Assert.True(stack.IsRoot);
            Assert.Equal(0, stack.Count);
            navigatorMock.Verify(e => e.BackToRoot(), Times.Never);
        }

        [Fact]
        public void ReplaceScreenCommand_Apply_InvokeReplace()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();
            var screenStack = new ScreenStack();
            screenStack.Push(new ScreenStub1());

            var screenFake = new Mock<Screen>().Object;
            var resolverFake = new Mock<IScreenResolver>().Object;
            var command = new ReplaceScreenNavCommand(screenFake, resolverFake);

            // -- Act:
            command.Apply(navigatorMock.Object, screenStack);

            // -- Assert:
            Assert.False(screenStack.IsRoot);
            Assert.Equal(1, screenStack.Count);
            Assert.Equal(screenFake, screenStack.CurrentScreen);
            navigatorMock.Verify(e => e.Replace(screenFake, resolverFake), Times.Once);
        }

        [Fact]
        public void ReplaceScreenCommand_ApplyToRoot_ThrowsException()
        {
            // -- Arrange:
            var navigatorMock = new Mock<Navigator>();
            var screenStack = new ScreenStack();
            var screenFake = new Mock<Screen>().Object;
            var resolverFake = new Mock<IScreenResolver>().Object;
            var command = new ReplaceScreenNavCommand(screenFake, resolverFake);

            // -- Act & Assert:
            Assert.Throws<InvalidOperationException>(
                () => command.Apply(navigatorMock.Object, screenStack));
        }
    }
}