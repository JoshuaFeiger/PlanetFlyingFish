using PlanetFlyingFish.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Windows;
using System.Windows.Forms;

namespace PlanetFlyingFish.PresentationLayer
{
    public class GameMainViewModel : ObservableObject
    {
        //----------------------------------------------
        #region ----------------------------METHODS


        //          ----------------------------------------------
        #region ----------------------------MISC. METHODS

        private Area PlayerArea(Player player)
        {
            return _areas[player.CurrentAreaPos(_areas)];
        }

        public void TravelToAreaIndex(int areaIndex)
        {
            TravelToAreaIndex(areaIndex, false);
        }

        public void TravelToAreaIndex(int areaIndex, bool ignoreErrors)
        {
            if (!(AccessibleAreas[areaIndex] == "ERR: DOESNOTEXIST"))
            {
                try
                {
                    UpdatePlayerAreaID(_playerOne, PlayerArea(_playerOne).ConnectedAreas[areaIndex]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    if (!ignoreErrors)
                    {
                        MessageView.Show($"Error: cannot travel to the area as it is out of range of the list of areas you can travel to. This likely means the interface failed to update.");
                    }
                }
            }
            else
            {
                if (!ignoreErrors)
                {
                    MessageView.Show($"Error: cannot travel to the area as it does not exist." +
                                     $"\nArea ID: {PlayerArea(_playerOne).ConnectedAreas[areaIndex]}");
                }
            }
        }

        public void TravelToArea(string areaID)
        {
            TravelToArea(areaID, false);
        }

        public void TravelToArea(string areaID, bool ignoreErrors)
        {
            if (PlayerArea(_playerOne).ConnectedAreas.Contains(areaID))
            {
                UpdatePlayerAreaID(_playerOne, areaID);
            }
            else
            {
                if (!ignoreErrors)
                {
                    bool areaExists = false;
                    foreach (Area area in _areas)
                    {
                        if (area.AreaID == areaID)
                        {
                            areaExists = true;
                        }
                    }
                    if (areaExists)
                    {
                        MessageView.Show($"Error: cannot travel to the area as it is out of range of the list of areas you can travel to. This likely means the interface failed to update.");
                    }
                    else
                    {
                        MessageView.Show($"Error: cannot travel to the area as it does not exist." +
                                         $"\nArea ID: {areaID}");
                    }
                }
            }
        }

        public void WarpToArea(string areaID, bool ignoreErrors)
        {
            bool areaExists = false;
            foreach (Area area in _areas)
            {
                if (area.AreaID == areaID)
                {
                    areaExists = true;
                }
            }
            if (areaExists)
            {
                UpdatePlayerAreaID(_playerOne, areaID);
            }
            else
            {
                MessageView.Show($"Error: cannot travel to the area as it does not exist." +
                             $"\nArea ID: {areaID}");
            }
        }

        public void UpdatePlayerAreaID(Player player, string areaID)
        {
            player.AreaID = areaID;
            AreaNameDisplay = "this is just to update";
            LocationInfo = "Yeah, that's all we do.";
            AccessibleAreas = new List<string> { "Why do I even bother?" };
            CentralImageChanged(_centralImageArtChosen);
            OnPropertyChanged(nameof(AreaItems));
        }

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
            }
        }

        #endregion
        //          ----------------------------------------------

        //          ----------------------------------------------
        #region ----------------------------INSTANTIATION METHODS
        public GameMainViewModel()
        {
            _playerOne = new Player();
            if (_playerOne.ItemInventory == null)
            {
                _playerOne.ItemInventory = new ObservableCollection<AnyNoun>();
            }
            _areas = DefaultAreas();
            _storyEvents = DefaultStoryEvents();
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
            if (_playerOne.ItemInventory == null)
            {
                _playerOne.ItemInventory = new ObservableCollection<AnyNoun>();
            }
            _areas = DefaultAreas();
            _storyEvents = DefaultStoryEvents();
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
            if (_playerOne.ItemInventory == null)
            {
                _playerOne.ItemInventory = new ObservableCollection<AnyNoun>();
            }
            _areas = DefaultAreas();
            _storyEvents = DefaultStoryEvents();
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
            if (_playerOne.ItemInventory == null)
            {
                _playerOne.ItemInventory = new ObservableCollection<AnyNoun>();
            }
            _areas = initialAreas;
            _storyEvents = DefaultStoryEvents();
            _priorityMessages = initialPriorityMessages;
            _sideMessages = initialSideMessages;
            _centralImagePath = "/PlanetFlyingFish;component/Resource/Images/MapTest.png";
            _centralImageArtChosen = "Map";
            MapArtName = "Map01";
            UpdatePriorityMessages();
            UpdateSideMessages();
        }

