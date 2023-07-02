using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame.Game.NPCs
{
    internal class Sheep : Animal
    {
        protected override void overrideSpiecesData()
        {
            visualRepr = " $ ";
            strength = 2;
            isAlive = true;
            myType = this;
        }
    }
}
