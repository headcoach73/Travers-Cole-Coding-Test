using System;
using System.Collections.Generic;
using System.Text;

namespace CodingTest.Commands
{
    /// <summary>
    /// Command profile used to define commands used for the command manager.
    /// </summary>
    abstract class CommandProfile
    {
        /// <summary>
        /// Loads the commands of this profile.
        /// </summary>
        /// <returns></returns>
        public abstract List<CommandBase> LoadCommands();
    }
}
