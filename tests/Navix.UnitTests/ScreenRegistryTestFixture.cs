using Moq;
using Navix.Abstractions;
using Navix.Exceptions;
using Navix.Internal;
using Navix.UnitTests.Stubs;
using NUnit.Framework;
using Spx.Reflection;

namespace Navix.UnitTests
{
    [TestFixture]
    public class ScreenRegistryTestFixture
    {
        [Test]
        public void NewScreenRegistryTest()
        {
            // - Act
            var screenRegistry = new ScreenRegistry();

            // - Assert
            CollectionAssert.IsEmpty(screenRegistry.Middlewares);
            Assert.IsTrue(screenRegistry.IsEmpty);
            Assert.IsNull(screenRegistry.RootScreenClass);
            Assert.IsFalse(screenRegistry.HasScreen(Class<Screen>.Get()));
        }

        [Test]
        public void RegisterScreenTest()
        {
            // - Arrange
            var screenClass = typeof(ScreenStub);
            var screenResolver = new Mock<IScreenResolver>().Object;
            var screenRegistry = new ScreenRegistry();

            // - Act
            screenRegistry.Register(screenClass, screenResolver);

            // - Assert
            Assert.IsFalse(screenRegistry.IsEmpty);
            Assert.IsNull(screenRegistry.RootScreenClass);
            Assert.IsTrue(screenRegistry.HasScreen(screenClass));
        }

        [Test]
        public void RegisterRootScreen()
        {
            // - Arrange
            var screenClass = typeof(ScreenStub);
            var screenResolver = new Mock<IScreenResolver>().Object;
            var screenRegistry = new ScreenRegistry();

            // - Act
            screenRegistry.Register(screenClass, screenResolver).AsRoot();

            // - Assert
            Assert.IsFalse(screenRegistry.IsEmpty);
            Assert.AreSame(screenClass, screenRegistry.RootScreenClass!.Value.Type);
            Assert.IsTrue(screenRegistry.HasScreen(screenClass));
        }

        [Test]
        public void ResolveScreenFromRegistryTest()
        {
            // - Arrange
            var screenClass = typeof(ScreenStub);
            var screen = new ScreenStub();
            var screenResolver = new Mock<IScreenResolver>().Object;
            var screenRegistry = new ScreenRegistry();
            screenRegistry.Register(screenClass, screenResolver);

            // - Act
            var entry = screenRegistry.Resolve(screen);

            // - Assert
            Assert.IsNotNull(entry);
            Assert.AreSame(screenResolver, entry.Resolver);
            Assert.IsFalse(entry.IsRoot);
        }

        [Test]
        public void ResolveNonExistentScreenFromRegistry()
        {
            // - Arrange
            var screen = new ScreenStub();
            var screenRegistry = new ScreenRegistry();

            // - Act & Assert
            var exception = Assert.Throws<UnregisteredScreenException>(
                () => screenRegistry.Resolve(screen));
            
            Assert.AreEqual(screen, exception!.Screen);
        }

        [Test]
        public void AddMiddlewareTest()
        {
            // - Arrange
            var screenRegistry = new ScreenRegistry();
            var middlewareStub = new Mock<INavigationMiddleware>().Object;

            // - Act
            screenRegistry.AddMiddleware(middlewareStub);

            // - Assert
            CollectionAssert.Contains(screenRegistry.Middlewares, middlewareStub);
        }
    }
}