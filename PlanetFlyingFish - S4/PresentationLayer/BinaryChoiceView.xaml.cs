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
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Interop;

namespace PlanetFlyingFish.PresentationLayer
{
    /// <summary>
    /// Interaction logic for BinaryChoiceView.xaml
    /// Close button disabling logic source: https://stackoverflow.com/a/17962671
    /// </summary>
    public partial class BinaryChoiceView : Window
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        const uint MF_BYCOMMAND = 0x00000000;
        const uint MF_GRAYED = 0x00000001;

        const uint SC_CLOSE = 0xF060;

        private bool _disableToolbarCloseButton;

        public enum CloseButton
        {
            TOOLBAR,
            BUTTON1,
            BUTTON2
        }

        private CloseButton _windowClosedWith;

        public CloseButton WindowClosedWith
        {
            get { return _windowClosedWith; }
        }

        public BinaryChoiceView()
        {
            InitializeComponent();
        }

        public BinaryChoiceView(string Message, string Button1Text, string Button2Text)
        {
            InitializeComponent();
            text.Text = Message;
            button1.Content = Button1Text;
            button2.Content = Button2Text;
            _disableToolbarCloseButton = false;
        }

        public BinaryChoiceView(string Message, string Button1Text, string Button2Text, bool DisableToolbarCloseButton)
        {
            InitializeComponent();
            text.Text = Message;
            button1.Content = Button1Text;
            button2.Content = Button2Text;
            _disableToolbarCloseButton = DisableToolbarCloseButton;
        }

        public static CloseButton Show(string message, string button1Text, string button2Text)
        {
            BinaryChoiceView binaryChoiceView = new BinaryChoiceView(message, button1Text, button2Text);
            binaryChoiceView.ShowDialog();
            return binaryChoiceView.WindowClosedWith;
        }

        public static CloseButton Show(string message, string button1Text, string button2Text, bool disableToolbarCloseButton)
        {
            BinaryChoiceView binaryChoiceView = new BinaryChoiceView(message, button1Text, button2Text, disableToolbarCloseButton);
            binaryChoiceView.ShowDialog();
            return binaryChoiceView.WindowClosedWith;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            if (_disableToolbarCloseButton)
            {
                base.OnSourceInitialized(e);

                // Disable toolbar close button
                IntPtr hwnd = new WindowInteropHelper(this).Handle;
                IntPtr hMenu = GetSystemMenu(hwnd, false);
                if (hMenu != IntPtr.Zero)
                {
                    EnableMenuItem(hMenu, SC_CLOSE, MF_BYCOMMAND | MF_GRAYED);
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _windowClosedWith = CloseButton.BUTTON1;
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            _windowClosedWith = CloseButton.BUTTON2;
            this.Close();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Application curApp = System.Windows.Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.SizeToContent = SizeToContent.Manual;
            try
            {
                GameMainView gameMainView = mainWindow as GameMainView; 
                if (Math.Round(gameMainView.WindowRatioViewbox.ActualWidth) + 16 == Math.Round(gameMainView.ActualWidth))
                {
                    this.Width = 200 * mainWindow.ActualWidth / mainWindow.MinWidth;
                    this.Height = this.Height * mainWindow.ActualWidth / mainWindow.MinWidth;
                }
                else
                {
                    this.Width = 200 * mainWindow.ActualHeight / mainWindow.MinHeight;
                    this.Height = this.Height * mainWindow.ActualHeight / mainWindow.MinHeight;
                }
            }
            catch (Exception)
            {
                this.Width = 200;
            }
            this.WindowRatioViewbox.Width = this.Width;
            this.WindowRatioViewbox.Height = this.Height;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.Measure(new Size());
            double leftPos, topPos;
            if (mainWindow.WindowState == WindowState.Maximized)
            {
                //If maximized, behave differently?
                leftPos = (System.Windows.SystemParameters.PrimaryScreenWidth - this.ActualWidth) / 2;
                topPos = (System.Windows.SystemParameters.PrimaryScreenHeight - this.ActualHeight) / 2;
                this.Left = leftPos;
                this.Top = topPos;
            }
            else
            {
                leftPos = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2;
                topPos = mainWindow.Top + (mainWindow.Height - this.ActualHeight) / 2;
            }
            this.Left = leftPos;
            this.Top = topPos;
            button1.Focus();
        }
    }
}
