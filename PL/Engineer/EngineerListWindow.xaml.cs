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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        public EngineerListWindow()
        {
            InitializeComponent();
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        /// <summary>
        /// Gets or sets the list of engineers displayed in the EngineerListWindow.
        /// </summary>
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        /// <summary>
        /// Identifies the EngineerList dependency property.
        /// </summary>
        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the experience level of an engineer.
        /// </summary>
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;

        /// <summary>
        /// Event handler for the EngineerExperience ComboBox SelectionChanged event.
        /// Updates the EngineerList based on the selected experience level.
        /// </summary>
        /// <param name="sender">The object that raised the event (a ComboBox).</param>
        /// <param name="e">Event data containing information about the selection change.</param>
        private void EngineerExperience_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (Experience == BO.EngineerExperience.None) ?
             s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == Experience)!;

        }

        /// <summary>
        /// Event handler for the AddButton Click event.
        /// Opens a new EngineerWindow for adding an engineer and updates the EngineerList.
        /// </summary>
        /// <param name="sender">The object that raised the event (a Button).</param>
        /// <param name="e">Event data containing information about the click event.</param>
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }

        /// <summary>
        /// Event handler for the ListViewOfEngineers MouseDoubleClick event.
        /// Opens a new EngineerWindow for editing an engineer and updates the EngineerList.
        /// </summary>
        /// <param name="sender">The object that raised the event (a ListView).</param>
        /// <param name="e">Event data containing information about the double-click event.</param>
        private void ListViewOfEngineers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? TheEngineer = (sender as ListView)?.SelectedItem as BO.Engineer;
            new EngineerWindow(TheEngineer!.Id).ShowDialog();
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }
    }
}
