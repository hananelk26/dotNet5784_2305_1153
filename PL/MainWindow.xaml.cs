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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void EngineerListWindow(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }

        private void Initialization(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult = MessageBox.Show("Would you like to create Initial data?",
                            "warning",
                            MessageBoxButton.YesNoCancel,
                            MessageBoxImage.Warning);
            switch (mbResult)
            {
                case MessageBoxResult.Yes:
                    DalTest.Initialization.Do();
                    break;

            }
        }
    }
}