        public GameMainViewModel(Player player, List<Area> initialAreas, List<StoryEvent> storyEvents, List<string> initialPriorityMessages, List<string> initialSideMessages)
        {
            _playerOne = player;
            if (_playerOne.ItemInventory == null)
            {
                _playerOne.ItemInventory = new ObservableCollection<AnyNoun>();
            }
            _areas = initialAreas;
            _storyEvents = storyEvents;
            _priorityMessages = initialPriorityMessages;
            _sideMessages = initialSideMessages;
            _centralImagePath = "/PlanetFlyingFish;component/Resource/Images/MapTest.png";
            _centralImageArtChosen = "Map";
            MapArtName = "Map01";
            UpdatePriorityMessages();
            UpdateSideMessages();
        }
        #endregion
        //          ----------------------------------------------

        //          ----------------------------------------------
        #region ----------------------------NOUN HANDLING
        public (Area movingFrom, Area movingTo) MoveNoun((AnyNoun noun, Area movingFrom, Area movingTo) moveData)
        {
            (Area movingFrom, Area movingTo) thing = (moveData.movingFrom, moveData.movingTo);

            if (moveData.movingFrom.AreaID == "AllAreas")
            {
                (AnyNoun objectToMove, Area areaToMoveFrom) = ScanAreasForNounID(moveData.noun.ID, _areas);
                _areas[_areas.IndexOf(areaToMoveFrom)].Nouns.Remove(objectToMove);
                moveData.movingTo.Nouns.Add(objectToMove);
            }
            else
            {
                moveData.movingTo.Nouns.Add(moveData.noun);
                moveData.movingFrom.Nouns.Remove(moveData.noun);
            }
            AreaItems = new List<string>();
            return thing;
        }

        public void RemoveNoun(AnyNoun noun, Area removeFrom)
        {
            Area area = new Area();
            if (removeFrom.AreaID == "AllAreas")
            {
                (AnyNoun objectToRemove, Area areaToRemoveFrom) = ScanAreasForNounID(noun.ID, _areas);
                _areas[_areas.IndexOf(areaToRemoveFrom)].Nouns.Remove(objectToRemove);
                area = areaToRemoveFrom;
            }
            else
            {
                removeFrom.Nouns.Remove(noun);
                area = removeFrom;
            }
            AreaItems = new List<string>();
        }

        #endregion
        //          ----------------------------------------------

        //          ----------------------------------------------
        #region ----------------------------PLAYER/NOUN INTERACTION

        public void DropItemFromInventory(AnyNoun item, Player player)
        {
            if (!(item == null))
            {
                PlayerArea(player).Nouns.Add(item);
                player.ItemInventory.Remove(item);
                OnPropertyChanged(nameof(AreaItems));
                OnPropertyChanged(nameof(InventoryDisplay));
            }
        }

        public void MoveIndexItemToInventory(int index, Player player)
        {
            if (!(PlayerArea(player).Nouns.Count == 0))
            {
                MoveItemToInventory(PlayerArea(player).Nouns[index], player);
                SelectedAreaItemNumber = 0;
            }
        }

