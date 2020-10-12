using Android.Support.V4.App;

namespace Spx.Navix.Xamarin.AndroidSupport
{
    public interface IFragmentScreenResolver: IScreenResolver
    {
        Fragment GetFragment(Screen screen);
    }
}