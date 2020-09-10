using NavixPrototype.App.Shared.Screens.Main;
using NavixPrototype.Navix;
using NavixPrototype.Navix.Android;

namespace NavixPrototype.App.Android.Screens.Main
{
    public class MainActivity
    {
        private INavigatorHolder _navigatorHolder;
        
        private string _userName;

        public MainActivity()
        {
            var screen = _navigatorHolder.GetScreen();
            if (screen is MainScreen mainScreen)
            {
                _userName = mainScreen.Props.UserName;
            }
            
            var navigator = new AndroidNavigator();
            _navigatorHolder.Hold(navigator);
        }
    }
}