using System;
using Spx.Navix.Commands;
using Spx.Navix.Exceptions;

namespace Spx.Navix
{
    public class Router: IRouter
    {
        private readonly ScreenRegistry _screenRegistry;
        private readonly NavigationManager _manager;

        public INavigatorHolder NavigatorHolder => _manager;

        public Router(ScreenRegistry screenRegistry)
        {
            _screenRegistry = screenRegistry;
            _manager = new NavigationManager();
        }

        private IScreenResolver TryGetScreenResolver<TScreen>(TScreen screen) where TScreen : Screen
        {
            if (screen == null)
                throw new NullReferenceException("Given screen is null");
            
            var resolver = _screenRegistry.Resolve<TScreen>()
                           ?? throw new UnregisteredScreenException(screen);

            return resolver;
        }

        public void NavigateTo<TScreen>(TScreen screen) where TScreen : Screen
        {
            var resolver = TryGetScreenResolver(screen);
            var command = new ForwardNavCommand(screen, resolver);
            
            _manager.SendCommand(command);
        }

        public void Replace<TScreen>(TScreen screen) where TScreen : Screen
        {
            var resolver = TryGetScreenResolver(screen);
            var command = new ReplaceNavCommand(screen, resolver);
            
            _manager.SendCommand(command);
        }

        public void Back()
        {
            var command = new BackNavCommand();
            _manager.SendCommand(command);
        }

        public void BackTo<TScreen>() where TScreen : Screen
        {
            var command = BackToNavCommand.For<TScreen>();
            _manager.SendCommand(command);
        }

        public void BackToRoot()
        {
            var command = new BackToRootNavCommand();
            _manager.SendCommand(command);
        }
    }
}