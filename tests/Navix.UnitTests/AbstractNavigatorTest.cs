using System;
using Moq;
using Spx.Navix.Platform;
using Spx.Reflection;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class AbstractNavigatorTest
    {
        private class AbstractNavigator : Navigator<IScreenResolver> {}
        
        [Fact]
        public void Navigator_TryInvokeMethods_ThrowsNotSupported()
        {
            // -- Arrange:
            var navigator = new AbstractNavigator();

            // -- Act & Assert:
            Assert.Throws<NotSupportedException>(
                () => navigator.Forward(null!, null!));
            
            Assert.Throws<NotSupportedException>(
                () => navigator.Back());
            
            Assert.Throws<NotSupportedException>(
                () => navigator.BackTo(null!));
            
            Assert.Throws<NotSupportedException>(
                () => navigator.BackToRoot());
            
            Assert.Throws<NotSupportedException>(
                () => navigator.Update(null!));
        }
    }
}