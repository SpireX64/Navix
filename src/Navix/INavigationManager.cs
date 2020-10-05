﻿using System.Collections.Generic;
using Spx.Navix.Abstractions;
using Spx.Navix.Commands;

namespace Spx.Navix
{
    public interface INavigationManager
    {
        public bool HasPendingCommands { get; }
        public void SendCommands(IEnumerable<INavCommand> navCommands);
    }
}