using System.Collections.Generic;
using Spx.Navix.Commands;

namespace Spx.Navix.Abstractions
{
    public interface ICommandsFactory
    {
        public ICollection<INavCommand> Forward(Screen screen);
        public ICollection<INavCommand> Back();
    }
}