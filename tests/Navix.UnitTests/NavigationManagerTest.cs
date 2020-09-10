using System;
using Moq;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class NavigationManagerTest
    {
        [Fact]
        public void NavigatorManager_TryGetNavigator_NavigatorIsNull()
        {
            // -- Arrange:
            var mng = new NavigationManager();
            
            // -- Assert:
            Assert.Null(mng.Navigator);
        }
        
        [Fact]
        public void NavigationManager_SetNavigator_NavigatorNotNull()
        {
            // -- Arrange:
            var mng = new NavigationManager();
            var navigatorMock = new Mock<Navigator>();
            var navigator = navigatorMock.Object;
            
            // -- Act:
            mng.SetNavigator(navigator);
            
            // -- Assert:
            Assert.NotNull(mng.Navigator);
            Assert.Equal(navigator, mng.Navigator);
        }

        [Fact]
        public void NavigationManager_SetNullNavigator_ThrowsArgumentNull()
        {
            // -- Arrange:
            var mng = new NavigationManager();
            
            // -- Act & Assert:
            Assert.Throws<ArgumentNullException>(
                () => mng.SetNavigator(null!));
        }

        [Fact]
        public void NavigationManager_RemoveNavigator_NavigatorIsNullAfterRemove()
        {
            
            // -- Arrange:
            var mng = new NavigationManager();
            var navigatorMock = new Mock<Navigator>();
            var navigator = navigatorMock.Object;
            mng.SetNavigator(navigator);
            
            // -- Act:
            mng.RemoveNavigator();
            
            // -- Assert:
            Assert.Null(mng.Navigator);
        }
    }
}