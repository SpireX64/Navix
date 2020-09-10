namespace NavixPrototype.Navix
{
    public interface IScreenRegistry
    {
        void Register<TScreen>(ScreenResolver<TScreen> screenResolver) where TScreen : Screen;
    }
}