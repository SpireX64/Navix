namespace NavixPrototype.Navix
{
    public abstract class Screen
    {
        public abstract string Name { get; }
    }

    public abstract class Screen<TProps>: Screen
    {
        public TProps Props { get; }

        public Screen(TProps props)
        {
            Props = props;
        }
    }
}