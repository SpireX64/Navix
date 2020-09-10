using System;
using Spx.Navix.Exceptions;
using Spx.Navix.UnitTests.Stubs;
using Xunit;

namespace Spx.Navix.UnitTests
{
    public class RouterTest
    {
        [Fact]
        public void Router_GetNavigatorHolder_NavigatorHolderNotNull()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            var router = new Router(registry);
            
            // -- Act:
            var holder = router.NavigatorHolder;
            
            // -- Assert:
            Assert.NotNull(holder);
        }

        [Fact]
        public void Router_TryNavigateToNull_ThrowsNullRef()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            var router = new Router(registry);
            
            // -- Act & Assert:
            Assert.Throws<NullReferenceException>(
                () => router.NavigateTo<Screen>(null!));
        }

        [Fact]
        public void Router_NavigateToNotRegisteredScreen_ThrowsScreenNotRegistered()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            var router = new Router(registry);
            var screen = new ScreenStub1();
            
            // -- Act & Assert:
            var exception = Assert.Throws<UnregisteredScreenException>(
                () => router.NavigateTo(screen));
            Assert.Equal(screen, exception.Screen);
        }

        [Fact]
        public void Router_NavigateToScreen_NoThrow()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            registry.Register<ScreenStub1>(new ScreenResolverStub1());

            var router = new Router(registry);
            var screen = new ScreenStub1();
            
            // Act:
            router.NavigateTo(screen);
            
            // Assert:
            // no-throw
        }
        
        [Fact]
        public void Router_TryReplaceToNull_ThrowsNullRef()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            var router = new Router(registry);
            
            // -- Act & Assert:
            Assert.Throws<NullReferenceException>(
                () => router.Replace<Screen>(null!));
        }

        [Fact]
        public void Router_ReplaceToNotRegisteredScreen_ThrowsScreenNotRegistered()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            var router = new Router(registry);
            var screen = new ScreenStub1();
            
            // -- Act & Assert:
            var exception = Assert.Throws<UnregisteredScreenException>(
                () => router.Replace(screen));
            Assert.Equal(screen, exception.Screen);
        }
        
        [Fact]
        public void Router_ReplaceScreen_NoThrow()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            registry.Register<ScreenStub1>(new ScreenResolverStub1());
            
            var router = new Router(registry);
            var screen = new ScreenStub1();
            
            // Act:
            router.Replace(screen);
            
            // Assert:
            // no-throw
        }
        
        [Fact]
        public void Router_BackToScreen_NoThrow()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            var router = new Router(registry);
            
            // Act:
            router.BackTo<ScreenStub1>();
            
            // Assert:
            // no-throw
        }
        
        [Fact]
        public void Router_Back_NoThrow()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            var router = new Router(registry);
            
            // Act:
            router.Back();
            
            // Assert:
            // no-throw
        }
        
        [Fact]
        public void Router_BackToRoot_NoThrow()
        {
            // -- Arrange:
            var registry = new ScreenRegistry();
            var router = new Router(registry);
            
            // Act:
            router.BackToRoot();
            
            // Assert:
            // no-throw
        }
    }
}