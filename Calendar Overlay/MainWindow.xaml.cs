using System;
using System.Windows;
using System.Windows.Shell;

namespace Calendar_Overlay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MouseMoveHandler(object sender, System.Windows.Input.MouseEventArgs e)
        {
            double yPosition = e.GetPosition(this).Y;
            if (yPosition < 90)
            {
                settingsPanel.Visibility = Visibility.Visible;
            }
            else
            {
                settingsPanel.Visibility = Visibility.Hidden;
            }
        }

        private void LoadWindowProperties(object sender, EventArgs e)
        {
            this.Top = Properties.Settings.Default.WindowTop;
            this.Left = Properties.Settings.Default.WindowLeft;
            this.Height = Properties.Settings.Default.WindowHeight;
            this.Width = Properties.Settings.Default.WindowWidth;
        }

        private void SaveWindowProperties(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.WindowTop = this.Top;
            Properties.Settings.Default.WindowLeft = this.Left;
            Properties.Settings.Default.WindowHeight = this.Height;
            Properties.Settings.Default.WindowWidth = this.Width;

            Properties.Settings.Default.Save();
        }

        private void ToggleClick(object sender, RoutedEventArgs e)
        {
            // the window can be resized but you can no longer close it with the button
            // there seems to be something that overlays the close button so it doesn't always register the click
            Application.Current.MainWindow.ResizeMode = ResizeMode.CanResize;
            WindowChrome chrome = new();
            chrome.ResizeBorderThickness = new Thickness(4);
            WindowChrome.SetWindowChrome(Application.Current.MainWindow, chrome);
        }
    }
}
