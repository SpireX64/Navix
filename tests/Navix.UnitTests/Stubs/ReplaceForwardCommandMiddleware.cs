using System.Diagnostics.CodeAnalysis;
using Spx.Navix.Abstractions;
using Spx.Navix.Commands;

namespace Spx.Navix.UnitTests.Stubs
{
    [ExcludeFromCodeCoverage]
    public class ReplaceForwardCommandMiddleware: INavigationMiddleware
    {
        private readonly INavCommand _command;

        public INavCommand? ExecutedCommand { get; private set; } = null;

        public ReplaceForwardCommandMiddleware(INavCommand command)
        {
            _command = command;
        }
        
        public void BeforeApply(Screen? currentScreen, ref INavCommand incomingCommand)
        {
            if (incomingCommand is ForwardNavCommand)
            {
                incomingCommand = _command;
            }
        }

        public void AfterApply(Screen? currentScreen, INavCommand command)
        {
            ExecutedCommand = command;
        }
    }
}