using Moq;
using Navix.Abstractions;
using Navix.Internal;
using NUnit.Framework;
using Spx.Reflection;

namespace Navix.UnitTests
{
    [TestFixture]
    public class ScreenEntryTestFixture
    {
        [Test]
        public void DefaultScreenEntryTest()
        {
            var screenClass = Class<Screen>.Get();
            var screenResolver = new Mock<IScreenResolver>().Object;

            // - Act
            var screenEntry = new ScreenEntry(screenClass, screenResolver);

            // - Assert
            Assert.AreEqual(screenClass, screenEntry.ScreenClass);
            Assert.AreEqual(screenResolver, screenEntry.Resolver);
            Assert.IsFalse(screenEntry.IsRoot);
        }

        [Test]
        public void SetEntryAsRootTest()
        {
            // - Arrange
            var screenClass = Class<Screen>.Get();
            var screenResolver = new Mock<IScreenResolver>().Object;
            var screenEntry = new ScreenEntry(screenClass, screenResolver);

            // - Act
            ((IScreenRegistrationConfig) screenEntry).AsRoot();

            // - Assert
            Assert.IsTrue(screenEntry.IsRoot);
        }
    }
}