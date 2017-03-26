using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.Models {

    // Yes, I could using System.Drawing
    // But that would require adding an additional reference for the sake of one class!
    public sealed class MapPoint {

        public int X { get; private set; }
        public int Y { get; private set; }

        public MapPoint(int x, int y) {
            X = x;
            Y = y;
        }
    }
}
