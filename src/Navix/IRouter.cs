using Spx.Reflection;

namespace Spx.Navix
{
    public interface IRouter
    {
        void NavigateTo(Screen screen);

        void Replace(Screen screen);
        
        void Back();

        void BackTo(Class<Screen> screenClass);

        void BackToRoot();
    }
}