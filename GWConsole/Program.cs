using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GWConsole.States;

namespace GWConsole {
    class Program {

        private static bool _run;
        private static bool _errorExit;
        private static float _gameTimer;
        private static string _errorMessage;
        private static Dictionary<string, IState> _states;
        private static Stack<IState> _stack;

        static void Main(string[] args) {
            // Do some setup
            _run = true;
            _errorExit = false;
            _gameTimer = 0;
            _states = new Dictionary<string, IState>() {
                { "MainMenu", new MainMenuState(0, "<< GW Console >>") },
                { "Map", new BaseMapState(0) },
                { "GameMenu", new GameMenuState(1, "<< Pause Menu >>") },
                { "OptionsMenu", new OptionsMenuState(2, "<< GW Console - Options >>") }
            };
            // Setup the Stack
            _stack = new Stack<IState>();
            _stack.Push(_states["MainMenu"]);
            _stack.Peek().OnEnter(); // OnEnter for the MainMenu!
            // Run the GameLoop
            do {
                if (_states != null && _states.Count() > 0) {
                    if (_stack != null && _stack.Count() > 0) {
                        IState currentState = _stack.Peek();
                        Render(currentState);
                        Update(currentState);
                        // Handle a "safe exit"
                        if (_stack.Count() == 0) {
                            _run = false;
                        }
                    }
                    else {
                        _errorExit = true;
                        _errorMessage = "Fatal error! Looks like the game stack is empty!";
                    }
                }
                else {
                    _errorExit = true;
                    _errorMessage = "Fatal error! Looks there is no game states available!";
                } 

            } while (_run && !_errorExit);

            if (_errorExit) {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(_errorMessage);
                Console.Write("Press return to close...");
                Console.ReadLine();
            }
        }

        static void Update(IState currentState) {
            int stateExitCode;
            string stateExitMessage;
            currentState.Update(_gameTimer, out stateExitCode, out stateExitMessage);

            // Exit code determines whether we are removing the state from the stack
            // Put it in a switch block as might be used in the future
            switch (stateExitCode) {
                case -2: // Double pop, because I needed to find a way to go from Game Menu back to Main Menu easily
                    DoublePopState(currentState);
                    break;
                case -1: // Simply a state remove command
                    PopState(currentState);
                    break;
                case 1: // Console Message
                    // TODO: fix some bug here
                    Console.WriteLine(stateExitMessage);
                    break;
                case 2: // State add command/change state command
                    AddState(stateExitMessage);
                    break;
                case 3: // Switch State Command
                    SwitchState(currentState, stateExitMessage);
                    break;
                case 4: // Change Menu Command
                default:
                    break;
            }
        }

        static void Render(IState currentState) {
            Console.Clear();
            currentState.Render();
        }

        static void AddState(string stateName) {
            if (_states.ContainsKey(stateName)) {
                _stack.Peek().OnExit();
                _stack.Push(_states[stateName]);
                _stack.Peek().OnEnter();
            }
        }

        static void PopState(IState currentState) {
            if (currentState != null) {
                currentState.OnExit();
                _stack.Pop();
            }
            if (_stack.Count() > 0) {
                _stack.Peek().OnEnter();
            }
        }

        static void DoublePopState(IState currentState) {
            if (currentState != null) {
                currentState.OnExit();
                _stack.Pop();
            }
            if (_stack.Count() > 0) {
                _stack.Peek().OnExit();
                _stack.Pop();

                if (_stack.Count() > 0) {
                    _stack.Peek().OnEnter();
                }
            }
        }

        static void SwitchState(IState currentState, string newStateName) {
            if (_states.ContainsKey(newStateName)) {
                if (currentState != null) {
                    currentState.OnExit();
                    _stack.Pop();
                }

                IState newState = _states[newStateName];
                newState.OnEnter();
                _stack.Push(newState);
            }
            else {
                throw new Exception("State does not exists!");
            }
        }
    }
}
