﻿using System;
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
            if (localisation == new Point(-1, -1)) { noLegalPlaceFromRandomMSG(); }
            else
            {
                callIInitiationHandler();
            }
        }
        protected Dandelion(Point destination)
        {
            localisation = destination;
            callIInitiationHandler();
        }
        protected override void overwriteSpiecesData()
        {
            visualRepr = " * ";
            str = 0;
            spieces = OrganismTypes.Dandelion;
            isAlive = true;
            home = World.GetInstance();
        }
        //----------------------------
        public override void Action()
        {
        }
        //----------------------------
        override protected void callIInitiationHandler()
        {
            IInitiationHandler.PlaceInWorld(this);
        }
    }
}
