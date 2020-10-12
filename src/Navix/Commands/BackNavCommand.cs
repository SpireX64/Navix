using Navix.Abstractions;

namespace Navix.Commands
{
    public sealed class BackNavCommand : INavCommand
    {
        public void Apply(Navigator navigator, ScreenStack screens)
        {
            screens.Pop();
            navigator.Back();
        }
    }
}