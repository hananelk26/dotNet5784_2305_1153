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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL;

/// <summary>
/// Interaction logic for GanttWindow.xaml
/// </summary>
public partial class GanttWindow : Window
{
    public GanttWindow()
    {
        InitializeComponent();
        StartDateOfProject = s_bl.Time.StartDate();
        EndDateOfProject = s_bl.Task.EndDateOfProject(StartDateOfProject);
        TaskList = s_bl.Task.ReadAll()
                   .OrderBy(task => task.ScheduledDate)
                   .ThenBy(task => task.Id);


    }

    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();




    public DateTime? EndDateOfProject
    {
        get { return (DateTime?)GetValue(EndDateOfProjectProperty); }
        set { SetValue(EndDateOfProjectProperty, value); }
    }

    // Using a DependencyProperty as the backing store for EndDateOfProject.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty EndDateOfProjectProperty =
        DependencyProperty.Register("EndDateOfProject", typeof(DateTime?), typeof(GanttWindow), new PropertyMetadata(null));





    public DateTime? StartDateOfProject
    {
        get { return (DateTime?)GetValue(StartDateOfProjectProperty); }
        set { SetValue(StartDateOfProjectProperty, value); }
    }

    // Using a DependencyProperty as the backing store for StartDateOfProject.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StartDateOfProjectProperty =
        DependencyProperty.Register("StartDateOfProject", typeof(DateTime?), typeof(GanttWindow), new PropertyMetadata(null));





    public IEnumerable<BO.Task> TaskList
    {
        get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TaskList.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(GanttWindow), new PropertyMetadata(null));

    private void ListBox_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
    {
        if (sender is ListBox listBox)
        {
            // Get the ScrollViewer within the ListBox
            ScrollViewer scrollViewer = GetScrollViewer(listBox);

            if (scrollViewer != null)
            {
                double delta = e.Delta;
                double scrollSpeed = 50.0; // Adjust the scroll speed as needed

                // Calculate the new offset based on the direction of the mouse wheel
                double newOffset = scrollViewer.VerticalOffset - (delta / scrollSpeed);

                // Ensure the new offset is within bounds
                newOffset = Math.Max(0, Math.Min(newOffset, scrollViewer.ScrollableHeight));

                // Set the new vertical offset directly without animation
                scrollViewer.ScrollToVerticalOffset(newOffset);

                e.Handled = true;
            }
        }
    }

    private ScrollViewer GetScrollViewer(DependencyObject depObj)
    {
        if (depObj is ScrollViewer viewer)
        {
            return viewer;
        }

        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

            ScrollViewer childViewer = GetScrollViewer(child);
            if (childViewer != null)
            {
                return childViewer;
            }
        }

        return null;
    }



}