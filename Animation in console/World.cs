using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.NPCs;
using SimulationGame.Interfaces;

namespace SimulationGame
{

    struct Field
    {
        public Field(Inhabitant inhabitant)
        {
            Inhabitant = inhabitant;
            Localisation = inhabitant.GetLocalisation();
        }
        public Field(Point localisation)
        {
            Inhabitant = null;
            Localisation = localisation;
        }
        public Point Localisation;
        public Inhabitant? Inhabitant;
    }
    enum OrganismTypes      //ByInitiative
    {
        Unknown = 0,
        Dandelion,
    }
    internal class World : IInitiationHandler        //singleton
    {
        //po zadeklarowaniu odpowiedniej ilości
        //pochodnych LifeForm, zaimplementuj je
        //w formie pyłku
        private World() { }
        private static World? inst = null;
        private List<Inhabitant> inhabitants = new();
        //world propeties:
        private const int volume = 5;
        private const string emptyFieldRepresentation = "   ";
        private const string gapBetweenFields = "\t";
        private const ConsoleColor backgroundColor = ConsoleColor.Black;
        private const ConsoleColor fieldColor = ConsoleColor.DarkGray;
        private const ConsoleColor pencilColor = ConsoleColor.Gray;
        //------------------public---------------------
        static public World GetInstance()
        {
            if (inst == null) { inst = new World(); }
            return inst;
        }
        public int GetVolume() { return volume; }
        public void TakeATurn()
        {
            callActions();
            print();
        }
        public int GetNumberOfFreeSpaces()
        {
            int freeSpaces = volume * volume;
            return freeSpaces - inst.inhabitants.Count();
        }
        public Field GetField(Point localisation)
        {
            if (inhabitants.Count() == 0) return new Field(localisation);
            else
            {
                Inhabitant[] arr = inhabitants.ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].GetX() == localisation.X && arr[i].GetY() == localisation.Y) return new Field(arr[i]);
                }
            }
            return new Field(localisation);
        }
        public void AddInhabitant(Inhabitant inhabitant)
        {
            inhabitants.Add(inhabitant);
            //sort by initiative
        }
        public void Reset(bool safety = false)
        {
            if (safety)
            {
                inhabitants = new();
            }
        }
        //-------------------private---------------------
        private void print()
        {
            Console.Clear();
            List<Inhabitant> inhabQueue = getInhabPrintQueue();
            foreach (Inhabitant dand in inhabQueue)
            {
                Console.WriteLine(dand.GetSpieces() + ":   x:" + dand.GetX() + ",   y:" + dand.GetY() + "\n");
            }
            int numbOfInhabPrinted = 0;
            for (int y = 0; y < volume; y++)
            {
                for (int x = 0; x < volume; x++)
                {
                    if (inhabQueue.Count == numbOfInhabPrinted)
                    {
                        printEmptyField();
                    }
                    else if (inhabQueue[numbOfInhabPrinted].GetY() == y && inhabQueue[numbOfInhabPrinted].GetX() == x)
                    {
                        printInhabRepresentation(inhabQueue[numbOfInhabPrinted]);
                        printGapBetweenFields();
                        numbOfInhabPrinted++;
                    }
                    else { printEmptyField(); }
                }

                /*
                foreach (Inhabitant dand in inhabQueue)
                {
                    System.Console.WriteLine(dand.GetSpieces() + ": " + dand.GetX() + ", " + dand.GetY() + "\n");
                }
                */

                Console.BackgroundColor = backgroundColor;
                Console.WriteLine("\n");
            }
        }
        private void printEmptyField()
        {
            Console.BackgroundColor = fieldColor;
            Console.Write(emptyFieldRepresentation);
            printGapBetweenFields();
        }
        private void printGapBetweenFields()
        {
            Console.BackgroundColor = backgroundColor;
            Console.Write(gapBetweenFields);
        }
        private void printInhabRepresentation(Inhabitant inhabitant)
        {
            inhabitant.Print();
            Console.Write(" ");
        }
        private List<Inhabitant> getInhabPrintQueue()
        {
            List<Inhabitant> resultQ = new List<Inhabitant>(inhabitants);
            resultQ.Sort(CompareInhabByX);
            resultQ.Sort(CompareInhabByY);
            return resultQ;
        }

        private void callActions()
        {
            foreach (Inhabitant inhabitant in inhabitants)
            {
                inhabitant.Action();
            }
        }
        private static int CompareInhabByX(Inhabitant x, Inhabitant y)
        {
            return x.GetX() - y.GetX();
        }
        private static int CompareInhabByY(Inhabitant x, Inhabitant y)
        {
            return x.GetY() - y.GetY();
        }
    }
}
