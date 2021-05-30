using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Navix.Abstractions;
using Navix.Internal;
using NUnit.Framework;

namespace Navix.UnitTests
{
    [TestFixture]
    public class NavigationManagerTestFixture
    {
        [Test]
        public void CreateNewManagerTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            
            // - Act
            var manager = new NavigationManager(registry);

            // - Assert
            Assert.IsFalse(manager.HasPendingCommands);
            CollectionAssert.IsEmpty(manager.Screens);
            Assert.AreEqual(0, manager.ScreensCount);
        }
        
        [Test]
        public void SetNavigatorToManagerTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var navigator = new Mock<Navigator>().Object;
            var manager = new NavigationManager(registry);


            // - Act
            manager.SetNavigator(navigator);

            // - Assert
            Assert.AreEqual(navigator, manager.Navigator);
        }

        [Test]
        public void RemoveNavigatorFromManagerTest()
        {
            // - Arrange
            
            var registry = new ScreenRegistry();
            var navigator = new Mock<Navigator>().Object;
            var manager = new NavigationManager(registry);
            manager.SetNavigator(navigator);
            
            // - Act
            manager.RemoveNavigator();

            // - Assert
            Assert.IsNull(manager.Navigator);
        }

        [Test]
        public void GetNavigatorSpecs()
        {
            // - Arrange
            var navigatorSpecs = new NavigatorSpecification(true, false, true);
            var navigatorMock = new Mock<Navigator>();
            navigatorMock
                .SetupGet(it => it.Specification)
                .Returns(navigatorSpecs);
            var registry = new ScreenRegistry();
            var manager = new NavigationManager(registry);
            manager.SetNavigator(navigatorMock.Object);
            
            // - Act
            var specs = manager.Specification;

            // - Assert
            Assert.IsNotNull(specs);
            Assert.AreEqual(navigatorSpecs, specs);
            navigatorMock.VerifyGet(it => it.Specification, Times.Once);
        }

        [Test]
        public void GetNavigatorSpecsWithoutNavigator()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var manager = new NavigationManager(registry);
            
            // - Act
            var specs = manager.Specification;

            // - Assert
            Assert.IsNotNull(specs);
            Assert.IsFalse(specs.ReplaceScreenSupported);
            Assert.IsFalse(specs.BackToRootSupported);
            Assert.IsFalse(specs.BackToScreenSupported);
        }

        [Test]
        public void SendCommandsWithoutNavigatorTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var commandMock = new Mock<INavCommand>();
            var commands = new[] {commandMock.Object};
            
            var manager = new NavigationManager(registry);

            
            // - Act
            manager.SendCommands(commands);
            
            // - Assert
            Assert.IsTrue(manager.HasPendingCommands);
            commandMock.Verify(
                it => it.Apply(
                    It.IsAny<Navigator>(),
                    It.IsAny<ScreenStack>()
                    ),
                    Times.Never
                );
        }

        [Test]
        public void SendCommandsWithNavigator()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var navigatorStub = new Mock<Navigator>().Object;
            var commandMock = new Mock<INavCommand>();
            var commands = new[] {commandMock.Object};
            
            var manager = new NavigationManager(registry);
            manager.SetNavigator(navigatorStub);
            
            // - Act
            manager.SendCommands(commands);

            // - Assert
            Assert.IsFalse(manager.HasPendingCommands);
            commandMock.Verify(
                it => it.Apply(
                    It.IsAny<Navigator>(),
                    It.IsAny<ScreenStack>()
                ),
                Times.Once
            );
        }

        [Test]
        public void MiddlewareInvokeOrderTest()
        {
            // - Arrange
            var invokeCounter = 0;
            var firstMiddlewareBeforeIndex = -1;
            var firstMiddlewareAfterIndex = -1;
            var secondMiddlewareBeforeIndex = -1;
            var secondMiddlewareAfterIndex = -1;
            
            var firstMiddlewareMock = new Mock<INavigationMiddleware>();
            firstMiddlewareMock
                .Setup(it => it.BeforeApply(
                    It.IsAny<Screen>(),
                    ref It.Ref<INavCommand>.IsAny))
                .Callback(() => firstMiddlewareBeforeIndex = invokeCounter++);
            firstMiddlewareMock
                .Setup(it => it.AfterApply(
                    It.IsAny<Screen>(),
                    It.IsAny<INavCommand>()))
                .Callback(() => firstMiddlewareAfterIndex = invokeCounter++);

            var secondMiddlewareMock = new Mock<INavigationMiddleware>();
            secondMiddlewareMock
                .Setup(it => it.BeforeApply(
                    It.IsAny<Screen>(),
                    ref It.Ref<INavCommand>.IsAny))
                .Callback(() => secondMiddlewareBeforeIndex = invokeCounter++);
            secondMiddlewareMock
                .Setup(it => it.AfterApply(
                    It.IsAny<Screen>(),
                    It.IsAny<INavCommand>()))
                .Callback(() => secondMiddlewareAfterIndex = invokeCounter++);

            
            var registry = new ScreenRegistry();
            registry.AddMiddleware(firstMiddlewareMock.Object);
            registry.AddMiddleware(secondMiddlewareMock.Object);
            
            var navigatorStub = new Mock<Navigator>().Object;
            var commandMock = new Mock<INavCommand>();
            var commands = new[] {commandMock.Object};
            
            var manager = new NavigationManager(registry);
            manager.SetNavigator(navigatorStub);
            
            // - Act
            manager.SendCommands(commands);

            // - Assert
            Assert.IsFalse(manager.HasPendingCommands);
            Assert.AreEqual(0, firstMiddlewareBeforeIndex);
            firstMiddlewareMock.Verify(
                it => it.BeforeApply(
                    It.IsAny<Screen>(),
                    ref It.Ref<INavCommand>.IsAny),
                Times.Once
            );
            
            Assert.AreEqual(1, secondMiddlewareBeforeIndex);
            secondMiddlewareMock.Verify(
                it => it.BeforeApply(
                    It.IsAny<Screen>(),
                    ref It.Ref<INavCommand>.IsAny),
                Times.Once
            );
            
            Assert.AreEqual(2, firstMiddlewareAfterIndex);
            firstMiddlewareMock.Verify(
                it => it.AfterApply(
                    It.IsAny<Screen>(),
                    It.IsAny<INavCommand>()),
                Times.Once
            );
            
            Assert.AreEqual(3, secondMiddlewareAfterIndex);
            secondMiddlewareMock.Verify(
                it => it.AfterApply(
                    It.IsAny<Screen>(),
                    It.IsAny<INavCommand>()),
                Times.Once
            );
        }

        [Test]
        public void ApplyPendingCommandsTest()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var commandMock = new Mock<INavCommand>();
            var commands = new[] {commandMock.Object};
            var manager = new NavigationManager(registry);
            var navigatorMock = new Mock<Navigator>();

            // - Act
            manager.SendCommands(commands);
            manager.SetNavigator(navigatorMock.Object);
            
            // - Assert
            Assert.IsFalse(manager.HasPendingCommands);
            commandMock.Verify(
                it => it.Apply(It.IsAny<Navigator>(),
                    It.IsAny<ScreenStack>()
                    ),
                Times.Once);
        }

        [Test]
        public void RemoveNavigatorOnApplyingCommands()
        {
            // - Arrange
            var registry = new ScreenRegistry();
            var manager = new NavigationManager(registry);
            
            var commandToRemoveNavigatorMock = new Mock<INavCommand>();
            commandToRemoveNavigatorMock
                .Setup(it => it.Apply(
                    It.IsAny<Navigator>(),
                    It.IsAny<ScreenStack>()))
                .Callback(() => manager.RemoveNavigator());
            var commandToStayPendingMock = new Mock<INavCommand>();
            
            var commands = new[]
            {
                commandToRemoveNavigatorMock.Object,
                commandToStayPendingMock.Object
            };
            var navigatorMock = new Mock<Navigator>();
            
            // - Act
            manager.SendCommands(commands);
            manager.SetNavigator(navigatorMock.Object);
            
            // - Assert
            Assert.IsNull(manager.Navigator);
            Assert.True(manager.HasPendingCommands);
            commandToRemoveNavigatorMock.Verify(
                it => it.Apply(It.IsAny<Navigator>(), It.IsAny<ScreenStack>()),
                Times.Once);
            commandToStayPendingMock.Verify(
                it => it.Apply(It.IsAny<Navigator>(), It.IsAny<ScreenStack>()),
                Times.Never);
        }
    }
}