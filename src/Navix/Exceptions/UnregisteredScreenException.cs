using System;

namespace Spx.Navix.Exceptions
{
    public class UnregisteredScreenException : NotImplementedException
    {
        public UnregisteredScreenException(Screen screen)
            : base($"Attempt navigate to unregistered screen '{screen.GetType().FullName}'!")
        {
            Screen = screen;
        }

        public Screen Screen { get; }
    }
}