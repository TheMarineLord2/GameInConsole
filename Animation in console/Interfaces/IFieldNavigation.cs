using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame.Interfaces
{
    internal interface IFieldNavigation
    {
        int NumbOfLegalPlacesAround(Point home, int range = 1)
        {
            int viablePlaces = 0;
            for (int i = 0; i < range * 2 + 1; i++)
            {
                for (int j = 0; j < range * 2 + 1; j++)
                {
                    if (CheckIfLegal(new Point(i, j))) { viablePlaces++; }
                }
            }

            return viablePlaces;

        }
        static bool CheckIfLegal(Point questioned)
        {
            if (questioned.X >= 0 && questioned.X < World.GetInstance().GetVolume() && 0 <= questioned.Y && questioned.Y < World.GetInstance().GetVolume()) { return true; }
            return false;
        }
        Field[] GetArrayOfViablePlaces(Point home, int range = 1)
        {
            int viablePlaces = 0;
            List<Field> result = new();
            for (int i = 0; i < range * 2 + 1; i++)
            {
                for (int j = 0; j < range * 2 + 1; j++)
                {
                    if (CheckIfLegal(new Point(i, j)))
                    {
                        result.Add(World.GetInstance().GetField(new Point(i, j)));
                        viablePlaces++;
                    }
                }
            }

            return result.ToArray();
        }
        static Point GetRandomPlace()  //returns exception, when no free spaces
        {
            int rand = new Random().Next(0, World.GetInstance().GetVolume() * World.GetInstance().GetVolume());
            int row = rand / World.GetInstance().GetVolume();
            int collumn = rand % World.GetInstance().GetVolume();

            //go through all possibilities, starting from random point
            for (int i = 0; i < World.GetInstance().GetVolume(); i++)
            {
                //changing row value once for all
                if (i + row >= World.GetInstance().GetVolume()) { row -= World.GetInstance().GetVolume(); }
                for (int j = 0; j < World.GetInstance().GetVolume(); j++)
                {
                    //changing collumn value once for all
                    if (j + collumn >= World.GetInstance().GetVolume()) { collumn -= World.GetInstance().GetVolume(); }
                    if (World.GetInstance().GetField(new(row + i, collumn + j)).Inhabitant == null) return new(row +i, collumn +j);
                }
            }
            Console.WriteLine("IFieldNavigation: No free space is aviable to find GetRandomPlace from \n" +
                "Returning illegal place.\n");
            return new(-1, -1);

        }
        static int GetNumberOfFreeSpaces()
        {
            return World.GetInstance().GetNumberOfFreeSpaces();
        }
    }
}
