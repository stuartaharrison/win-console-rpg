using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.Models {

    public sealed class MapBoundary {

        public int XStart { get; set; }
        public int YStart { get; set; }
        public int XWidth { get; set; }
        public int YHeight { get; set; }
        public int BaseMapWidth { get; set; }
        public int BaseMapHeight { get; set; }
        public int TrueMapWidth { get; set; }
        public int TrueMapHeight { get; set; }

        public MapBoundary(int startX, int startY, int x, int y, int width, int height, int tWidth, int tHeight) {
            XStart = startX;
            YStart = startY;
            XWidth = x;
            YHeight = y;
            BaseMapWidth = width;
            BaseMapHeight = height;
            TrueMapWidth = tWidth;
            TrueMapHeight = tHeight;
        }
    }
}
