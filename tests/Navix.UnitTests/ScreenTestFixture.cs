using Navix.UnitTests.Stubs;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class ScreenTestFixture
    {
        [Test]
        public void ScreenPropertiesTest()
        {
            // - Arrange

            var screen = new ScreenStub();

            // - Act
            var screenName = screen.Name;

            // - Assert
            Assert.AreEqual(nameof(ScreenStub), screenName);
            
        }   
    }
}