using System.Diagnostics.CodeAnalysis;
using Navix.Abstractions;
using Navix.Commands;

namespace Navix.UnitTests.Stubs
{
    [ExcludeFromCodeCoverage]
    public class ReplaceForwardCommandMiddleware : INavigationMiddleware
    {
        private readonly INavCommand _command;

        public ReplaceForwardCommandMiddleware(INavCommand command)
        {
            _command = command;
        }

        public INavCommand? ExecutedCommand { get; private set; } = null;

        public void BeforeApply(Screen? currentScreen, ref INavCommand incomingCommand)
        {
            if (incomingCommand is ForwardNavCommand) incomingCommand = _command;
        }

        public void AfterApply(Screen? currentScreen, INavCommand command)
        {
            ExecutedCommand = command;
        }
    }
}