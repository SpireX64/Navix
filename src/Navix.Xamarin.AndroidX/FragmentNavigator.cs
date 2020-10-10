using Android.Support.Annotation;
using Android.Support.V4.App;

namespace Spx.Navix.Xamarin.AndroidX
{
    public class FragmentNavigator: Navigator
    {
        private readonly FragmentManager _fragmentManager;
        private readonly int _containerId;
        
        public override NavigatorSpecification Specification { get; } = new NavigatorSpecification()
        {

        };

        public FragmentNavigator(FragmentManager fragmentManager, [IdRes] int containerId)
        {
            _fragmentManager = fragmentManager;
            _containerId = containerId;
        }
        
        public override void Forward(Screen screen, IScreenResolver resolver)
        {
            if (!(resolver is IFragmentScreenResolver fragmentResolver)) return;
            
            var fragment = fragmentResolver.GetFragment(screen);
            _fragmentManager.BeginTransaction()
                .Add(_containerId, fragment)
                .AddToBackStack(null)
                .Commit();
        }

        public override void Back()
        {
            _fragmentManager.PopBackStack();
        }
    }
}