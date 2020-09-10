using System.Collections.Concurrent;

namespace Spx.Navix
{
    public class ScreenRegistry
    {
        private readonly ConcurrentDictionary<int, object>
            _screenResolversMap = new ConcurrentDictionary<int, object>();

        public bool IsEmpty => _screenResolversMap.IsEmpty;

        public void Register<TScreen>(IScreenResolver<TScreen> resolver) where TScreen : Screen
        {
            var screenTypeHash = typeof(TScreen).GetHashCode();
            _screenResolversMap.TryAdd(screenTypeHash, resolver);
        }

        public bool HasScreen<TScreen>() where TScreen : Screen
        {
            var screenTypeHash = typeof(TScreen).GetHashCode();
            return _screenResolversMap.ContainsKey(screenTypeHash);
        }

        public IScreenResolver<TScreen>? Resolve<TScreen>() where TScreen : Screen
        {
            var screenTypeHash = typeof(TScreen).GetHashCode();
            if (_screenResolversMap.TryGetValue(screenTypeHash, out var resolver))
                return (IScreenResolver<TScreen>) resolver;
            else
                return null;
        }
    }
}