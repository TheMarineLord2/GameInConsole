using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame.NPCs
{
    internal class Organism:Entity
    {
        public Organism()
        {
            overwriteSpiecesData();
        }
        public Organism(Point destination)
        {
            overwriteSpiecesData();
        }
        public virtual void Action() { Reproduce(); }
        //------------------protected---------------------
        protected virtual void overwriteSpiecesData()
        {
            visualRepr = "_";
            strength = 0;
            spieces = 0;
            isAlive = false;
            home = World.GetInstance();
        }
        protected virtual void Reproduce() { }
    }
}
