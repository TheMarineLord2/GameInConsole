using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animation_in_console
{
    internal class IInitiationHandler    //works as Visitor
    {
        static public void PlaceInWorld(Inhabitant inhabitant)
        {
            System.Console.WriteLine("IInitiationHandler: Unexpected abstract inhabitant appeared.\n");
        }
        static public void PlaceInWorld(Dandelion dandelion)
        {
            //dandelion should inherit constructor from Inhabitant
            if (IFieldNavigation.CheckIfLegal(dandelion.GetLocalisation())) { World.GetInstance().AddInhabitant(dandelion); }
            else System.Console.WriteLine("IInitiationHandler: New Dandelions localisation is illegal.");
        }
    }
}
