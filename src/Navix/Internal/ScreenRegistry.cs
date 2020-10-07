using System;
using System.Collections.Concurrent;
using Spx.Navix.Abstractions;
using Spx.Navix.Exceptions;
using Spx.Reflection;

namespace Spx.Navix.Internal
{
    internal sealed class ScreenRegistry : IScreenRegistry
    {
        private readonly ConcurrentDictionary<int, IScreenResolver>
            _screenResolversMap = new ConcurrentDictionary<int, IScreenResolver>();

        public bool IsEmpty => _screenResolversMap.IsEmpty;
        public Type? RootScreenType { get; private set; }

        public void Register(Class<Screen> screenClass, IScreenResolver resolver)
        {
            var typeHash = screenClass.GetHashCode();
            _screenResolversMap.TryAdd(typeHash, resolver);
        }

        public void RegisterAsRoot(Class<Screen> rootScreenClass)
        {
            RootScreenType = rootScreenClass.Type;
        }

        public bool HasScreen(Class<Screen> screenClass)
        {
            var typeHash = screenClass.GetHashCode();
            return _screenResolversMap.ContainsKey(typeHash);
        }

        public IScreenResolver Resolve(Screen screen)
        {
            var typeHash = screen.GetType().GetHashCode();
            return _screenResolversMap.TryGetValue(typeHash, out var resolver)
                ? resolver
                : throw new UnregisteredScreenException(screen);
        }
    }
}