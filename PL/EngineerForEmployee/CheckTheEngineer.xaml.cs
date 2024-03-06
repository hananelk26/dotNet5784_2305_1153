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

namespace PL.EngineerForEmployee;

/// <summary>
/// Interaction logic for CheckTheEngineer.xaml
/// </summary>
/// 
using System.ComponentModel;

public class EmployeeViewModel : INotifyPropertyChanged
{
    private string _employeeId;

    public string EmployeeId
    {
        get => _employeeId;
        set
        {
            if (_employeeId != value)
            {
                _employeeId = value;
                OnPropertyChanged(nameof(EmployeeId));
            }
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


public partial class CheckTheEngineer : Window
{
    private EmployeeViewModel _viewModel;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public CheckTheEngineer()
    {
        InitializeComponent();
        _viewModel = new EmployeeViewModel();
        DataContext = _viewModel;
    }

    private void ContinueButton_Click(object sender, RoutedEventArgs e)
    {
        string idEngineer = _viewModel.EmployeeId;
        try
        {
           var en = s_bl.Engineer.Read(int.Parse(idEngineer));
            if (en != null)
            {
                new MainWindowEngineer(int.Parse(idEngineer)).ShowDialog();
            }
        }
        catch (Exception ex) 
        {
            MessageBox.Show(ex.Message,"ERROR",MessageBoxButton.OK,MessageBoxImage.Error);
        }

    }

    private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = !e.Text.All(char.IsDigit);
    }


}
