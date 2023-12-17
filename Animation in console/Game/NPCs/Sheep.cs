using SimulationGame.Game.Handlers;
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
        }

        protected override List<Field> filterThroughFields(List<Field> options) 
        {
            List<Field> consideredFields = new();
            for (int i = 0; i < options.Count - 1; i++)
            {
                if (options[i].inhabitant!=null && options[i].inhabitant.GetType().IsSubclassOf(typeof(Plant)))
                {
                    consideredFields.Add(options[i]);
                }
            }
            if (consideredFields.Count == 0) { return options; }
            else { return consideredFields; }
        }
    }
}
