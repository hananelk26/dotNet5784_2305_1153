using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL;

/// <summary>
/// Converts an ID value to content for display purposes.
/// </summary>
class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


class ConvertDateForProjectToVisibility : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Visibility.Collapsed : Visibility.Visible;
        }

        return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


class ConvertDateForProjectToVisibilityFromTrueTOVisibility : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class ConvertDateForProjectToIsEnabled : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (value is bool boolValue)
        {
            return !boolValue;

        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

class ConvertDateToWidth : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get(); //get the Bl instance
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is BO.Task task)
        {
            TimeSpan requiredEffortTimeOfCurrentTask = (TimeSpan)task.RequiredEfforTime!; //get the task duration
            TimeSpan allProjectDuration;
            try
            {
                allProjectDuration = (TimeSpan)(s_bl.Task.EndDateOfProject(s_bl.Time.StartDate())! - s_bl.Time.StartDate()!)!; //get the project duration
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;

            }

            return (requiredEffortTimeOfCurrentTask / allProjectDuration) * 1000; //return the task width
        }
        return 0;
    }


    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }



}


class ConvertDateToMargin : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get(); //get the Bl instance

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is BO.Task task)
        {
            DateTime ScheduledDateOfCurrentTask = (DateTime)task.ScheduledDate!; //get the task duration

            TimeSpan allProjectDuration;
            DateTime startDateOfProject;
            DateTime endDateOfProject;

            try
            {
                startDateOfProject = (DateTime)s_bl.Time.StartDate()!;
                endDateOfProject = (DateTime)s_bl.Task.EndDateOfProject(startDateOfProject)!;
                allProjectDuration = (TimeSpan)(endDateOfProject - startDateOfProject); //get the project duration
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return 0;

            }

            return new Thickness((((TimeSpan)(ScheduledDateOfCurrentTask - startDateOfProject) / allProjectDuration) * 1000), 0, 0, 0); //return the task margin
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


class ConvertStatusToColor : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is BO.Task task)
        {
            if (task.Status == BO.Status.Done)
                return "#e6e619";
            if (task.Status == BO.Status.OnTrack)
                return "#808080";
            if (task.Status == BO.Status.Scheduled)
                return "#5353ec";
        }
        return 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


