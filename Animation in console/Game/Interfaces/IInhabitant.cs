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
    internal enum BattleResults
    {
        WIN,
        DRAW,    
        LOSS,
    }
    internal interface IInhabitant
    {
        Point GetLocalisation();
        void Die();
        int GetInitiative();
        int GetStrength();
        void TakeTurn();
        void Print();
        bool IsAlive();
    }
}
