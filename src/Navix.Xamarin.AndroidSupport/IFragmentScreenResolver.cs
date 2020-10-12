using Android.Support.Annotation;
using Android.Support.V4.App;

namespace Navix.Xamarin.AndroidSupport
{
    public interface IFragmentScreenResolver: IScreenResolver
    {
        [NonNull]
        Fragment GetFragment(Screen screen);
    }
}