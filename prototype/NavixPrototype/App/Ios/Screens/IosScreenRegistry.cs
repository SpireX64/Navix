using NavixPrototype.App.Android.Screens.Main;
using NavixPrototype.App.Shared.Screens.Main;
using NavixPrototype.Navix;

namespace NavixPrototype.App.Ios.Screens
{
    public class IosScreenRegistry : NavigatorConfig
    {
        public override void ConfigureScreens(IScreenRegistry registry)
        {
            registry.Register<MainScreen>(new MainScreenResolver());
        }
    }
}