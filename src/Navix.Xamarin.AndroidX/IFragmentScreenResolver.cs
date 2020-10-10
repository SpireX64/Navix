using Android.Support.V4.App;

namespace Spx.Navix.Xamarin.AndroidX
{
    public interface IFragmentScreenResolver: IScreenResolver
    {
        Fragment GetFragment(Screen screen);
    }
}