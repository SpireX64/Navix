namespace Spx.Navix
{
    public interface IRouter
    {
        void NavigateTo<TScreen>(TScreen screen) where TScreen : Screen;

        void Replace<TScreen>(TScreen screen) where TScreen : Screen;
        
        void Back();

        void BackTo<TScreen>() where TScreen : Screen;

        void BackToRoot();
    }
}