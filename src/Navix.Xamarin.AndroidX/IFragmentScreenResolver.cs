using AndroidX.Annotations;
using AndroidX.Fragment.App;

namespace Spx.Navix.Xamarin.AndroidX
{
    public interface IFragmentScreenResolver : IScreenResolver
    {
        [NonNull]
        Fragment GetFragment(Screen screen);
    }
}