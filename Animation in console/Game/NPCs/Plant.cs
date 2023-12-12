using SimulationGame.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame.Game.NPCs
{
    internal abstract class Plant : Inhabitant
    {
        protected virtual void Action() 
        { 
            Reproduce();
        }

        override public void TakeTurn()
        {
            Console.WriteLine("Taking turn of a plant");
            Action();
        }
    }
}
