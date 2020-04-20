using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _priorityMessages = new List<string>
            {
                "Hello.",
                "These are the default priority messages.",
                "Method()"
            };
            _sideMessages = new List<string>
            {
                "Hello.",
                "These are the default side messages.",
                "Method ()"
            };
            _centralImagePath = "/PlanetFlyingFish;component/Resource/Images/MapTest.png";
            _locationInfo = "Hello, person. We are at a place.";
            _centralImageArtChosen = "Map";
            MapArtName = "Map01";
            UpdatePriorityMessages();
            UpdateSideMessages();
        }

		public GameMainViewModel(Player player, List<string> initialMessages)
		{
			_playerOne = player;
			_priorityMessages = initialMessages;
            _sideMessages = new List<string>
            {
                "Hello.",
                "These are the default side messages.",
                "Method (player, initialPriorityMessages)"
            };
            _centralImagePath = "/PlanetFlyingFish;component/Resource/Images/MapTest.png";
            _locationInfo = "Hello, person. We are at a place.";
            _centralImageArtChosen = "Map";
            MapArtName = "Map01";
            UpdatePriorityMessages();
            UpdateSideMessages();
        }

        public GameMainViewModel(Player player, List<string> initialPriorityMessages, List<string> initialSideMessages)
        {
            _playerOne = player;
            _priorityMessages = initialPriorityMessages;
            _sideMessages = initialSideMessages;
            _centralImagePath = "/PlanetFlyingFish;component/Resource/Images/MapTest.png";
            _locationInfo = "Hello, person. We are at a place.";
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
                    //todo: add in actual art name here
                    CentralImageDisplayPath = $"\\Resource\\Images\\SurfaceRoomArt01.png";
                    break;
                default:
                    throw new Exception("No valid option was sent.");
                    //break;
            }
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

        private List<string> _priorityMessages;

        private List<string> _sideMessages;

        private string _centralImagePath;

        private string _locationInfo;

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
			set { _playerOne = value; }
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
            set
            {
                _priorityMessageDisplay = MessageDisplayAsString(_priorityMessages);
                OnPropertyChanged(nameof(PriorityMessageDisplay));
            }
            get { return _priorityMessageDisplay; }
        }

        public string SideMessageDisplay
        {
            set
            {
                _sideMessageDisplay = MessageDisplayAsString(_sideMessages);
                OnPropertyChanged(nameof(SideMessageDisplay)); 
            }
            get { return _sideMessageDisplay; }
        }

        public string PlayerInfoDisplay
        {
            get {
                return $"{PlayerOne.Name} \n" +
              $"ID: {PlayerOne.ID} \n" +
              $"{PlayerOne.HealthPoints} HP";
            }
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
            get { return _locationInfo; }
            set { _locationInfo = value; }
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

        #endregion
        //----------------------------------------------
    }
}
