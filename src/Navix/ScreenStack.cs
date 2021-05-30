using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Navix
{
    public sealed class ScreenStack : IEnumerable<Screen>
    {
        private readonly ConcurrentStack<Screen> _screens = new ConcurrentStack<Screen>();

        internal ScreenStack()
        {
        }

        public bool IsRoot => _screens.Count <= 1;

        public uint Count => Convert.ToUInt16(_screens.Count);

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
            if (_screens.Count <= 1) return;
            var root = _screens.LastOrDefault();
            _screens.Clear();
            _screens.Push(root);
        }
    }
}