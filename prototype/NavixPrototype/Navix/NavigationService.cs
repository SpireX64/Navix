namespace NavixPrototype.Navix
{
    public class NavigationService
    {
        private readonly NavigatorConfig _config;

        public NavigationService(NavigatorConfig config)
        {
            _config = config;
            NavigatorHolder = new NavigatorHolder();
            Router = new Router(NavigatorHolder);
        }

        public NavigatorHolder NavigatorHolder { get; }
        
        public Router Router { get; }
    }
}