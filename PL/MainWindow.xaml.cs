using PL.Engineer;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
            // Initialize MediaPlayer
            var mediaPlayer = new MediaPlayer();

            // Set the audio source
            mediaPlayer.Open(new Uri("https://open.spotify.com/track/5zdau2dtmOUHF3CJ3odKfb?si=37770a9acea947a3"));

            // Play the audio
            mediaPlayer.Play();
        }

        /// <summary>
        /// Event handler for the EngineerListWindow button click.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void EngineerListWindow(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }

        /// <summary>
        /// Event handler for the Initialization button click.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void Initialization(object sender, RoutedEventArgs e)
        {

            MessageBoxResult mbResult = MessageBox.Show("Would you like to create Initial data?",
                            "warning",
                            MessageBoxButton.YesNoCancel,
                            MessageBoxImage.Question);
            switch (mbResult)
            {
                case MessageBoxResult.Yes:
                    //  DalTest.Initialization.Do();
                    s_bl.InitializeDB();
                    break;

            }
        }

        /// <summary>
        /// Event handler for the resetAllData button click.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void resetAllData(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show("Would you like to reset all data?",
                            "warning",
                            MessageBoxButton.YesNoCancel,
                            MessageBoxImage.Warning);
            switch (mbResult)
            {
                case MessageBoxResult.Yes:
                    s_bl.ResetsAllEntitiesInTheData();
                    s_bl.resetDataConfig();
                    break;

            }


        }
    }
}