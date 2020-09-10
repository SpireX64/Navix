namespace Spx.Navix.Commands
{
    public readonly struct ForwardNavCommand: INavigationCommand
    {
        public Screen Screen { get; }
        public IScreenResolver Resolver { get; }

        public ForwardNavCommand(Screen screen, IScreenResolver resolver)
        {
            Screen = screen;
            Resolver = resolver;
        }
    }
}