using System.Collections.Generic;
using Android.Support.Annotation;
using Android.Support.V4.App;

namespace Navix.Xamarin.AndroidSupport
{
    public class AndroidNavigator: Navigator
    {
        private readonly FragmentActivity _activity;
        private readonly FragmentManager _fragmentManager;
        private readonly int _containerId;

        private readonly Stack<Screen> _internalFragmentsStack = new Stack<Screen>();

        public AndroidNavigator([NonNull] FragmentActivity activity, [NonNull] FragmentManager fragmentManager, [IdRes] int containerId)
        {
            _activity = activity;
            _fragmentManager = fragmentManager;
            _containerId = containerId;
        }

        public AndroidNavigator([NonNull] FragmentActivity activity, [IdRes] int containerId)
        {
            _activity = activity;
            _fragmentManager = activity.SupportFragmentManager;
            _containerId = containerId;
        }

        public override NavigatorSpecification Specification { get; } = new NavigatorSpecification();

        public override void Forward(Screen screen, IScreenResolver resolver)
        {
            switch (resolver)
            {
                case IActivityScreenResolver activityScreenResolver:
                    ForwardActivity(screen, activityScreenResolver);
                    break;

                case IFragmentScreenResolver fragmentScreenResolver:
                    ForwardFragment(screen, fragmentScreenResolver);
                    break;
            }
        }

        public override void Back()
        {
            if (_internalFragmentsStack.Count > 0)
            {
                _fragmentManager.PopBackStack();
                _internalFragmentsStack.Pop();
            }
            else
                _activity.Finish();
        }

        private void ForwardActivity(Screen screen, IActivityScreenResolver resolver)
        {
            var intent = resolver.GetActivityIntent(screen, _activity);
            _activity.StartActivity(intent);
        }

        private void ForwardFragment(Screen screen, IFragmentScreenResolver resolver)
        {
            var fragment = resolver.GetFragment(screen);

            _fragmentManager.BeginTransaction()
                .Replace(_containerId, fragment)
                .AddToBackStack(screen.Name)
                .Commit();

            _internalFragmentsStack.Push(screen);
        }
    }
}
