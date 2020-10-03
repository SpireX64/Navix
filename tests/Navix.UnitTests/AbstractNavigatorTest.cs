using System;
using Spx.Navix.Platform;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class AbstractNavigatorTest
    {
        private class AbstractNavigator : Navigator<IScreenResolver>
        {
            public override NavigatorSpec Specification { get; } = new NavigatorSpec();
        }

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

        [Fact]
        public void Navigator_CheckDefaultSpec_AllFalse()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();
            var spec = navigator.Specification;

            // -- Assert:
            Assert.False(spec.BackSupport);
            Assert.False(spec.BackToSupport);
            Assert.False(spec.BackToRootSupport);
        }
    }
}