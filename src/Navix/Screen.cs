namespace Spx.Navix
{
    public abstract class Screen
    {
        
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