using System.Collections.Generic;
using Spx.Navix.Platform;

namespace Spx.Navix.Commands
{
    public interface ICommandsFactory
    {
        public ICollection<INavCommand> Forward(NavigatorSpec spec, Screen screen);
        public ICollection<INavCommand> Back(NavigatorSpec spec);
    }
}