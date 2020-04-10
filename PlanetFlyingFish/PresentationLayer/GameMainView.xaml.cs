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
            scrollPriorityMessagesOnNextUpdate = false;

            scrollSideMessagesOnNextUpdate = false;

            _gameMainViewModel = gameMainViewModel;

            InitializeComponent();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MapArtSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem mapArtSelectedItem = comboBox.SelectedItem as ComboBoxItem;

            if (mapArtSelectedItem.Content != null)
            {
                string mapArtSelection = mapArtSelectedItem.Content.ToString();
                _gameMainViewModel.CentralImageChanged(mapArtSelection);
            }
        }

        private void PriorityMessagesChanged(object sender, TextChangedEventArgs e)
        {
            if (scrollPriorityMessagesOnNextUpdate)
            {
                PriorityMessageBox.ScrollToVerticalOffset(PriorityMessageBox.VerticalOffset + 40);
            }

            scrollPriorityMessagesOnNextUpdate = true;
        }

        private void SideMessagesChanged(object sender, TextChangedEventArgs e)
        {
            if (scrollSideMessagesOnNextUpdate)
            {
                SideMessageBox.ScrollToVerticalOffset(SideMessageBox.VerticalOffset + 35);
            }

            scrollSideMessagesOnNextUpdate = true;
        }

        private bool scrollPriorityMessagesOnNextUpdate { get; set; }

        private bool scrollSideMessagesOnNextUpdate { get; set; }
    }
}
