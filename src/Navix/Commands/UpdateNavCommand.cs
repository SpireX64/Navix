using Spx.Navix.Platform;

namespace Spx.Navix.Commands
{
    public sealed class UpdateNavCommand: INavCommand
    {
        private readonly Screen _screen;

        public UpdateNavCommand(Screen screen)
        {
            _screen = screen;
        }
        
        public void Apply(Navigator navigator)
        {
            navigator.Update(_screen);
        }
    }
}