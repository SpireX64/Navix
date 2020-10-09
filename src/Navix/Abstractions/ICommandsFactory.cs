using System.Collections.Generic;
using Spx.Reflection;

namespace Spx.Navix.Abstractions
{
    public interface ICommandsFactory
    {
        public ICollection<INavCommand> Forward(Screen screen);
        public ICollection<INavCommand> Back();
        public ICollection<INavCommand> BackToScreen(Class<Screen> screenClass);
        public ICollection<INavCommand> BackToRoot();
    }
}