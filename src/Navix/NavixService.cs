using System;

namespace Spx.Navix
{
    public class NavixService
    {
        private bool _initialized = false;
        private readonly ScreenRegistry _registry = new ScreenRegistry();

        private Router? _router = null;

        public bool IsInitialized => _initialized;

        public void Initialize(INavixConfig config)
        {
            if (_initialized) 
                throw new InvalidOperationException();
            
            if (config == null) 
                throw new ArgumentNullException(nameof(config));

            config.ConfigureScreens(_registry);
            
            _router = new Router(_registry);
            
            _initialized = true;
        }

        public Router GetRouter()
        {
            if (!_initialized)
                throw new InvalidOperationException();

            return _router!;
        }
    }
}