using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetFlyingFish.Models
{
    public abstract class AnyNoun : ObservableObject
    {
        public enum ActionType
        {
            ItemGet,
            Battle,
            BattleNewEnemies,
            KillPlayer,
            HPUp,
            MaxHPUp,
            NoAction
        }

        public abstract void Battle(Player player);

        public virtual void DefaultInteract()
        {
            //Something something "No problem here"
        }

        protected int _id;
        
        protected string _name;
        
        protected int _healthPoints;

        protected int _maxHP;

        protected bool _canPickUp;
        
        protected string _description;
        
        protected List<ActionType> _actionsOnInteract;

        protected List<string> _interactActionsMessages;

        

        protected string _useMessage;

        protected List<ActionType> _actionsOnUse;

        protected List<string> _useActionsMessages;

        


        protected List<AnyNoun> _itemsToGive;

        protected List<AnyNoun> _newEnemiesToBattle;

        protected int _hpUpLevel;

        protected int _maxHPUpLevel;



        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public int HealthPoints
		{
			get { return _healthPoints; }
			set 
            {
                if (!((_healthPoints + value) < MaxHP) || (MaxHP < 1) /*|| (MaxHP == null)*/)
                {
                    _healthPoints = value;
                }
                else
                {
                    _healthPoints = MaxHP;
                }
            }
		}

        public int MaxHP
        {
            get { return _maxHP; }
            set { _maxHP = value; }
        }

        public bool CanPickUp
        {
            get { return _canPickUp; }
            set { _canPickUp = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public List<ActionType> ActionsOnInteract
        {
            get { return _actionsOnInteract; }
            set { _actionsOnInteract = value; }
        }

        public List<string> InteractActionsMessages
        {
            get { return _interactActionsMessages; }
            set { _interactActionsMessages = value; }
        }

        public string UseMessage
        {
            get { return _useMessage; }
            set { _useMessage = value; }
        }

        public List<ActionType> ActionsOnUse
        {
            get { return _actionsOnUse; }
            set { _actionsOnUse = value; }
        }

        public List<string> UseActionsMessages
        {
            get { return _useActionsMessages; }
            set { _useActionsMessages = value; }
        }

        public List<AnyNoun> ItemsToGive
        {
            get { return _itemsToGive; }
            set { _itemsToGive = value; }
        }

        public List<AnyNoun> NewEnemiesToBattle
        {
            get { return _newEnemiesToBattle; }
            set { _newEnemiesToBattle = value; }
        }

        public int HPUpLevel
        {
            get { return _hpUpLevel; }
            set { _hpUpLevel = value; }
        }

        public int MaxHPUpLevel
        {
            get { return _maxHPUpLevel; }
            set { _maxHPUpLevel = value; }
        }
    }
}
