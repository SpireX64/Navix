namespace Spx.Navix.Abstractions
{
    public interface INavigationMiddleware
    {
        void BeforeApply(Screen? currentScreen, ref INavCommand incomingCommand);
        void AfterApply(Screen? currentScreen, INavCommand command);
    }
}