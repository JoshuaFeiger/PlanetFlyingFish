using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetFlyingFish.Models
{
    public abstract class AnyNoun : ObservableObject
    {
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

        private bool _viewInfoRestricted;

        protected bool _canBeWeapon;

        protected string _description;
        
        protected List<GameActionGroup> _actionsOnInteract;

        protected string _useMessage;

        protected List<GameActionGroup> _actionsOnUse;

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
                if (!((_healthPoints + value) < MaxHP) || (MaxHP < 1))
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
            get {
                if (_healthPoints > _maxHP)
                {
                    _maxHP = _healthPoints;
                }
                return _maxHP; }
            set { _maxHP = value; }
        }

        public bool ViewInfoRestricted
        {
            get { return _viewInfoRestricted; }
            set { _viewInfoRestricted = value; }
        }

        public bool CanPickUp
        {
            get { return _canPickUp; }
            set { _canPickUp = value; }
        }

        public bool CanBeWeapon
        {
            get { return _canBeWeapon; }
            set { _canBeWeapon = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public List<GameActionGroup> ActionsOnInteract
        {
            get { return _actionsOnInteract; }
            set { _actionsOnInteract = value; }
        }

        public string UseMessage
        {
            get { return _useMessage; }
            set { _useMessage = value; }
        }

        public List<GameActionGroup> ActionsOnUse
        {
            get { return _actionsOnUse; }
            set {_actionsOnUse = value; }
        }
    }
}
