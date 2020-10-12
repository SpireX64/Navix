using Navix.Abstractions;

namespace Navix.Commands
{
    public class BackToRootNavCommand : INavCommand
    {
        public void Apply(Navigator navigator, ScreenStack screens)
        {
            if (screens.IsRoot) return;

            screens.Clear();
            navigator.BackToRoot();
        }
    }
}