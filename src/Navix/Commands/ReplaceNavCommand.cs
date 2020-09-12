namespace Spx.Navix.Commands
{
    public readonly struct ReplaceNavCommand: INavigationCommand
    {
        public Screen Screen { get; }
        public IScreenResolver Resolver { get; }

        public ReplaceNavCommand(Screen screen, IScreenResolver resolver)
        {
            Screen = screen;
            Resolver = resolver;
        }
    }
}