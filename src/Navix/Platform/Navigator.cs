﻿using System;

namespace Spx.Navix.Platform
{
    public abstract class Navigator<TResolver> where TResolver: IScreenResolver
    {
        public abstract NavigatorSpec Specification { get; }
        
        public virtual void Forward(Screen screen, TResolver resolver)
        {
            throw new NotImplementedException();
        }

        public virtual void Back()
        {
            throw new NotSupportedException();
        }

        public virtual void BackTo(Screen screen)
        {
            throw new NotSupportedException();
        }

        public virtual void BackToRoot()
        {
            throw new NotSupportedException();
        }

        public virtual void Update(Screen screen)
        {
        }
    }
}