using Spx.Navix.Platform;
using Spx.Reflection;

namespace Spx.Navix
{
    public interface IScreenRegistry
    {
        bool IsEmpty { get; }
        
        void Register(Class<Screen> screenClass, IScreenResolver resolver);
        
        bool HasScreen(Class<Screen> screenClass);
        
        IScreenResolver? Resolve(Class<Screen> screenClass);
    }
}