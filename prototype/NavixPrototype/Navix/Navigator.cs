using System;

namespace NavixPrototype.Navix
{
    public abstract class Navigator
    {
        public abstract void NavigateTo(Screen screen);
        public abstract void Replace(Screen screen);
        public abstract void Back();
        public abstract void BackTo(Type screenType);
        public abstract void BackToRoot();
    }
}