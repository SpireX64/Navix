using Spx.Navix.Platform;

namespace Spx.Navix
{
    public interface INavigatorHolder
    {
        public Navigator? Navigator { get; }
        public void SetNavigator(Navigator navigator);
        public void RemoveNavigator();
    }
}