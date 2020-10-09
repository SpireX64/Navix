using System.Diagnostics.CodeAnalysis;
using Moq;
using Spx.Navix.Abstractions;
using Spx.Navix.Internal;
using Spx.Navix.Internal.Defaults;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class DefaultNavixConfigTest
    {
        [Fact]
        public void NavixConfig_GetRouter_ReturnsDefaultRouter()
        {
            // -- Arrange:
            var navixConfig = new DefaultNavixConfig();
            var navigationManagerStub = new Mock<INavigationManager>().Object;
            var commandsFactoryStub = new Mock<ICommandsFactory>().Object;

            // -- Act:
            var router = navixConfig.GetRouter(navigationManagerStub, commandsFactoryStub);

            // -- Assert:
            Assert.NotNull(router);
            Assert.IsType<DefaultRouter>(router);
        }

        [Fact]
        public void NavixConfig_GetCommandsFactory_ReturnsDefaultCommandsFactory()
        {
            // -- Arrange:
            var navixConfig = new DefaultNavixConfig();
            var registry = new ScreenRegistry();

            // -- Act:
            var factory = navixConfig.GetCommandsFactory(registry);

            // -- Assert:
            Assert.NotNull(factory);
            Assert.IsType<DefaultCommandsFactory>(factory);
        }

        [Fact]
        public void NavixConfig_AddMiddleware_MiddlewareAddedToService()
        {
            // -- Arrange:
            var middlewareStub = new Mock<INavigationMiddleware>().Object;
            var registry = new ScreenRegistry();
            var config = new DefaultNavixConfig {Middleware = middlewareStub};

            // -- Act:
            config.Configure(registry);

            // -- Assert:
            Assert.Contains(middlewareStub, config.Middlewares);
        }

        [ExcludeFromCodeCoverage]
        private class DefaultNavixConfig : NavixConfig
        {
            public INavigationMiddleware? Middleware { get; set; } = null;

            public override void Configure(IScreenRegistry registry)
            {
                if (Middleware != null)
                    AddMiddleware(Middleware);
            }
        }
    }
}