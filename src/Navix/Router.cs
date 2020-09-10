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

        private IScreenResolver<TScreen> TryGetScreenResolver<TScreen>(TScreen screen) where TScreen : Screen
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
            var command = new ForwardNavCommand<TScreen>(screen, resolver);
            
            _manager.ExecuteCommand(command);
        }

        public void Replace<TScreen>(TScreen screen) where TScreen : Screen
        {
            var resolver = TryGetScreenResolver(screen);
            var command = new ReplaceNavCommand<TScreen>(screen, resolver);
            
            _manager.ExecuteCommand(command);
        }

        public void Back()
        {
            var command = new BackNavCommand();
            _manager.ExecuteCommand(command);
        }

        public void BackTo<TScreen>() where TScreen : Screen
        {
            var command = BackToNavCommand.For<TScreen>();
            _manager.ExecuteCommand(command);
        }

        public void BackToRoot()
        {
            var command = new BackToRootNavCommand();
            _manager.ExecuteCommand(command);
        }
    }
}