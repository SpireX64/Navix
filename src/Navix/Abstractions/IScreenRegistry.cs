using System;
using Spx.Reflection;

namespace Spx.Navix.Abstractions
{
    public interface IScreenRegistry
    {
        public bool IsEmpty { get; }

        public Type? RootScreenType { get; }

        public void Register(Class<Screen> screenClass, IScreenResolver resolver);

        public void RegisterAsRoot(Class<Screen> rootScreenClass);

        public bool HasScreen(Class<Screen> screenClass);

        public IScreenResolver Resolve(Screen screen);
    }
}