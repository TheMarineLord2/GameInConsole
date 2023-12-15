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
        protected Inhabitant? _myType;
        protected bool _isAlive;
        protected World _home;

        // ------     should be avaiable to everyone
        public Point GetLocalisation() { return _localisation; }
        
        public int GetStrength()
        {
            return _strength;
        }

        public int GetInitiative() { return _initiative; }
        
        public void Print() { Console.Write(_visualRepr); }
        
        public virtual void TakeTurn() { }
        
        public Inhabitant? GetMyType() { return _myType; }
        public void Die()
        {
            // change status of object.
            // remove from World
            // let the C# deconstruct it
            Field myField = _home.GetField(_localisation);
            myField.inhabitant = null;
        }

        // ------    protected methods
        protected virtual void overrideSpiecesData()
        {
            _visualRepr = "_";
            _strength = 0;
            _initiative = 0;
            _isAlive = false;
            _myType = this;

        }
        
        protected virtual void reproduce() { }

        // ------   should be kept protected
        protected void move(Field destination)
        {
            throw new NotImplementedException();
        }

        protected void setLocalisation(Point localisation)
        {
            _localisation = localisation;
        }
        protected Field pickDestination(List<Field> fields)
        {
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
