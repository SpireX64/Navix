using Navix.UnitTests.Stubs;
using Xunit;

namespace Navix.UnitTests
{
    public class ScreenTest
    {
        [Fact]
        public void Screen_GetName_NameIsClassName()
        {
            // -- Arrange:
            var screen = new ScreenStub1();

            // -- Act:
            var screenName = screen.Name;

            // -- Assert:
            Assert.Equal(nameof(ScreenStub1), screenName);
        }
    }
}