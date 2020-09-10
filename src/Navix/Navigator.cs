using System;

namespace Spx.Navix
{
    public abstract class Navigator
    {
        public abstract void OnForward(Screen screen, IScreenResolver resolver);
        public abstract void OnReplace(Screen screen, IScreenResolver resolver);
        public abstract void OnBack();
        public abstract void OnBackTo(Type screenType);
        public abstract void OnBackToRoot();
    }
}