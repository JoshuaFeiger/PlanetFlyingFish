using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetFlyingFish.Models
{
    public class Area
    {
		private List<AnyNoun> _objects;

		private int _timesVisited;

		public int TimesVisited
		{
			get { return _timesVisited; }
			set { _timesVisited = value; }
		}


		public List<AnyNoun> Objects
		{
			get { return _objects; }
			set { _objects = value; }
		}

	}
}
