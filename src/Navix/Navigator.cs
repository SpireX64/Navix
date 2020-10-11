using System;

namespace Spx.Navix
{
    /// <summary>
    ///     Performs navigation specific to the current platform
    /// </summary>
    public abstract class Navigator
    {
        /// <summary>
        ///     Current implementation specification
        /// </summary>
        public abstract NavigatorSpecification Specification { get; }

        /// <summary>
        ///     Open new screen and add it to the screens sequence.
        /// </summary>
        /// <param name="screen">The screen on which the navigating is performed</param>
        /// <param name="resolver">Screen resolver</param>
        public abstract void Forward(Screen screen, IScreenResolver resolver);

        /// <summary>
        ///     Return to the previous screen in the sequence.
        /// </summary>
        public abstract void Back();

        /// <summary>
        ///     Return to the specified screen in the screens sequence.
        /// </summary>
        /// <param name="screen">The screen to return to</param>
        /// <exception cref="NotSupportedException"></exception>
        public virtual void BackToScreen(Screen screen)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Return to the root screen.
        /// </summary>
        /// <exception cref="NotSupportedException"></exception>
        public virtual void BackToRoot()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        ///     Replace current screen.
        /// </summary>
        /// <param name="screen">The screen to replace the current one</param>
        /// <param name="resolver">
        ///     Screen resolver<</param>
        /// <exception cref="NotSupportedException"></exception>
        public virtual void Replace(Screen screen, IScreenResolver resolver)
        {
            throw new NotSupportedException();
        }
    }
}