using Android.Content;
using AndroidX.Annotations;

namespace Navix.Xamarin.AndroidX
{
    public interface IActivityScreenResolver : IScreenResolver
    {
        [NonNull]
        Intent GetActivityIntent(Screen screen, Context context);
    }
}