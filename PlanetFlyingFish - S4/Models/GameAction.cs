using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanetFlyingFish.Models
{
	/// <summary>
	/// GameAction is an abstract class, and several 
	/// </summary>
    public abstract class GameAction
    {
		protected string _textForAction;

		protected bool _expireOnCompleted;

		public bool ExpireOnCompleted
		{
			get { return _expireOnCompleted; }
			set { _expireOnCompleted = value; }
		}

		public string TextForAction
		{
			get { return _textForAction; }
			set { _textForAction = value; }
		}

		public class ItemGet : GameAction
		{
			public ItemGet(AnyNoun item)
			{
				_itemsToGive = new List<AnyNoun>(){ item };
				_textForAction = $"You got: {item.Name}!";
			}

			public ItemGet(AnyNoun item, string text)
			{
				_itemsToGive[0] = item;
				_textForAction = text;
			}

			public ItemGet(List<AnyNoun> items)
			{
				_itemsToGive = items;
				string text = "";
				foreach (AnyNoun item in items)
				{
					text = text + $"You got: {item.Name}!";
					if (!(item == items[items.Count - 1]))
					{
						text = text + "\n";
					}
				}
				_textForAction = text;
			}

			public ItemGet(List<AnyNoun> items, string text, bool justSendTheDefaultMessages = false)
			{
				_itemsToGive = items;
				_textForAction = text;
				if (justSendTheDefaultMessages)
				{
					foreach (AnyNoun item in items)
					{
						text = text + $"You got: {item.Name}!";
						if (!(item == items[items.Count - 1]))
						{
							text = text + "\n";
						}
					}
				}
			}

			private List<AnyNoun> _itemsToGive;

			public List<AnyNoun> ItemsToGive
			{
				get { return _itemsToGive; }
				set { _itemsToGive = value; }
			}
		}

		public class Battle : GameAction
		{

			public Battle(string callerName)
			{
				_callerName = callerName;
				_textForAction = $"You have defeated {callerName}!";
			}

			private string _callerName;

			public string CallerName
			{
				get { return _callerName; }
				set { _callerName = value; }
			}

		}

		public class BattleNewEnemies : GameAction
		{
			private List<AnyNoun> _enemiesToFight;

			public List<AnyNoun> EnemiesToFight
			{
				get { return _enemiesToFight; }
				set { _enemiesToFight = value; }
			}
		}

		public class KillPlayer : GameAction
		{
			public KillPlayer()
			{
				_textForAction = "You have died.";
			}
		}

		public class HPUp : GameAction
		{
			public HPUp(int amount)
			{
				_hpUpLevel = amount;
				_textForAction = $"You recovered {amount} HP!";
			}

			public HPUp(int amount, string textForAction, bool includeDefaultText = false)
			{
				_hpUpLevel = amount;
				_textForAction = textForAction;
				if (includeDefaultText)
				{
					_textForAction = _textForAction + $"\nYou recovered {amount} HP!";
				}
			}

			private int _hpUpLevel;

			public int HPUpLevel
			{
				get { return _hpUpLevel; }
				set { _hpUpLevel = value; }
			}
		}

		public class MaxHPUp : GameAction
		{
			public MaxHPUp(int amount)
			{
				_maxHPUpLevel = amount;
				_textForAction = $"Your max HP increased by {amount}!";
			}

			public MaxHPUp(int amount, string textForAction, bool includeDefaultText = false)
			{
				_maxHPUpLevel = amount;
				_textForAction = textForAction;
				if (includeDefaultText)
				{
					_textForAction = _textForAction + $"\nYour max HP increased by {amount}!";
				}
			}

			private int _maxHPUpLevel;

			public int MaxHPUpLevel
			{
				get { return _maxHPUpLevel; }
				set { _maxHPUpLevel = value; }
			}
		}

		public class ForceTravel : GameAction
		{
			public ForceTravel(string areaID)
			{
				_areaID = areaID;
			}

			public ForceTravel(string areaID, bool ignoreErrors)
			{
				_areaID = areaID;
				_ignoreErrors = ignoreErrors;
			}

			private string _areaID;

			public string AreaID
			{
				get { return _areaID; }
				set { _areaID = value; }
			}

			private bool _ignoreErrors;

			public bool IgnoreErrors
			{
				get { return _ignoreErrors; }
				set { _ignoreErrors = value; }
			}

		}

		public class ForceWarp : GameAction
		{
			public ForceWarp(string areaID)
			{
				_areaID = areaID;
			}

			public ForceWarp(string areaID, bool ignoreErrors)
			{
				_areaID = areaID;
				_ignoreErrors = ignoreErrors;
			}

			private string _areaID;

			public string AreaID
			{
				get { return _areaID; }
				set { _areaID = value; }
			}

			private bool _ignoreErrors;

			public bool IgnoreErrors
			{
				get { return _ignoreErrors; }
				set { _ignoreErrors = value; }
			}
		}

		public class TriggerStoryEvent : GameAction
        {
            public TriggerStoryEvent(string storyEventName)
            {
                _storyEventName = storyEventName;
            }

            private string _storyEventName;

            public string StoryEventName
            {
                get { return _storyEventName; }
                set { _storyEventName = value; }
            }
        }

		public class PerformActionsIf : GameAction
		{
			private (bool onTrue, bool onFalse) _closeOnTrueOrFalse;

			private bool _answer;

			private GameActionGroup _actionsOnTrue;

			private GameActionGroup _actionsOnFalse;

			/// <summary>
			/// This field/property allows the GameAction to delete itself only when the boolean is true, or only when it is false, or neither.
			/// </summary>
			public (bool onTrue, bool onFalse) CloseOnTrueOrFalse
			{
				get { return _closeOnTrueOrFalse; }
				set { _closeOnTrueOrFalse = value; }
			}

			public bool Answer
			{
				get { return _answer; }
				set { _answer = value; }
			}

			public GameActionGroup ActionsOnTrue
			{
				get { return _actionsOnTrue; }
				set { _actionsOnTrue = value; }
			}

			public GameActionGroup ActionsOnFalse
			{
				get { return _actionsOnFalse; }
				set { _actionsOnFalse = value; }
			}

			public GameActionGroup ActionsToTake
			{
				get
				{
					GameActionGroup actionsToTake = new GameActionGroup();
					if (Answer == true)
					{
						if (!(_actionsOnTrue == null))
						{
							actionsToTake = _actionsOnTrue;
						}
					}
					else
					{
						if (!(_actionsOnFalse == null))
						{
							actionsToTake = _actionsOnFalse;
						}
					}
					return actionsToTake;
				}
			}

			public class DiceRollSucceed : PerformActionsIf
			{
				public DiceRollSucceed()
				{
					_ints = new List<int> { 1, 2, 3 };
				}

				public DiceRollSucceed(List<int> ints)
				{
					_ints = ints;
				}

				private List<int> _ints;

				public List<int> Ints
				{
					get { return _ints; }
					set { _ints = value; }
				}

			}

			public class PlayerOneHasItem : PerformActionsIf
			{
				public PlayerOneHasItem(int itemID)
				{
					_itemID = itemID;
				}

				private int _itemID;

				public int ItemID
				{
					get { return _itemID; }
					set { _itemID = value; }
				}
			}
		}

		public class AddOrRemoveStoryTriggers : GameAction
		{
			public enum Option
			{
				Add,
				Remove
			}

			private List<string> _storyTriggers;

			private Option _addOrRemove;

			public Option AddOrRemove
			{
				get { return _addOrRemove; }
				set { _addOrRemove = value; }
			}

			public List<string> StoryTriggers
			{
				get { return _storyTriggers; }
				set { _storyTriggers = value; }
			}

			public AddOrRemoveStoryTriggers(Option addOrRemove, List<string> storyTriggers)
			{
				_addOrRemove = addOrRemove;
				_storyTriggers = storyTriggers;
			}
		}

		public class Trade : GameAction
		{
			public Trade(int playerTakes, int characterTakes)
			{
				_playerTakes = new List<int> { playerTakes };
				_characterTakes = new List<int> { characterTakes };
			}

			public Trade(List<int> playerTakes, List<int> characterTakes)
			{
				_playerTakes = playerTakes;
				_characterTakes = characterTakes;
			}

			private List<int> _playerTakes;

			public List<int> PlayerTakes
			{
				get { return _playerTakes; }
				set { _playerTakes = value; }
			}

			private List<int> _characterTakes;

			public List<int> CharacterTakes
			{
				get { return _characterTakes; }
				set { _characterTakes = value; }
			}

		}

		public class AddToPriorityMessages : GameAction
		{
			private string _textToAdd;

			public string TextToAdd
			{
				get { return _textToAdd; }
				set { _textToAdd = value; }
			}

			private bool _addLines;

			public bool AddLines
			{
				get { return _addLines; }
				set { _addLines = value; }
			}

		}

		public class AddToSideMessages : GameAction
		{
			private string _textToAdd;

			public string TextToAdd
			{
				get { return _textToAdd; }
				set { _textToAdd = value; }
			}

			private bool _addLines = true;

			public bool AddLines
			{
				get { return _addLines; }
				set { _addLines = value; }
			}

		}

		public class NoAction : GameAction
		{

		}

		//Dialog objects are also Actions.
	}
}
