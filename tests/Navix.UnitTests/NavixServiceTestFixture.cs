using System;
using Moq;
using Navix.Abstractions;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class NavixServiceTestFixture
    {
        [Test]
        public void ConfigureOnServiceCreatedTest()
        {
            // - Arrange
            var configMock = new Mock<NavixConfig> {CallBase = true};

            // - Act
            var _ = new NavixService(configMock.Object);

            // - Assert
            configMock.Verify(
                it => it.Configure(It.IsAny<IScreenRegistrar>()),
                Times.Once
            );
        }

        [Test]
        public void ThrowOnNullConfig()
        {
            // - Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new NavixService(null!);
            });
        }
        
        [Test]
        public void RouterFactoryTest()
        {
            // - Arrange
            var configMock = new Mock<NavixConfig> {CallBase = true};

            var service = new NavixService(configMock.Object);

            // - Act
            var router = service.Router;
            var router2 = service.Router;

            // - Assert
            Assert.IsNotNull(router);
            Assert.AreSame(router, router2);
            configMock.Verify(
                it => it.GetCommandsFactory(It.IsAny<IScreenRegistry>()), 
                Times.Once
            );
            configMock.Verify(
                it => it.GetRouter(
                    It.IsAny<IScreenRegistry>(), 
                    It.IsAny<INavigationManager>(), 
                    It.IsAny<ICommandsFactory>()
                ),
                Times.Once
            );
        }

        [Test]
        public void NavHolderFactoryTest()
        {
            // - Arrange
            var configMock = new Mock<NavixConfig> {CallBase = true};
            var service = new NavixService(configMock.Object);

            // - Act
            var holder = service.NavigatorHolder;
            var holder2 = service.NavigatorHolder;

            // - Assert
            Assert.IsNotNull(holder);
            Assert.AreSame(holder, holder2);
        }
    }
}