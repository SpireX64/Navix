using System;
using System.Collections.Concurrent;
using Spx.Navix.Commands;

namespace Spx.Navix
{
    public class NavigationManager : INavigatorHolder
    {
        private readonly object _navigatorLocker = new object();
        private Navigator? _navigator = null;

        private readonly ConcurrentQueue<INavigationCommand> _pendingCommands =
            new ConcurrentQueue<INavigationCommand>();

        public Navigator? Navigator => _navigator;

        public bool HasPendingCommands => !_pendingCommands.IsEmpty;

        public void SetNavigator(Navigator navigator)
        {
            lock (_navigatorLocker)
            {
                _navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
                while (!_pendingCommands.IsEmpty)
                {
                    if (_pendingCommands.TryDequeue(out var command))
                        ApplyCommand(command);
                }
            }
        }

        public void RemoveNavigator()
        {
            lock (_navigatorLocker)
            {
                _navigator = null;
            }
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