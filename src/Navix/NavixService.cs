using System;
using Spx.Navix.Abstractions;
using Spx.Navix.Internal;

namespace Spx.Navix
{
    public sealed class NavixService
    {
        private readonly NavigationManager _navigatorManager;
        private readonly ScreenRegistry _registry = new ScreenRegistry();

        public NavixService(NavixConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.ConfigureScreens(_registry);

            _navigatorManager = new NavigationManager();

            var commandsFactory = config.GetCommandsFactory(_registry);
            Router = config.GetRouter(_navigatorManager, commandsFactory);
        }

        public IRouter Router { get; }

        public INavigatorHolder NavigatorHolder => _navigatorManager;
    }
}