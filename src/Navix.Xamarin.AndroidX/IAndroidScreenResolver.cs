using Android.Content;

namespace Spx.Navix.Xamarin.AndroidX
{
    public interface IAndroidScreenResolver: IScreenResolver
    {
        Intent GetActivityIntent(Context context);
    }
}