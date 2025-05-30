﻿using BO;
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



    public TaskWindow(int TheID = 0, bool currentTaskIsAssigned = false)
    {
        InitializeComponent();
        if (TheID == 0)
        {
            CurrentTask = new BO.Task(); // add mode
            CurrentTask.CreatedAtDate = (DateTime)s_bl.MainClock.GetMainClock();
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
        SelectedDependencies = SelectedDependencies.Where(task => task.Id != TheID);// In the situation of updating a task, you need to make sure that the task you want to update does not appear in the dependent list.

        if (s_bl.Time.StartDate() != null)
        {
            DateOfProject = true;
        }
        else
        {
            DateOfProject = false;
        }

        theCurrentTaskIsAssigned = currentTaskIsAssigned;

        if (currentTaskIsAssigned)
        {
            if (CurrentTask.Status == BO.Status.Scheduled)
            {
                TheStartDateButtonHasBeenPressed = false;
            }
            else
            {
                TheStartDateButtonHasBeenPressed = true;
            }
        }
    }

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();



    public bool theCurrentTaskIsAssigned
    {
        get { return (bool)GetValue(theCurrentTaskIsAssignedProperty); }
        set { SetValue(theCurrentTaskIsAssignedProperty, value); }
    }

    // Using a DependencyProperty as the backing store for theCurrentTaskIsAssigned.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty theCurrentTaskIsAssignedProperty =
        DependencyProperty.Register("theCurrentTaskIsAssigned", typeof(bool), typeof(TaskWindow), new PropertyMetadata(null));



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

        //if (s_bl.IsCircularDependency(SelectedDependencies.Where(t => t.IsSelected == true).ToList(),) == false)
        //{
        //}

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
            catch (Exception ex)
            {
                MessageBox.Show("This task does not appear in the system", "Error adding an task", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

    private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
    {

    }

    private void CheckBox_Checked(object sender, RoutedEventArgs e)
    {
        string? alias = (sender as CheckBox)?.Content as string;
        if (alias != null)
        {
            SelectedDependencies = SelectedDependencies.Select(t =>
            {
                if (t.Alias == alias)
                    t.IsSelected = true;
                return t;
            }); // Convert the result back to a list and assign it to SelectedDependencies
        }
    }

    private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
    {
        string? alias = (sender as CheckBox)?.Content as string;
        if (alias != null)
        {
            SelectedDependencies = SelectedDependencies.Select(t =>
            {
                if (t.Alias == alias)
                    t.IsSelected = false;
                return t;
            }); // Convert the result back to a list and assign it to SelectedDependencies
        }
    }



    private void StartTaskButton_Click(object sender, RoutedEventArgs e)
    {
        CurrentTask.StartDate = s_bl.MainClock.GetMainClock();
        CurrentTask.Status = Status.OnTrack;
        try
        {
            s_bl.Task.Update(CurrentTask);
            // Display a success message
            MessageBox.Show("The task started successfully",
                                "task update",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            this.Close();
        }
        catch (Exception)
        {
            MessageBox.Show("This task does not appear in the system", "Error adding an task", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void FinishTaskButton_Click(object sender, RoutedEventArgs e)
    {
        CurrentTask.CompleteDate = s_bl.MainClock.GetMainClock();
        CurrentTask.Status = BO.Status.Done;
        try
        {
            s_bl.Task.Update(CurrentTask);
            // Display a success message
            MessageBox.Show("The task complete successfully",
                                "task update",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            this.Close();
        }
        catch (Exception)
        {
            MessageBox.Show("This task does not appear in the system", "Error adding an task", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }



    public bool TheStartDateButtonHasBeenPressed
    {
        get { return (bool)GetValue(TheStartDateButtonHasBeenPressedProperty); }
        set { SetValue(TheStartDateButtonHasBeenPressedProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TheStartDateButtonHasBeenPressed.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TheStartDateButtonHasBeenPressedProperty =
        DependencyProperty.Register("TheStartDateButtonHasBeenPressed", typeof(bool), typeof(TaskWindow), new PropertyMetadata(null));


}
