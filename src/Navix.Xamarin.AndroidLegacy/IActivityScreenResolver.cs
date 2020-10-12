using Android.Content;
using Navix;

namespace Navix.Xamarin.AndroidLegacy
{
    public interface IActivityScreenResolver: IScreenResolver
    {
        Intent GetActivityIntent(Screen screen, Context context);
    }
}