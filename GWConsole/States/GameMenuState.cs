using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.States {

    public sealed class GameMenuState : BaseMenuState {

        public GameMenuState(int baseMenuIndex) : base(baseMenuIndex) {
            _selectedMenu = baseMenuIndex;
        }

        public GameMenuState(int baseMenuIndex, string menuHeading) : base(baseMenuIndex, menuHeading) {
            _selectedMenu = baseMenuIndex;
            _menuHeading = menuHeading;
        }
    }
}
