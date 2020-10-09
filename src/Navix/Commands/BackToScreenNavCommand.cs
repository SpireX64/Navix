using System;
using System.Linq;
using Spx.Navix.Abstractions;
using Spx.Navix.Exceptions;
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
            var screen = screens.FirstOrDefault(e => e.GetType() == _screenType);
            if (screen is null)
                throw new ScreenNotFoundException(_screenType);

            while (screens.CurrentScreen != screen)
            {
                screens.Pop();
            }
            
            navigator.BackToScreen(screen);
        }
    }
}