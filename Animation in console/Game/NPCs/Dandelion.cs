using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Game.Interfaces;
using SimulationGame.Game.Handlers;

namespace SimulationGame.Game.NPCs
{
    internal class Dandelion : Plant
    {
        // Plant, that wildly spreads.
        // no significant actions or interactions with other dandelions.
        // Simple weed
        public Dandelion()
        {
            overrideSpiecesData();
            _localisation = InhabitantMovementHandler.GetAnyRandomPlace();
            if (_localisation == new Point(-1, -1)) {}
            else
            {
                Dandelion copy= this;
                InhabitantMovementHandler.callIInitiationHandler(ref copy) ;
            }
        }
        protected Dandelion(Point destination)
        {
            overrideSpiecesData();
            _localisation = destination;
            Dandelion copy = this;
            InhabitantMovementHandler.callIInitiationHandler(ref copy);
        }
        protected override void overrideSpiecesData()
        {
            _visualRepr = " * ";
            _strength = 0;
            _initiative = 2;
            _isAlive = true;
            _myType = this;
        }
        // ---------------------------
        protected override void Reproduce()
        {
            if(new Random().Next(4)==0 )
            new Dandelion();
        }
    }
}
