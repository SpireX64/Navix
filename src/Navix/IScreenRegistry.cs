using Spx.Navix.Platform;
using Spx.Reflection;

namespace Spx.Navix
{
    public interface IScreenRegistry
    {
        void Register(Class<Screen> screenClass, IScreenResolver resolver);
    }
}