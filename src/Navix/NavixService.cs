using System;
using Spx.Navix.Abstractions;
using Spx.Navix.Internal;

namespace Spx.Navix
{
    public sealed class NavixService
    {
        private readonly ScreenRegistry _registry = new ScreenRegistry();
        private readonly NavigationManager _navigationManager;

        public NavixService(NavixConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.ConfigureScreens(_registry);

            _navigationManager = new NavigationManager();

            var commandsFactory = config.GetCommandsFactory(_registry);
            Router = config.GetRouter(_navigationManager, commandsFactory);
        }

        public IRouter Router { get; }

        public INavigatorHolder NavigationHolder => _navigationManager;
    }
}