namespace NavixPrototype.Navix.Commands
{
    public readonly struct ReplaceCommand: INavigationCommand
    {
        public readonly Screen Screen;

        public ReplaceCommand(Screen screen)
        {
            Screen = screen;
        }
    }
}