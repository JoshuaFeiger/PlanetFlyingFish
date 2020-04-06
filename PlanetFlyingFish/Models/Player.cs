using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetFlyingFish.Models
{
    public class Player : Character
    {
        private int _deathCount;

        public int DeathCount
        {
            get { return _deathCount; }
            set { _deathCount = value; }
        }

        public void Die()
        {
            _deathCount++;
        }

    }
}
