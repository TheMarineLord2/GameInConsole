using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Game.Interfaces;

namespace SimulationGame.Game.Shell
{
    internal class Shell
    {
        static Shell? instance = null;
        private Shell() { }
        static public void InitiateShell(TextReader? input = null, TextWriter? output = null)
        {
            // if null -> initialise
            instance ??= new Shell();
            input ??= Console.In;
            output??= Console.Out;
            instance.start(input, output);
        }
        void start(TextReader input, TextWriter output)
        {
            output.WriteLine("Greetings, traveller!");
        }
    }
}
