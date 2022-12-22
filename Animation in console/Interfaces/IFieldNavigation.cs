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
        static bool CheckIfLegal(Point questioned)
        {
            if (questioned.X >= 0 && questioned.X < World.GetInstance().GetVolume() && 0 <= questioned.Y && questioned.Y < World.GetInstance().GetVolume()) { return true; }
            return false;
        }
        static Point GetEmptyPointAround(Point localisation, int range = 1)
        {
            List<Field> surroundings = IFieldNavigation.GetArrayOfPlacesAround(localisation, range);
            int rand = new Random().Next(surroundings.Count);
            for(int i=0; i<surroundings.Count; i++)
            {
                if(i+rand >= surroundings.Count)
                {
                    rand -= surroundings.Count;
                }
                if (surroundings[i + rand].Inhabitant == null) { return surroundings[i + rand].Localisation; }
            }
            Console.WriteLine("No empty place around" + localisation.X + ", " + localisation.Y);
            return new(-1, -1);
        }
        static List<Field> GetArrayOfPlacesAround(Point home, int range = 1)        //gets list of places around Point home, without itself.
        {
            List<Field> result = new();
            for (int i = -range; i <= range; i++)
            {
                for (int j =-range; j <= range; j++)
                {
                    if(i!=0 || j!=0)
                    {
                        if (CheckIfLegal(new Point(home.X + i, home.Y + j)))
                        {
                            result.Add(World.GetInstance().GetField(new Point(home.X + i, home.Y + j)));
                        }
                    }
                }
            }
            return result;
        }
        static Point GetRandomPlace()  //returns exception, when no free spaces
        {
            int volume = World.GetInstance().GetVolume();
            Random rand = new Random();
            int row = rand.Next(0, volume);
            int collumn = rand.Next(0, volume);
                
            for (int i = 0; i < volume; i++)
            {
                if (i + row >= volume)
                {   //giving offset:
                    row -= volume;
                }
                for (int j = 0; j < volume; j++)
                {
                    if (j + collumn >= volume)
                    {   //giving offset:
                        collumn -= volume;
                    }
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
