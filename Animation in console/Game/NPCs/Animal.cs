using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Game.Handlers;

namespace SimulationGame.Game.NPCs
{
    internal abstract class Animal : Inhabitant
    {
        public override void TakeTurn()
        {
            Action();
        }
        // --- protected methods
        protected List<Field> lookAround(int range)
        {
            return InhabitantMovementHandler.GetArrayOfViableDestinations(_localisation, range);
        }

        protected virtual Field pickDestination(List<Field> options)
        {
            int fieldNumber = new Random().Next(0, options.Count);
            return options[fieldNumber];
        }
        protected void moveRandomly()
        {
            Field targetDestination = pickDestination(lookAround(1));
            // InhabitantMovementHandler.Move(this, Field)
        }
    }
}
