using SimulationGame.Game.Handlers;
using SimulationGame.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame.Game.NPCs
{
    internal class Sheep : Animal
    {
        // Simple animal.
        // Tends to eat plants.
        // If not, then move back. Do not attack animals.
        public Sheep()
        {
            Console.WriteLine("Called constructor of a sheep");
            overrideSpiecesData();
            _localisation = InhabitantMovementHandler.GetAnyRandomPlace();
            if (_localisation == new Point(-1, -1)) { }
            else
            {
                Sheep copy = this;
                InhabitantMovementHandler.callIInitiationHandler(ref copy);
            }
        }
        protected override void overrideSpiecesData()
        {
            _visualRepr = " $ ";
            _strength = 2;
            _isAlive = true;
            _targets = typeof(Plant);
        }
    }
}
