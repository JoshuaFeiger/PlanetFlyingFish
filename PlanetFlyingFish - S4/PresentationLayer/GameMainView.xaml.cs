using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
                PriorityMessageScroll.ScrollToVerticalOffset(PriorityMessageScroll.ExtentHeight + 40);
            }

            scrollPriorityMessagesOnNextUpdate = true;
        }

        private void SideMessagesChanged(object sender, TextChangedEventArgs e)
        {
            if (scrollSideMessagesOnNextUpdate)
            {
                SideMessageScroll.ScrollToVerticalOffset(SideMessageScroll.ExtentHeight + 35);
            }
            scrollSideMessagesOnNextUpdate = true;
        }

        private bool scrollPriorityMessagesOnNextUpdate { get; set; }

        private bool scrollSideMessagesOnNextUpdate { get; set; }

        private void ConfirmTravelButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMainViewModel.TravelToAreaIndex(TravelToLocationCombo.SelectedIndex);
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
                ItemsDisplay.Focus();
            }
            else
            {
                ItemsDisplay.SelectedIndex = selectedIndex;
                ItemsDisplay.Focus();
            }
        }

        private void UseButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ItemsDisplay.SelectedIndex;
            _gameMainViewModel.UseItem(_gameMainViewModel.SelectedInvItem, _gameMainViewModel.PlayerOne);
            ItemsDisplay.SelectedIndex = selectedIndex;
            ItemsDisplay.Focus();
        }

        private void ItemsDisplay_LostFocus(object sender, RoutedEventArgs e)
        {
            if (DropButton.IsFocused || UseButton.IsFocused)
            {
                ItemsDisplay.SelectedItem = -1;
                _gameMainViewModel.SelectedInvItem = null;
            }
        }

        private void InvestigateButton_Click(object sender, RoutedEventArgs e)
        {
            _gameMainViewModel.InvestigateIndexItem(ItemSelect.SelectedIndex, _gameMainViewModel.PlayerOne);
        }

        private void ItemSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _gameMainViewModel.SelectedAreaItemNumber = ItemSelect.SelectedIndex;
        }

        public Viewbox WindowRatioViewbox
        {
            get { return windowRatioViewbox; }
        }

        private void PriorityMessageBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            
            BeginMessageBoxScrollKeyboard(PriorityMessageBox, PriorityMessageScroll, e);
        }

        private void PriorityMessageScroll_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            EndMessageBoxScrollKeyboard(PriorityMessageBox, PriorityMessageScroll, SideMessageBox, e);
        }

        private void SideMessageBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            BeginMessageBoxScrollKeyboard(SideMessageBox, SideMessageScroll, e);
            
        }

        private void SideMessageScroll_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            EndMessageBoxScrollKeyboard(SideMessageBox, SideMessageScroll, LocationInfoBlock, e);
        }

        private void LocationInfoBlock_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            BeginMessageBoxScrollKeyboard(LocationInfoBlock, LocationInfoScroll, e);
        }

        private void LocationInfoScroll_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            EndMessageBoxScrollKeyboard(LocationInfoBlock, LocationInfoScroll, MapArtSelector, e);
        }

        private void ItemDescriptionBlock_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            BeginMessageBoxScrollKeyboard(ItemDescriptionBlock, ItemDescriptionScroll, e);
        }

        private void ItemDescriptionScroll_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            EndMessageBoxScrollKeyboard(ItemDescriptionBlock, ItemDescriptionScroll, DropButton, e);
        }

        private void BeginMessageBoxScrollKeyboard(TextBox textBox, ScrollViewer scrollViewer, KeyboardFocusChangedEventArgs e)
        {
            if (e.OldFocus != scrollViewer && (Keyboard.IsKeyDown(Key.Tab) || (textBox == ItemDescriptionBlock && Keyboard.IsKeyDown(Key.I))))
            {
                //textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                //scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //textBox.Height = Double.NaN;
                //textBox.IsTabStop = true;
                scrollViewer.Focus();
            }
        }

        private void EndMessageBoxScrollKeyboard(TextBox textBox, ScrollViewer scrollViewer, Control nextInLine, KeyboardFocusChangedEventArgs e)
        {
            if (e.NewFocus != scrollViewer)
            {
                //textBox.Height = textBox.MinHeight;
                //textBox.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                //scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                if (e.NewFocus == textBox /*Keyboard.IsKeyDown(Key.Tab)*/)
                {
                    nextInLine.Focus();
                }
            }
        }

        private void ComboBoxEXKeyInput(object sender, KeyEventArgs e)
        {
            ComboBox senderCombo = sender as ComboBox;
            if (e.Key == Key.Enter)
            {
                senderCombo.IsDropDownOpen = true;
            }
        }

        private void ItemsDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D)
            {
                DropButton.Focus();
            }
            else if (e.Key == Key.U)
            {
                UseButton.Focus();
            }
            else if (e.Key == Key.I)
            {
                ItemDescriptionBlock.Focus();
            }
        }
    }
}
