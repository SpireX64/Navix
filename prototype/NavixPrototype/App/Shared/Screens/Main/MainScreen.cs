using NavixPrototype.Navix;

namespace NavixPrototype.App.Shared.Screens.Main
{
    public class MainScreen: Screen<MainScreenProps>
    {
        public override string Name { get; } = nameof(MainScreen);

        public MainScreen(MainScreenProps props) : base(props)
        {
        }
    }
}