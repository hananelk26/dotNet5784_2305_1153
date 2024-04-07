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
        public TaskListWindow(int TheIdOfEngineer = 0)
        {
            if (s_bl.Time.StartDate() != null)
            {
                DateForeProject = true;
            }
            else
            {
                DateForeProject = false;
            }
            InitializeComponent();

            ListTaskForSpesificEngineer = false;

            if (TheIdOfEngineer != 0)
            {
                var TheExperience = s_bl.Engineer.Read(TheIdOfEngineer)!.Level;
                var tasks = s_bl.Task.ReadAllTaskInList(t => t.Complexyity <= TheExperience && t.Status == BO.Status.Scheduled && (t.Dependencies == null || t.Dependencies.Any(x => x.Status != BO.Status.Done) == false));
                TaskList = tasks.ToList();
                ListTaskForSpesificEngineer = true;
            }

            IdOfTheEngineer = TheIdOfEngineer;
        }

        static int IdOfTheEngineer;

        public bool ListTaskForSpesificEngineer
        {
            get { return (bool)GetValue(ListTaskForSpesificEngineerProperty); }
            set { SetValue(ListTaskForSpesificEngineerProperty, value); }
        }

        public static readonly DependencyProperty ListTaskForSpesificEngineerProperty =
            DependencyProperty.Register("ListTaskForSpesificEngineer", typeof(bool), typeof(TaskListWindow), new PropertyMetadata(null));

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        /// <summary>
        /// Gets or sets the experience level of an task.
        /// </summary>
        public BO.EngineerExperience Experience { get; set; } = BO.EngineerExperience.None;

        public BO.Status StatusOfTask { get; set; } = BO.Status.None;

        private void TaskExperience_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ListTaskForSpesificEngineer)
            {
                TaskList = (Experience == BO.EngineerExperience.None) ?
      s_bl?.Task.ReadAllTaskInList()! : s_bl?.Task.ReadAllTaskInList(item => item.Complexyity == Experience)!;
            }
        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ListTaskForSpesificEngineer)
            {
                TaskList = (StatusOfTask == BO.Status.None) ?
        s_bl?.Task.ReadAllTaskInList()! : s_bl?.Task.ReadAllTaskInList(item => item.Status == StatusOfTask)!;
            }
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
            if (!ListTaskForSpesificEngineer)
            {
                BO.TaskInList? TheTask = (sender as ListView)?.SelectedItem as BO.TaskInList;
                new TaskWindow(TheTask!.Id).ShowDialog();
                TaskList = s_bl?.Task.ReadAllTaskInList()!;
            }
            else
            {
                try
                {

                    var task = s_bl.Task.ReadAll(t => t.Status == Status.Scheduled && t.Engineer != null && t.Engineer.Id == IdOfTheEngineer).FirstOrDefault();
                    if (task != null) // In the event that a task that has not yet started is listed under the engineer's name, then the task is removed from that engineer because he wants to take another task
                    {
                        task.Engineer = null;
                        s_bl.Task.Update(task);
                    }

                    var currentEngineer = s_bl.Engineer.Read(IdOfTheEngineer);
                    if (currentEngineer!.Task == null || (s_bl.Task.Read(currentEngineer.Task!.Id)!).Status != Status.OnTrack)// check that there is no  task that assigned to engineer and not completed
                    {
                        BO.TaskInList? TheTask = (sender as ListView)?.SelectedItem as BO.TaskInList;
                        var TaskOfEngineer = s_bl.Task.Read(TheTask!.Id);
                        TaskOfEngineer!.Engineer = new EngineerInTask()
                        {
                            Id = IdOfTheEngineer,
                            Name = s_bl.Engineer.Read(IdOfTheEngineer)!.Name
                        };

                        s_bl.Task.Update(TaskOfEngineer);
                        // Display a success message
                        MessageBox.Show("The task was successfully assigned",
                                            "task assignment",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
                    }
                    else
                    {
                        throw new Exception("The engineer cannot select a task because he is assigned a task that has not yet been completed");
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error assigning a task", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // mode of adding
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ListTaskForSpesificEngineer && !DateForeProject)
            {
                new TaskWindow().ShowDialog();
                TaskList = s_bl?.Task.ReadAllTaskInList()!;
            }
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
