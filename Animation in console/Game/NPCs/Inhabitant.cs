using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Game.Interfaces;

namespace SimulationGame.Game.NPCs
{
    internal abstract class Inhabitant : INonPlayerCharacter
    {
        protected Inhabitant()
        {
            home = World.GetInstance();
            overrideSpiecesData();
            localisation = ObjectStatusMovementsInteractions.GetRandomPlace();
        }
        protected Inhabitant(Point destination)
        {
            home = World.GetInstance();
            overrideSpiecesData();
            localisation = destination;
        }
        protected Point localisation;
        protected string visualRepr;
        protected int strength;
        protected int initiative;
        protected Inhabitant? myType;
        protected bool isAlive;
        protected World home;
        //-----------------------------     implemented in interface
        public Point GetLocalisation() { return localisation; }
        public int GetInitiative() { return initiative; }
        public void Print() { Console.Write(visualRepr); }
        public virtual void TakeTurn() { }
        public Inhabitant? getMyType() { return myType; }
        //-----------------------------     protected methods
        protected virtual void overrideSpiecesData()
        {
            visualRepr = "_";
            strength = 0;
            initiative = 0;
            isAlive = false;
            myType = this;

        }
        protected virtual void Action() { }
        protected virtual void Reproduce() { }

    }
}
