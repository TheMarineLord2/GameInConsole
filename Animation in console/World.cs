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
    enum OrganismTypes      //ByInitiative
    {
        Unknown = 0,
        Dandelion,
        Sheep,
    }
    struct Field
    {
        public Field(Entity inhabitant)
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
        public Entity? Inhabitant;
    }
    internal class World : IInitiationHandler        //singleton
    {
        //po zadeklarowaniu odpowiedniej ilości
        //pochodnych LifeForm, zaimplementuj je
        //w formie pyłku
        private World() { }
        private static World? inst = null;
        private List<Entity> entities = new();
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
            return freeSpaces - inst.entities.Count();
        }
        public Field GetField(Point localisation)
        {
            if (entities.Count() == 0) return new Field(localisation);
            else
            {
                Entity[] arr = entities.ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].GetX() == localisation.X && arr[i].GetY() == localisation.Y) return new Field(arr[i]);
                }
            }
            return new Field(localisation);
        }
        public void AddEntity(Organism organism)
        {
            entities.Add(organism);
            // add -> sort by initiative
        }
        public void Reset(bool safety = false)
        {
            if (safety)
            {
                entities = new();
            }
        }
        //-------------------private---------------------
        private void print()
        {
            Console.Clear();
            List<Entity> entityQueue = getEntitiesPrintQueue();
            int numbOfEntitiesPrinted = 0;
            for (int y = 0; y < volume; y++)
            {
                for (int x = 0; x < volume; x++)
                {
                    if (entityQueue.Count == numbOfEntitiesPrinted)
                    {
                        printEmptyField();
                    }
                    else if (entityQueue[numbOfEntitiesPrinted].GetY() == y && entityQueue[numbOfEntitiesPrinted].GetX() == x)
                    {
                        printEntityRepresentation(entityQueue[numbOfEntitiesPrinted]);
                        printGapBetweenFields();
                        numbOfEntitiesPrinted++;
                    }
                    else { printEmptyField(); }
                }
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
        private void printEntityRepresentation(Entity entity)
        {
            entity.Print();
            Console.Write(" ");
        }
        private List<Entity> getEntitiesPrintQueue()
        {
            List<Entity> resultQ = new List<Entity>(entities);
            resultQ.Sort(CompareEntitiesByX);
            resultQ.Sort(CompareEntitiesByY);
            return resultQ;
        }

        private void callActions()
        {
            foreach (Organism organism in entities)
            {
                organism.Action();
            }
        }
        private static int CompareEntitiesByX(Entity x, Entity y)
        {
            return x.GetX() - y.GetX();
        }
        private static int CompareEntitiesByY(Entity x, Entity y)
        {
            return x.GetY() - y.GetY();
        }
    }
}
