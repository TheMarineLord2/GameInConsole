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
    internal abstract class Inhabitant : IInhabitant
    {
        protected Inhabitant()
        {
            _home = World.This();
            overrideSpiecesData();
            _localisation = InhabitantMovementHandler.GetAnyRandomPlace();
        }
        
        protected Inhabitant(Point destination)
        {
            _home = World.This();
            overrideSpiecesData();
            _localisation = destination;
        }

        // fields
        protected Point _localisation;
        protected string _visualRepr;
        protected int _strength;
        protected int _initiative;
        protected bool _isAlive;
        protected World _home;

        // ------     should be avaiable to everyone
        public Point GetLocalisation() { return _localisation; }
        
        public int GetStrength()
        {
            return _strength;
        }

        public bool IsAlive() { return _isAlive; }

        public int GetInitiative() { return _initiative; }
        
        public void Print() { Console.Write(_visualRepr); }
        
        public virtual void TakeTurn() { }
        
        public void Die()
        {
            // change status of object.
            // remove from World
            // let the C# deconstruct it
            _isAlive = false;
        }

        // ------    protected methods
        protected virtual void overrideSpiecesData()
        {
            _visualRepr = "_";
            _strength = 0;
            _initiative = 0;
            _isAlive = false;

        }
        
        protected virtual void reproduce() { }

        protected void move(Field destination)
        {
            // check if field is not taken by any means
            // You can do this twice. preferabbly take it from _home
            if (destination.inhabitant == null || destination.inhabitant.IsAlive()==false)
            {
                _localisation = destination.localisation;
            }
        }

        protected void setLocalisation(Point localisation)
        {
            _localisation = localisation;
        }

        protected virtual List<Field> filterThroughFields(List<Field> fields) { return fields; }

        protected Field pickDestination(List<Field> fields)
        {
            fields = filterThroughFields(fields);
            int rng = new Random().Next(0,fields.Count());
            return fields[rng];
        }

        protected BattleResults getBattleResults<T> (T defender) where T : IInhabitant
        {
            int enemyStrength = defender.GetStrength();
            if (enemyStrength > _strength) { return BattleResults.LOSS; }
            else if(enemyStrength < _strength) { return BattleResults.WIN; }
            else { return BattleResults.DRAW; }
        }
    }
}