        public void MoveItemToInventory(AnyNoun item, Player player)
        {
            if (!(item == null) && PlayerArea(player).Nouns.Contains(item) && (item.CanPickUp == true))
            {
                player.ItemInventory.Add(item);
                PlayerArea(player).Nouns.Remove(item);
                OnPropertyChanged(nameof(InventoryDisplay));
                OnPropertyChanged(nameof(AreaItems));
                string messageText = PerformItemActions(item, player, item.ActionsOnInteract);
                if (messageText != "")
                {
                    BinaryChoiceView.CloseButton closeButton = BinaryChoiceView.Show(messageText, "OK", "View Info");
                    if (closeButton == BinaryChoiceView.CloseButton.BUTTON2)
                    {
                        DisplayItemInfoMessage(item);
                    }
                }
            }
            else if (!item.CanPickUp == true)
            {
                if (item is Character)
                {
                    MessageView.Show("Don't try to pick up people!");
                }
                else
                {
                    MessageView.Show("You cannot pick up this item.");
                }

            }
        }

        public void InvestigateIndexItem(int index, Player player)
        {
            if (!(index < 0 || PlayerArea(player).Nouns.Count == 0))
            {
                InvestigateItem(PlayerArea(player).Nouns[index], player);
            }
        }

        public void InvestigateItem(AnyNoun item, Player player)
        {
            string messageText = item.Description + PerformItemActions(item, player, item.ActionsOnInteract);
            if (messageText == "")
            {
                messageText = ("No problem here.");
            }
            if (!item.ViewInfoRestricted)
            {
                BinaryChoiceView.CloseButton closeButton;
                if (!(messageText == "NO_WINDOW"))
                {
                    closeButton = BinaryChoiceView.Show(messageText, "OK", "View Info");
                }
                else
                {
                    closeButton = BinaryChoiceView.Show($"View info for -{item.Name}-?", "No", "Yes");
                }
                if (closeButton == BinaryChoiceView.CloseButton.BUTTON2)
                {
                    DisplayItemInfoMessage(item);
                }
            }
            else if (!(messageText == "NO_WINDOW"))
            {
                MessageView.Show(messageText);
            }
        }

        public void DisplayItemInfoMessage(AnyNoun item)
        {
            MessageView.Show($"Info for ~{item.Name}~\n\n" +
                                        $"ID: {item.ID} \n" +
                                        $"{item.HealthPoints}/{item.MaxHP} HP");

        }

        public void UseItem(AnyNoun item, Player player)
        {
            if (!(item == null))
            {
                string messageText = item.UseMessage + PerformItemActions(item, player, item.ActionsOnUse);
                if (messageText == "")
                {
                    messageText = ("Nothing happens!");
                }
                if (!(messageText == "NO_WINDOW"))
                {
                    MessageView.Show(messageText);
                }
            }
        }

