using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetFlyingFish.Models
{
    public class Area
    {
		private List<AnyNoun> _nouns;

		private List<string> _connectedAreas;

		private int _timesVisited;

		private string _artName;

		private string _locationInfo;

		private string _areaName;

		private string _areaID;


		/// <summary>
		/// A list of the "Nouns" in this Area. Any sort of object goes here, but so do NPCs and...
		/// any noun that's contained in this Area.
		/// </summary>
		public List<AnyNoun> Nouns
		{
			get { return _nouns; }
			set { _nouns = value; }
		}

		/// <summary>
		/// A list of areas that are open for travel from this area,
		/// </summary>
		public List<string> ConnectedAreas
		{
			get { return _connectedAreas; }
			set { _connectedAreas = value; }
		}

		/// <summary>
		/// Pretty self-explanatory. The number of times this place has been visited by a Player object.
		/// </summary>
		public int TimesVisited
		{
			get { return _timesVisited; }
			set { _timesVisited = value; }
		}

		/// <summary>
		/// The filename of the art used for this map.
		/// </summary>
		public string ArtName
		{
			get { return _artName; }
			set { _artName = value; }
		}

		/// <summary>
		/// Text that describes the Area. Currently used in the GameView.
		/// </summary>
		public string LocationInfo
		{
			get { return _locationInfo; }
			set { _locationInfo = value; }
		}

		/// <summary>
		/// The Area's name, as displayed in the GameView.
		/// </summary>
		public string AreaName
		{
			get { return _areaName; }
			set { _areaName = value; }
		}

		/// <summary>
		/// The Area's name, when referenced by parts of the program.
		/// </summary>
		public string AreaID
		{
			get { return _areaID; }
			set { _areaID = value; }
		}
	}
}
