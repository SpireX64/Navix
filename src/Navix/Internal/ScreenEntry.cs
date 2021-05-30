using Navix.Abstractions;
using Spx.Reflection;

namespace Navix.Internal
{
    public sealed class ScreenEntry : IScreenEntry, IScreenRegistrationConfig
    {
        public ScreenEntry(Class<Screen> screenClass, IScreenResolver resolver)
        {
            ScreenClass = screenClass;
            Resolver = resolver;
        }

        public Class<Screen> ScreenClass { get; }
        public IScreenResolver Resolver { get; }
        public bool IsRoot { get; private set; }

        IScreenRegistrationConfig IScreenRegistrationConfig.AsRoot()
        {
            IsRoot = true;
            return this;
        }
    }
}