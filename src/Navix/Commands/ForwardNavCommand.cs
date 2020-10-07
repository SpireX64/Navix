using Spx.Navix.Abstractions;

namespace Spx.Navix.Commands
{
    public sealed class ForwardNavCommand : INavCommand
    {
        private readonly Screen _screen;
        private readonly IScreenResolver _screenResolver;

        public ForwardNavCommand(Screen screen, IScreenResolver resolver)
        {
            _screen = screen;
            _screenResolver = resolver;
        }

        public void Apply(Navigator navigator)
        {
            navigator.Forward(_screen, _screenResolver);
        }
    }
}