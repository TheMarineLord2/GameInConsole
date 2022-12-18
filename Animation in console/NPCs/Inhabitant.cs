using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Interfaces;

namespace SimulationGame.NPCs
{
    internal abstract class Inhabitant
    {
        protected Inhabitant()
        {
            overwriteSpiecesData();
        }
        protected Inhabitant(Point destination)
        {
            overwriteSpiecesData();
        }
        protected virtual void overwriteSpiecesData()
        {
            visualRepr = "_";
            str = 0;
            spieces = 0;
            isAlive = false;
            home = World.GetInstance();
        }
        protected Point localisation;
        protected string visualRepr;
        protected int str;
        protected OrganismTypes spieces;
        protected bool isAlive;
        protected World home;
        //-----------------------------
        public int GetX() { return localisation.X; }
        public int GetY() { return localisation.Y; }
        public Point GetLocalisation() { return localisation; }
        public OrganismTypes GetSpieces() { return spieces; }
        public int GetStr() { return str; }
        public void Print() { Console.Write(visualRepr); }
        public abstract void Action();
        //-----------------------------
        protected virtual void callIInitiationHandler()
        {
            IInitiationHandler.PlaceInWorld(this);
        }
        protected Point tryGettingRandomPlace()
        {
            if (IFieldNavigation.GetNumberOfFreeSpaces() == 0)
            {
                Console.WriteLine("Inhabitant: No free space is aviable \n" +
                "Returning illegal place.\n");
                return new(-1, -1);
            }
            else return IFieldNavigation.GetRandomPlace();
        }
        protected void noLegalPlaceFromRandomMSG() { Console.WriteLine(spieces + ": Not calling initiation handler. No legal place was returned from random \n"); }
    }
}
