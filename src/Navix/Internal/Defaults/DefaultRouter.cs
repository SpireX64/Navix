using System;
using System.Collections.Generic;
using Navix.Abstractions;
using Spx.Reflection;

namespace Navix.Internal.Defaults
{
    internal class DefaultRouter : IRouter
    {
        private readonly ICommandsFactory _commandsFactory;
        private readonly INavigationManager _navigationManager;

        public DefaultRouter(INavigationManager navigationManager, ICommandsFactory commandsFactory)
        {
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
            var commands = _commandsFactory.Back();
            _navigationManager.SendCommands(commands);
        }

        public void BackToScreen(Class<Screen> screenClass)
        {
            var commands = _commandsFactory.BackToScreen(
                _navigationManager.Screens,
                _navigationManager.Specification,
                screenClass);
            _navigationManager.SendCommands(commands);
        }

        public void BackToRoot()
        {
            var commands = _commandsFactory.BackToRoot(
                _navigationManager.Screens,
                _navigationManager.Specification);
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