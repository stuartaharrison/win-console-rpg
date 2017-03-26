using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GWConsole.Controllers;
using GWConsole.Models;

namespace GWConsole.States {

    public class BaseMapState : IState {

        protected readonly char _wallIcon = '#';
        protected readonly ConsoleColor _defaultMapColour = ConsoleColor.Gray;
        protected readonly ConsoleColor _wallColour = ConsoleColor.DarkRed;

        protected int _selectedMapId;
        protected Map _selectedMap;

        public BaseMapState(int mapId) {
            _selectedMapId = mapId;
        }

        public virtual void OnEnter() {
            _selectedMap = MapController.GetMap(_selectedMapId);

            if (SaveController.LoadedPlayer == null) {
                SaveController.New();
            }
            SaveController.LoadedPlayer.SetPosition(new MapPoint(_selectedMap.PlayerStartX, _selectedMap.PlayerStartY));
        }

        public virtual void OnExit() {

        }

        public virtual void Render() {
            if (_selectedMap != null) {
                Console.WriteLine(_selectedMap.Title);
                Console.WriteLine("---------------------------");

                MapBoundary boundary = MapController.GetMapBoundary(_selectedMap);

                for (int x = boundary.XStart; x < boundary.XWidth; x++) {
                    for (int y = boundary.YStart; y < boundary.YHeight; y++) {
                        Console.SetCursorPosition(x, y);
                        var mapPoint = new MapPoint(x, y);
                        // Draw the border
                        if (x == boundary.XStart || x == boundary.XWidth - 1 || y == boundary.YStart || y == boundary.YHeight - 1) {
                            Console.ForegroundColor = _wallColour;
                            Console.Write(_wallIcon);
                            Console.ForegroundColor = _defaultMapColour;
                        }


                    }
                }

                // Place the Player
                if (SaveController.LoadedPlayer != null) {
                    int playerX = SaveController.LoadedPlayer.X;
                    int playerY = SaveController.LoadedPlayer.Y;
                    Console.SetCursorPosition(playerX, playerY);
                    Console.Write(SaveController.LoadedPlayer.PlayerIcon);
                    Console.SetCursorPosition(playerX, playerY); // Important and required otherwise cursor is placed 1 to the right
                }
            }
        }

        public virtual void Update(float elapsedTime, out int exitCode, out string exitMessage) {
            exitCode = 0;
            exitMessage = string.Empty;

            ConsoleKeyInfo playerInput = Console.ReadKey();

            if (SaveController.LoadedPlayer != null && _selectedMap != null) {
                MapBoundary boundary = MapController.GetMapBoundary(_selectedMap);
                if (playerInput.Key == ConsoleKey.LeftArrow) {
                    SaveController.LoadedPlayer.MovePlayer(Player.PlayerDirection.Left, boundary);
                }
                if (playerInput.Key == ConsoleKey.RightArrow) {
                    SaveController.LoadedPlayer.MovePlayer(Player.PlayerDirection.Right, boundary);
                }
                if (playerInput.Key == ConsoleKey.UpArrow) {
                    SaveController.LoadedPlayer.MovePlayer(Player.PlayerDirection.Up, boundary);
                }
                if (playerInput.Key == ConsoleKey.DownArrow) {
                    SaveController.LoadedPlayer.MovePlayer(Player.PlayerDirection.Down, boundary);
                }
            }

            if (playerInput.Key == ConsoleKey.Escape) {
                // Load the Game Menu Screen
                exitCode = 2;
                exitMessage = "GameMenu";
            }
        }
    }
}
