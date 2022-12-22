using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Interfaces;

namespace SimulationGame.NPCs
{
    internal abstract class Entity
    {
        protected Entity()
        {
        }
        protected Point localisation;
        protected string visualRepr;
        protected int strength;
        protected OrganismTypes spieces;
        protected bool isAlive;
        protected World home;
        //-----------------------------
        public int GetX() { return localisation.X; }
        public int GetY() { return localisation.Y; }
        public Point GetLocalisation() { return localisation; }
        public OrganismTypes GetSpieces() { return spieces; }
        public int GetStrength() { return strength; }
        public void Print() { Console.Write(visualRepr); }
        //-----------------------------
        protected void CallIInitiationHandler()
        {
            IInitiationHandler.PlaceInWorld(this);
        }
        protected Point TryGettingRandomPlace()
        {
            if (IFieldNavigation.GetNumberOfFreeSpaces() == 0)
            {
                Console.WriteLine("Inhabitant: No free space is aviable \n" +
                "Returning illegal place.\n");
                return new(-1, -1);
            }
            else return IFieldNavigation.GetRandomPlace();
        }
        protected void NoLegalPlaceFromRandomMSG() {
            Console.WriteLine(spieces + ": Not calling initiation handler. No legal place was returned from random \n");
        }
    }
}
