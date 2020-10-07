using System.Collections.Generic;

namespace Spx.Navix.Abstractions
{
    public interface ICommandsFactory
    {
        public ICollection<INavCommand> Forward(Screen screen);
        public ICollection<INavCommand> Back();
    }
}