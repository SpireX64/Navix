using System.Collections.Generic;
using Spx.Navix.Abstractions;

namespace Spx.Navix
{
    /// <summary>
    /// Controls navigation
    /// </summary>
    public interface INavigationManager
    {
        /// <summary>
        /// Checks if there are any pending navigation commands in the queue
        /// </summary>
        public bool HasPendingCommands { get; }
        
        /// <summary>
        /// Current sequence of screens
        /// </summary>
        public IEnumerable<Screen> Screens { get; }
        
        /// <summary>
        /// Specification of the used navigator
        /// </summary>
        public NavigatorSpecification Specification { get; }
        
        /// <summary>
        /// Sends navigation commands to the execution queue
        /// </summary>
        /// <param name="navCommands">Navigation commands</param>
        public void SendCommands(IEnumerable<INavCommand> navCommands);
    }
}