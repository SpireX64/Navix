using System;
using Moq;
using Spx.Navix.Abstractions;
using Spx.Navix.Internal.Defaults;
using Spx.Navix.UnitTests.Stubs;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class NavixServiceTest
    {
        [Fact]
        public void NavixService_CreateWithNullConfig()
        {
            // -- Act & Assert:
            Assert.Throws<ArgumentNullException>(
                () => new NavixService(null!));
        }

        [Fact]
        public void NavixService_CreateWithDefaultConfig()
        {
            // -- Arrange:
            var configStub = new Mock<NavixConfig>();
            configStub.DefaultValueProvider = DefaultValueProvider.Mock;

            // -- Act:
            var navix = new NavixService(configStub.Object);

            // -- Assert:
            Assert.NotNull(navix.NavigatorHolder);
            Assert.NotNull(navix.Router);
        }

        [Fact]
        public void NavixService_OverrideRouter_ReturnsCustomRouter()
        {
            // -- Arrange:
            var customRouterStub = new Mock<IRouter>().Object;
            var configMock = new Mock<NavixConfig>();
            configMock.Setup(e => e.GetRouter(It.IsAny<INavigationManager>(), It.IsAny<ICommandsFactory>()))
                .Returns(customRouterStub);

            // -- Act:
            var navix = new NavixService(configMock.Object);

            // -- Assert:
            Assert.NotNull(navix.Router);
            Assert.Equal(customRouterStub, navix.Router);
        }

        [Fact]
        public void NavixService_OverrideCommandsFactory_ReturnsCustomFactory()
        {
            // -- Arrange:
            var screen = new ScreenStub1();
            var customCommandsFactoryMock = new Mock<ICommandsFactory>
                {DefaultValueProvider = DefaultValueProvider.Mock};

            var configMock = new Mock<NavixConfig>();
            configMock.Setup(e => e.GetRouter(It.IsAny<INavigationManager>(), It.IsAny<ICommandsFactory>()))
                .Returns((INavigationManager m, ICommandsFactory f) => new DefaultRouter(m, f));
            configMock.Setup(e => e.GetCommandsFactory(It.IsAny<IScreenRegistry>()))
                .Returns(customCommandsFactoryMock.Object);

            // -- Act:
            var navix = new NavixService(configMock.Object);
            var router = navix.Router;
            router.Forward(screen);

            // -- Assert:
            // Custom commands factory will be used by navigation manager
            customCommandsFactoryMock.Verify(e => e.Forward(screen), Times.Once);
        }
    }
}