using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetFlyingFish.Models
{
    public class Player : Character
    {
        public void Die()
        {
            _deathCount++;
        }

        private int _deathCount;

        protected string _areaID;

        public int DeathCount
        {
            get { return _deathCount; }
            set { _deathCount = value; }
        }

        public string AreaID
        {
            get { return _areaID; }
            set { _areaID = value; }
        }

        public int CurrentAreaPos(List<Area> areasToSearch)
        {
            List<string> listOfAreaIDs = new List<string>();
            foreach (Area area in areasToSearch)
            {
                listOfAreaIDs.Add(area.AreaID);
            }
            return listOfAreaIDs.IndexOf(_areaID);
        }
    }
}
