using SimulationGame.Game.NPCs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SimulationGame.Game.Interfaces
{
    internal interface INonPlayerCharacter
    {
        Point GetLocalisation();
        int GetInitiative();
        void TakeTurn();
        void Print();
        Inhabitant? getMyType();
    }
}
