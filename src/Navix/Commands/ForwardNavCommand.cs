﻿using Navix.Abstractions;

namespace Navix.Commands
{
    public sealed class ForwardNavCommand : INavCommand
    {
        private readonly Screen _screen;
        private readonly IScreenResolver _screenResolver;

        public ForwardNavCommand(Screen screen, IScreenResolver resolver)
        {
            _screen = screen;
            _screenResolver = resolver;
        }

        public void Apply(Navigator navigator, ScreenStack screens)
        {
            screens.Push(_screen);
            navigator.Forward(_screen, _screenResolver);
        }
    }
}