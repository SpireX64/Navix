using System;
using System.Collections.Generic;
using Spx.Navix.Abstractions;
using Spx.Navix.Commands;

namespace Spx.Navix.Internal.Defaults
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
    }
}