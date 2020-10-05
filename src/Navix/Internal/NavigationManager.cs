using System;
using Spx.Navix.Commands;
using Spx.Navix.Platform;

namespace Spx.Navix.Internal
{
    public class NavigationManager: INavigatorHolder
    {
        private readonly ICommandsFactory _commandsFactory;

        public NavigationManager(ICommandsFactory commandsFactory)
        {
            _commandsFactory = commandsFactory 
                               ?? throw new ArgumentNullException(nameof(commandsFactory));
        }

        public Navigator? Navigator { get; private set; }

        public void SetNavigator(Navigator navigator)
        {
            Navigator = navigator;
        }

        public void RemoveNavigator()
        {
            Navigator = null;
        }
    }
}