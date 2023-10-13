﻿using SimulationGame.Game.NPCs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame.Game.Interfaces
{
    internal interface IInhabitant
    {
        Point GetLocalisation();
        void SetLocalisation(Point localisation);
        int GetInitiative();
        void TakeTurn();
        void Print();
        public void Die();  //World.Inhabitants World.NewBornBuffor
        public void MoveBack();
        public void TakeThisField();
    }
}