using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GWConsole.Controllers;
using GWConsole.Models;

namespace GWConsole.States {

    public abstract class BaseMenuState : IState {

        protected readonly ConsoleColor _defaultConsoleColour = ConsoleColor.Gray;
        protected readonly ConsoleColor _menuSelectConsoleColour = ConsoleColor.DarkCyan;
        protected readonly ConsoleColor _menuMessageConsoleColour = ConsoleColor.DarkRed;

        protected int _selectedMenu = 0;
        protected int _selectedMenuIndex = 0;
        protected MenuOption[] _renderedMenu;
        protected string _menuHeading;
        protected string _menuMessage;

        public BaseMenuState(int baseMenuIndex) {
            _selectedMenu = baseMenuIndex;
        }

        public BaseMenuState(int baseMenuIndex, string menuHeading) {
            _selectedMenu = baseMenuIndex;
            _menuHeading = menuHeading;
        }

        public virtual void OnEnter() {
            if (_renderedMenu == null) {
                _renderedMenu = MenuController.GetMenu(_selectedMenu);
            }
        }

        public virtual void OnExit() {

        }

        public virtual void Render() {
            if (!String.IsNullOrWhiteSpace(_menuHeading)) {
                Console.WriteLine(_menuHeading);
                Console.WriteLine("----------------");
            }

            for (int i = 0; i < _renderedMenu.Length; i++) {
                if (i == _selectedMenuIndex) {
                    Console.ForegroundColor = _menuSelectConsoleColour;
                    Console.WriteLine(_renderedMenu[i].Display);
                    Console.ForegroundColor = _defaultConsoleColour;
                }
                else {
                    Console.WriteLine(_renderedMenu[i].Display);
                }
            }

            if (!String.IsNullOrWhiteSpace(_menuMessage)) {
                Console.ForegroundColor = _menuMessageConsoleColour;
                Console.WriteLine();
                Console.WriteLine(_menuMessage);
                Console.ForegroundColor = _defaultConsoleColour;
            }
        }

        public virtual void Update(float elapsedTime, out int exitCode, out string exitMessage) {
            exitCode = 0;
            exitMessage = string.Empty;
            _menuMessage = null;

            ConsoleKeyInfo playerInput = Console.ReadKey();

            if (playerInput.Key == ConsoleKey.UpArrow) {
                MenuUp();
            }
            if (playerInput.Key == ConsoleKey.DownArrow) {
                MenuDown();
            }

            // Check for Action Key!
            if (playerInput.Key == ConsoleKey.X) {
                PlayMenuSound();
                exitCode = _renderedMenu[_selectedMenuIndex].ExitCode;
                exitMessage = _renderedMenu[_selectedMenuIndex].ExitMessage;

                if (exitCode == 1) {
                    _menuMessage = exitMessage;
                }
            }
        }

        protected virtual void MenuUp() {
            PlayMenuSound();
            _selectedMenuIndex--;
            if (_selectedMenuIndex < 0) {
                _selectedMenuIndex = _renderedMenu.Length - 1;
            }
        }

        protected virtual void MenuDown() {
            PlayMenuSound();
            _selectedMenuIndex++;
            if (_selectedMenuIndex > (_renderedMenu.Length - 1)) {
                _selectedMenuIndex = 0;
            }
        }

        protected virtual void PlayMenuSound() {
            AudioController.PlayOneShot(0);
        }
    }
}
