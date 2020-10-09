using System;
using Spx.Navix.Abstractions;
using Spx.Reflection;

namespace Spx.Navix.Commands
{
    public sealed class BackToScreenNavCommand: INavCommand
    {
        private readonly Type _screenType;
        
        public BackToScreenNavCommand(Class<Screen> screenClass)
        {
            _screenType = screenClass.Type;
        }
        
        public void Apply(Navigator navigator, ScreenStack screens)
        {
            navigator.BackToScreen(_screenType);
        }
    }
}