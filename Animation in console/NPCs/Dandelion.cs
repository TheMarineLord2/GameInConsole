using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Interfaces;

namespace SimulationGame.NPCs
{
    internal class Dandelion : Plant
    {
        public Dandelion()
        {
            localisation = IFieldNavigation.GetRandomPlace();
            if (localisation == new Point(-1, -1)) { NoLegalPlaceFromRandomMSG(); }
            else
            {
                CallIInitiationHandler();
            }
        }
        protected Dandelion(Point destination)
        {
            localisation = destination;
            CallIInitiationHandler();
        }
        protected override void overwriteSpiecesData()
        {
            visualRepr = " * ";
            strength = 0;
            spieces = OrganismTypes.Dandelion;
            isAlive = true;
            home = World.GetInstance();
        }
        //----------------------------
    }
}
