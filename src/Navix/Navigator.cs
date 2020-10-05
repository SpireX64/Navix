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
    }
}
