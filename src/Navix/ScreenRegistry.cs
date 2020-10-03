﻿using System.Collections.Concurrent;
using Spx.Reflection;

namespace Spx.Navix
{
    public class ScreenRegistry: IScreenRegistry
    {
        private readonly ConcurrentDictionary<int, IScreenResolver>
            _screenResolversMap = new ConcurrentDictionary<int, IScreenResolver>();

        public bool IsEmpty => _screenResolversMap.IsEmpty;

        public void Register(Class<Screen> screenClass, IScreenResolver resolver)
        {
            var typeHash = screenClass.GetHashCode();
            _screenResolversMap.TryAdd(typeHash, resolver);
        }

        public bool HasScreen(Class<Screen> screenClass)
        {
            var typeHash = screenClass.GetHashCode();
            return _screenResolversMap.ContainsKey(typeHash);
        }

        public IScreenResolver? Resolve(Class<Screen> screenClass)
        {
            var typeHash = screenClass.GetHashCode();
            return _screenResolversMap.TryGetValue(typeHash, out var resolver) 
                ? resolver 
                : null;
        }
    }
}