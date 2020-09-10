namespace Spx.Navix
{
    public interface IScreenRegistry
    {
        void Register<TScreen>(IScreenResolver resolver) where TScreen : Screen;
    }
}