        public string PerformItemActions(AnyNoun item, Player player, List<GameActionGroup> actionsList)
        {
            string messageText = "";
            if (!(actionsList == null))
            {
                foreach (GameActionGroup actions in actionsList)
                {
                    if (!(actions == null) && player.StoryTriggers.Contains(actions.StoryTriggerID))
                    {
                        foreach (GameAction action in actions.ActionsToPerform)
                        {
                            if (!(action.TextForAction == null || action.TextForAction == ""))
                            {
                                messageText = (messageText + "\n" + action.TextForAction);
                            }
                            switch (action)
                            {
                                case GameAction.ItemGet itemGet:
                                    foreach (AnyNoun itemToGive in itemGet.ItemsToGive)
                                    {
                                        player.ItemInventory.Add(itemToGive);
                                    }
                                    break;
                                case GameAction.Battle battle:
                                    item.Battle(player);
                                    break;
                                case GameAction.BattleNewEnemies battleNewEnemies:
                                    break;
                                case GameAction.KillPlayer killPlayer:
                                    player.Die();
                                    break;
                                case GameAction.HPUp hpUp:
                                    player.HealthPoints += hpUp.HPUpLevel;
                                    OnPropertyChanged(nameof(PlayerInfoDisplay));
                                    break;
                                case GameAction.MaxHPUp maxHPUp:
                                    player.MaxHP += maxHPUp.MaxHPUpLevel;
                                    OnPropertyChanged(nameof(PlayerInfoDisplay));
                                    break;
                                case GameAction.NoAction noAction:
                                    break;
                                case Dialog.BasicText basicTextDialog:
                                    MessageView.Show(basicTextDialog.Text);
                                    break;
                                case Dialog.YesOrNo yesOrNoDialog:
                                    BinaryChoiceView.CloseButton answer = BinaryChoiceView.Show(yesOrNoDialog.Text, yesOrNoDialog.ButtonText.yesText, yesOrNoDialog.ButtonText.noText, true);
                                    if (answer == BinaryChoiceView.CloseButton.BUTTON1)
                                    {
                                        yesOrNoDialog.Answer = true;
                                        yesOrNoDialog.ExpireOnCompleted = yesOrNoDialog.CloseOnYesOrNo.yes;
                                    }
                                    else
                                    {
                                        yesOrNoDialog.Answer = false;
                                        yesOrNoDialog.ExpireOnCompleted = yesOrNoDialog.CloseOnYesOrNo.no;
                                    }
                                    PerformItemActionsAndShowMessage(item, player, new List<GameActionGroup> { yesOrNoDialog.ActionsToTake });
                                    break;
                                case GameAction.ForceTravel forceTravel:
                                    TravelToArea(forceTravel.AreaID, forceTravel.IgnoreErrors);
                                    break;
                                case GameAction.ForceWarp forceWarp:
                                    WarpToArea(forceWarp.AreaID, forceWarp.IgnoreErrors);
                                    break;
                                case GameAction.TriggerStoryEvent triggerStoryEvent:
                                    StoryEvent storyEvent = ScanStoryEventsForEventName(triggerStoryEvent.StoryEventName, StoryEvents);
                                    PerformStoryEventActions(storyEvent, Areas);
                                    break;
                                case GameAction.AddOrRemoveStoryTriggers addOrRemoveStoryTriggers:
                                    if (addOrRemoveStoryTriggers.AddOrRemove == GameAction.AddOrRemoveStoryTriggers.Option.Add)
                                    {
                                        player.StoryTriggers.AddRange(addOrRemoveStoryTriggers.StoryTriggers);
                                    }
                                    else
                                    {
                                        foreach (string storyTrigger in addOrRemoveStoryTriggers.StoryTriggers)
                                        {
                                            player.StoryTriggers.Remove(storyTrigger);
                                        }
                                    }
                                    break;
                                case GameAction.Trade trade:
                                    try
                                    {
                                        Character character = (Character)item;
                                        List<int> deleteIndexes = new List<int>();
                                        foreach (AnyNoun noun in player.ItemInventory)
                                        {
                                            if (trade.CharacterTakes.Contains(noun.ID))
                                            {
                                                character.ItemInventory.Add(noun);
                                                deleteIndexes.Add(player.ItemInventory.IndexOf(noun));
                                            }
                                        }
                                        foreach (int index in deleteIndexes)
                                        {
                                            player.ItemInventory.RemoveAt(index);
                                        }
                                        deleteIndexes = new List<int>();
                                        foreach (AnyNoun noun in character.ItemInventory)
                                        {
                                            if (trade.PlayerTakes.Contains(noun.ID))
                                            {
                                                player.ItemInventory.Add(noun);
                                                deleteIndexes.Add(character.ItemInventory.IndexOf(noun));
                                            }
                                        }
                                        foreach (int index in deleteIndexes)
                                        {
                                            player.ItemInventory.RemoveAt(index);
                                        }
                                    }
                                    catch (InvalidCastException)
                                    {
                                        if (item == null)
                                        {
                                            throw new System.ArgumentException("The item was empty. It needs to (a.) exist and (b.) be the type Character, or a class inheriting from it.", "item");
                                        }
                                        else
                                        {
                                            throw new System.ArgumentException("The item was not of the type Character, and therefore had no inventory to trade with.", "item");
                                        }
                                        throw;
                                    }
                                    break;
                                case GameAction.PerformActionsIf.DiceRollSucceed diceRollSucceed:
                                    NPC nPC = item as NPC;
                                    diceRollSucceed.Answer = nPC.DiceRollSucceed(diceRollSucceed.Ints);
                                    PerformActionsIf_MainCode(diceRollSucceed, item, player);
                                    break;
                                case GameAction.PerformActionsIf.PlayerOneHasItem playerOneHasItem:
                                    foreach (AnyNoun inventoryItem in player.ItemInventory)
                                    {
                                        if (inventoryItem.ID == playerOneHasItem.ItemID)
                                        {
                                            playerOneHasItem.Answer = true;
                                        }
                                    }
                                    PerformActionsIf_MainCode(playerOneHasItem, item, player);
                                    break;
                                case GameAction.AddToPriorityMessages addToPriorityMessages:
                                    AddToMessagesScript(addToPriorityMessages.TextToAdd, PriorityMessages, addToPriorityMessages.AddLines);
                                    UpdatePriorityMessages();
                                    break;
                                case GameAction.AddToSideMessages addToSideMessages:
                                    AddToMessagesScript(addToSideMessages.TextToAdd, SideMessages, addToSideMessages.AddLines);
                                    UpdateSideMessages();
                                    break;
                                default:
                                    break;
                            }
                        }
                        for (int i = 0; i < actions.ActionsToPerform.Count; i++)
                        {
                            if (actions.ActionsToPerform[i].ExpireOnCompleted)
                            {
                                actions.ActionsToPerform.RemoveAt(i);
                                i = i - 1;
                            }
                        }
                    }
                }
            }
            return messageText;
        }

