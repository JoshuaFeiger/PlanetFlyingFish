using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace PlanetFlyingFish.Models
{
    /// <summary>
    /// The GameActionGroup class is a class that contains a list of GameActions, with some extra data as as well.
    /// </summary>
    public class GameActionGroup
    {
        private string _storyTriggerID = "Default";

        private List<GameAction> _actionsToPerform;

        private List<string> _storyTriggersToClear;

        private List<string> _storyTriggersToAdd;


        public string StoryTriggerID
        {
            get { return _storyTriggerID; }
            set { _storyTriggerID = value; }
        }

        public List<GameAction> ActionsToPerform
        {
            get { return _actionsToPerform; }
            set { _actionsToPerform = value; }
        }

        public List<string> StoryTriggersToClear
        {
            get { return _storyTriggersToClear; }
            set { _storyTriggersToClear = value; }
        }

        public List<string> StoryTriggersToAdd
        {
            get { return _storyTriggersToAdd; }
            set { _storyTriggersToAdd = value; }
        }
    }
}
