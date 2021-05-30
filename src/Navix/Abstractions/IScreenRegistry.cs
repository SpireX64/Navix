using System;
using System.Collections.Generic;
using Navix.Internal;
using Spx.Reflection;

namespace Navix.Abstractions
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
        ///     Checks if the given screen is registered
        /// </summary>
        /// <param name="screenClass">Screen class</param>
        /// <returns>true, when screen is registered</returns>
        public bool HasScreen(Class<Screen> screenClass);

        public ScreenEntry Resolve(Screen screen);

        public Class<Screen>? RootScreenClass { get; }

        public IReadOnlyCollection<INavigationMiddleware> Middlewares { get; }
    }
}