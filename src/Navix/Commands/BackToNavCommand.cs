using System;

namespace Spx.Navix.Commands
{
    public struct BackToNavCommand: INavigationCommand
    {
        public Type ScreenType { get; }

        public static BackToNavCommand For<TScreen>() where TScreen: Screen
        {
            var type = typeof(TScreen);
            return new BackToNavCommand(type);
        }

        public BackToNavCommand(Type screenType)
        {
            ScreenType = screenType;
        }
    }
}