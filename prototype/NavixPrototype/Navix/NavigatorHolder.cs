using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NavixPrototype.Navix.Commands;

namespace NavixPrototype.Navix
{
    public class NavigatorHolder: INavigatorHolder
    {
        private ConcurrentQueue<INavigationCommand[]> _pendingCommands = new ConcurrentQueue<INavigationCommand[]>();

        private volatile Navigator _navigator = null;
        
        public Screen CurrentScreen { get; }
        public Navigator Navigator => _navigator;

        public void executeCommands(IEnumerable<INavigationCommand> commands)
        {
            if (_navigator != null)
            {
                applyCommands(commands);
            }
            else
            {
                _pendingCommands.Enqueue(commands.ToArray());
            }
        }

        private void applyCommands(IEnumerable<INavigationCommand> commands)
        {
            foreach (var command in commands)
            {
                switch (command)
                { 
                    case ForwardCommand forwardCommand:
                        Navigator.NavigateTo(forwardCommand.Screen);
                        break;
                    
                    case ReplaceCommand replaceCommand:
                        Navigator.Replace(replaceCommand.Screen);
                        break;
                    
                    case BackToCommand backToCommand:
                        Navigator.BackTo(backToCommand.ScreenType);
                        break;
                    
                    case BackCommand backCommand:
                        Navigator.Back();
                }
            }
        }

        public void Hold(Navigator navigator)
        {
            _navigator = navigator;
        }

        public void Free()
        {
            _navigator = null;
        }
    }
}