using System.Linq;
using Moq;
using Navix.Commands;
using Navix.Internal;
using Navix.Internal.Defaults;
using Navix.UnitTests.Stubs;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class DefaultCommandsFactoryTestFixture
    {
        [Test]
        public void ForwardFactoryTest()
        {
            // - Arrange
            var screen = new ScreenStub();
            var resolver = new Mock<IScreenResolver>().Object;
            
            var registry = new ScreenRegistry();
            registry.Register(typeof(ScreenStub), resolver);
            
            var cf = new DefaultCommandsFactory(registry);

            // - Act
            var commands = cf.Forward(screen);

            // - Assert
            Assert.AreEqual(1, commands.Count);
            
            var command = commands.FirstOrDefault();
            Assert.IsNotNull(command);
            Assert.IsInstanceOf<ForwardNavCommand>(command);
        }

        [Test]
        public void BackFactoryTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var cf = new DefaultCommandsFactory(registry);

            // - Act
            var commands = cf.Back();

            // - Assert
            Assert.AreEqual(1, commands.Count);
            
            var command = commands.FirstOrDefault();
            Assert.IsNotNull(command);
            Assert.IsInstanceOf<BackNavCommand>(command);
        }

        [Test]
        public void SupportedBackToScreenFactoryTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var screenStack = new ScreenStack();
            var specs = new NavigatorSpecification(true, false, false);
            
            var screenA = new Mock<Screen>().Object;
            var screenB = new ScreenStub();
            screenStack.Push(screenA, screenB, screenA, screenA);
            
            var cf = new DefaultCommandsFactory(registry);
            
            // - Act
            var commands = cf.BackToScreen(screenStack, specs, typeof(ScreenStub));
            
            // - Assert
            Assert.AreEqual(1, commands.Count);
            var command = commands.FirstOrDefault();
            Assert.IsNotNull(command);
            Assert.IsInstanceOf<BackToScreenNavCommand>(command);
        }

        [Test]
        public void UnsupportedBackToScreenFactoryTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var screenStack = new ScreenStack();
            var specs = new NavigatorSpecification(false, false, false);
            
            var screenA = new Mock<Screen>().Object;
            var screenB = new ScreenStub();
            screenStack.Push(screenA, screenB, screenA, screenA);
            
            var cf = new DefaultCommandsFactory(registry);
            
            // - Act
            var commands = cf.BackToScreen(screenStack, specs, typeof(ScreenStub));

            // - Assert
            Assert.AreEqual(2, commands.Count);
            CollectionAssert.AllItemsAreInstancesOfType(commands, typeof(BackNavCommand));
        }

        [Test]
        public void SupportedBackToRootFactoryTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var screenStack = new ScreenStack();
            var specs = new NavigatorSpecification(false, true, false);
            
            var screenA = new Mock<Screen>().Object;
            var screenB = new ScreenStub();
            screenStack.Push(screenB, screenA, screenA);
            
            var cf = new DefaultCommandsFactory(registry);
          
            // - Act
            var commands = cf.BackToRoot(screenStack, specs);

            // - Assert
            Assert.AreEqual(1, commands.Count);
            var command = commands.FirstOrDefault();
            Assert.IsInstanceOf<BackToRootNavCommand>(command);
        }

        [Test]
        public void UnsupportedBackToRootFactoryTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var screenStack = new ScreenStack();
            var specs = new NavigatorSpecification(false, false, false);
            
            var screenA = new Mock<Screen>().Object;
            var screenB = new ScreenStub();
            screenStack.Push(screenB, screenA, screenA);
            
            var cf = new DefaultCommandsFactory(registry);
          
            // - Act
            var commands = cf.BackToRoot(screenStack, specs);

            // - Assert
            Assert.AreEqual(screenStack.Count - 1, commands.Count);
            CollectionAssert.AllItemsAreInstancesOfType(commands, typeof(BackNavCommand));
        }

        [Test]
        public void SupportedReplaceFactoryTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var screenStack = new ScreenStack();
            var specs = new NavigatorSpecification(false, false, true);
            
            var screenA = new Mock<Screen>().Object;
            var screenB = new ScreenStub();
            var screenBResolver = new Mock<IScreenResolver>().Object;
            registry.Register(typeof(ScreenStub), screenBResolver);
            
            screenStack.Push(screenB, screenA, screenA);
            
            var cf = new DefaultCommandsFactory(registry);
          
            // - Act
            var commands = cf.ReplaceScreen(screenStack, specs, screenB);

            // - Assert
            Assert.AreEqual(1, commands.Count);
            CollectionAssert.AllItemsAreInstancesOfType(commands, typeof(ReplaceScreenNavCommand));
        }

        [Test]
        public void UnsupportedReplaceFactoryTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var screenStack = new ScreenStack();
            var specs = new NavigatorSpecification(false, false, false);
            
            var screenA = new Mock<Screen>().Object;
            var screenB = new ScreenStub();
            var screenBResolver = new Mock<IScreenResolver>().Object;
            registry.Register(typeof(ScreenStub), screenBResolver);
            
            screenStack.Push(screenB, screenA, screenA);
            
            var cf = new DefaultCommandsFactory(registry);
          
            // - Act
            var commands = cf.ReplaceScreen(screenStack, specs, screenB);

            // - Assert
            Assert.AreEqual(2, commands.Count);
            Assert.IsInstanceOf<BackNavCommand>(
                commands.ElementAtOrDefault(0));
            Assert.IsInstanceOf<ForwardNavCommand>(
                commands.ElementAtOrDefault(1));
        }
    }
}