using System.Collections.Generic;
using Spx.Reflection;

namespace Navix.Abstractions
{
    /// <summary>
    ///     Responsible for generating commands depending on the conditions of the request and the environment
    /// </summary>
    public interface ICommandsFactory
    {
        /// <summary>
        ///     Generates a sequence of commands to navigate to the specified screen
        /// </summary>
        /// <param name="screen">The screen on which the navigating is performed</param>
        /// <returns>Sequence of navigation commands</returns>
        public ICollection<INavCommand> Forward(Screen screen);


        /// <summary>
        ///     Generates a sequence of commands to return to the previous screen
        /// </summary>
        /// <returns>Sequence of navigation commands</returns>
        public ICollection<INavCommand> Back();


        /// <summary>
        ///     Generates a sequence of commands to return to a specific screen
        /// </summary>
        /// <param name="screens">Current sequence of screens</param>
        /// <param name="spec">Specification of the used navigator</param>
        /// <param name="screenClass">The specific screen class to return to</param>
        /// <returns>Sequence of navigation commands</returns>
        public ICollection<INavCommand> BackToScreen(IEnumerable<Screen> screens, NavigatorSpecification spec,
            Class<Screen> screenClass);


        /// <summary>
        ///     Generates a sequence of commands to return to the root screen
        /// </summary>
        /// <param name="screens">Current sequence of screens</param>
        /// <param name="spec">Specification of the used navigator</param>
        /// <returns>Sequence of navigation commands</returns>
        public ICollection<INavCommand> BackToRoot(IEnumerable<Screen> screens, NavigatorSpecification spec);


        /// <summary>
        ///     Generates a sequence of commands to replace the current screen with the specified
        /// </summary>
        /// <param name="screens">Current sequence of screens</param>
        /// <param name="spec">Specification of the used navigator</param>
        /// <param name="screen">The screen to replace the current one</param>
        /// <returns>Sequence of navigation commands</returns>
        public ICollection<INavCommand> ReplaceScreen(IEnumerable<Screen> screens, NavigatorSpecification spec,
            Screen screen);
    }
}