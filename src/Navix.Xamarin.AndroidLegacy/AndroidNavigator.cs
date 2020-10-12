using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Android.App;
#pragma warning disable 618

namespace Spx.Navix.Xamarin.AndroidLegacy
{
    public class AndroidNavigator: Navigator
    {
        private readonly Activity _activity;
        private readonly FragmentManager _fragmentManager;
        private readonly int _containerId;

        private readonly Stack<Screen> _internalFragmentStack = new Stack<Screen>();

        public AndroidNavigator(Activity activity, FragmentManager fragmentManager, int containerId)
        {
            _activity = activity;
            _fragmentManager = fragmentManager;
            _containerId = containerId;
        }

        public AndroidNavigator(Activity activity, int containerId)
        {
            _activity = activity;
            _containerId = containerId;
            _fragmentManager = activity.FragmentManager;
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
            throw new NotImplementedException();
        }

        private void ForwardActivity(Screen screen, IActivityScreenResolver resolver)
        {
            var intent = resolver.GetActivityIntent(screen, _activity);
            _activity.StartActivity(intent);
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        private void ForwardFragment(Screen screen, IFragmentScreenResolver resolver)
        {
            var fragment = resolver.GetFragment(screen);

            _fragmentManager.BeginTransaction()
                .Replace(_containerId, fragment)
                .AddToBackStack(screen.Name)
                .Commit();

            _internalFragmentStack.Push(screen);
        }
    }
}