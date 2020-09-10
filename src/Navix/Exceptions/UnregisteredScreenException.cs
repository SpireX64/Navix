using System;

namespace Spx.Navix.Exceptions
{
    public class UnregisteredScreenException: NotImplementedException
    {
        public Screen Screen { get; }

        public UnregisteredScreenException(Screen screen)
        {
            Screen = screen;
        }
    }
}