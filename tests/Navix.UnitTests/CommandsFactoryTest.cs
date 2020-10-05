using System;
using System.Linq;
using Moq;
using Spx.Navix.Commands;
using Spx.Navix.Internal;
using Spx.Navix.UnitTests.Stubs;
using Spx.Reflection;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class CommandsFactoryTest
    {
        [Fact]
        public void CommandsFactory_CreateInstance_NoThrows()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            
            // -- Act:
            var factory = new CommandsFactory(registry);
            
            // -- Assert:
            Assert.NotNull(factory);
        }

        [Fact]
        public void CommandsFactory_CreateInstanceWithNullRegistry_ThrowsArgumentNull()
        {
            // -- Assert & Act:
            Assert.Throws<ArgumentNullException>(
                () => new CommandsFactory(null!));
        }

        [Fact]
        public void CommandsFactory_TryCreateForwardCommand_CommandCreated()
        {
            // -- Arrange:
            var screen = new ScreenStub1();
            var registryMock = new Mock<IScreenRegistry>();
            registryMock.Setup(e => e.Resolve(screen))
                .Returns((Screen _) => new ScreenResolverStub1());

            var factory = new CommandsFactory(registryMock.Object);

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
            var factory = new CommandsFactory(new ScreenRegistry());
            
            // -- Act:
            var commands = factory.Back();
            
            // -- Assert:
            Assert.NotNull(commands);
            Assert.Single(commands);

            var command = commands.FirstOrDefault();
            Assert.NotNull(command);
            Assert.IsType<BackNavCommand>(command);
        }
    }
}