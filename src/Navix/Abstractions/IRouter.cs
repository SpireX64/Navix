using System.Collections.Generic;
using Spx.Reflection;

namespace Spx.Navix.Abstractions
{
    /// <summary>
    /// The interface for high-level navigation
    /// </summary>
    public interface IRouter
    {
        IEnumerable<Screen> Screens { get; }
        
        /// <summary>
        /// Open new screen and add it to the screens sequence.
        /// </summary>
        /// <param name="screen"></param>
        void Forward(Screen screen);

        
        /// <summary>
        /// Return to the previous screen in the sequence.
        /// </summary>
        void Back();

        
        /// <summary>
        /// Return to the specified screen in the screens sequence.
        /// </summary>
        /// <param name="screenClass">The specific screen class to return to</param>
        void BackToScreen(Class<Screen> screenClass);

        
        /// <summary>
        /// Return to the root screen.
        /// </summary>
        void BackToRoot();

        
        /// <summary>
        /// Replace current screen.
        /// </summary>
        /// <param name="screen">The screen to replace the current one</param>
        void Replace(Screen screen);
    }
}