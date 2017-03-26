using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.Models {

    public sealed class MenuOption {

        public int ExitCode { get; private set; }
        public string Display { get; private set; }
        public string ExitMessage { get; private set; }

        public MenuOption(string display, string exitMessage, int exitCode) {
            Display = display;
            ExitMessage = exitMessage;
            ExitCode = exitCode;
        }
    }
}
