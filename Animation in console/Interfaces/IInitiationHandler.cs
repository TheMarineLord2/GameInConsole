﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimulationGame.NPCs;

namespace SimulationGame.Interfaces
{
    internal class IInitiationHandler    //works as Visitor
    {
        static public void PlaceInWorld(Entity entity)
        {
            Console.WriteLine("IInitiationHandler: Unexpected entity appeared.\n");
        }
        static public void PlaceInWorld(Dandelion dandelion)
        {
            //dandelion should inherit constructor from Inhabitant
            if (IFieldNavigation.CheckIfLegal(dandelion.GetLocalisation())) { World.GetInstance().AddEntity(dandelion); }
            else Console.WriteLine("IInitiationHandler: New Dandelions localisation is illegal.");
        }
    }
}
