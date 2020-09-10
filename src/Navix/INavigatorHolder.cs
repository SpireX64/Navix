namespace Spx.Navix
{
    public interface INavigatorHolder
    {
        Navigator? Navigator { get; }
        void SetNavigator(Navigator navigator);
        void RemoveNavigator();
    }
}