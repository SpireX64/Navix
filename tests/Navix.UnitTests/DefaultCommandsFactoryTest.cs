using System;
using System.Collections.Immutable;
using System.Linq;
using Moq;
using Spx.Navix.Abstractions;
using Spx.Navix.Commands;
using Spx.Navix.Internal;
using Spx.Navix.Internal.Defaults;
using Spx.Navix.UnitTests.Stubs;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class DefaultCommandsFactoryTest
    {
        [Fact]
        public void CommandsFactory_CreateInstance_NoThrows()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();

            // -- Act:
            var factory = new DefaultCommandsFactory(registry);

            // -- Assert:
            Assert.NotNull(factory);
        }

        [Fact]
        public void CommandsFactory_CreateInstanceWithNullRegistry_ThrowsArgumentNull()
        {
            // -- Assert & Act:
            Assert.Throws<ArgumentNullException>(
                () => new DefaultCommandsFactory(null!));
        }

        [Fact]
        public void CommandsFactory_TryCreateForwardCommand_CommandCreated()
        {
            // -- Arrange:
            var screen = new ScreenStub1();
            var registryMock = new Mock<IScreenRegistry>();
            registryMock.Setup(e => e.Resolve(screen))
                .Returns((Screen _) => new ScreenResolverStub1());

            var factory = new DefaultCommandsFactory(registryMock.Object);

            // -- Act:
            var commands = factory.Forward(screen);

            // -- Assert:
            registryMock.Verify(e => e.Resolve(screen), Times.Once);

            Assert.NotNull(commands);
            Assert.Single(commands);

            var command = commands.FirstOrDefault();
            Assert.NotNull(command);
            Assert.IsType<ForwardNavCommand>(command);
        }

        [Fact]
        public void CommandsFactory_TryCreateBackCommand_CommandCreated()
        {
            // -- Arrange:
            var factory = new DefaultCommandsFactory(new ScreenRegistry());

            // -- Act:
            var commands = factory.Back();

            // -- Assert:
            Assert.NotNull(commands);
            Assert.Single(commands);

            var command = commands.FirstOrDefault();
            Assert.NotNull(command);
            Assert.IsType<BackNavCommand>(command);
        }

        [Fact]
        public void CommandsFactory_TryCreateBackToScreenCommand_CommandCreated()
        {
            // -- Arrange:
            var factory = new DefaultCommandsFactory(new ScreenRegistry());
            var spec = new NavigatorSpecification {BackToScreenSupported = true};
            var stack = new ScreenStack();

            // -- Act:
            var commands = factory.BackToScreen(stack, spec, typeof(ScreenStub1));

            // -- Assert:
            Assert.NotNull(commands);
            Assert.Single(commands);

            var command = commands.FirstOrDefault();
            Assert.NotNull(command);
            Assert.IsType<BackToScreenNavCommand>(command);
        }

        [Fact]
        public void CommandsFactory_BackToScreenWhenNotSupported_SomeBackCommandsCreated()
        {
            // -- Arrange:
            var factory = new DefaultCommandsFactory(new ScreenRegistry());
            var spec = new NavigatorSpecification {BackToScreenSupported = false};
            var stack = new ScreenStack();
            stack.Push(new ScreenStub1());
            stack.Push(new ScreenStub2());

            // -- Act:
            var commands = factory.BackToScreen(stack, spec, typeof(ScreenStub1));

            // -- Assert:
            Assert.NotNull(commands);
            Assert.Equal(1, commands.Count);

            var commandTypes = commands
                .Select(e => e.GetType())
                .ToImmutableArray();

            var backCommandType = typeof(BackNavCommand);
            Assert.True(commandTypes.All(t => t == backCommandType));
        }

        [Fact]
        public void CommandsFactory_TryCreateBackToRootCommand_CommandCreated()
        {
            // -- Arrange:
            var factory = new DefaultCommandsFactory(new ScreenRegistry());
            var spec = new NavigatorSpecification {BackToRootSupported = true};
            var stack = new ScreenStack();

            // -- Act:
            var commands = factory.BackToRoot(stack, spec);

            // -- Assert:
            Assert.NotNull(commands);
            Assert.Single(commands);

            var command = commands.FirstOrDefault();
            Assert.NotNull(command);
            Assert.IsType<BackToRootNavCommand>(command);
        }

        [Fact]
        public void CommandsFactory_BackToRootWhenNotSupported_SomeBackCommandsCreated()
        {
            // -- Arrange:
            var factory = new DefaultCommandsFactory(new ScreenRegistry());
            var spec = new NavigatorSpecification {BackToRootSupported = false};

            var stack = new ScreenStack();
            stack.Push(new ScreenStub1());
            stack.Push(new ScreenStub2());

            // -- Act:
            var commands = factory.BackToRoot(stack, spec);

            // -- Assert:
            Assert.NotNull(commands);
            Assert.Equal(2, commands.Count);

            var commandTypes = commands
                .Select(e => e.GetType())
                .ToImmutableArray();

            var backCommandType = typeof(BackNavCommand);
            Assert.True(commandTypes.All(t => t == backCommandType));
        }

        [Fact]
        public void CommandsFactory_TryCreateReplaceScreenCommand_CommandCreated()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            registry.Register(typeof(ScreenStub1), new ScreenResolverStub1());
            var factory = new DefaultCommandsFactory(registry);
            var spec = new NavigatorSpecification {ReplaceScreenSupported = true};
            var stack = new ScreenStack();

            // -- Act:
            var commands = factory.ReplaceScreen(stack, spec, new ScreenStub1());

            // -- Assert:
            Assert.NotNull(commands);
            Assert.Single(commands);

            var command = commands.FirstOrDefault();
            Assert.NotNull(command);
            Assert.IsType<ReplaceScreenNavCommand>(command);
        }

        [Fact]
        public void CommandsFactory_ReplaceWhenNotSupported_BackAndForwardCommandsCreated()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            registry.Register(typeof(ScreenStub1), new ScreenResolverStub1());
            var factory = new DefaultCommandsFactory(registry);
            var spec = new NavigatorSpecification {ReplaceScreenSupported = false};
            var stack = new ScreenStack();

            // -- Act:
            var commands = factory.ReplaceScreen(stack, spec, new ScreenStub1());

            // -- Assert:
            Assert.NotNull(commands);
            Assert.Equal(2, commands.Count);

            var commandTypes = commands
                .Select(e => e.GetType())
                .ToImmutableArray();
            Assert.Equal(typeof(BackNavCommand), commandTypes[0]);
            Assert.Equal(typeof(ForwardNavCommand), commandTypes[1]);
        }
    }
}