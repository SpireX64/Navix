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
            var entry = _registry.Resolve(screen);
            var command = new ForwardNavCommand(screen, entry.Resolver);
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
                    .PositionOf(screenClass.Type);

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
            {
                var count = screens.Count();
                for (var i = 1; i < count; i++)
                    commands.Add(new BackNavCommand());
            }

            return commands;
        }

        public ICollection<INavCommand> ReplaceScreen(
            IEnumerable<Screen> screens,
            NavigatorSpecification spec,
            Screen screen)
        {
            var entry = _registry.Resolve(screen);

            var commands = new List<INavCommand>();
            if (spec.ReplaceScreenSupported)
            {
                commands.Add(new ReplaceScreenNavCommand(screen, entry.Resolver));
            }
            else
            {
                commands.Add(new BackNavCommand());
                commands.Add(new ForwardNavCommand(screen, entry.Resolver));
            }

            return commands;
        }
    }
}
