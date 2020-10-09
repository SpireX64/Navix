using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Spx.Navix
{
    public sealed class ScreenStack : IEnumerable<Screen>
    {
        private readonly ConcurrentStack<Screen> _screens = new ConcurrentStack<Screen>();

        internal ScreenStack()
        {
        }

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

        public IEnumerator<Screen> GetEnumerator()
        {
            return _screens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Push(params Screen[] screens)
        {
            _screens.PushRange(screens);
        }

        public Screen? Pop()
        {
            if (!_screens.TryPop(out var screen))
                throw new InvalidOperationException();
            return screen;
        }

        public void Clear()
        {
            _screens.Clear();
        }
    }
}