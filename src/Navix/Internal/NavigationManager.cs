using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Spx.Navix.Abstractions;

namespace Spx.Navix.Internal
{
    internal sealed class NavigationManager : INavigationManager, INavigatorHolder
    {
        private readonly ConcurrentQueue<INavCommand> _pendingCommands = new ConcurrentQueue<INavCommand>();

        public bool HasPendingCommands => !_pendingCommands.IsEmpty;

        public void SendCommands(IEnumerable<INavCommand> navCommands)
        {
            foreach (var command in navCommands)
                if (Navigator is null)
                    _pendingCommands.Enqueue(command);
                else
                    command.Apply(Navigator);
        }

        public Navigator? Navigator { get; private set; }

        public void SetNavigator(Navigator navigator)
        {
            Navigator = navigator
                        ?? throw new ArgumentNullException(nameof(navigator));

            ApplyPendingCommands();
        }

        public void RemoveNavigator()
        {
            Navigator = null;
        }

        private void ApplyPendingCommands()
        {
            while (!_pendingCommands.IsEmpty)
            {
                if (Navigator is null) return;
                _pendingCommands.TryDequeue(out var command);
                command?.Apply(Navigator);
            }
        }
    }
}