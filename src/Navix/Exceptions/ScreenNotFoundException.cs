using System;
using Spx.Reflection;

namespace Spx.Navix.Exceptions
{
    public class ScreenNotFoundException : InvalidOperationException
    {
        public Class<Screen> ScreenClass { get; }

        public ScreenNotFoundException(Class<Screen> screenClass)
            : base($"Given screen '{screenClass.Type.Name}' not found!")
        {
            ScreenClass = screenClass;
        }
    }
}