using System;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class AbstractNavigatorTest
    {
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

        private class AbstractNavigator : Navigator
        {
        }
    }
}