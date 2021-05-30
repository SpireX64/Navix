using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Navix.Abstractions;
using Navix.Internal.Defaults;

namespace Navix
{
    /// <summary>
    ///     Class for configuring the navigation system
    /// </summary>
    public abstract class NavixConfig
    {
        /// <summary>
        ///     Navigation configuration
        /// </summary>
        /// <param name="reg">Screens registrar</param>
        public abstract void Configure(IScreenRegistrar reg);

        /// <summary>
        ///     Allows to override the used navigation commands factory
        /// </summary>
        /// <param name="registry">Screens registry</param>
        /// <returns>Navigation commands factory instance</returns>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
        public virtual ICommandsFactory GetCommandsFactory(IScreenRegistry registry)
        {
            return new DefaultCommandsFactory(registry);
        }

        /// <summary>
        ///     Allows to override the used router
        /// </summary>
        /// <param name="navigationManager">Navigation manager</param>
        /// <param name="commandsFactory">Navigation commands factory</param>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
        public virtual IRouter GetRouter(IScreenRegistry registry, INavigationManager navigationManager, ICommandsFactory commandsFactory)
        {
            return new DefaultRouter(registry, navigationManager, commandsFactory);
        }
    }
}