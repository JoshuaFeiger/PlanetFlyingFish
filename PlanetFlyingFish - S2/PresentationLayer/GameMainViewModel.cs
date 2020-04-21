using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PlanetFlyingFish.Models;

namespace PlanetFlyingFish.PresentationLayer
{
    public class GameMainViewModel : ObservableObject
    {
        //----------------------------------------------
        #region ----------------------------METHODS

        //              --------------------------------
        #region ----------------------------INSTANTIATION METHODS
        public GameMainViewModel()
		{
            _playerOne = new Player();
            _areas = DefaultAreas();
            _priorityMessages = new List<string>
            {
                "Hello.",
                "These are the default priority messages.",
                "Method ()"
            };
            _sideMessages = new List<string>
            {
                "Hello.",
                "These are the default side messages.",
                "Method ()"
            };
            _centralImagePath = "/PlanetFlyingFish;component/Resource/Images/MapTest.png";
            _centralImageArtChosen = "Map";
            MapArtName = "Map01";
            UpdatePriorityMessages();
            UpdateSideMessages();
        }

		public GameMainViewModel(Player player, List<string> initialPriorityMessages)
		{
			_playerOne = player;
            _areas = DefaultAreas();
            _priorityMessages = initialPriorityMessages;
            _sideMessages = new List<string>
            {
                "Hello.",
                "These are the default side messages.",
                "Method (player, initialPriorityMessages)"
            };
            _centralImagePath = "/PlanetFlyingFish;component/Resource/Images/MapTest.png";
            _centralImageArtChosen = "Map";
            MapArtName = "Map01";
            UpdatePriorityMessages();
            UpdateSideMessages();
        }

        public GameMainViewModel(Player player, List<string> initialPriorityMessages, List<string> initialSideMessages)
        {
            _playerOne = player;
            _areas = DefaultAreas();
            _priorityMessages = initialPriorityMessages;
            _sideMessages = initialSideMessages;
            _centralImagePath = "/PlanetFlyingFish;component/Resource/Images/MapTest.png";
            _centralImageArtChosen = "Map";
            MapArtName = "Map01";
            UpdatePriorityMessages();
            UpdateSideMessages();
        }

        public GameMainViewModel(Player player, List<Area> initialAreas, List<string> initialPriorityMessages, List<string> initialSideMessages)
        {
            _playerOne = player;
            _areas = initialAreas;
            _priorityMessages = initialPriorityMessages;
            _sideMessages = initialSideMessages;
            _centralImagePath = "/PlanetFlyingFish;component/Resource/Images/MapTest.png";
            _centralImageArtChosen = "Map";
            MapArtName = "Map01";
            UpdatePriorityMessages();
            UpdateSideMessages();
        }
        #endregion
        //              --------------------------------

        private string MessageDisplayAsString(List<string> messages)
        {
            return string.Join("\n\n", messages);
        }

        public void CentralImageChanged(string centralImageChoice)
        {
            _centralImageArtChosen = centralImageChoice;
            switch (centralImageChoice)
            {
                case "Map":
                    CentralImageDisplayPath = $"\\Resource\\Images\\{MapArtName}.png";
                    break;
                case "Art":
                    CentralImageDisplayPath = $"\\Resource\\Images\\{PlayerArea(_playerOne).ArtName}.png";
                    break;
                default:
                    throw new Exception("No valid option was sent.");
                    //break;
            }
        }

        /// <summary>
        /// Moves a Noun object from one Area to another.
        /// </summary>
        /// <param name="nounPos">The position of our target item in the Nouns list of movingFrom.</param>
        public void MoveNoun(int nounPos, ref Area movingFrom, ref Area movingTo)
        {
            AnyNoun nounToMove = movingTo.Nouns[nounPos];
        }

        /// <summary>
        /// Searches an Area object for a Noun, and returns its position.
        /// </summary>
        private int FindNounPos(AnyNoun nounToFind, Area areaToSearch)
        {
            return areaToSearch.Nouns.IndexOf(nounToFind);
        }

        private int FindAreaPos(string areaID, List<Area> areasToSearch)
        {
            List<string> listOfAreaIDs = new List<string>();
            foreach (Area area in areasToSearch)
            {
                listOfAreaIDs.Add(area.AreaID);
            }
            return listOfAreaIDs.IndexOf(areaID);
        }

        private Area PlayerArea(Player player)
        {
            return _areas[player.CurrentAreaPos(_areas)];
        }

        public void TravelToArea(int areaIndex)
        {
            if (!(AccessibleAreas[areaIndex] == "ERR: DOESNOTEXIST"))
            {
                try
                {
                    _playerOne.AreaID = PlayerArea(_playerOne).ConnectedAreas[areaIndex];
                    AreaNameDisplay = "this is just to update";
                    LocationInfo = "Yeah, that's all we do.";
                    AccessibleAreas = new List<string> { "Why do I even bother?" };
                    CentralImageChanged(_centralImageArtChosen);
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show($"Error: cannot travel to the area as it is out of range of the list of areas you can travel to. This likely means the interface failed to update.");
                }
            }
            else
            {
                MessageBox.Show($"Error: cannot travel to the area as it does not exist.\nArea ID: {PlayerArea(_playerOne).ConnectedAreas[areaIndex]}");
            }
        }

