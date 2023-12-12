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
        void SetLocalisation(Point localisation);
        int GetInitiative();
        int GetStrength();
        void TakeTurn();
        void Print();
        public void Die();  //World.Inhabitants World.NewBornBuffor
        public void MoveBack();
        public void TakeThisField();
        public BattleResults Attack<T>(T inhabitant) where T : IInhabitant;
        public void Move<T>(ref T mob, Field destination) where T : IInhabitant;
    }
}
