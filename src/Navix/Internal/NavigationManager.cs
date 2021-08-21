using System;
using System.Collections.Generic;
using Navix.Abstractions;

namespace Navix.Internal
{
    internal sealed class NavigationManager : INavigationManager, INavigatorHolder
    {
        private volatile Navigator? _navigator;
        private readonly IScreenRegistry _registry;
        private readonly Queue<INavCommand> _pendingCommands = new Queue<INavCommand>();

        // ReSharper disable once NotAccessedField.Local
        private readonly ScreenStack _screens = new ScreenStack();

        public NavigationManager(IScreenRegistry registry)
        {
            _registry = registry;
        }

        public bool HasPendingCommands => _pendingCommands.Count > 0;
        public IEnumerable<Screen> Screens => _screens;
        public uint ScreensCount => _screens.Count;

        public NavigatorSpecification Specification { get; private set; }

        public void SendCommands(IEnumerable<INavCommand> navCommands)
        {
            foreach (var command in navCommands)
                if (Navigator is null)
                {
                    _pendingCommands.Enqueue(command);
                }
                else
                {
                    var commandRef = command;
                    InvokeMiddlewaresBefore(ref commandRef);
                    commandRef.Apply(Navigator, _screens);
                    InvokeMiddlewaresAfter(commandRef);
                }
        }

        public Navigator? Navigator
        {
            get => _navigator;
            private set => _navigator = value;
        }

        public void SetNavigator(Navigator navigator)
        {
            Navigator = navigator
                        ?? throw new ArgumentNullException(nameof(navigator));

            Specification = navigator.Specification;

            ApplyPendingCommands();
        }

        public void RemoveNavigator()
        {
            _navigator = null;
        }

        private void ApplyPendingCommands()
        {
            while (_pendingCommands.Count > 0)
            {
                if (_navigator is null) return;
                var command = _pendingCommands.Dequeue();

                InvokeMiddlewaresBefore(ref command);
                command.Apply(_navigator, _screens);
                InvokeMiddlewaresAfter(command);
            }
        }

        private void InvokeMiddlewaresBefore(ref INavCommand incomingCommand)
        {
            foreach (var middleware in _registry.Middlewares)
                middleware.BeforeApply(_screens.CurrentScreen, ref incomingCommand);
        }

        private void InvokeMiddlewaresAfter(INavCommand command)
        {
            foreach (var middleware in _registry.Middlewares)
                middleware.AfterApply(_screens.CurrentScreen, command);
        }
    }
}
