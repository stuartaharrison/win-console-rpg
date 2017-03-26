using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.Models {

    public class Player {

        public readonly char PlayerIcon = '@';
        public enum PlayerDirection {
            Up, Down, Left, Right
        }

        public bool IsOnMap { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Name { get; private set; }

        public Player (string name, int x, int y) {            
            IsOnMap = false;
            X = x;
            Y = y;
            Name = name;
        }

        public bool IsAtPosition(MapPoint point) {
            if (X == point.X && Y == point.Y) {
                return true;
            }
            return false;
        }

        public void SetPosition(MapPoint point) {
            X = point.X;
            Y = point.Y;
        }

        // TODO: check, may have some bugs!
        public void MovePlayer(PlayerDirection direction, MapBoundary mapBoundary) {
            if (direction == PlayerDirection.Up) {
                Y--;
                if (Y <= mapBoundary.YStart) {
                    Y = mapBoundary.YStart + 1;
                }
            }
            else if (direction == PlayerDirection.Down) {
                Y++;
                if (Y >= mapBoundary.TrueMapHeight) {
                    Y = mapBoundary.TrueMapHeight;
                }
            }
            else if (direction == PlayerDirection.Left) {
                X--;
                if (X <= mapBoundary.XStart) {
                    X = mapBoundary.XStart + 1;
                }
            }
            else if (direction == PlayerDirection.Right) {
                X++;
                if (X >= mapBoundary.TrueMapWidth) {
                    X = mapBoundary.TrueMapWidth;
                }
            }
        }
    }
}
