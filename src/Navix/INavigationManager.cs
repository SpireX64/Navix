using System.Collections.Generic;
using Spx.Navix.Commands;

namespace Spx.Navix
{
    public interface INavigationManager
    {
        void SendCommands(ICollection<INavCommand> navCommands);
    }
}