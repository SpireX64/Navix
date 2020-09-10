using NavixPrototype.App.Android.Screens.Main;
using NavixPrototype.Navix;

namespace NavixPrototype.App.Android.Screens
{
    public class ScreensConfig: NavigatorConfig
    {
        public override void ConfigureScreens(IScreenRegistry registry)
        {
            registry.Register(new MainScreenResolver());
        }
    }
}