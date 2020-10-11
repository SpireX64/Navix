using System;
using Spx.Reflection;

namespace Spx.Navix.Abstractions
{
    /// <summary>
    ///     Screen registry
    /// </summary>
    public interface IScreenRegistry
    {
        /// <summary>
        ///     Gets a value that indicates whether the registry is empty
        /// </summary>
        public bool IsEmpty { get; }

        /// <summary>
        ///     Returns the type of root screen
        /// </summary>
        public Type? RootScreenType { get; }

        /// <summary>
        ///     Registers the screen
        /// </summary>
        /// <param name="screenClass">Screen class</param>
        /// <param name="resolver">Screen resolver</param>
        public void Register(Class<Screen> screenClass, IScreenResolver resolver);

        /// <summary>
        ///     Defines the type of root screen
        /// </summary>
        /// <param name="rootScreenClass">Root screen class</param>
        public void RegisterAsRoot(Class<Screen> rootScreenClass);

        /// <summary>
        ///     Checks if the given screen is registered
        /// </summary>
        /// <param name="screenClass">Screen class</param>
        /// <returns>true, when screen is registered</returns>
        public bool HasScreen(Class<Screen> screenClass);

        public IScreenResolver Resolve(Screen screen);
    }
}