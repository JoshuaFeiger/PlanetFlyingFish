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
        }

        public GameMainViewModel(Player player, List<string> initialPriorityMessages, List<string> initialSideMessages)
        {
            _playerOne = player;
            _priorityMessages = initialPriorityMessages;
            _sideMessages = initialSideMessages;
            _centralImagePath = "/PlanetFlyingFish;component/Resource/Images/MapTest.png";
            _locationInfo = "Hello, person. We are at a place.";
        }

        private Player _playerOne;

        private List<string> _priorityMessages;

        private List<string> _sideMessages;

        private string _centralImagePath;

        private string _locationInfo;

        public Player PlayerOne
		{
			get { return _playerOne; }
			set { _playerOne = value; }
		}

        public List<string> PriorityMessaages
        {
            get { return _priorityMessages; }
            set { _priorityMessages = value; }
        }

        public List<string> SideMessages
        {
            get { return _sideMessages; }
            set 
            { 
                _sideMessages = value;
                OnPropertyChanged(nameof(SideMessageDisplay));
            }
        }

        /// <summary>
        /// return the list of strings as a single string, adding two new lines between each entry
        /// </summary>
        public string PriorityMessageDisplay
		{
            get { return MessageDisplayAsString(_priorityMessages); }
		}

        public string SideMessageDisplay
        {
            get { return MessageDisplayAsString(_sideMessages); }
        }

        private string MessageDisplayAsString(List<string> messages)
        {
            return string.Join("\n\n", messages);
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
            set { _centralImagePath = value; }
        }

        public string LocationInfo
        {
            get { return _locationInfo; }
            set { _locationInfo = value; }
        }
    }
}
