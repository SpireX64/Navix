using NavixPrototype.App.Shared.Screens.Main;
using NavixPrototype.Navix.Android;
using NavixPrototype.Stubs.Android;

namespace NavixPrototype.App.Android.Screens.Main
{
    public class MainScreenResolver: AndroidScreenResolver<MainScreen>
    {
        public override Intent GetActivityIntent()
        {
            if (IncomingScreen.Props.UserName != null)
            {
                return new Intent();
            }
            else
            {
                return new Intent();
            }
        }
    }
}