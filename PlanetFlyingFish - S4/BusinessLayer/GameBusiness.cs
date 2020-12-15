using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanetFlyingFish.PresentationLayer;
using PlanetFlyingFish.Models;
using PlanetFlyingFish.DataLayer;

namespace PlanetFlyingFish.BusinessLayer
{
    public class GameBusiness
    {
        GameMainViewModel _gameMainViewModel;
        Player _player;
        List<string> _priorityMessages;
        List<string> _sideMessages;
        List<Area> _areas;
        List<StoryEvent> _storyEvents;


        public GameBusiness()
        {
            InitializeGame();
        }

        /// <summary>
        /// Begin the game by setting up the data and opening a game window.
        /// </summary>
        private void InitializeGame()
        {
            InitializeDataSet();
            InstantiateAndShowView();
        }

        private void InitializeDataSet()
        {
            _player = GameData.PlayerData();
            _priorityMessages = GameData.PriorityMessageData();
            _sideMessages = GameData.SideMessageData();
            _areas = GameData.AreasData();
            _storyEvents = GameData.DefaultStoryEvents();
        }

        private void InstantiateAndShowView()
        {
            _gameMainViewModel = new GameMainViewModel(_player, _areas, _storyEvents, _priorityMessages, _sideMessages);
            GameMainView gameMainView = new GameMainView(_gameMainViewModel);

            gameMainView.DataContext = _gameMainViewModel;

            gameMainView.Show();
        }
    }
}
