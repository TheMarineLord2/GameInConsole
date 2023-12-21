using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Game.Handlers;
using SimulationGame.Game.Interfaces;

namespace SimulationGame.Game.NPCs
{
    internal abstract class Animal : Inhabitant
    {
        protected int _movementRange = 1;
        protected int _timeSinceLastMeal = 0;
        protected int _maxFastingTime = 5;
        protected int _mealsToReproduce = 2;
        protected int _mealsEaten = 0;
        protected Type _targets = null;
        protected virtual void Act() 
        {
            Console.WriteLine("ACTING ACTING ACTING");
            Field destination = pickDestination(lookAround(_movementRange));
            if(destination.localisation == new Point(-1, -1))
            {
                // do not move. Animal is still able to perform any necessary actions
            }
            else {
                if (destination.inhabitant == null) 
                {
                    // Console.WriteLine("No plant near. Moved to empty space "+destination.localisation.ToString());
                    move(destination);
                }
                else
                {
                    // attack and decide what to do
                    switch (getBattleResults(destination.inhabitant))
                    {
                        case Interfaces.BattleResults.WIN:
                            {
                                if (ThisInhabitantIsFood(destination.inhabitant)) { _mealsEaten++; }
                                destination.inhabitant.Die();
                                move(destination);
                                Console.WriteLine("Won. Tried moving to destination " + destination.localisation.ToString());
                                reproduce();
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

            
        }
        public override void TakeTurn()
        {
            Act();
        }
        // --- protected methods
        protected virtual bool ThisInhabitantIsFood(IInhabitant target)
        {
            if(_targets == null) { return false; }
            else return target.GetType().IsSubclassOf(_targets);
        }
        protected List<Field> lookAround(int range)
        {
            return InhabitantMovementHandler.GetArrayOfViableDestinations(_localisation, range);
        }
        protected override List<Field> filterThroughFields(List<Field> options)
        {
            List<Field> consideredFields = new();
            for (int i = 0; i < options.Count - 1; i++)
            {
                if (options[i].inhabitant != null && ThisInhabitantIsFood(options[i].inhabitant))
                {
                    consideredFields.Add(options[i]);
                }
            }
            if (consideredFields.Count == 0) { return options; }
            else { return consideredFields; }
        }
    }
}
