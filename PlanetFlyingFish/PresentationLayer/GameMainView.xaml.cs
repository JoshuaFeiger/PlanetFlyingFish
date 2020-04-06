using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PlanetFlyingFish.Models;
using PlanetFlyingFish.PresentationLayer;

namespace PlanetFlyingFish.PresentationLayer
{
    /// <summary>
    /// Interaction logic for GameMainView.xaml
    /// </summary>
    public partial class GameMainView : Window
    {
        GameMainViewModel _gameMainViewModel;

        public GameMainView(GameMainViewModel gameMainViewModel)
        {
            _gameMainViewModel = gameMainViewModel;

            InitializeComponent();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MapArtSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _gameMainViewModel.SideMessages.Add("e ");
            //todo: So it does set. We've gotta figure out why, then, the other stuff isn't updating.
        }
    }
}
