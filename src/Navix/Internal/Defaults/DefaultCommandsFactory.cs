using System;
using System.Collections.Generic;
using System.Linq;
using Navix.Abstractions;
using Navix.Commands;
using Spx.Collections.Linq;
using Spx.Reflection;

namespace Navix.Internal.Defaults
{
    internal sealed class DefaultCommandsFactory : ICommandsFactory
    {
        private readonly IScreenRegistry _registry;

        public DefaultCommandsFactory(IScreenRegistry registry)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        }

        public ICollection<INavCommand> Forward(Screen screen)
        {
            var resolver = _registry.Resolve(screen);

            var command = new ForwardNavCommand(screen, resolver);
            return new[] {command};
        }

        public ICollection<INavCommand> Back()
        {
            var command = new BackNavCommand();
            return new[] {command};
        }

        public ICollection<INavCommand> BackToScreen(
            IEnumerable<Screen> screens,
            NavigatorSpecification spec,
            Class<Screen> screenClass)
        {
            var commands = new List<INavCommand>();
            if (spec.BackToScreenSupported)
            {
                commands.Add(new BackToScreenNavCommand(screenClass));
            }
            else
            {
                var position = screens
                    .Select(e => e.GetType())
                    .PositionOf<Type>(screenClass);

                for (var i = 0; i < position; i++)
                    commands.Add(new BackNavCommand());
            }

            return commands;
        }

        public ICollection<INavCommand> BackToRoot(
            IEnumerable<Screen> screens,
            NavigatorSpecification spec)
        {
            var commands = new List<INavCommand>();
            if (spec.BackToRootSupported)
                commands.Add(new BackToRootNavCommand());
            else
                // ReSharper disable once PossibleMultipleEnumeration
                for (var i = 0; i < screens.Count(); i++)
                    commands.Add(new BackNavCommand());
            return commands;
        }

        public ICollection<INavCommand> ReplaceScreen(
            IEnumerable<Screen> screens,
            NavigatorSpecification spec,
            Screen screen)
        {
            var resolver = _registry.Resolve(screen);

            var commands = new List<INavCommand>();
            if (spec.ReplaceScreenSupported)
            {
                commands.Add(new ReplaceScreenNavCommand(screen, resolver));
            }
            else
            {
                commands.Add(new BackNavCommand());
                commands.Add(new ForwardNavCommand(screen, resolver));
            }

            return commands;
        }
    }
}