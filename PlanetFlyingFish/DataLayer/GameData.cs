using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetFlyingFish.Models;

namespace PlanetFlyingFish.DataLayer
{
    public class GameData
    {
        public static List<string> InitialPriorityMessages()
        {
            return new List<string>
            {
                "Hello.",
                "These are the initial priority messages.",
                "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
            };
        }

        public static List<string> InitialSideMessages()
        {
            return new List<string>
            {
                "Hello.",
                "These are the initial side-messages."
            };
        }

        public static Player PlayerData()
        {
            return DefaultPlayerData();
        }

        public static Player DefaultPlayerData()
        {
            return new Player()
            {
                //inital player properties
                ID = 6384,
                Name = "Unit #6384",
                LocationID = "SurfaceRoom001",
                HealthPoints = 100,
            };
        }
    }
}
