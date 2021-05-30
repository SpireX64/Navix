using System.Collections.Generic;
using Moq;
using Navix.Abstractions;
using Navix.Exceptions;
using Navix.Internal.Defaults;
using Navix.UnitTests.Stubs;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class DefaultRouterTestFixture
    {
        [Test]
        public void CurrentScreensGetterTest()
        {
            // - Arrange
            var expectedScreens = new Screen[]
            {
                new ScreenStub(),
                new ScreenStub(),
            };
            
            var registryMock = new Mock<IScreenRegistry>();
            var navManagerMock = new Mock<INavigationManager>();
            navManagerMock
                .SetupGet(it => it.Screens)
                .Returns(expectedScreens);
            
            var commandsFactoryMock = new Mock<ICommandsFactory>();
            
            var router = new DefaultRouter(
                registryMock.Object, 
                navManagerMock.Object, 
                commandsFactoryMock.Object
            );

            // - Act
            var screens = router.Screens;

            // - Assert
            navManagerMock.VerifyGet(it => it.Screens,Times.Once);
            CollectionAssert.AreEqual(expectedScreens, screens);
        }
        
        [Test]
        public void NavigateForwardTest()
        {
            // - Arrange
            var screen = new ScreenStub();
            
            var registryMock = new Mock<IScreenRegistry>();
            var navManagerMock = new Mock<INavigationManager>();
            var commandsFactoryMock = new Mock<ICommandsFactory>();
            
            var router = new DefaultRouter(
                registryMock.Object, 
                navManagerMock.Object, 
                commandsFactoryMock.Object
            );

            // - Act
            router.Forward(screen);
            
            // - Assert
            commandsFactoryMock.Verify(
                it => it.Forward(screen),
                Times.Once);
            
            navManagerMock.Verify(
                it => it.SendCommands(It.IsAny<IEnumerable<INavCommand>>()),
                Times.Once);
        }

        [Test]
        public void NavigateBackTest()
        {
            // - Arrange
            const int screensCount = 2;
            var registryMock = new Mock<IScreenRegistry>();
            var navManagerMock = new Mock<INavigationManager>();
            navManagerMock
                .SetupGet(it => it.ScreensCount)
                .Returns(screensCount);
            
            var commandsFactoryMock = new Mock<ICommandsFactory>();
            
            var router = new DefaultRouter(
                registryMock.Object, 
                navManagerMock.Object, 
                commandsFactoryMock.Object
            );

            // - Act
            router.Back();

            // - Assert
            commandsFactoryMock.Verify(
                it => it.Back(),
                Times.Once);
            navManagerMock.Verify(
                it => it.SendCommands(It.IsAny<IEnumerable<INavCommand>>()),
                Times.Once);
        }

        [Test]
        public void NavigateBackOnSingleScreenTest()
        {
            // - Arrange
            const int screensCount = 1;
            var rootScreenType = typeof(ScreenStub);
            
            var registryMock = new Mock<IScreenRegistry>();
            registryMock
                .SetupGet(it => it.RootScreenClass)
                .Returns(rootScreenType);
            
            var navManagerMock = new Mock<INavigationManager>();
            navManagerMock
                .SetupGet(it => it.ScreensCount)
                .Returns(screensCount);
            
            var commandsFactoryMock = new Mock<ICommandsFactory>();
            
            var router = new DefaultRouter(
                registryMock.Object, 
                navManagerMock.Object, 
                commandsFactoryMock.Object
            );

            // - Act
            router.Back();

            // - Assert
            commandsFactoryMock.Verify(
                it => it.ReplaceScreen(
                    It.IsAny<IEnumerable<Screen>>(),
                    It.IsAny<NavigatorSpecification>(),
                    It.IsAny<ScreenStub>()
                ),
                Times.Once);
            navManagerMock.Verify(
                it => it.SendCommands(It.IsAny<IEnumerable<INavCommand>>()),
                Times.Once);
        }

        [Test]
        public void NavigateBackOnSingleScreenWithoutDefinedRoot()
        {
            // - Arrange
            const int screensCount = 1;

            var registryMock = new Mock<IScreenRegistry>();

            var navManagerMock = new Mock<INavigationManager>();
            navManagerMock
                .SetupGet(it => it.ScreensCount)
                .Returns(screensCount);

            var commandsFactoryMock = new Mock<ICommandsFactory>();

            var router = new DefaultRouter(
                registryMock.Object,
                navManagerMock.Object,
                commandsFactoryMock.Object
            );

            // - Act & Assert
            Assert.Throws<RootScreenIsNotDefinedException>(
                () => router.Back());
        }

        [Test]
        public void NavigateBackToScreenTest()
        {
            // - Arrange
            const int screensCount = 2;
            var screen = typeof(ScreenStub);
            
            var registryMock = new Mock<IScreenRegistry>();

            var navManagerMock = new Mock<INavigationManager>();
            navManagerMock
                .SetupGet(it => it.ScreensCount)
                .Returns(screensCount);

            var commandsFactoryMock = new Mock<ICommandsFactory>();

            var router = new DefaultRouter(
                registryMock.Object,
                navManagerMock.Object,
                commandsFactoryMock.Object
            );
            
            // - Act
            router.BackToScreen(screen);

            // - Assert
            commandsFactoryMock.Verify(
                it => it.BackToScreen(
                    It.IsAny<IEnumerable<Screen>>(),
                    It.IsAny<NavigatorSpecification>(),
                    screen),
                Times.Once);
            
            navManagerMock.Verify(
                it => it.SendCommands(It.IsAny<IEnumerable<INavCommand>>()),
                Times.Once);
        }

        [Test]
        public void NavigateBackToScreenOnSingleScreenTest()
        {
            // - Arrange
            const int screensCount = 1;
            var screen = typeof(ScreenStub);
            
            var registryMock = new Mock<IScreenRegistry>();

            var navManagerMock = new Mock<INavigationManager>();
            navManagerMock
                .SetupGet(it => it.ScreensCount)
                .Returns(screensCount);

            var commandsFactoryMock = new Mock<ICommandsFactory>();

            var router = new DefaultRouter(
                registryMock.Object,
                navManagerMock.Object,
                commandsFactoryMock.Object
            );

            // - Act
            router.BackToScreen(screen);

            // - Assert
            commandsFactoryMock.Verify(
                it => it.BackToScreen(
                    It.IsAny<IEnumerable<Screen>>(),
                    It.IsAny<NavigatorSpecification>(),
                    screen),
                Times.Never);
            
            navManagerMock.Verify(
                it => it.SendCommands(It.IsAny<IEnumerable<INavCommand>>()),
                Times.Never);
        }

        [Test]
        public void NavigateBackToRootScreenTest()
        {
            // - Arrange
            const int screensCount = 2;
            
            var registryMock = new Mock<IScreenRegistry>();

            var navManagerMock = new Mock<INavigationManager>();
            navManagerMock
                .SetupGet(it => it.ScreensCount)
                .Returns(screensCount);

            var commandsFactoryMock = new Mock<ICommandsFactory>();

            var router = new DefaultRouter(
                registryMock.Object,
                navManagerMock.Object,
                commandsFactoryMock.Object
            );

            // - Act
            router.BackToRoot();

            // - Assert
            commandsFactoryMock.Verify(
                it => it.BackToRoot(
                    It.IsAny<IEnumerable<Screen>>(),
                    It.IsAny<NavigatorSpecification>()
                ),
                Times.Once);
            
            navManagerMock.Verify(
                it => it.SendCommands(It.IsAny<IEnumerable<INavCommand>>()),
                Times.Once);
        }

        [Test]
        public void NavigateBackToRootOnSingleScreenAndRootNotDefined()
        {
            // - Arrange
            const int screensCount = 1;
            
            var registryMock = new Mock<IScreenRegistry>();

            var navManagerMock = new Mock<INavigationManager>();
            navManagerMock
                .SetupGet(it => it.ScreensCount)
                .Returns(screensCount);

            var commandsFactoryMock = new Mock<ICommandsFactory>();

            var router = new DefaultRouter(
                registryMock.Object,
                navManagerMock.Object,
                commandsFactoryMock.Object
            );

            // - Act & Assert
            Assert.Throws<RootScreenIsNotDefinedException>(
                () => router.BackToRoot());
        }

        [Test]
        public void NavigateBackToRootOnSingleScreen()
        {
            // - Arrange
            const int screensCount = 1;
            var rootScreenType = typeof(ScreenStub);
            
            var registryMock = new Mock<IScreenRegistry>();
            registryMock
                .SetupGet(it => it.RootScreenClass)
                .Returns(rootScreenType);
            
            var navManagerMock = new Mock<INavigationManager>();
            navManagerMock
                .SetupGet(it => it.ScreensCount)
                .Returns(screensCount);
            
            var commandsFactoryMock = new Mock<ICommandsFactory>();
            
            var router = new DefaultRouter(
                registryMock.Object, 
                navManagerMock.Object, 
                commandsFactoryMock.Object
            );

            // - Act
            router.BackToRoot();
            
            // - Assert
            commandsFactoryMock.Verify(
                it => it.ReplaceScreen(
                    It.IsAny<IEnumerable<Screen>>(),
                    It.IsAny<NavigatorSpecification>(),
                    It.IsAny<ScreenStub>()
                ),
                Times.Once);
            
            navManagerMock.Verify(
                it => it.SendCommands(It.IsAny<IEnumerable<INavCommand>>()),
                Times.Once);
        }

        [Test]
        public void NavigateBackToRootButCurrentScreenIsRoot()
        {
            // - Arrange
            const int screensCount = 1;
            var rootScreenType = typeof(ScreenStub);
            
            var registryMock = new Mock<IScreenRegistry>();
            registryMock
                .SetupGet(it => it.RootScreenClass)
                .Returns(rootScreenType);
            
            var navManagerMock = new Mock<INavigationManager>();
            navManagerMock
                .SetupGet(it => it.ScreensCount)
                .Returns(screensCount);
            navManagerMock
                .SetupGet(it => it.Screens)
                .Returns(new[]
                {
                    new ScreenStub(),
                });
            
            var commandsFactoryMock = new Mock<ICommandsFactory>();
            
            var router = new DefaultRouter(
                registryMock.Object,
                navManagerMock.Object,
                commandsFactoryMock.Object
            );

            // - Act
            router.BackToRoot();

            // - Assert
            commandsFactoryMock.Verify(
                it => it.BackToRoot(
                    It.IsAny<IEnumerable<Screen>>(),
                    It.IsAny<NavigatorSpecification>()
                ),
                Times.Never);
            
            navManagerMock.Verify(
                it => it.SendCommands(It.IsAny<IEnumerable<INavCommand>>()),
                Times.Never);
        }

        [Test]
        public void ReplaceScreenTest()
        {
            // - Arrange
            var screen = new ScreenStub();
            
            var registryMock = new Mock<IScreenRegistry>();
            var navManagerMock = new Mock<INavigationManager>();
            var commandsFactoryMock = new Mock<ICommandsFactory>();
            
            var router = new DefaultRouter(
                registryMock.Object, 
                navManagerMock.Object, 
                commandsFactoryMock.Object
            );

            // - Act
            router.Replace(screen);

            // - Assert
            commandsFactoryMock.Verify(
                it => it.ReplaceScreen(
                    It.IsAny<IEnumerable<Screen>>(),
                    It.IsAny<NavigatorSpecification>(),
                    screen),
                Times.Once);
            
            navManagerMock.Verify(
                it => it.SendCommands(It.IsAny<IEnumerable<INavCommand>>()),
                Times.Once);
        }
    }
}