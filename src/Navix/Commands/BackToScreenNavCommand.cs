using System;
using System.Linq;
using Navix.Abstractions;
using Navix.Exceptions;
using Spx.Reflection;

namespace Navix.Commands
{
    public sealed class BackToScreenNavCommand : INavCommand
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

            while (screens.CurrentScreen != screen) screens.Pop();

            navigator.BackToScreen(screen);
        }
    }
}