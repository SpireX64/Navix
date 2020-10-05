using System;
using Spx.Navix.Platform;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class AbstractNavigatorTest
    {
        private class AbstractNavigator : Navigator {}

        [Fact]
        public void Navigator_TryForward_ThrowsNotImpl()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotImplementedException>(
                () => navigator.Forward(null!, null!));
        }

        [Fact]
        public void Navigator_TryBack_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.Back());
        }

        [Fact]
        public void Navigator_TryBackToScreen_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.BackTo(null!));
        }

        [Fact]
        public void Navigator_TryBackToRootScreen_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.BackToRoot());
        }

        [Fact]
        public void Navigator_TryUpdateScreen_NoThrows()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            navigator.Update(null!);
        }
    }
}