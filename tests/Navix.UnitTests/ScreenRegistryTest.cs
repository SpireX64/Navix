using Spx.Navix.UnitTests.Stubs;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class ScreenRegistryTest
    {
        [Fact]
        public void ScreenRegistry_CreateEmptyRegistry_RegistryIsEmpty()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();

            // -- Assert:
            Assert.True(registry.IsEmpty);
        }

        [Fact]
        public void ScreenRegistry_RegisterScreenType_ScreenRegistered()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            var screenResolver = new ScreenResolverStub1();
            var screenType = typeof(ScreenStub1);

            // -- Act:
            registry.Register(screenType, screenResolver);

            // -- Assert:
            Assert.False(registry.IsEmpty);
            Assert.True(registry.HasScreen(screenType));
            Assert.Equal(screenResolver, registry.Resolve(screenType));
        }

        [Fact]
        public void ScreenRegistry_TryCheckAndGetNotRegistered_ReturnsNull()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            var screenType = typeof(ScreenStub1);

            // -- Assert:
            Assert.True(registry.IsEmpty);
            Assert.False(registry.HasScreen(screenType));
            Assert.Null(registry.Resolve(screenType));
        }

        // -- Arrange:
        // -- Act:
        // -- Assert:
    }
}