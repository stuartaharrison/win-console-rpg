using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GWConsole.Models;

namespace GWConsole.Controllers {

    // Will need to heavily modify this at some point!
    public static class SaveController {

        public static Player LoadedPlayer { get; private set; }

        public static void New() {
            LoadedPlayer = new Player("TestPlayer", 0, 0);
        }

        public static void Load(int saveId) {

        }

        public static void Save() {

        }
    }
}
