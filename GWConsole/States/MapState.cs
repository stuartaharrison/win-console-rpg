using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.States {

    public sealed class MapState : IState {

        private string[,] map = new string[,] {
            { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" },
            { "|", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "|" },
            { "|", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "|" },
            { "|", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "|" },
            { "|", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "|" },
            { "|", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "|" },
            { "|", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "|" },
            { "|", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "|" },
            { "|", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "|" },
            { "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-", "-" }
        };

        public void OnEnter() {

        }

        public void OnExit() {

        }

        public void Render() {
            // TODO: add a map factory to load visuals
            // For now, I am going to generate a basic grid map
            for (int y = 0; y < map.GetLength(0); y++) {
                for (int x = 0; x < map.GetLength(1); x++) {
                    Console.SetCursorPosition(x, y);
                    Console.Write(map[y, x]);
                }
            }
        }

        public void Update(float elapsedTime, out int exitCode, out string exitMessage) {
            exitCode = 0;
            exitMessage = string.Empty;

            ConsoleKeyInfo playerInput = Console.ReadKey();

            if (playerInput.Key == ConsoleKey.Escape) {
                // Load the Game Menu Screen
                exitCode = 2;
                exitMessage = "GameMenu";
            }
        }
    }
}
