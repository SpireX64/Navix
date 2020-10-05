using System;
using Moq;
using Spx.Navix.Commands;
using Spx.Navix.Internal;
using Spx.Navix.Platform;
using Spx.Navix.UnitTests.Stubs;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class NavigationManagerTest
    {
        [Fact]
        public void NavigationManager_SetNavigator_NavigatorApplied()
        {
            // -- Arrange:
            var navigatorFake = new Mock<Navigator>().Object;
            var commandsFactoryFake = new Mock<ICommandsFactory>().Object;
            var manager = new NavigationManager(commandsFactoryFake);
            
            // -- Act:
            manager.SetNavigator(navigatorFake);
            
            // -- Assert:
            Assert.NotNull(manager.Navigator);
            Assert.Equal(navigatorFake, manager.Navigator);
        }

        [Fact]
        public void NavigatorManager_RemoveNavigator_NavigatorRemoved()
        {
            // -- Arrange:
            var navigatorFake = new Mock<Navigator>().Object;
            var commandsFactoryFake = new Mock<ICommandsFactory>().Object;
            var manager = new NavigationManager(commandsFactoryFake);
            manager.SetNavigator(navigatorFake);
            
            // -- Act:
            manager.RemoveNavigator();
            
            // -- Assert:
            Assert.Null(manager.Navigator);
        }

        [Fact]
        public void NavigatorManager_CreateWithNullCommandsFactory_ThrowsArgumentNull()
        {
            // -- Act & Assert:
            Assert.Throws<ArgumentNullException>(
                () => new NavigationManager(null!));
        }
    }
}