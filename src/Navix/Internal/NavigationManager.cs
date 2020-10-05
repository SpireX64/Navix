using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Spx.Navix.Commands;
using Spx.Navix.Platform;

namespace Spx.Navix.Internal
{
    public class NavigationManager: INavigationManager, INavigatorHolder
    {
        private readonly ConcurrentQueue<INavCommand> _pendingCommands = new ConcurrentQueue<INavCommand>();

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

        public bool HasPendingCommands => !_pendingCommands.IsEmpty;

        public void SendCommands(IEnumerable<INavCommand> navCommands)
        {
            foreach (var command in navCommands)
            {
                if (Navigator is null)
                    _pendingCommands.Enqueue(command);
                else
                    command.Apply(Navigator);
            }
        }

        private void ApplyPendingCommands()
        {
            if (Navigator is null) return;
            
            while (!_pendingCommands.IsEmpty)
            {
                if(Navigator is null) return;
                
                if (!_pendingCommands.TryDequeue(out var command)) continue;
                
                command.Apply(Navigator);
            }
        }
    }
}