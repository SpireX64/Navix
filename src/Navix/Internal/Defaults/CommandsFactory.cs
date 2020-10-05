using System;
using System.Collections.Generic;
using Spx.Navix.Commands;
using Spx.Navix.Exceptions;

namespace Spx.Navix.Internal
{
    public sealed class CommandsFactory : ICommandsFactory
    {
        private readonly IScreenRegistry _registry;

        public CommandsFactory(IScreenRegistry registry)
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
    }
}