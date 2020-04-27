using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PlanetFlyingFish.Models;
using System.Collections.ObjectModel;

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
            //todo: Remove this when the time comes to remove this
            _playerOne.ItemInventory = new System.Collections.ObjectModel.ObservableCollection<AnyNoun>();
            
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
                    OnPropertyChanged(nameof(AreaItems));
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

        public void InvestigateIndexItem(int index, Player player)
        {
            if (!(PlayerArea(player).Nouns.Count == 0))
            {
                InvestigateItem(PlayerArea(player).Nouns[index], player);
            }
        }

        public void InvestigateItem(AnyNoun item, Player player)
        {
            string messageText = item.Description + PerformItemActions(item, player, item.ActionsOnInteract);
            item.ActionsOnInteract = new List<AnyNoun.ActionType> { AnyNoun.ActionType.NoAction };
            if (messageText == "")
            {
                messageText = ("No problem here.");
            }
            MessageBox.Show(messageText);
        }

        public void UseItem(AnyNoun item, Player player)
        {
            if (!(item == null))
            {
                string messageText = item.UseMessage + PerformItemActions(item, player, item.ActionsOnUse);
                item.ActionsOnUse = new List<AnyNoun.ActionType> { AnyNoun.ActionType.NoAction };
                if (messageText == "")
                {
                    messageText = ("Nothing happens!");
                }
                MessageBox.Show(messageText);
            }
        }

        public string PerformItemActions(AnyNoun item, Player player,  List<AnyNoun.ActionType> actions)
        {
            //todo: do things for actions
            string messageText = "";
            if (!(actions == null))
            {
                foreach (AnyNoun.ActionType action in actions)
                {
                    switch (action)
                    {
                        case AnyNoun.ActionType.ItemGet:
                            foreach (AnyNoun itemToGive in item.ItemsToGive)
                            {
                                player.ItemInventory.Add(itemToGive);
                                messageText = (messageText + $"\nYou got a {itemToGive.Name}!");
                            }
                            break;
                        case AnyNoun.ActionType.Battle:
                            //todo: might need to rework the implementation of battles to work in this ViewModel instead
                            item.Battle(player);
                            break;
                        case AnyNoun.ActionType.BattleNewEnemies:
                            break;
                        case AnyNoun.ActionType.KillPlayer:
                            player.Die();
                            break;
                        case AnyNoun.ActionType.HPUp:
                            player.HealthPoints += item.HPUpLevel;
                            messageText = (messageText + $"\nYou recieved {item.HPUpLevel} Health Points!");
                            OnPropertyChanged(nameof(PlayerInfoDisplay));
                            break;
                        case AnyNoun.ActionType.MaxHPUp:
                            player.MaxHP += item.MaxHPUpLevel;
                            messageText = (messageText + $"\nYour max Health Points were increased by {item.MaxHPUpLevel}!");
                            OnPropertyChanged(nameof(PlayerInfoDisplay));
                            break;
                        case AnyNoun.ActionType.NoAction:
                            break;
                        default:
                            break;
                    }
                }
            }
            return messageText;
        }

        public void MoveIndexItemToInventory(int index, Player player)
        {
            if (!(PlayerArea(player).Nouns.Count == 0))
            {
                MoveItemToInventory(PlayerArea(player).Nouns[index], player);
            }
        }

        public void MoveItemToInventory(AnyNoun item, Player player)
        {
            if (!(item == null) && (item.CanPickUp == true))
            {
                player.ItemInventory.Add(item);
                PlayerArea(player).Nouns.Remove(item);
                OnPropertyChanged(nameof(InventoryDisplay));
                OnPropertyChanged(nameof(AreaItems));
            }
            else if (!item.CanPickUp == true)
            {
                MessageBox.Show("You cannot pick up this item.");
            }
        }

        public void DropItemFromInventory(AnyNoun item, Player player)
        {
            if(!(item == null))
            {
                PlayerArea(player).Nouns.Add(item);
                player.ItemInventory.Remove(item);
                OnPropertyChanged(nameof(AreaItems));
                OnPropertyChanged(nameof(InventoryDisplay));
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
                    ArtName = "SurfaceRoomArt01",
                    Nouns = new List<AnyNoun>
                    {
                        new AnyObject
                        {
                            Name = $"A small black slab",
                            CanPickUp = true,
                            HealthPoints = 10,
                            ID = 1905,
                            Description = "It has a blinking light, but it won't respond when talked to. Strange.",
                            ActionsOnInteract = new List<AnyNoun.ActionType>{AnyNoun.ActionType.ItemGet},
                            ItemsToGive = new List<AnyNoun>
                            {
                            new AnyObject
                            {
                                Name = $"Vinyl plastic yellow bird",
                                CanPickUp = true,
                                HealthPoints = 10,
                                ID = 948,
                                Description = "It is wet for some reason."
                            }
                            },
                            ActionsOnUse = new List<AnyNoun.ActionType>{AnyNoun.ActionType.HPUp},
                            HPUpLevel = 10
                        }
                    }
                },
                new Area
                {
                    AreaID = "SurfRoom002",
                    AreaName = "Surface Room 002",
                    ConnectedAreas = new List<string>{ "SurfRoom003", "SurfRoom001" },
                    LocationInfo = "Some room next to the cold, dark room you woke up in.",
                    ArtName = "SurfaceRoomArt01",
                    Nouns = new List<AnyNoun>
                    {
                        new AnyObject
                        {
                        Name = $"Vinyl plastic yellow bird",
                        CanPickUp = true,
                        HealthPoints = 10,
                        ID = 947,
                        Description = "It is wet for some reason.",
                        ActionsOnUse = new List<AnyNoun.ActionType>{AnyNoun.ActionType.MaxHPUp},
                        MaxHPUpLevel = 10
                        }
                    }
                },
                new Area
                {
                    AreaID = "SurfRoom003",
                    AreaName = "Surface Room 003",
                    ConnectedAreas = new List<string>{ "SurfRoom001", "SurfRoom002", "Quack" },
                    LocationInfo = "A duck-shaped room. (Don't ask why.)",
                    ArtName = "SurfaceRoomArt02",
                    Nouns = new List<AnyNoun>
                    {
                        new AnyObject
                        {
                        Name = $"Crate",
                        CanPickUp = false,
                        HealthPoints = 10,
                        ID = 100,
                        Description = "A crate. It just... sits here. It's too heavy to pick it up."
                        }
                    }
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

        private AnyNoun _selectedInvItem;

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

        public AnyNoun SelectedInvItem
        {
            get { return _selectedInvItem; }
            set
            {
                _selectedInvItem = value;
                OnPropertyChanged(nameof(SelectedInvItem));
            }
        }

        public string PlayerInfoDisplay
        {
            get
            {
                return $"{PlayerOne.Name} \n" +
              $"ID: {PlayerOne.ID} \n" +
              $"{PlayerOne.HealthPoints}/{PlayerOne.MaxHP} HP";
            }
            set { OnPropertyChanged(nameof(PlayerInfoDisplay)); }
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

        public List<string> AreaItems
        {
            get
            {
                List<string> areaItems = new List<string>();
                if (!(PlayerArea(_playerOne).Nouns == null))
                {
                    foreach (AnyNoun item in PlayerArea(_playerOne).Nouns)
                    {
                        areaItems.Add(item.Name);
                    }
                }
                return areaItems;
            }
            set { OnPropertyChanged(nameof(AreaItems)); }
        }

        public ObservableCollection<AnyNounInventoryDisplay> InventoryDisplay
        {
            get
            {
                ObservableCollection<AnyNounInventoryDisplay> inventoryDisplay = new ObservableCollection<AnyNounInventoryDisplay>();
                foreach (AnyNoun noun in PlayerOne.ItemInventory)
                {
                    string thisVar = noun.Name;
                    inventoryDisplay.Add(new AnyNounInventoryDisplay { Name = thisVar });
                }
                return inventoryDisplay;
            }
            set
            {
                OnPropertyChanged(nameof(InventoryDisplay));
            }
        }

        #endregion
        //----------------------------------------------
    }
}
