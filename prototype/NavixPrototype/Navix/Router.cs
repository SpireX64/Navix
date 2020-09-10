namespace NavixPrototype.Navix
{
    public class Router
    {
        private readonly NavigatorHolder _navigatorHolder;

        public Router(NavigatorHolder navigatorHolder)
        {
            _navigatorHolder = navigatorHolder;
        }

        public void NavigateTo(Screen screen)
        {
            _navigatorHolder.Navigator.NavigateTo(screen);
        }

        public void BackTo<TScreen>() where TScreen : Screen
        {
            _navigatorHolder.Navigator.BackTo<TScreen>();
        }

        public void Back()
        {
            _navigatorHolder.Navigator.Back();
        }
    }
}