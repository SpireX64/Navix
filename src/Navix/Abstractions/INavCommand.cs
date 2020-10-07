namespace Spx.Navix.Abstractions
{
    public interface INavCommand
    {
        void Apply(Navigator navigator, ScreenStack screens);
    }
}