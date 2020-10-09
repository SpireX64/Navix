using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Spx.Navix.Abstractions;
using Spx.Navix.Internal.Defaults;

namespace Spx.Navix
{
    /// <summary>
    /// Class for configuring the navigation system
    /// </summary>
    public abstract class NavixConfig
    {
        private readonly List<INavigationMiddleware> _middlewares = new List<INavigationMiddleware>();

        internal IReadOnlyCollection<INavigationMiddleware> Middlewares => _middlewares;

        /// <summary>
        /// Navigation configuration
        /// </summary>
        /// <param name="registry">Screens registry</param>
        public abstract void Configure(IScreenRegistry registry);

        /// <summary>
        /// Allows to override the used navigation commands factory
        /// </summary>
        /// <param name="registry">Screens registry</param>
        /// <returns>Navigation commands factory instance</returns>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
        public virtual ICommandsFactory GetCommandsFactory(IScreenRegistry registry)
        {
            return new DefaultCommandsFactory(registry);
        }

        /// <summary>
        /// Allows to override the used router
        /// </summary>
        /// <param name="navigationManager">Navigation manager</param>
        /// <param name="commandsFactory">Navigation commands factory</param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
        public virtual IRouter GetRouter(INavigationManager navigationManager, ICommandsFactory commandsFactory)
        {
            return new DefaultRouter(navigationManager, commandsFactory);
        }

        /// <summary>
        /// Adds middleware to the navigation command execution pipeline
        /// </summary>
        /// <param name="middleware">Middleware</param>
        protected void AddMiddleware(INavigationMiddleware middleware)
        {
            _middlewares.Add(middleware);
        }
    }
}