        public void AddToMessagesScript(string add, List<string> messages, bool addLines)
        {
            if (addLines)
            {
                add = $"\n\n{add}";
            }
            messages.Add(add);
        }

        public void PerformActionsIf_MainCode(GameAction.PerformActionsIf performActionsIf, AnyNoun item, Player player)
        {
            if (performActionsIf.Answer == true)
            {
                performActionsIf.ExpireOnCompleted = performActionsIf.CloseOnTrueOrFalse.onTrue;
            }
            else
            {
                performActionsIf.ExpireOnCompleted = performActionsIf.CloseOnTrueOrFalse.onFalse;
            }
            PerformItemActionsAndShowMessage(item, player, new List<GameActionGroup> { performActionsIf.ActionsToTake });
        }

        public void PerformItemActionsAndShowMessage(AnyNoun item, Player player, List<GameActionGroup> actionsList)
        {
            string messageText = PerformItemActions(item, player, actionsList);
            if (!(messageText == ""))
            {
                MessageView.Show(messageText);
            }
        }

        #endregion
        //          ----------------------------------------------

        //          ----------------------------------------------
        #region ----------------------------STORY EVENTS/AREA EFFECTS

        public Area PerformAreaEffectActions(EffectAreaInstructions effectArea, Area area)
        {
            //check to make sure the selected area is not null.
            if (!(area == null))
            {
                foreach (AreaEffect effect in effectArea.AreaEffects)
                {
                    switch (effect)
                    {
                        case AreaEffect.AddNoun addNoun:
                            if (!(area.AreaID == "AllAreas"))
                            {
                                area.Nouns.Add(addNoun.NounToAdd);
                                AreaItems = new List<string>();
                            }
                            break;
                        case AreaEffect.MoveNoun moveNoun:
                            Area areaToMoveTo = ScanAreasForAreaID(moveNoun.AreaToMoveToID, _areas);
                            (area, areaToMoveTo) = MoveNoun((ScanAreaForNounID(moveNoun.NounToMoveID, area), area, areaToMoveTo));
                            break;
                        case AreaEffect.RemoveNoun removeNoun:
                            RemoveNoun(ScanAreaForNounID(removeNoun.NounToRemoveID, area), area);
                            break;
                        case AreaEffect.UnlockTravel unlockTravel:
                            if (!(area.AreaID == "AllAreas"))
                            {
                                area.ConnectedAreas.Add(unlockTravel.UnlockedAreaID);
                            }
                            break;
                        case AreaEffect.EditProperty editProperty:
                            AnyNoun objectToEdit = ScanAreaForNounID(editProperty.ObjectID, area);
                            System.Reflection.PropertyInfo propertyInfo = editProperty.PropertyID;
                            if (objectToEdit == null)
                            {
                                throw new System.ArgumentException("Object cannot be null. It's likely there's a faulty ID involved here.", "objectToEdit");
                            }
                            else
                            {
                                if (editProperty.ValueEdited == null)
                                {
                                    Debug.WriteLine($"\n\n--DEV NOTE--\n" +
                                                    $"One of an object's properties has been set to a null value! This may cause errors!\n" +
                                                    $"~{objectToEdit.Name}~\n\n");
                                }
                                propertyInfo.SetValue(objectToEdit, editProperty.ValueEdited);
                            }
                            AreaItems = new List<string>();
                            break;
                        default:
                            break;
                    }
                }
            }
            return area;
        }

