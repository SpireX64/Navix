using Spx.Navix.Abstractions;

namespace Spx.Navix.Commands
{
    public class ReplaceScreenNavCommand: INavCommand
    {
        private readonly Screen _screen;

        public ReplaceScreenNavCommand(Screen screen)
        {
            _screen = screen;
        }
        
        public void Apply(Navigator navigator, ScreenStack screens)
        {
            navigator.Replace(_screen);
        }
    }
}