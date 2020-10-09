using Spx.Navix.Abstractions;

namespace Spx.Navix.Commands
{
    public class ReplaceScreenNavCommand : INavCommand
    {
        private readonly IScreenResolver _resolver;
        private readonly Screen _screen;

        public ReplaceScreenNavCommand(Screen screen, IScreenResolver resolver)
        {
            _screen = screen;
            _resolver = resolver;
        }

        public void Apply(Navigator navigator, ScreenStack screens)
        {
            screens.Pop();
            screens.Push(_screen);
            navigator.Replace(_screen, _resolver);
        }
    }
}