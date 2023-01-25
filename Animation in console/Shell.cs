using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame
{
    internal class Shell : IConsoleToCommands
    {
        private Shell() {}
        static Shell? inst = null;
        static void initShell()
        {
            if(inst == null) { inst = new Shell(); }
            inst.shellStart();
        }
        void shellStart() {
            World mainWorld = World.GetInstance();
            if (IConsoleToCommands.runHandler() == 0)
            {//end program
            }
        }
    }
}
