using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Game.Interfaces;
using SimulationGame.Game;

namespace SimulationGame.Game.NPCs
{
    internal class Dandelion : Plant
    {
        public Dandelion()
        {
            overrideSpiecesData();
            localisation = ObjectStatusMovementsInteractions.GetRandomPlace();
            if (localisation == new Point(-1, -1)) {}
            else
            {
                Dandelion copy= this;
                ObjectStatusMovementsInteractions.callIInitiationHandler(ref copy) ;
            }
        }
        protected Dandelion(Point destination)
        {
            overrideSpiecesData();
            localisation = destination;
            Dandelion copy = this;
            ObjectStatusMovementsInteractions.callIInitiationHandler(ref copy);
        }
        protected override void overrideSpiecesData()
        {
            visualRepr = " * ";
            strength = 0;
            initiative = 2;
            isAlive = true;
            myType = this;
        }
        //---------------------------
        protected override void Reproduce()
        {
            if(new Random().Next(4)==0 )
            new Dandelion();
        }
    }
}
