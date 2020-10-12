using AndroidX.Annotations;
using AndroidX.Fragment.App;

namespace Navix.Xamarin.AndroidX
{
    public interface IFragmentScreenResolver : IScreenResolver
    {
        [NonNull]
        Fragment GetFragment(Screen screen);
    }
}