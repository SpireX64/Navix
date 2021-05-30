using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Navix.Abstractions;
using Navix.Exceptions;
using Spx.Reflection;

namespace Navix.Internal
{
    internal sealed class ScreenRegistry : IScreenRegistry, IScreenRegistrar
    {
        private readonly ConcurrentDictionary<int, ScreenEntry> _screens =
            new ConcurrentDictionary<int, ScreenEntry>();

        private readonly List<INavigationMiddleware> _middlewares =
            new List<INavigationMiddleware>();
        
        public bool IsEmpty => _screens.IsEmpty;

        public Class<Screen>? RootScreenClass => _screens.Values.FirstOrDefault(it => it.IsRoot)?.ScreenClass;
        public IReadOnlyCollection<INavigationMiddleware> Middlewares => _middlewares.AsReadOnly();

        public IScreenRegistrationConfig Register(Class<Screen> screenClass, IScreenResolver resolver)
        {
            var typeHash = screenClass.GetHashCode();
            var entry = new ScreenEntry(screenClass, resolver);
            _screens.TryAdd(typeHash, entry);
            return entry;
        }

        public void AddMiddleware(INavigationMiddleware middleware)
        {
            _middlewares.Add(middleware);
        }

        public bool HasScreen(Class<Screen> screenClass)
        {
            var typeHash = screenClass.GetHashCode();
            return _screens.ContainsKey(typeHash);
        }

        public ScreenEntry Resolve(Screen screen)
        {
            var typeHash = screen.GetType().GetHashCode();
            return _screens.TryGetValue(typeHash, out var entry)
                ? entry
                : throw new UnregisteredScreenException(screen);
        }
    }
}