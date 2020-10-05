namespace Spx.Navix.Abstractions
{
    public interface INavigatorHolder
    {
        public Navigator? Navigator { get; }
        public void SetNavigator(Navigator navigator);
        public void RemoveNavigator();
    }
}