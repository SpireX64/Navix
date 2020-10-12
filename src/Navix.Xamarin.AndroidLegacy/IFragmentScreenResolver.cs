#pragma warning disable 618
using Android.App;

namespace Navix.Xamarin.AndroidLegacy
{
    interface IFragmentScreenResolver: IScreenResolver
    {
        Fragment GetFragment(Screen screen);
    }
}