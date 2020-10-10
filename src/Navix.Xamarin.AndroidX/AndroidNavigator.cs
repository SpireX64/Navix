using System;

namespace Spx.Navix.Xamarin.AndroidX
{
    public class AndroidNavigator: Navigator
    {
        public override NavigatorSpecification Specification { get; }
        
        public override void Forward(Screen screen, IScreenResolver resolver)
        {
            throw new NotImplementedException();
        }

        public override void Back()
        {
            throw new NotImplementedException();
        }
    }
}