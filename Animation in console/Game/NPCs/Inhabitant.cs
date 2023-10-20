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

        // ------     implemented in interface
        public Point GetLocalisation() { return _localisation; }
        public int GetInitiative() { return _initiative; }
        public void Print() { Console.Write(_visualRepr); }
        public virtual void TakeTurn() { }
        public Inhabitant? getMyType() { return _myType; }

        // ------    protected methods
        protected virtual void overrideSpiecesData()
        {
            _visualRepr = "_";
            _strength = 0;
            _initiative = 0;
            _isAlive = false;
            _myType = this;

        }
        protected virtual void Action() {   /* do notning yet */}
        protected virtual void Reproduce() { }

        public void Die()
        {
            // change status of object.
            // remove from World
            // let the C# deconstruct it
            throw new NotImplementedException();
        }

        public void Escape()
        {
            throw new NotImplementedException();
        }

        public void TakeThisField()
        {
            throw new NotImplementedException();
        }

        public void MoveBack()
        {
            throw new NotImplementedException();
        }

        public void SetLocalisation(Point localisation)
        {
            _localisation = localisation;
        }
        public CombatStats GetCombatStats()
        {
            throw new NotImplementedException();
        }
    }
}
