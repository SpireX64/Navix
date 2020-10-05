using System;
using Spx.Navix.Commands;

namespace Spx.Navix.Internal
{
    public class DefaultRouter: IRouter
    {
        private readonly INavigationManager _navigationManager;
        private readonly ICommandsFactory _commandsFactory;

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