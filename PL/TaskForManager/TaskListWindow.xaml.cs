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

            if (s_bl.Time.StartDate() != null)
            {
                DateForeProject = true;
            }
            else
            {
                DateForeProject = false;
            }
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
            s_bl?.Task.ReadAllTaskInList()! : s_bl?.Task.ReadAllTaskInList(item => item.Complexyity == Experience)!;
        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (StatusOfTask == BO.Status.None) ?
           s_bl?.Task.ReadAllTaskInList()! : s_bl?.Task.ReadAllTaskInList(item => item.Status == StatusOfTask)!;
        }


        /// <summary>
        /// Gets or sets the list of engineers displayed in the EngineerListWindow.
        /// </summary>
        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        /// <summary>
        /// Identifies the TaskList dependency property.
        /// </summary>
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>), typeof(TaskListWindow), new PropertyMetadata(null));


        // mode of update
        private void ListViewOfTask_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.TaskInList? TheTask = (sender as ListView)?.SelectedItem as BO.TaskInList;
            new TaskWindow(TheTask!.Id).ShowDialog();
            TaskList = s_bl?.Task.ReadAllTaskInList()!;
        }

        // mode of adding
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
            TaskList = s_bl?.Task.ReadAllTaskInList()!;
        }

        public bool DateForeProject
        {
            get { return (bool)GetValue(DateForeProjectProperty); }
            set { SetValue(DateForeProjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DateForeProject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateForeProjectProperty =
            DependencyProperty.Register("DateForeProject", typeof(bool), typeof(TaskWindow), new PropertyMetadata(null));

    }
}