        private List<Area> DefaultAreas()
        {
            List<Area> defaultAreas = new List<Area>
            {
                new Area
                {
                    AreaID = "SurfRoom001",
                    AreaName = "Surface Room 001",
                    ConnectedAreas = new List<string>{ "SurfRoom002" },
                    LocationInfo = "You woke up in this cold, dark room.",
                    ArtName = "SurfaceRoomArt01"
                },
                new Area
                {
                    AreaID = "SurfRoom002",
                    AreaName = "Surface Room 002",
                    ConnectedAreas = new List<string>{ "SurfRoom003", "SurfRoom001" },
                    LocationInfo = "Some room next to the cold, dark room you woke up in.",
                    ArtName = "SurfaceRoomArt01"
                },
                new Area
                {
                    AreaID = "SurfRoom003",
                    AreaName = "Surface Room 003",
                    ConnectedAreas = new List<string>{ "SurfRoom001", "SurfRoom002", "Quack" },
                    LocationInfo = "A duck-shaped room. (Don't ask why.)",
                    ArtName = "SurfaceRoomArt02"
                }
            };
            return defaultAreas;
        }

        //              --------------------------------
        #region ----------------------------"UPDATE ONPROPERTYCHANGED" METHODS

        public void UpdatePriorityMessages()
        {
            List<string> AListWithVeryLittlePoint = PriorityMessages;
            PriorityMessages = AListWithVeryLittlePoint;
        }

        public void UpdateSideMessages()
        {
            List<string> AListWithVeryLittlePoint = SideMessages;
            SideMessages = AListWithVeryLittlePoint;
        }

        #endregion
        //              --------------------------------

        #endregion
        //----------------------------------------------

        //----------------------------------------------
        #region ----------------------------FIELDS

        private Player _playerOne;

        private List<Area> _areas;

        private List<string> _priorityMessages;

        private List<string> _sideMessages;

        private string _centralImagePath;

        private string _priorityMessageDisplay;

        private string _sideMessageDisplay;

        private string _centralImageArtChosen;

        private string _mapArtName;

        #endregion
        //----------------------------------------------

        //----------------------------------------------
        #region ----------------------------PROPERTIES

        public Player PlayerOne
		{
			get { return _playerOne; }
			set 
            { 
                _playerOne = value;
                OnPropertyChanged(nameof(PlayerOne));
                OnPropertyChanged(nameof(PlayerInfoDisplay));
            }
		}

        public List<Area> Areas
        {
            get { return _areas; }
            set { _areas = value; }
        }

        public List<string> PriorityMessages
        {
            get { return _priorityMessages; }
            set 
            { 
                _priorityMessages = value;
                PriorityMessageDisplay = "This string is pointless.";
            }
        }

        public List<string> SideMessages
        {
            get { return _sideMessages; }
            set 
            { 
                _sideMessages = value;
                SideMessageDisplay = "This string is actually pointless.";
            }
        }


        /// <summary>
        /// return the list of strings as a single string, adding two new lines between each entry
        /// </summary>
        public string PriorityMessageDisplay
		{
            get { return _priorityMessageDisplay; }
            set
            {
                _priorityMessageDisplay = MessageDisplayAsString(_priorityMessages);
                OnPropertyChanged(nameof(PriorityMessageDisplay));
            }
        }

        public string SideMessageDisplay
        {
            get { return _sideMessageDisplay; }
            set
            {
                _sideMessageDisplay = MessageDisplayAsString(_sideMessages);
                OnPropertyChanged(nameof(SideMessageDisplay)); 
            }
        }

        public string PlayerInfoDisplay
        {
            get 
            {
                return $"{PlayerOne.Name} \n" +
              $"ID: {PlayerOne.ID} \n" +
              $"{PlayerOne.HealthPoints} HP";
            }
            set { OnPropertyChanged(nameof(PlayerInfoDisplay)); }
        }

        public string CentralImageDisplayPath
        {
            get { return _centralImagePath; }
            set 
            { 
                _centralImagePath = value;
                OnPropertyChanged(nameof(CentralImageDisplayPath));
            }
        }

        public string LocationInfo
        {
            get 
            {
                string locationInfo = "Error. Could not find info.";
                if (!(FindAreaPos(_playerOne.AreaID, _areas) == -1))
                {
                    locationInfo = Areas[FindAreaPos(_playerOne.AreaID, _areas)].LocationInfo;
                }
                return locationInfo;
            }
            set { OnPropertyChanged(nameof(LocationInfo)); }
        }

        public string MapArtName
        {
            get { return _mapArtName; }
            set 
            { 
                _mapArtName = value;
                CentralImageChanged(_centralImageArtChosen);
            }
        }

        public string AreaNameDisplay
        {
            get 
            {
                string areaName = "Area_Error";
                if (!(_playerOne.CurrentAreaPos(Areas) == -1))
                {
                    areaName = PlayerArea(_playerOne).AreaName;
                }
                return areaName;
            }
            set 
            {
                OnPropertyChanged(nameof(AreaNameDisplay)); 
            }
        }

        public List<string> AccessibleAreas
        {
            get
            {
                List<string> accessibleAreas = new List<string>();
                foreach (string id in PlayerArea(_playerOne).ConnectedAreas)
                {
                    if (!(FindAreaPos(id, Areas) == -1))
                    {
                        accessibleAreas.Add(Areas[FindAreaPos(id, Areas)].AreaName);
                    }
                    else
                    {
                        accessibleAreas.Add("ERR: DOESNOTEXIST");
                    }
                }
                return accessibleAreas;
            }
            set
            {
                OnPropertyChanged(nameof(AccessibleAreas));
            }
        }

        #endregion
        //----------------------------------------------
    }
}
