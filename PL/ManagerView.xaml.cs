using PL.Engineer;
using PL.Task;
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

namespace PL;


/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class ManagerView : Window
{
    public ManagerView()
    {
        InitializeComponent();
        if (s_bl.Time.StartDate() == null)
        {
            isADateForProject = false;
        }
        else
        {
            isADateForProject = true;
        }
       
        isResetButtonHasBeenPressed = false;
        DateOfProject = s_bl.Time.StartDate();  

    }

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

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
                isResetButtonHasBeenPressed = false;
                break;
            default:
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
                isResetButtonHasBeenPressed = true;
                break;
            default:
                break;

        }

        isADateForProject = false;
    }

    private void TaskListButton_Click(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }

    private void DateOfStartProjectDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
    {
        var Date = (DateTime)((sender as DatePicker).SelectedDate);
        s_bl.Time.SetStartDate(Date);
        s_bl.Task.PutDatesOnAllExistingTasks(Date);
        isADateForProject = true;
        DateOfProject = s_bl.Time.StartDate();
    }



    public bool isADateForProject
    {
        get { return (bool)GetValue(isADateForProjectProperty); }
        set { SetValue(isADateForProjectProperty, value); }
    }

    // Using a DependencyProperty as the backing store for isADateForProject.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty isADateForProjectProperty =
        DependencyProperty.Register("isADateForProject", typeof(bool), typeof(ManagerView), new PropertyMetadata(null));

    private void GantButton_Click(object sender, RoutedEventArgs e)
    {
        new GanttWindow().Show();
    }




    public bool isResetButtonHasBeenPressed
    {
        get { return (bool)GetValue(isResetButtonHasBeenPressedProperty); }
        set { SetValue(isResetButtonHasBeenPressedProperty, value); }
    }

    // Using a DependencyProperty as the backing store for isResetButtonHasBeenPressed.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty isResetButtonHasBeenPressedProperty =
        DependencyProperty.Register("isResetButtonHasBeenPressed", typeof(bool), typeof(ManagerView), new PropertyMetadata(null));





    public DateTime? DateOfProject
    {
        get { return (DateTime?)GetValue(DateOfProjectProperty); }
        set { SetValue(DateOfProjectProperty, value); }
    }

    // Using a DependencyProperty as the backing store for DateOfProject.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DateOfProjectProperty =
        DependencyProperty.Register("DateOfProject", typeof(DateTime?), typeof(ManagerView), new PropertyMetadata(null));






}
