using System;
using Spx.Navix.Commands;
using Spx.Navix.Exceptions;
using Spx.Reflection;

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

        private IScreenResolver TryGetScreenResolver(Screen screen)
        {
            if (screen == null)
                throw new NullReferenceException("Given screen is null");

            var screenType = screen.GetType();
            var resolver = _screenRegistry.Resolve(screenType)
                           ?? throw new UnregisteredScreenException(screen);

            return resolver;
        }

        public void NavigateTo(Screen screen)
        {
            var resolver = TryGetScreenResolver(screen);
            var command = new ForwardNavCommand(screen, resolver);
            
            _manager.SendCommand(command);
        }

        public void Replace(Screen screen)
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

        public void BackTo(Class<Screen> screenClass)
        {
            var command = new BackToNavCommand(screenClass);
            _manager.SendCommand(command);
        }

        public void BackToRoot()
        {
            var command = new BackToRootNavCommand();
            _manager.SendCommand(command);
        }
    }
}