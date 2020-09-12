using System.Collections.Concurrent;

namespace Spx.Navix
{
    public class ScreenRegistry: IScreenRegistry
    {
        private readonly ConcurrentDictionary<int, IScreenResolver>
            _screenResolversMap = new ConcurrentDictionary<int, IScreenResolver>();

        public bool IsEmpty => _screenResolversMap.IsEmpty;

        public void Register<TScreen>(IScreenResolver resolver) where TScreen : Screen
        {
            var screenTypeHash = typeof(TScreen).GetHashCode();
            _screenResolversMap.TryAdd(screenTypeHash, resolver);
        }

        public bool HasScreen<TScreen>() where TScreen : Screen
        {
            var screenTypeHash = typeof(TScreen).GetHashCode();
            return _screenResolversMap.ContainsKey(screenTypeHash);
        }

        public IScreenResolver? Resolve<TScreen>() where TScreen : Screen
        {
            var screenTypeHash = typeof(TScreen).GetHashCode();
            return _screenResolversMap.TryGetValue(screenTypeHash, out var resolver) 
                ? resolver 
                : null;
        }
    }
}