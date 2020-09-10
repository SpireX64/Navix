namespace NavixPrototype.App
{
    public class Application
    {
        public Application()
        {
            _router.NavigateTo(new MainScreen(new MainScreenParams
            {
                Data = _resolver.Data
            }));
        }
    }

    public class Activity
    {
        public void OnCreate()
        {
            var screen = _navigatorHolder.GetScreen();

            if (screen is MainScreen mainScreen)
            {
                mainScreen.Params
            }
            
            
        }
    }
}