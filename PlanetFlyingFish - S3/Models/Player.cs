using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PlanetFlyingFish.Models
{
    public class Player : Character
    {
        public void Die()
        {
            DeathCount++;
        }

        private int _deathCount;

        protected string _areaID;

        private ObservableCollection<AnyNoun> _itemInventory;

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

        public ObservableCollection<AnyNoun> ItemInventory
        {
            get { return _itemInventory; }
            set { _itemInventory = value; }
        }
    }
}
