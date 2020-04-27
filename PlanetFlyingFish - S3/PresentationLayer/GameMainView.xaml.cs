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

        private void ConfirmTravelButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMainViewModel.TravelToArea(TravelToLocationCombo.SelectedIndex);
        }

        private void SortItemUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(ItemsDisplay.SelectedIndex <= 0))
            {
                int selectedIndex = ItemsDisplay.SelectedIndex;
                List<AnyNoun> list = new List<AnyNoun>();
                foreach (AnyNoun item in ItemsDisplay.ItemsSource)
                {
                    list.Add(item);
                }
                var view = CollectionViewSource.GetDefaultView(ItemsDisplay.ItemsSource);
                view?.SortDescriptions.Clear();
                foreach (var column in ItemsDisplay.Columns)
                {
                    column.SortDirection = null;
                }
                view = list as System.ComponentModel.ICollectionView;
                ItemsDisplay.SelectedIndex = selectedIndex;
                AnyNoun itemToMoveUp = list[selectedIndex];
                AnyNoun itemToMoveDown = list[selectedIndex - 1];
                list[selectedIndex] = itemToMoveDown;
                list[selectedIndex - 1] = itemToMoveUp;
                _gameMainViewModel.PlayerOne.ItemInventory.Clear();
                foreach (AnyNoun item in list)
                {
                    _gameMainViewModel.PlayerOne.ItemInventory.Add(item);
                }
                view = list as System.ComponentModel.ICollectionView;
                ItemsDisplay.SelectedIndex = selectedIndex - 1;
            }
            
        }

        private void SortItemDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(ItemsDisplay.SelectedIndex >= ItemsDisplay.Items.Count - 1))
            {
                int selectedIndex = ItemsDisplay.SelectedIndex;
                AnyNoun itemToMoveDown = ItemsDisplay.SelectedItem as AnyNoun;
                AnyNoun itemToMoveUp = ItemsDisplay.Items[selectedIndex + 1] as AnyNoun;
                _gameMainViewModel.PlayerOne.ItemInventory[selectedIndex] = itemToMoveUp;
                _gameMainViewModel.PlayerOne.ItemInventory[selectedIndex + 1] = itemToMoveDown;
                ItemsDisplay.SelectedIndex = selectedIndex + 1;
            }

        }

        private void TakeItemButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMainViewModel.MoveIndexItemToInventory(ItemSelect.SelectedIndex, _gameMainViewModel.PlayerOne);
        }

        private void DropButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ItemsDisplay.SelectedIndex;
            _gameMainViewModel.DropItemFromInventory(_gameMainViewModel.SelectedInvItem, _gameMainViewModel.PlayerOne);
            if (selectedIndex == ItemsDisplay.Items.Count)
            {
                ItemsDisplay.SelectedIndex = selectedIndex - 1;
            }
            else
            {
                ItemsDisplay.SelectedIndex = selectedIndex;
            }
        }

        private void InvestigateButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMainViewModel.InvestigateIndexItem(ItemSelect.SelectedIndex, _gameMainViewModel.PlayerOne);
        }

        private void UseButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMainViewModel.UseItem(_gameMainViewModel.SelectedInvItem, _gameMainViewModel.PlayerOne);
        }
    }
}
