using Spx.Navix.Platform;

namespace Spx.Navix.Commands
{
    public interface INavCommand
    {
        void Apply(Navigator navigator);
    }
}