        public List<Area> PerformStoryEventActions(StoryEvent storyEvent, List<Area> areas)
        {
            foreach (EffectAreaInstructions effectArea in storyEvent.EffectAreas)
            {
                Area areaSelected = ScanAreasForAreaID(effectArea.AreaID, areas);
                areaSelected = PerformAreaEffectActions(effectArea, areaSelected);
            }
            return areas;
        }

        #endregion
        //          ----------------------------------------------

        //          ----------------------------------------------
        #region ----------------------------"SCAN" METHODS

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

        public Area ScanAreasForAreaID(string areaID, List<Area> areas)
        {
            Area areaSelected = null;
            try
            {
                foreach (Area area in areas)
                {
                    if (area.AreaID == areaID)
                    {
                        areaSelected = area;
                        break;
                    }
                }
            }
            catch (Exception)
            {

            }
            if (areaSelected == null)
            {
                if (areaID == "AllAreas")
                {
                    areaSelected = AllAreas;
                }
            }
            return areaSelected;
        }

        public AnyNoun ScanAreaForNounID(int nounID, Area area)
        {
            AnyNoun nounSelected = null;
            try
            {
                foreach (AnyNoun noun in area.Nouns)
                {
                    if (noun.ID == nounID)
                    {
                        nounSelected = noun;
                        break;
                    }
                }
            }
            catch (Exception)
            {

            }
            return nounSelected;
        }

