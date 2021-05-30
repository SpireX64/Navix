using Moq;
using Moq.Protected;
using Navix.Abstractions;
using Navix.Internal.Defaults;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class NavixConfigTestFixture
    {
        [Test]
        public void DefaultRouterFactoryTest()
        {
            // - Arrange
            var navixConfigMock = new Mock<NavixConfig> {CallBase = true};

            var screenRegistry = new Mock<IScreenRegistry>().Object;
            var navManager = new Mock<INavigationManager>().Object;
            var commandFactory = new Mock<ICommandsFactory>().Object;
            
            // - Act
            var router = navixConfigMock.Object.GetRouter(
                screenRegistry,
                navManager,
                commandFactory
            );
            
            // - Assert
            Assert.IsNotNull(router);
            Assert.IsInstanceOf<DefaultRouter>(router);
        }

        [Test]
        public void DefaultCommandsFactoryTest()
        {
            // - Arrange
            var navixConfigMock = new Mock<NavixConfig> {CallBase = true};
            var screenRegistry = new Mock<IScreenRegistry>().Object;
            

            // - Act
            var commandsFactory = navixConfigMock.Object
                .GetCommandsFactory(screenRegistry);

            // - Assert
            Assert.IsNotNull(commandsFactory);
            Assert.IsInstanceOf<DefaultCommandsFactory>(commandsFactory);
        }
    }
}