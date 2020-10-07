using System;
using Spx.Navix.Abstractions;

namespace Spx.Navix.Internal.Defaults
{
    internal class DefaultRouter : IRouter
    {
        private readonly ICommandsFactory _commandsFactory;
        private readonly INavigationManager _navigationManager;

        public DefaultRouter(INavigationManager navigationManager, ICommandsFactory commandsFactory)
        {
            _navigationManager = navigationManager
                                 ?? throw new ArgumentNullException(nameof(navigationManager));

            _commandsFactory = commandsFactory
                               ?? throw new ArgumentNullException(nameof(commandsFactory));
        }

        public void Forward(Screen screen)
        {
            var commands = _commandsFactory.Forward(screen);
            _navigationManager.SendCommands(commands);
        }

        public void Back()
        {
            var commands = _commandsFactory.Back();
            _navigationManager.SendCommands(commands);
        }
    }
}