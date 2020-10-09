using System;

namespace Spx.Navix
{
    public abstract class Navigator
    {
        public virtual void Forward(Screen screen, IScreenResolver resolver)
        {
            throw new NotImplementedException();
        }

        public virtual void Back()
        {
            throw new NotSupportedException();
        }

        public virtual void BackToScreen(Screen screen)
        {
            throw new NotSupportedException();
        }

        public virtual void BackToRoot()
        {
            throw new NotSupportedException();
        }

        public virtual void Replace(Screen screen)
        {
            throw new NotSupportedException();
        }
    }
}