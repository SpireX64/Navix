using System;
using System.Collections.Generic;
using AndroidX.Annotations;
using AndroidX.Fragment.App;

namespace Navix.Xamarin.AndroidX
{
    public class AndroidFragmentNavigator: Navigator
    {
        private readonly FragmentActivity _activity;
        private readonly int _containerId;
        private readonly FragmentManager _fragmentManager;
        private readonly Stack<Screen> _internalFragmentsStack = new Stack<Screen>();

        public override NavigatorSpecification Specification { get; } = new NavigatorSpecification()
        {
            ReplaceScreenSupported = true,
        };
        
        public AndroidFragmentNavigator([NonNull] FragmentActivity activity, [NonNull] FragmentManager fragmentManager,
            [IdRes] int containerId)
        {
            _activity = activity;
            _fragmentManager = fragmentManager;
            _containerId = containerId;
        }

        public AndroidFragmentNavigator([NonNull] FragmentActivity activity, [IdRes] int containerId)
        {
            _activity = activity;
            _fragmentManager = activity.SupportFragmentManager;
            _containerId = containerId;
        }
        
        public override void Forward(Screen screen, IScreenResolver resolver)
        {
            if (resolver is IFragmentScreenResolver fragmentScreenResolver)
                FragmentForward(screen, fragmentScreenResolver);
            else
                throw new NotSupportedException("Only fragments allowed");
        }

        private void FragmentForward(Screen screen, IFragmentScreenResolver fragmentScreenResolver)
        {
            var fragment = fragmentScreenResolver.GetFragment(screen);
            
            _internalFragmentsStack.Push(screen);
            _fragmentManager.BeginTransaction()
                .Replace(_containerId, fragment)
                .AddToBackStack(screen.Name)
                .Commit();
        }

        public override void Back()
        {
            _fragmentManager.PopBackStack();
            _internalFragmentsStack.Pop();
        }

        public override void Replace(Screen screen, IScreenResolver resolver)
        {
            if (resolver is IFragmentScreenResolver fragmentScreenResolver)
                FragmentReplace(screen, fragmentScreenResolver);
            else
                throw new NotSupportedException("Only fragments allowed");
        }

        private void FragmentReplace(Screen screen, IFragmentScreenResolver resolver)
        {
            var fragment = resolver.GetFragment(screen);
            
            _internalFragmentsStack.Pop();
            _internalFragmentsStack.Push(screen);
            _fragmentManager.BeginTransaction()
                .Replace(_containerId, fragment)
                .Commit();
        }

        public override void BackToRoot()
        {
            _internalFragmentsStack.Clear();
            for (var i = 0; i < _fragmentManager.BackStackEntryCount; i++)
            {
                _fragmentManager.PopBackStack();
            }
        }
    }
}