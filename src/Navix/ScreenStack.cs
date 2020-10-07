using System;
using System.Collections.Concurrent;

namespace Spx.Navix
{
    public sealed class ScreenStack
    {
        internal ScreenStack()
        {
        }
        
        private readonly ConcurrentStack<Screen> _screens = new ConcurrentStack<Screen>();

        public bool IsRoot => _screens.IsEmpty;

        public int Count => _screens.Count;

        public Screen? CurrentScreen
        {
            get
            {
                _screens.TryPeek(out var screen);
                return screen;
            }
        }

        public void Push(params Screen[] screens)
        {
            _screens.PushRange(screens);
        }

        public Screen? Pop()
        {
            if(!_screens.TryPop(out var screen))
                throw new InvalidOperationException();
            return screen;
        }
    }
}