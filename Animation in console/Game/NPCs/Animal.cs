using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame.Game.NPCs
{
    internal abstract class Animal : Inhabitant
    {
        public override void TakeTurn()
        {
            Action();
        }
    }
}
