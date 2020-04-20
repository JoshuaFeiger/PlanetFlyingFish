using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetFlyingFish.Models
{
    public abstract class AnyNoun : ObservableObject
    {
        protected int _id;
        protected string _name;
        protected string _locationID;
        protected int _healthPoints;
        protected bool _canPickUp;

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

        public string LocationID
        {
            get { return _locationID; }
            set { _locationID = value; }
        } 

		public int HealthPoints
		{
			get { return _healthPoints; }
			set { _healthPoints = value; }
		}

        public bool CanPickUp
        {
            get { return _canPickUp; }
            set { _canPickUp = value; }
        }

        public abstract void Battle();

        public virtual void DefaultInteract()
        {
            //Something something "No problem here"
        } 
    }
}
