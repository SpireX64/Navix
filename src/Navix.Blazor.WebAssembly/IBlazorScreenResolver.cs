namespace Navix.Blazor.WebAssembly
{
    public interface IBlazorScreenResolver : IScreenResolver
    {
        public NavigationIntent GetNavigationIntent(Screen screen);
    }
}