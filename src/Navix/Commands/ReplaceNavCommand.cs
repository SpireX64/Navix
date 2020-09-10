namespace Spx.Navix.Commands
{
    public readonly struct ReplaceNavCommand<TScreen>: INavigationCommand where TScreen: Screen
    {
        public TScreen Screen { get; }
        public IScreenResolver<TScreen> Resolver { get; }

        public ReplaceNavCommand(TScreen screen, IScreenResolver<TScreen> resolver)
        {
            Screen = screen;
            Resolver = resolver;
        }
    }
}