        public (AnyNoun, Area) ScanAreasForNounID(int nounID, List<Area> areas)
        {
            Area areaSelected = new Area();
            AnyNoun nounSelected = new AnyObject();
            try
            {
                foreach (Area area in areas)
                {
                    foreach (AnyNoun noun in area.Nouns)
                    {
                        if (noun.ID == nounID)
                        {
                            areaSelected = area;
                            nounSelected = noun;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return (nounSelected, areaSelected);
        }

        public StoryEvent ScanStoryEventsForEventName(string eventName, List<StoryEvent> storyEvents)
        {
            StoryEvent storyEventFound = new StoryEvent("Error");
            try
            {
                foreach (StoryEvent storyEvent in storyEvents)
                {
                    if (storyEvent.EventName == eventName)
                    {
                        storyEventFound = storyEvent;
                        break;
                    }
                }
            }
            catch (Exception)
            {

            }
            return storyEventFound;
        }
        #endregion
        //          ----------------------------------------------

        //          ----------------------------------------------
        #region ----------------------------"DEFAULTS" METHODS

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
                            ActionsOnInteract = new List<GameActionGroup>
                            {
                                new GameActionGroup
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>()
                                    {
                                        new GameAction.ItemGet(
                                        new AnyObject
                                        {
                                            Name = $"Vinyl plastic yellow bird",
                                            CanPickUp = true,
                                            HealthPoints = 10,
                                            ID = 948,
                                            Description = "It is wet for some reason."
                                        })
                                    }
                                }
                            },

                            ActionsOnUse = new List<GameActionGroup>
                            {
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>()
                                    {
                                        new GameAction.HPUp(10, "You find a battery inside, and drain its power.", true)
                                    }
                                }
                            }
                        },
                        new AnyObject
                        {
                            Name = $"Twisted metal",
                            CanPickUp = true,
                            HealthPoints = 10,
                            ID = 2634,
                            Description = "Seems to be used to hold papers together.",
                            ActionsOnInteract = new List<GameActionGroup>()
                            {
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>()
                                    {
                                        new GameAction.ItemGet
                                        (new AnyObject
                                        {
                                            Name = $"Papers",
                                            CanPickUp = true,
                                            HealthPoints = 10,
                                            ID = 1239,
                                            Description = "They contain some data about the planet's atmosphere."
                                        })
                                    }
                                }
                            }
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
                            ActionsOnUse = new List<GameActionGroup>()
                            {
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>
                                    {
                                        new GameAction.MaxHPUp(10)
                                    }
                                }
                            }
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
                        },
                        new NPC
                        {
                            Name = "Unit #3754 \"Vacchan\"",
                            CanPickUp = false,
                            HealthPoints = 190,
                            ID = 3754,
                            Description = "NO_WINDOW",
                            ActionsOnInteract = new List<GameActionGroup>
                            {
                                new GameActionGroup()
                                {
                                    StoryTriggerID = "Default",
                                    ActionsToPerform = new List<GameAction>
                                    {
                                        new Dialog.YesOrNo()
                                        {
                                            CloseOnYesOrNo = (true, false),
                                            Text = "Do you want a ducky?",
                                            ButtonText = ("Yes", "Heck nah"),
                                            ActionsOnYes = new GameActionGroup()
                                            {
                                                StoryTriggerID = "Default",
                                                ActionsToPerform = new List<GameAction>()
                                                {
                                                    new Dialog.BasicText("Yay! I love giving people duckies!"){ ExpireOnCompleted = false },
                                                    new Dialog.BasicText("And yet, you must win a dice roll to recieve it!"),
                                                    //---------------------------------------------------------------------------------------------------------------------------------------------
                                                    //---------------------------------------------------------------------------------------------------------------------------------------------
                                                    //---------------------------------------------------------------------------------------------------------------------------------------------
                                                    //---------------------------------------------------------------------------------------------------------------------------------------------
                                                    //---------------------------------------------------------------------------------------------------------------------------------------------
                                                    //Add a way for her to give you the ducky ONLY IF you win the dice roll! (probably a general boolean "perform {actions} IF" action)
                                                    new GameAction.ItemGet(new AnyObject()
                                                    { Name = $"Vinyl plastic yellow bird",
                                                    CanPickUp = true,
                                                    HealthPoints = 10,
                                                    ID = 956,
                                                    Description = "It is wet for some reason.",
                                                    ActionsOnUse = new List<GameActionGroup>()
                                                    {
                                                        new GameActionGroup()
                                                        {
                                                            StoryTriggerID = "Default",
                                                            ActionsToPerform = new List<GameAction>
                                                            {
                                                                new GameAction.MaxHPUp(10)
                                                            }
                                                        }
                                                    }}){ ExpireOnCompleted = false }
                                                }
                                            },
                                            ActionsOnNo = new GameActionGroup()
                                            {
                                                StoryTriggerID = "Default",
                                                ActionsToPerform = new List<GameAction>()
                                                {
                                                    new Dialog.BasicText("Oh, okay..."){ ExpireOnCompleted = false },
                                                    new GameAction.MaxHPUp(10){TextForAction = "", ExpireOnCompleted = false},
                                                    new GameAction.MaxHPUp(10),
                                                    new GameAction.TriggerStoryEvent("StoryEventTest2")
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            return defaultAreas;
        }

        private List<StoryEvent> DefaultStoryEvents()
        {
            List<StoryEvent> defaultStoryEvents = new List<StoryEvent>()
            {
                new StoryEvent("StoryEventTest")
                {
                    EffectAreas = new List<EffectAreaInstructions>()
                    {
                        new EffectAreaInstructions()
                        {
                            AreaID = "SurfRoom003",
                            AreaEffects = new List<AreaEffect>()
                            {
                                new AreaEffect.MoveNoun()
                                {
                                    NounToMoveID = 100,
                                    AreaToMoveToID = "SurfRoom002"
                                }
                            }
                        }
                    }
                },
                new StoryEvent("StoryEventTest2")
                {
                    EffectAreas = new List<EffectAreaInstructions>()
                    {
                        new EffectAreaInstructions()
                        {
                            AreaID = "SurfRoom003",
                            AreaEffects = new List<AreaEffect>()
                            {
                                new AreaEffect.EditProperty()
                                {
                                    ObjectID = 3754,
                                    PropertyID = typeof(AnyNoun).GetProperty(nameof(AnyNoun.Name)),
                                    ValueEdited = "Unit #3754 \"Vacchan SUPREME\""
                                },
                                new AreaEffect.RemoveNoun()
                                {
                                    NounToRemoveID = 3754
                                },
                                new AreaEffect.AddNoun()
                                {
                                    NounToAdd = new AnyObject
                                    {
                                        Name = $"Crate",
                                        CanPickUp = false,
                                        HealthPoints = 10,
                                        ID = 105,
                                        Description = "A crate. It just... sits here. It's too heavy to pick it up."
                                    }
                                }
                            }
                        }
                    }
                }
            };
            return defaultStoryEvents;
        }
        #endregion
        //          ----------------------------------------------

        //          ----------------------------------------------
        #region ----------------------------"UPDATE" METHODS

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
        //          ----------------------------------------------

        #endregion
        //----------------------------------------------

        //----------------------------------------------
        #region ----------------------------FIELDS

        private Player _playerOne;

        private List<Area> _areas;

        private List<StoryEvent> _storyEvents;

        private List<string> _priorityMessages;

        private List<string> _sideMessages;

        private string _centralImagePath;

        private string _priorityMessageDisplay;

        private string _sideMessageDisplay;

        private string _centralImageArtChosen;

        private string _mapArtName;

        private AnyNoun _selectedInvItem;

        private int _selectedAreaItemNumber;

        private Area _allAreas;

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

        public List<StoryEvent> StoryEvents
        {
            get { return _storyEvents; }
            set { _storyEvents = value; }
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
                OnPropertyChanged(nameof(ItemInfoDisplay));
            }
        }

        public int SelectedAreaItemNumber
        {
            get { return _selectedAreaItemNumber; }
            set
            {
                _selectedAreaItemNumber = value;
                ItemInfoDisplay = "Update Text";
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
                    if ((PlayerArea(_playerOne).Nouns.Count == 0))
                    {
                        areaItems.Add("--Empty!--");
                    }
                    else
                    {
                        foreach (AnyNoun item in PlayerArea(_playerOne).Nouns)
                        {
                            areaItems.Add(item.Name);
                        }
                    }
                }
                return areaItems;
            }
            set { OnPropertyChanged(nameof(AreaItems)); }
        }

        public string ItemInfoDisplay
        {
            get
            {
                AnyNoun SelectedItem = new AnyObject();
                Area playerArea = PlayerArea(_playerOne);
                if (!(SelectedInvItem == null))
                {
                    SelectedItem = SelectedInvItem;
                    return $"Name: {SelectedItem.Name} \n" +
                           $"ID: {SelectedItem.ID} \n" +
                           $"{SelectedItem.HealthPoints}/{SelectedItem.MaxHP} HP \n" +
                           $"Description:\n" +
                           $"{SelectedInvItem.Description}";
                }
                else
                {
                    return "";
                }
            }
            set { OnPropertyChanged(nameof(ItemInfoDisplay)); }
        }

        public ObservableCollection<AnyNounInventoryDisplay> InventoryDisplay
        {
            get
            {
                ObservableCollection<AnyNounInventoryDisplay> inventoryDisplay = new ObservableCollection<AnyNounInventoryDisplay>();
                if (!(PlayerOne.ItemInventory == null))
                {
                    foreach (AnyNoun noun in PlayerOne.ItemInventory)
                    {
                        string thisVar = noun.Name;
                        inventoryDisplay.Add(new AnyNounInventoryDisplay { Name = thisVar });
                    }
                }
                return inventoryDisplay;
            }
            set
            {
                OnPropertyChanged(nameof(InventoryDisplay));
            }
        }

        public Area AllAreas
        {
            get
            {
                List<AnyNoun> nouns = new List<AnyNoun>();
                foreach (Area area in _areas)
                {
                    nouns.AddRange(area.Nouns);
                }
                Area allAreas = new Area()
                {
                    AreaID = "AllAreas",
                    AreaName = "All Areas",
                    Nouns = nouns,
                    LocationInfo = "All the areas in this game instance."
                };
                _allAreas = allAreas;
                return allAreas;
            }
        }


        #endregion
        //----------------------------------------------
    }
}
