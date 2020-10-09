using System.Collections.Generic;
using Spx.Reflection;

namespace Spx.Navix.Abstractions
{
    public interface ICommandsFactory
    {
        public ICollection<INavCommand> Forward(Screen screen);
        public ICollection<INavCommand> Back();
        public ICollection<INavCommand> BackToScreen(IEnumerable<Screen> screens, NavigatorSpecification spec, Class<Screen> screenClass);
        public ICollection<INavCommand> BackToRoot(IEnumerable<Screen> screens, NavigatorSpecification spec);
        public ICollection<INavCommand> ReplaceScreen(IEnumerable<Screen> screens, NavigatorSpecification spec, Screen screen);
    }
}