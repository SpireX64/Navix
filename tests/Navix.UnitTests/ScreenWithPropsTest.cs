using Spx.Navix.UnitTests.Stubs;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class ScreenWithPropsTest
    {
        [Fact]
        public void Screen_CreateScreenWithProps_Props()
        {
            // -- Arrange:
            var props = new ScreenStubProps(10);
            
            // -- Act:
            var screen = new ScreenStubWithProps(props);

            // -- Assert:
            Assert.Equal(props.Id, screen.Props.Id);
        }
    }
}