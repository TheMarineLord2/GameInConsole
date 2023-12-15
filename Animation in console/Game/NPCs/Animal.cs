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
        int _movementRange = 1;
        protected virtual void Act() 
        {
            Field destination = pickDestination(lookAround(_movementRange));

            if (destination.inhabitant == null) { move(destination); }
            else 
            {
                // attack and decide what to do
                switch (getBattleResults(destination.inhabitant))
                {
                    case Interfaces.BattleResults.WIN:
                        {
                            destination.inhabitant.Die();
                            move(destination);
                            break;
                        }
                    case Interfaces.BattleResults.LOSS:
                        {
                            this.Die();
                            break;
                        }
                        case Interfaces.BattleResults.DRAW:
                        {
                            // be passive
                            break;
                        }

                } 
            }
        }
        public override void TakeTurn()
        {
            Act();
        }
        // --- protected methods
        protected List<Field> lookAround(int range)
        {
            return InhabitantMovementHandler.GetArrayOfViableDestinations(_localisation, range);
        }
    }
}
