using System;
using Spx.Reflection;

namespace Navix.Exceptions
{
    public class ScreenNotFoundException : InvalidOperationException
    {
        public ScreenNotFoundException(Class<Screen> screenClass)
            : base($"Given screen '{screenClass.Type.Name}' not found!")
        {
            ScreenClass = screenClass;
        }

        public Class<Screen> ScreenClass { get; }
    }
}