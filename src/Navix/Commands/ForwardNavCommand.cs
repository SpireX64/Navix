namespace Spx.Navix.Commands
{
    public readonly struct ForwardNavCommand<TScreen>: INavigationCommand where TScreen: Screen
    {
        public TScreen Screen { get; }
        public IScreenResolver<TScreen> Resolver { get; }

        public ForwardNavCommand(TScreen screen, IScreenResolver<TScreen> resolver)
        {
            Screen = screen;
            Resolver = resolver;
        }
    }
}