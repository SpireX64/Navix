using System;
using Android.App;
using Android.Content;
using Android.Util;

namespace Spx.Navix.Xamarin.AndroidX
{
    public class AndroidNavigator : Navigator
    {
        private readonly Activity _activity;

        public AndroidNavigator(Activity activity)
        {
            _activity = activity;
        }

        public override NavigatorSpecification Specification { get; }

        public override void Forward(Screen screen, IScreenResolver resolver)
        {
            Log.Debug("Navigator", $"Screen: {screen}, Resolver: {resolver}");
            if (!(resolver is IAndroidScreenResolver androidScreenResolver)) return;

            var intent = androidScreenResolver.GetActivityIntent(_activity);
            Log.Debug("Navigator", $"Intent: {intent}");
            _activity.StartActivity(intent);
        }

        public override void Back()
        {
            _activity.Finish();
        }
    }
}