using Spx.Reflection;

namespace Navix.Abstractions
{
    public interface IScreenEntry
    {
        abstract Class<Screen> ScreenClass { get; }
        
        abstract IScreenResolver Resolver { get; }
        
        abstract bool IsRoot { get; }
    }
}