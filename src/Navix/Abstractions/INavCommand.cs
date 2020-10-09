namespace Spx.Navix.Abstractions
{
    public interface INavCommand
    {
        /// <summary>
        /// Performs an operation on a navigator
        /// </summary>
        /// <param name="navigator">The instance of navigator being used</param>
        /// <param name="screens">Sequence of navigation commands</param>
        void Apply(Navigator navigator, ScreenStack screens);
    }
}