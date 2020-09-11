namespace Spx.Navix.UnitTests.Stubs
{
    public readonly struct ScreenStubProps
    {
        public int Id { get; }

        public ScreenStubProps(int id)
        {
            Id = id;
        }
    }
    
    public class ScreenStubWithProps: Screen<ScreenStubProps>
    {
        public ScreenStubWithProps(ScreenStubProps props) : base(props)
        {
        }
    }

    public class ScreenStubWithPropsResolver : IScreenResolver
    {
    }
}