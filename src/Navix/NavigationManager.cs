using System;
using System.Collections.Concurrent;
using Spx.Navix.Commands;

namespace Spx.Navix
{
    public class NavigationManager : INavigatorHolder
    {
        private volatile Navigator? _navigator = null;

        private readonly ConcurrentQueue<INavigationCommand> _pendingCommands =
            new ConcurrentQueue<INavigationCommand>();

        public Navigator? Navigator => _navigator;

        public void SetNavigator(Navigator navigator)
        {
            _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
        }

        public void RemoveNavigator()
        {
            _navigator = null;
        }

        public void SendCommand(INavigationCommand command)
        {
            if (_navigator is null)
                _pendingCommands.Enqueue(command);
            else
                ApplyCommand(command);
        }

        private void ApplyCommand(INavigationCommand command)
        {
            switch (command)
            {
                case ForwardNavCommand forwardNavCommand:
                    _navigator!.OnForward(
                        forwardNavCommand.Screen,
                        forwardNavCommand.Resolver);
                    break;

                case ReplaceNavCommand replaceNavCommand:
                    _navigator!.OnReplace(
                        replaceNavCommand.Screen,
                        replaceNavCommand.Resolver);
                    break;

                case BackNavCommand _:
                    _navigator!.OnBack();
                    break;

                case BackToNavCommand backToNavCommand:
                    _navigator!.OnBackTo(backToNavCommand.ScreenType);
                    break;

                case BackToRootNavCommand _:
                    _navigator!.OnBackToRoot();
                    break;
            }
        }
    }
}