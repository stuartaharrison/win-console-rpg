using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWConsole.States {

    public interface IState {

        void Update(float elapsedTime, out int exitCode, out string exitMessage);
        void Render();
        void OnEnter();
        void OnExit();
    }
}
