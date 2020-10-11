namespace Spx.Navix.Abstractions
{
    /// <summary>
    ///     This is a special software, embedded in the Navix pipeline,
    ///     to listen and control the flow of navigation commands.
    /// </summary>
    public interface INavigationMiddleware
    {
        /// <summary>
        ///     Called before the navigation command is executed.
        /// </summary>
        /// <param name="currentScreen">Current screen object</param>
        /// <param name="incomingCommand">Incoming command to execute</param>
        void BeforeApply(Screen? currentScreen, ref INavCommand incomingCommand);

        /// <summary>
        ///     Called after command execution
        /// </summary>
        /// <param name="currentScreen">Current screen object</param>
        /// <param name="command">The command that was executed</param>
        void AfterApply(Screen? currentScreen, INavCommand command);
    }
}