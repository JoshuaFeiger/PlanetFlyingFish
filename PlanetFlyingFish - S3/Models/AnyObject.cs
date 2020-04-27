using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetFlyingFish.Models
{
    public class AnyObject : AnyNoun
    {
        public override void Battle(Player player)
        {
            //something something "can't fight"
        }

        public override void DefaultInteract()
        {
            //Something something "No problem here"
        }
    }
}
