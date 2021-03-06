﻿using System;
using Navix.Abstractions;
using Navix.Internal;

namespace Navix
{
    /// <summary>
    ///     The core of the Navix system.
    ///     Used to initialize and maintain system components.
    /// </summary>
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
            //_navigationManager.SetMiddlewares(config.Middlewares);

            Router = config.GetRouter(_registry, _navigationManager, commandsFactory);
        }

        /// <summary>
        ///     The interface for high-level navigation
        /// </summary>
        public IRouter Router { get; }

        /// <summary>
        ///     Navigator holder
        /// </summary>
        public INavigatorHolder NavigatorHolder => _navigationManager;
    }
}