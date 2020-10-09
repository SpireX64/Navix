using System;
using Spx.Navix.UnitTests.Stubs;
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
        
        [Fact]
        public void Navigator_TryBackToScreen_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.BackToScreen(new ScreenStub1()));
        }

        [Fact]
        public void Navigator_TryBackToRoot_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.BackToRoot());
        }

        [Fact]
        public void Navigator_TryReplaceScreen_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();
            
            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.Replace(new ScreenStub1(), new ScreenResolverStub1()));
        }

        private class AbstractNavigator : Navigator
        {
        }
    }
}