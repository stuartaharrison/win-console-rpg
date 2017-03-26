using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.Models {

    public class Map {

        public int Id { get; private set; }
        public string Title { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int PlayerStartX { get; set; }
        public int PlayerStartY { get; set; }

        public Map(int id, string title, int width, int height) {
            Id = id;
            Title = title;
            Width = width;
            Height = height;
        }
    }
}
