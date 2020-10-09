using Spx.Navix.Abstractions;

namespace Spx.Navix.Commands
{
    public class BackToRootNavCommand: INavCommand
    {
        public void Apply(Navigator navigator, ScreenStack screens)
        {
            screens.Clear();
            navigator.BackToRoot();
        }
    }
}