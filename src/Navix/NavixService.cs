using System;

namespace Spx.Navix
{
    public class NavixService
    {
        private bool _initialized = false;
        private ScreenRegistry _registry = new ScreenRegistry();

        public bool IsInitialized => _initialized;

        public void Initialize(INavixConfig config)
        {
            if (_initialized) 
                throw new InvalidOperationException();
            
            if (config == null) 
                throw new ArgumentNullException(nameof(config));

            config.ConfigureScreens(_registry);
            
            _initialized = true;
        }
    }
}