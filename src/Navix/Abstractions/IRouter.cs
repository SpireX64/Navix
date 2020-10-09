using Spx.Reflection;

namespace Spx.Navix.Abstractions
{
    public interface IRouter
    {
        void Forward(Screen screen);

        void Back();

        void BackToScreen(Class<Screen> screenClass);
    }
}