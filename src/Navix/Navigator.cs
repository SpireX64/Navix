using System;

namespace Spx.Navix
{
    public abstract class Navigator
    {
        public abstract NavigatorSpecification Specification { get; }

        public abstract void Forward(Screen screen, IScreenResolver resolver);

        public abstract void Back();

        public virtual void BackToScreen(Screen screen)
        {
            throw new NotSupportedException();
        }

        public virtual void BackToRoot()
        {
            throw new NotSupportedException();
        }

        public virtual void Replace(Screen screen, IScreenResolver resolver)
        {
            throw new NotSupportedException();
        }
        
        
    }
}