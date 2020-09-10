namespace Spx.Navix
{
    public interface IScreenRegistry
    {
        void Register<TScreen>(IScreenResolver<TScreen> resolver) where TScreen : Screen;
    }
}