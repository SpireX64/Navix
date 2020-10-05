using Spx.Reflection;

namespace Spx.Navix.Abstractions
{
    public interface IScreenRegistry
    {
        public bool IsEmpty { get; }
        
        public void Register(Class<Screen> screenClass, IScreenResolver resolver);
        
        public bool HasScreen(Class<Screen> screenClass);

        public IScreenResolver Resolve(Screen screen);
    }
}