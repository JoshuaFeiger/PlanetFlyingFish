using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PlanetFlyingFish.Models;

namespace PlanetFlyingFish.Models
{
    class NPC : Character
    {
        private int _diceSidesCount = 6;

        public int DiceSidesCount
        {
            get { return _diceSidesCount; }
            set { _diceSidesCount = value; }
        }


        public int DiceRoll()
        {
            Random random = new Random();
            return random.Next(1, 6);
        }

        public bool DiceRollSucceed(List<int> ints)
        {
            return ints.Contains(DiceRoll());
        }
    }
}
