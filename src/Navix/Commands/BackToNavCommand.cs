using System;
using Spx.Reflection;

namespace Spx.Navix.Commands
{
    public struct BackToNavCommand: INavigationCommand
    {
        public Type ScreenType { get; }

        public BackToNavCommand(Class<Screen> screenType)
        {
            ScreenType = screenType;
        }
    }
}