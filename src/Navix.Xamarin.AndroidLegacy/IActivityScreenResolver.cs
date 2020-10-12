using Android.Content;

namespace Spx.Navix.Xamarin.AndroidLegacy
{
    public interface IActivityScreenResolver: IScreenResolver
    {
        Intent GetActivityIntent(Screen screen, Context context);
    }
}