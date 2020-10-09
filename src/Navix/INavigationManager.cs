using System.Collections.Generic;
using Spx.Navix.Abstractions;

namespace Spx.Navix
{
    public interface INavigationManager
    {
        public bool HasPendingCommands { get; }
        public IEnumerable<Screen> Screens { get; }
        public NavigatorSpecification Specification { get; }
        public void SendCommands(IEnumerable<INavCommand> navCommands);
    }
}