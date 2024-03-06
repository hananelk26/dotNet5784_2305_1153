using BO;
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

namespace PL.Task;

/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
public partial class TaskWindow : Window
{

 

    public TaskWindow(int TheID = 0)
    {
        InitializeComponent();
        if (TheID == 0)
        {
            CurrentTask = new BO.Task(); // add mode
        }
        else
        {
            try
            {
                CurrentTask = s_bl.Task.Read(TheID)!; // Update mode
            }
            catch (Exception)
            {
                Console.WriteLine("There is no task in the system with such an ID card");
            }
        }

        CurrentTask.Dependencies ??= new List<TaskInList>();
          
        // mark our dependencies
        SelectedDependencies = s_bl.Task.ReadAllTaskInList()
            .Select(t =>
            {
                // check if this task exists in dependencies and mark it
                if (CurrentTask.Dependencies.Find(t2 => t.Id == t2.Id) != null)
                {
                    t.IsSelected = true;
                }
                return t;
            });

        if (s_bl.Time.StartDate() != null)
        {
            DateOfProject = true;
        }
        else
        {
            DateOfProject= false;
        }


    }

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


    public BO.Task CurrentTask
    {
        get { return (BO.Task)GetValue(CurrentTaskProperty); }
        set { SetValue(CurrentTaskProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CurrentTask.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentTaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));

    public IEnumerable<TaskInList> SelectedDependencies // temporary list to save dependencies for task
    {
        get { return (IEnumerable<TaskInList>)GetValue(SelectedDependenciesProperty); }
        set { SetValue(SelectedDependenciesProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CurrentTask.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty SelectedDependenciesProperty =
        DependencyProperty.Register("SelectedDependencies", typeof(IEnumerable<TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));



    public bool DateOfProject
    {
        get { return (bool)GetValue(DateOfProjectProperty); }
        set { SetValue(DateOfProjectProperty, value); }
    }

    // Using a DependencyProperty as the backing store for DateForeProject.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DateOfProjectProperty =
        DependencyProperty.Register("DateOfProject", typeof(bool), typeof(TaskWindow), new PropertyMetadata(null));



    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void AddOrUpdateButton_Click(object sender, RoutedEventArgs e)
    {
        // Cast the sender to a Button
        Button clickedButton = (sender as Button)!;
        // Retrieve the text content of the clicked button
        string buttonText = clickedButton.Content.ToString()!;

        CurrentTask.Dependencies = SelectedDependencies.Where(t => t.IsSelected == true).ToList();


        // Check if the button text is "Add"
        if (buttonText == "Add")
        {
            try
            {
                s_bl.Task.Create(CurrentTask);
                // Display a success message
                MessageBox.Show("The task was successfully added",
                                    "Adding an task",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception me)
            {
                MessageBox.Show(me.Message, "Error adding an task", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        else
        {
            try
            {
                s_bl.Task.Update(CurrentTask);
                // Display a success message
                MessageBox.Show("The task updated successfully",
                                    "task update",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("This task does not appear in the system", "Error adding an task", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine("This task does not appear in the system.");
            }
        }

    }

    private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
    {

    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}
