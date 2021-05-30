using Spx.Reflection;

namespace Navix.Abstractions
{
    public interface IScreenRegistrar
    {
        /// <summary>
        ///     Registers the screen
        /// </summary>
        /// <param name="screenClass">Screen class</param>
        /// <param name="resolver">Screen resolver</param>
        public IScreenRegistrationConfig Register(Class<Screen> screenClass, IScreenResolver resolver);

        public void AddMiddleware(INavigationMiddleware middleware);
    }
}