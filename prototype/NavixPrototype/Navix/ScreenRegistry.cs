using System;
using NavixPrototype.App.Android.Screens.Main;

namespace NavixPrototype.Navix
{
    public class ScreenRegistry: IScreenRegistry
    {
        public void Register<TScreen>(ScreenResolver<TScreen> screenResolver) where TScreen : Screen
        {
            throw new NotImplementedException();
        }
    }
}