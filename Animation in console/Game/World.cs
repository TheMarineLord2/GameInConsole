using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.Game.Interfaces;
using SimulationGame.Game.NPCs;

namespace SimulationGame.Game
{
    // created to allow passing information about fields with or withoud inhabitants
    struct Field    
    {
        public Field(IInhabitant inhabitant)
        {
            this.inhabitant = inhabitant;
            localisation = inhabitant.GetLocalisation();
        }
        public Field(Point localisation)
        {
            inhabitant = null;
            this.localisation = localisation;
        }
        public Point localisation;
        public IInhabitant? inhabitant;
    }

    enum OrganismTypes      //ByInitiative
    {
        Unknown = 0,
        Dandelion,
        Sheep,
    }

    internal class World
    {
        private World() { }
        private static World? inst = null;
        private List<IInhabitant> inhabitantList = new();
        private List<IInhabitant> newBornInhabitantBuffor = new();
        private int turnNumber = 0;

        // world propeties:
        private const int volume = 7;
        private const string emptyFieldRepresentation = "   ";
        private const string gapBetweenFields = "\t";
        private const ConsoleColor backgroundColor = ConsoleColor.Black;
        private const ConsoleColor fieldColor = ConsoleColor.DarkGray;
        private const ConsoleColor pencilColor = ConsoleColor.Gray;



        // ------------------ public ---------------------
        static public World This()
        {
            if (inst == null) { inst = new World(); }
            return inst;
        }
        
        public int GetVolume() { return volume; }
        
        public void TakeATurn()
        {
            rearrangeInhabitantList();
            turnNumber++;
            print();
            callActions();
        }
        
        public int GetNumberOfFreeSpaces()
        {
            int freeSpaces = volume * volume;
            return freeSpaces - inst.inhabitantList.Count;
        }
        
        public Field GetField(Point localisation)
        {
            if (inhabitantList.Count() == 0) return new Field(localisation);
            else
            {
                IInhabitant[] arr = inhabitantList.ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].GetLocalisation().X == localisation.X && arr[i].GetLocalisation().Y == localisation.Y) return new Field(arr[i]);
                }
            }
            return new Field(localisation);
        }
        
        public void AddInhabitant(IInhabitant mob)
        {
            // sort by _initiative
            newBornInhabitantBuffor.Add(mob);
        }
        
        public void Reset(bool safety = false)
        {
            if (safety)
            {
                inhabitantList = new();
            }
        }


        // ------------------- private ---------------------
        private void print()
        {
            // instead of clearing screen every time, try to just say where on the screen it should be displayed
            Console.Clear();        //clears only current screen
            Console.WriteLine("Turn: " + turnNumber);
            Console.WriteLine("Number of inhabitantList: " + inhabitantList.Count());
            List<IInhabitant> inhabQueue = getInhabPrintQueue();
            int numbOfInhabPrinted = 0;
            for (int y = 0; y < volume; y++)
            {
                for (int x = 0; x < volume; x++)
                {
                    if (inhabQueue.Count == numbOfInhabPrinted)
                    {
                        printEmptyField();
                    }
                    else if (inhabQueue[numbOfInhabPrinted].GetLocalisation().Y == y && inhabQueue[numbOfInhabPrinted].GetLocalisation().X == x)
                    {
                        printInhabRepresentation(inhabQueue[numbOfInhabPrinted]);
                        printGapBetweenFields();
                        numbOfInhabPrinted++;
                    }
                    else { printEmptyField(); }
                }

                /*
                foreach (inhabitant dand in inhabQueue)
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
        
        private void printInhabRepresentation(IInhabitant inhabitant)
        {
            inhabitant.Print();
            Console.Write(" ");
        }
        
        private List<IInhabitant> getInhabPrintQueue()
        {
            List<IInhabitant> resultQ = new List<IInhabitant>(inhabitantList);
            resultQ.Sort(CompareInhabByX);
            resultQ.Sort(CompareInhabByY);
            return resultQ;
        }
        
        private void rearrangeInhabitantList() {
            foreach(IInhabitant mob in newBornInhabitantBuffor)
            {
                inhabitantList.Add(mob);
                Console.WriteLine("inhabitant " + mob.GetLocalisation() + " added to newBornInhabitantBuffor");
            }
            newBornInhabitantBuffor = new();
            inhabitantList = getInhabPrintQueue();
        }

        private void callActions()
        {
            foreach (IInhabitant inhabitant in inhabitantList)
            {
                Console.WriteLine("Taking turn of " + inhabitant.ToString());
                inhabitant.TakeTurn();
            }
        }
        
        private static int CompareInhabByX(IInhabitant x, IInhabitant y)
        {
            return x.GetLocalisation().X - y.GetLocalisation().X;
        }
        
        private static int CompareInhabByY(IInhabitant x, IInhabitant y)
        {
            return x.GetLocalisation().Y - y.GetLocalisation().Y;
        }
    }
}
