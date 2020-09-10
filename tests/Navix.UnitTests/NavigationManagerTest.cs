using System;
using Moq;
using Spx.Navix.Commands;
using Spx.Navix.UnitTests.Stubs;
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
            Assert.False(mng.HasPendingCommands);
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
            Assert.False(mng.HasPendingCommands);
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

        [Fact]
        public void NavigationManager_SendForwardNavCommand_NavigatorOnForwardCalled()
        {
            // -- Arrange:
            var mng = new NavigationManager();
            var navigatorMock = new Mock<Navigator>();
            var navigator = navigatorMock.Object;
            mng.SetNavigator(navigator);
            
            var command = new ForwardNavCommand(new ScreenStub1(), new ScreenResolverStub1());
            
            // -- Act:
            mng.SendCommand(command);
            
            // -- Assert:
            navigatorMock.Verify(
                e => e.OnForward(command.Screen, command.Resolver), 
                Times.Once);
        }
        
        [Fact]
        public void NavigationManager_SendReplaceNavCommand_NavigatorOnReplaceCalled()
        {
            // -- Arrange:
            var mng = new NavigationManager();
            var navigatorMock = new Mock<Navigator>();
            var navigator = navigatorMock.Object;
            mng.SetNavigator(navigator);
            
            var command = new ReplaceNavCommand(new ScreenStub1(), new ScreenResolverStub1());
            
            // -- Act:
            mng.SendCommand(command);
            
            // -- Assert:
            navigatorMock.Verify(
                e => e.OnReplace(command.Screen, command.Resolver), 
                Times.Once);
        }
        
        [Fact]
        public void NavigationManager_SendBackNavCommand_NavigatorOnBackCalled()
        {
            // -- Arrange:
            var mng = new NavigationManager();
            var navigatorMock = new Mock<Navigator>();
            var navigator = navigatorMock.Object;
            mng.SetNavigator(navigator);
            
            var command = new BackNavCommand();
            
            // -- Act:
            mng.SendCommand(command);
            
            // -- Assert:
            navigatorMock.Verify(
                e => e.OnBack(), 
                Times.Once);
        }
        
        [Fact]
        public void NavigationManager_SendBackToNavCommand_NavigatorOnBackToCalled()
        {
            // -- Arrange:
            var mng = new NavigationManager();
            var navigatorMock = new Mock<Navigator>();
            var navigator = navigatorMock.Object;
            mng.SetNavigator(navigator);
            
            var command = new BackToNavCommand(typeof(ScreenStub1));
            
            // -- Act:
            mng.SendCommand(command);
            
            // -- Assert:
            navigatorMock.Verify(
                e => e.OnBackTo(command.ScreenType), 
                Times.Once);
        }
        
        [Fact]
        public void NavigationManager_SendBackToRootNavCommand_NavigatorOnBackToRootCalled()
        {
            // -- Arrange:
            var mng = new NavigationManager();
            var navigatorMock = new Mock<Navigator>();
            var navigator = navigatorMock.Object;
            mng.SetNavigator(navigator);
            
            var command = new BackToRootNavCommand();
            
            // -- Act:
            mng.SendCommand(command);
            
            // -- Assert:
            navigatorMock.Verify(
                e => e.OnBackToRoot(),
                Times.Once);
        }

        [Fact]
        public void NavigationManager_SendCommandWithoutNavigator_CommandIsPending()
        {
            // -- Arrange:
            var mng = new NavigationManager();
            
            // -- Act:
            mng.SendCommand(new BackNavCommand());
            
            // -- Assert:
            Assert.True(mng.HasPendingCommands);
        }

        [Fact]
        public void NavigationManager_WithPendingCommandsSetNavigator_CommandsWasApplied()
        {
            // -- Arrange:
            var mng = new NavigationManager();
            var navigatorMock = new Mock<Navigator>();
            var navigator = navigatorMock.Object;
            
            mng.SendCommand(new BackNavCommand());
            mng.SendCommand(new BackToRootNavCommand());
            
            // -- Act:
            mng.SetNavigator(navigator);
            
            // -- Assert
            Assert.False(mng.HasPendingCommands);
            navigatorMock.Verify(
                e => e.OnBack(), 
                Times.Once);
            navigatorMock.Verify(
                e => e.OnBackToRoot(),
                Times.Once);
        }
    }
}