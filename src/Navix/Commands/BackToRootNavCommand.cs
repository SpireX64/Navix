using Spx.Navix.Platform;

namespace Spx.Navix.Commands
{
    public sealed class BackToRootNavCommand : INavCommand
    {
        public void Apply(Navigator navigator)
        {
            navigator.BackToRoot();
        }
    }
}