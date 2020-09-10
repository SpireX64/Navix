namespace NavixPrototype.Navix
{
    public interface INavigatorHolder
    {
        void Hold(Navigator navigator);
        void Free();
    }
}