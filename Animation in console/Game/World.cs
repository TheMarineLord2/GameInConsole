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
        Sheep,
    }
    internal class World
    {
        private World() { }
        private static World? inst = null;
        private List<INonPlayerCharacter> inhabitants = new();
        private List<INonPlayerCharacter> newBornList = new();
        private int turnNumber = 0;
        // world propeties:
        private const int volume = 7;
        private const string emptyFieldRepresentation = "   ";
        private const string gapBetweenFields = "\t";
        private const ConsoleColor backgroundColor = ConsoleColor.Black;
        private const ConsoleColor fieldColor = ConsoleColor.DarkGray;
        private const ConsoleColor pencilColor = ConsoleColor.Gray;
        // ------------------ public ---------------------
        static public World GetInstance()
        {
            if (inst == null) { inst = new World(); }
            return inst;
        }
        public int GetVolume() { return volume; }
        public void TakeATurn()
        {
            sortOutInhabitants();
            turnNumber++;
            print();
            callActions();
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
                    if (arr[i].GetLocalisation().X == localisation.X && arr[i].GetLocalisation().Y == localisation.Y) return new Field(arr[i]);
                }
            }
            return new Field(localisation);
        }
        public void AddInhabitant(INonPlayerCharacter mob)
        {
            // sort by initiative
            newBornList.Add(mob);
        }
        public void Reset(bool safety = false)
        {
            if (safety)
            {
                inhabitants = new();
            }
        }
        // ------------------- private ---------------------
        private void print()
        {
            Console.Clear();        //clears only current screen
            Console.WriteLine("Turn: " + turnNumber);
            Console.WriteLine("Number of inhabitants: " + inhabitants.Count());
            List<Inhabitant> inhabQueue = getInhabPrintQueue();
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
        private List<INonPlayerCharacter> getInhabPrintQueue()
        {
            List<INonPlayerCharacter> resultQ = new List<INonPlayerCharacter>(inhabitants);
            resultQ.Sort(CompareInhabByX);
            resultQ.Sort(CompareInhabByY);
            return resultQ;
        }
        private void sortOutInhabitants() {
            foreach(INonPlayerCharacter mob in newBornList)
            {
                inhabitants.Add(mob);
                Console.WriteLine("Inhabitant " + mob.GetLocalisation() + " added to newBornList");
            }
            newBornList = new();
            inhabitants = getInhabPrintQueue();
        }

        private void callActions()
        {
            foreach (Inhabitant inhabitant in inhabitants)
            {
                Console.WriteLine("Taking turn of " + inhabitant.ToString());
                inhabitant.TakeTurn();
            }
        }
        private static int CompareInhabByX(INonPlayerCharacter x, INonPlayerCharacter y)
        {
            return x.GetLocalisation().X - y.GetLocalisation().X;
        }
        private static int CompareInhabByY(INonPlayerCharacter x, INonPlayerCharacter y)
        {
            return x.GetLocalisation().Y - y.GetLocalisation().Y;
        }
    }
}
