using System;
using Spx.Navix.Commands;

namespace Spx.Navix
{
    public class NavigationManager: INavigatorHolder
    {
        private volatile Navigator? _navigator = null;

        public Navigator? Navigator => _navigator;
        public void SetNavigator(Navigator navigator)
        {
            _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
        }

        public void RemoveNavigator()
        {
            _navigator = null;
        }

        public void ExecuteCommand(INavigationCommand command)
        {
            
        }
    }
}