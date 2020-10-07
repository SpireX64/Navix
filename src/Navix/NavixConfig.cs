using Spx.Navix.Abstractions;
using Spx.Navix.Internal.Defaults;

namespace Spx.Navix
{
    public abstract class NavixConfig
    {
        public abstract void Configure(IScreenRegistry registry);

        public virtual ICommandsFactory GetCommandsFactory(IScreenRegistry registry)
        {
            return new DefaultCommandsFactory(registry);
        }

        public virtual IRouter GetRouter(INavigationManager navigationManager, ICommandsFactory commandsFactory)
        {
            return new DefaultRouter(navigationManager, commandsFactory);
        }
    }
}