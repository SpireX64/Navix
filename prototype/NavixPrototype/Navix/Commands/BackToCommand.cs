using System;

namespace NavixPrototype.Navix.Commands
{
    public readonly struct BackToCommand: INavigationCommand
    {
        public readonly Type ScreenType;

        public BackToCommand(Type screenType)
        {
            ScreenType = screenType;
        }
    }
}