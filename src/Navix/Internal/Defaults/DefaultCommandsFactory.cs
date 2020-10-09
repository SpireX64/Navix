using System;
using System.Collections.Generic;
using Spx.Navix.Abstractions;
using Spx.Navix.Commands;
using Spx.Reflection;

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

        public ICollection<INavCommand> BackToScreen(Class<Screen> screenClass)
        {
            var command = new BackToScreenNavCommand(screenClass);
            return new[] {command};
        }

        public ICollection<INavCommand> BackToRoot()
        {
            var command = new BackToRootNavCommand();
            return new[] {command};
        }

        public ICollection<INavCommand> ReplaceScreen(Screen screen)
        {
            var resolver = _registry.Resolve(screen);
            var command = new ReplaceScreenNavCommand(screen, resolver);
            return new[] {command};
        }
    }
}