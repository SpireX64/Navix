﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Spx.Navix
{
    public sealed class ScreenStack: IEnumerable<Screen>
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

        public IEnumerator<Screen> GetEnumerator() => _screens.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Clear() => _screens.Clear();
    }
}