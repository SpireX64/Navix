using Android.App;
#pragma warning disable 618

namespace Spx.Navix.Xamarin.AndroidLegacy
{
    interface IFragmentScreenResolver: IScreenResolver
    {
        Fragment GetFragment(Screen screen);
    }
}