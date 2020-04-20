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
                "And here is a very long message so that you can feel less lonely in this bare-bones first test of the engine we're using to make this subpar game. Thank you for trying it, by the way. And thank you for reading this message. It means a lot to me that someone would do something like that, and that someone would read this message, especially considering that you have no idea when I'm going to stop talking.",
                "",
                "...Are you actually interested in this text, or do you just want to see where it ends? Are you paying attention to me at all? Is anyone? If no one is, is there a reason I should keep persevering at the things I do? Should I do them just to say I did them? Should I do them because they're something I always dreamed to do? If that's such a good reason, why does my dream pursuit always seem to go so poorly...",
                "I don't know anymore... Why should I keep going? If you've scrolled down this far, you're probably just making sure scrolling works right. Well, it does. No need to keep going down. Unless you're trying to find the bottom... \nWait... \nYou're not listening anyway...",
                "*sigh*",
                "Well, I guess it's time to finish writing this and move on... if there's any reason to...",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "",
                "...",
                "...I'm sure I'll find it."
            };
        }

        public static List<string> InitialSideMessages()
        {
            return new List<string>
            {
                "Hello.",
                "These are the initial side-messages.",
                "...",
                "Is there anything more you're expecting?"
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
                AreaID = "SurfRoom001",
                HealthPoints = 100,
            };
        }
    }
}
