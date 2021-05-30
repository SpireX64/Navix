using System;
using Moq;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class DefaultNavigatorTestFixture
    {
        [Test]
        public void BackToScreenIsNotSupportedByDefault()
        {
            // - Arrange
            var screen = new Mock<Screen>().Object;
            var defaultNavigator = new Mock<Navigator> {CallBase = true};

            // - Act & Assert
            Assert.Throws<NotSupportedException>(
                () => defaultNavigator.Object.BackToScreen(screen));
        }

        [Test]
        public void BackToRootIsNotSupportedByDefault()
        {
            // - Arrange
            var defaultNavigator = new Mock<Navigator>() {CallBase = true};

            // - Act & Assert
            Assert.Throws<NotSupportedException>(
                () => defaultNavigator.Object.BackToRoot());
        }
        
        [Test]
        public void ReplaceScreenIsNotSupportedByDefault()
        {
            // - Arrange
            var screen = new Mock<Screen>().Object;
            var screenResolver = new Mock<IScreenResolver>().Object;
            var defaultNavigator = new Mock<Navigator>() {CallBase = true};

            // - Act & Assert
            Assert.Throws<NotSupportedException>(
                () => defaultNavigator.Object.Replace(screen, screenResolver));
        }
    }
}