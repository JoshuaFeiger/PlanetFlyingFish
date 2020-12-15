using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PlanetFlyingFish.PresentationLayer
{
    /// <summary>
    /// Interaction logic for MessageView.xaml
    /// </summary>
    public partial class MessageView : Window
    {
        public MessageView(string Message)
        {
            
            InitializeComponent();
            text.Text = Message;
            okButton.Content = "OK";
            //this.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            //this.Arrange(new Rect(this.DesiredSize));
            

        }

        public MessageView(string Message, string ButtonText)
        {
            InitializeComponent();
            text.Text = Message;
            okButton.Content = ButtonText;
        }

        public static void Show(string message)
        {
            MessageView messageView = new MessageView(message);
            messageView.ShowDialog();
        }

        public static void Show(string message, string buttonText)
        {
            MessageView messageView = new MessageView(message, buttonText);
            messageView.ShowDialog();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
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
            okButton.Focus();
        }
    }
}
