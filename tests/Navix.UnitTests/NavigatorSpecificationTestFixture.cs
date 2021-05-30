using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class NavigatorSpecificationTestFixture
    {
        [Test]
        public void DefaultSpecificationTest()
        {
            // - Act
            var specification = new NavigatorSpecification();

            // - Assert
            Assert.IsFalse(specification.ReplaceScreenSupported);
            Assert.IsFalse(specification.BackToRootSupported);
            Assert.IsFalse(specification.BackToScreenSupported);
        }

        [Test]
        public void SetSpecificationTest()
        {
            // - Act
            var specification = new NavigatorSpecification(true, true, true);

            // - Assert
            Assert.IsTrue(specification.ReplaceScreenSupported);
            Assert.IsTrue(specification.BackToRootSupported);
            Assert.IsTrue(specification.BackToScreenSupported);
        }
    }
}