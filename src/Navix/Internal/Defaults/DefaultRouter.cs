using System;
using System.Collections.Generic;
using System.Linq;
using Navix.Abstractions;
using Navix.Exceptions;
using Spx.Reflection;

namespace Navix.Internal.Defaults
{
    internal class DefaultRouter : IRouter
    {
        private readonly ICommandsFactory _commandsFactory;
        private readonly IScreenRegistry _registry;
        private readonly INavigationManager _navigationManager;

        public DefaultRouter(IScreenRegistry registry, INavigationManager navigationManager, ICommandsFactory commandsFactory)
        {
            _registry = registry 
                        ?? throw new ArgumentNullException(nameof(navigationManager));
            
            _navigationManager = navigationManager
                                 ?? throw new ArgumentNullException(nameof(navigationManager));

            _commandsFactory = commandsFactory
                               ?? throw new ArgumentNullException(nameof(commandsFactory));
        }

        public IEnumerable<Screen> Screens => _navigationManager.Screens;

        public void Forward(Screen screen)
        {
            var commands = _commandsFactory.Forward(screen);
            _navigationManager.SendCommands(commands);
        }

        public void Back()
        {
            var count = _navigationManager.ScreensCount;
            if (count > 1)
            {
                var commands = _commandsFactory.Back();
                _navigationManager.SendCommands(commands);
            }
            else if (count == 1)
            {
                BackToRoot();
            }
        }

        public void BackToScreen(Class<Screen> screenClass)
        {
            var count = _navigationManager.ScreensCount;
            if (count <= 1) return;
            
            var commands = _commandsFactory.BackToScreen(
                _navigationManager.Screens,
                _navigationManager.Specification,
                screenClass
            );
            _navigationManager.SendCommands(commands);
        }

        public void BackToRoot()
        {
            ICollection<INavCommand> commands;
            if (_navigationManager.ScreensCount == 1)
            {
                var rootScreenClass = _registry.RootScreenClass;
                if (rootScreenClass is null)
                    throw new RootScreenIsNotDefinedException();

                var screen = _navigationManager.Screens.FirstOrDefault();
                if (screen != null && screen.GetType() == rootScreenClass) return;

                commands = _commandsFactory.ReplaceScreen(
                    _navigationManager.Screens,
                    _navigationManager.Specification,
                    (Screen) Activator.CreateInstance(rootScreenClass)
                );
            }
            else
            {
                commands = _commandsFactory.BackToRoot(
                    _navigationManager.Screens,
                    _navigationManager.Specification
                );
            }

            _navigationManager.SendCommands(commands);
        }

        public void Replace(Screen screen)
        {
            var commands = _commandsFactory.ReplaceScreen(
                _navigationManager.Screens,
                _navigationManager.Specification,
                screen);
            _navigationManager.SendCommands(commands);
        }
    }
}
