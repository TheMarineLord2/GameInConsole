using SimulationGame.Game.Interfaces;
using SimulationGame.Game.NPCs;
using System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame.Game.Handlers
{
    internal class InhabitantMovementHandler
    {
        // initiating Inhabitants
        public static void callIInitiationHandler<T>(ref T mob) where T : IInhabitant
        {
            if (CheckIfLegal(mob.GetLocalisation())) { World.This().AddInhabitant(mob); }
            else Console.WriteLine("IInitiationHandler: New " + mob.GetType() + " _localisation is illegal.");
            Console.WriteLine(mob.GetLocalisation());
        }

        public static bool isAnyPlaceAvaiable()
        {
            if (World.This().GetNumberOfFreeSpaces()== 0)
            {
                Console.WriteLine("inhabitant: No free space is aviable \n" +
                "Returning illegal place.\n");
                return false;
            }
            else return true;
        }

        public static Point GetAnyRandomPlace()
        {
            if (isAnyPlaceAvaiable())
            {
                int rand = new Random().Next(0, World.This().GetVolume() * World.This().GetVolume());
                int row = rand / World.This().GetVolume();
                int collumn = rand % World.This().GetVolume();

                //go through all possibilities, starting from random point
                for (int i = 0; i < World.This().GetVolume(); i++)
                {
                    //changing row value once for all
                    if (i + row >= World.This().GetVolume()) { row -= World.This().GetVolume(); }
                    for (int j = 0; j < World.This().GetVolume(); j++)
                    {
                        //changing collumn value once for all
                        if (j + collumn >= World.This().GetVolume()) { collumn -= World.This().GetVolume(); }
                        if (World.This().GetField(new(row + i, collumn + j)).inhabitant == null) return new(row + i, collumn + j);
                    }
                }
            }

            return new(-1, -1);
        }

        // technically possible fields with their propeties
        public static List<Field> GetArrayOfViableDestinations(Point home, int range = 1)
        {
            int viablePlaces = 0;
            List<Field> result = new();
            for (int i = 0; i < range * 2 + 1; i++)
            {
                for (int j = 0; j < range * 2 + 1; j++)
                {
                    if (CheckIfLegal(new Point(i, j)))
                    {
                        result.Add(World.This().GetField(new Point(i, j)));
                        viablePlaces++;
                    }
                }
            }
            return result;
        }

        static bool CheckIfLegal(Point questioned)
        {
            if (questioned.X >= 0 && questioned.X < World.This().GetVolume() && 0 <= questioned.Y && questioned.Y < World.This().GetVolume()) { return true; }
            return false;
        }

        static void Move<T>(ref T mob, Field destination) where T:IInhabitant {
            if(destination.inhabitant == null) { mob.SetLocalisation(destination.localisation); }
            else
            {
                InhabitantInteractionHandler.Interact(mob, destination.inhabitant);
            }
        }
    }
}