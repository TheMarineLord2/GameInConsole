﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame.Game.NPCs
{
    internal class Sheep : Animal
    {
        protected override void overrideSpiecesData()
        {
            _visualRepr = " $ ";
            _strength = 2;
            _isAlive = true;
            _myType = this;
        }
    }
}
