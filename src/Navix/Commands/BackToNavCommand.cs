using Spx.Navix.Platform;

namespace Spx.Navix.Commands
{
    public sealed class BackToNavCommand : INavCommand
    {
        private readonly Screen _targetScreen;

        public BackToNavCommand(Screen targetScreen)
        {
            _targetScreen = targetScreen;
        }
        
        public void Apply(Navigator navigator)
        {
            navigator.BackTo(_targetScreen);
        }
    }
}