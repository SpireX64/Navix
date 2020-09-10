namespace NavixPrototype.Navix.Commands
{
    public readonly struct ForwardCommand: INavigationCommand
    {
        public readonly Screen Screen;

        public ForwardCommand(Screen screen)
        {
            Screen = screen;
        }
    }
}