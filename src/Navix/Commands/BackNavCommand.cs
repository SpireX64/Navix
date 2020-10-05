using Spx.Navix.Abstractions;

namespace Spx.Navix.Commands
{
    public sealed class BackNavCommand : INavCommand
    {
        public void Apply(Navigator navigator)
        {
            navigator.Back();
        }
    }
}