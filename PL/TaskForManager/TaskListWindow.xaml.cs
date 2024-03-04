using BO;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TakkListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        public TaskListWindow()
        {
            InitializeComponent();
        }

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        /// <summary>
        /// Gets or sets the experience level of an task.
        /// </summary>
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;

        public BO.Status StatusOfTask { get; set; } = BO.Status.None;

        private void TaskExperience_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (Experience == BO.EngineerExperience.None) ?
            s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Complexyity == Experience)!;
        }

        

        /// <summary>
        /// Gets or sets the list of engineers displayed in the EngineerListWindow.
        /// </summary>
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        /// <summary>
        /// Identifies the TaskList dependency property.
        /// </summary>
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));

        private void ListViewOfEngineers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (StatusOfTask == BO.Status.None) ?
           s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Status == StatusOfTask)!;
        }
    }
}
