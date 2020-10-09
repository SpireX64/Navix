using System;
using System.Collections.Generic;
using Spx.Navix.Abstractions;

namespace Spx.Navix.Internal
{
    internal sealed class NavigationManager : INavigationManager, INavigatorHolder
    {
        private readonly Queue<INavCommand> _pendingCommands = new Queue<INavCommand>();

        // ReSharper disable once NotAccessedField.Local
        private readonly Type? _rootScreenType;
        private readonly ScreenStack _screens = new ScreenStack();
        private IReadOnlyCollection<INavigationMiddleware> _middlewares;

        public NavigationManager(IScreenRegistry registry)
        {
            _rootScreenType = registry.RootScreenType;
            _middlewares = new INavigationMiddleware[0];
        }

        public bool HasPendingCommands => _pendingCommands.Count > 0;
        public IEnumerable<Screen> Screens => _screens;

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

        public Navigator? Navigator { get; private set; }

        public void SetNavigator(Navigator navigator)
        {
            Navigator = navigator
                        ?? throw new ArgumentNullException(nameof(navigator));

            Specification = navigator.Specification;

            ApplyPendingCommands();
        }

        public void RemoveNavigator()
        {
            Navigator = null;
        }

        private void ApplyPendingCommands()
        {
            while (_pendingCommands.Count > 0)
            {
                if (Navigator is null) return;
                var command = _pendingCommands.Dequeue();

                InvokeMiddlewaresBefore(ref command);
                command.Apply(Navigator, _screens);
                InvokeMiddlewaresAfter(command);
            }
        }

        public void SetMiddlewares(IReadOnlyCollection<INavigationMiddleware> middlewares)
        {
            _middlewares = middlewares;
        }

        private void InvokeMiddlewaresBefore(ref INavCommand incomingCommand)
        {
            foreach (var middleware in _middlewares)
                middleware.BeforeApply(_screens.CurrentScreen, ref incomingCommand);
        }

        private void InvokeMiddlewaresAfter(INavCommand command)
        {
            foreach (var middleware in _middlewares)
                middleware.AfterApply(_screens.CurrentScreen, command);
        }
    }
}