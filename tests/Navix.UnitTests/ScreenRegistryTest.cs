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
            
            // -- Act:
            registry.Register<ScreenStub1>(screenResolver);

            // -- Assert:
            Assert.False(registry.IsEmpty);
            Assert.True(registry.HasScreen<ScreenStub1>());
            Assert.Equal(screenResolver, registry.Resolve<ScreenStub1>());
        }

        [Fact]
        public void ScreenRegistry_TryCheckAndGetNotRegistered_ReturnsNull()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            
            // -- Assert:
            Assert.True(registry.IsEmpty);
            Assert.False(registry.HasScreen<ScreenStub1>());
            Assert.Null(registry.Resolve<ScreenStub1>());
        }

        // -- Arrange:
        // -- Act:
        // -- Assert:
    }
}