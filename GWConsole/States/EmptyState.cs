using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.States {

    public class EmptyState : IState {

        public void Update(float elapsedTime, out int exitCode, out string exitMessage) {
            exitCode = 0;
            exitMessage = string.Empty;
        }

        public void Render() {

        }

        public void OnEnter() {

        }

        public void OnExit() {

        }
    }
}
