using System.Collections.Generic;
using Spx.Navix.Platform;

namespace Spx.Navix.Commands
{
    public interface ICommandsFactory
    {
        public ICollection<INavCommand> Forward(Screen screen);
        public ICollection<INavCommand> Back();
    }
}