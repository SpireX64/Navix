using System;
using Moq;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class NavixServiceTest
    {
        [Fact]
        public void NavixService_CreateNew_NavixNotInitialized()
        {
            // -- Arrange:
            var navix = new NavixService();
            
            // -- Assert:
            Assert.False(navix.IsInitialized);
        }
        
        [Fact]
        public void NavixService_Initialize_NavixInitialized()
        {
            // -- Arrange:
            var configMock = new Mock<INavixConfig>();
            var navix = new NavixService();
            
            // -- Act:
            navix.Initialize(configMock.Object);
            
            // -- Assert:
            Assert.True(navix.IsInitialized);
            configMock.Verify(
                e => e.ConfigureScreens(It.IsAny<IScreenRegistry>()), 
                Times.Once);
        }

        [Fact]
        public void NavixService_PassNullAsConfiguration_ThrowsArgumentNull()
        {
            // -- Arrange:
            var navix = new NavixService();
            
            // -- Act & Assert:
            Assert.Throws<ArgumentNullException>(
                () => navix.Initialize(null!));
            Assert.False(navix.IsInitialized);
        }

        [Fact]
        public void NavixService_InitializeTwice_ThrowsInvalidOperation()
        {
            // -- Arrange:
            var configMock = new Mock<INavixConfig>();
            var navix = new NavixService();
            navix.Initialize(configMock.Object);

            // -- Act & Assert:
            Assert.Throws<InvalidOperationException>(
                () => navix.Initialize(configMock.Object));
        }

        [Fact]
        public void NavixService_GetRouterWithoutInit_ThrowsInvalidOperation()
        {
            // -- Arrange:
            var navix = new NavixService();
            
            // -- Act & Assert:
            Assert.Throws<InvalidOperationException>(
                () => navix.GetRouter());
        }

        [Fact]
        public void NavixService_GetRouterAfterInit()
        {
            // -- Arrange:
            var configMock = new Mock<INavixConfig>();
            var navix = new NavixService();
            navix.Initialize(configMock.Object);
            
            // -- Act:
            var router = navix.GetRouter();
            
            // -- Assert:
            Assert.NotNull(router);
        }

        [Fact]
        public void NavixService_GetRouterTwice_RoutersAreSame()
        {
            // -- Arrange:
            var configMock = new Mock<INavixConfig>();
            var navix = new NavixService();
            navix.Initialize(configMock.Object);
            
            // -- Act:
            var router1 = navix.GetRouter();
            var router2 = navix.GetRouter();
            
            // -- Assert:
            Assert.Equal(router1, router2);
        }
    }
}