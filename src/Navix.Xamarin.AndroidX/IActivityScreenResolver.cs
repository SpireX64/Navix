using Android.Content;
using AndroidX.Annotations;

namespace Spx.Navix.Xamarin.AndroidX
{
    public interface IActivityScreenResolver : IScreenResolver
    {
        [NonNull]
        Intent GetActivityIntent(Screen screen, Context context);
    }
}