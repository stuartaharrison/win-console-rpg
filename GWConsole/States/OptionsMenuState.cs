using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.States {

    public sealed class OptionsMenuState : BaseMenuState {

        public OptionsMenuState(int baseMenuIndex) : base(baseMenuIndex) {
            _selectedMenu = baseMenuIndex;
        }

        public OptionsMenuState(int baseMenuIndex, string menuHeading) : base(baseMenuIndex, menuHeading) {
            _selectedMenu = baseMenuIndex;
            _menuHeading = menuHeading;
        }
    }
}
