using Android.Content;

namespace Spx.Navix.Xamarin.AndroidSupport
{
    public interface IActivityScreenResolver: IScreenResolver
    {
        Intent GetActivityIntent(Screen screen, Context context);
    }
}