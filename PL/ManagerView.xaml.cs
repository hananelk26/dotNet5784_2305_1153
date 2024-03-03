using PL.Engineer;
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

namespace PL
{
  
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public ManagerView()
        {
            InitializeComponent();
            
           
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
