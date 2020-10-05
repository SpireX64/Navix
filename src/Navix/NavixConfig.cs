using Spx.Navix.Abstractions;
using Spx.Navix.Commands;
using Spx.Navix.Internal.Defaults;

namespace Spx.Navix
{
    public abstract class NavixConfig
    {
        public abstract void ConfigureScreens(IScreenRegistry registry);

        public virtual ICommandsFactory GetCommandsFactory(IScreenRegistry registry)
        {
            return new CommandsFactory(registry);
        }

        public virtual IRouter GetRouter(INavigationManager navigationManager, ICommandsFactory commandsFactory)
        {
            return new DefaultRouter(navigationManager, commandsFactory);
        }
    }
}