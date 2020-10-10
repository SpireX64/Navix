using Android.App;
using Android.Content;

namespace Spx.Navix.Xamarin.AndroidX
{
    public interface IActivityScreenResolver: IScreenResolver
    {
        Intent GetActivityIntent(Screen screen);
    }
}