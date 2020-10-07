using System;
using Spx.Navix.Abstractions;
using Spx.Navix.Internal;

namespace Spx.Navix
{
    public sealed class NavixService
    {
        private readonly NavigationManager _navigationManager;
        private readonly ScreenRegistry _registry = new ScreenRegistry();

        public NavixService(NavixConfig config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            config.Configure(_registry);

            var commandsFactory = config.GetCommandsFactory(_registry);
            _navigationManager = new NavigationManager(_registry);
            Router = config.GetRouter(_navigationManager, commandsFactory);
        }

        public IRouter Router { get; }

        public INavigatorHolder NavigatorHolder => _navigationManager;
    }
}