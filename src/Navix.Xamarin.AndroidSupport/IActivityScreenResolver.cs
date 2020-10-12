using Android.Content;
using Android.Support.Annotation;

namespace Spx.Navix.Xamarin.AndroidSupport
{
    public interface IActivityScreenResolver: IScreenResolver
    {
        [NonNull]
        Intent GetActivityIntent(Screen screen, Context context);
    }
}