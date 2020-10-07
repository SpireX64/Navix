using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Spx.Navix.Abstractions;
using Spx.Navix.Internal.Defaults;

namespace Spx.Navix
{
    public abstract class NavixConfig
    {
        private readonly List<INavigationMiddleware> _middlewares = new List<INavigationMiddleware>();

        public abstract void Configure(IScreenRegistry registry);

        [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
        public virtual ICommandsFactory GetCommandsFactory(IScreenRegistry registry)
        {
            return new DefaultCommandsFactory(registry);
        }

        [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
        public virtual IRouter GetRouter(INavigationManager navigationManager, ICommandsFactory commandsFactory)
        {
            return new DefaultRouter(navigationManager, commandsFactory);
        }

        protected void AddMiddleware(INavigationMiddleware middleware)
        {
            _middlewares.Add(middleware);
        }

        internal IReadOnlyCollection<INavigationMiddleware> Middlewares => _middlewares;
    }
}