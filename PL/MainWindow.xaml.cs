using PL.EngineerForEmployee;
using System;
using System.ComponentModel;
using System.Windows;

namespace PL
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            CustomDateTime = s_bl.MainClock.GetMainClock();
        }

        private DateTime? _customDateTime;

        public DateTime? CustomDateTime
        {
            get { return _customDateTime; }
            set
            {
                if (_customDateTime != value)
                {
                    _customDateTime = value;
                    OnPropertyChanged(nameof(CustomDateTime));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new ManagerView().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new CheckTheEngineer().ShowDialog();
        }

        private void AddYearButton_Click(object sender, RoutedEventArgs e)
        {
            s_bl.MainClock.addYear(1);
            CustomDateTime = s_bl.MainClock.GetMainClock();
        }

        private void AddDayButton_Click(object sender, RoutedEventArgs e)
        {
            s_bl.MainClock.addDay(1);
            CustomDateTime = s_bl.MainClock.GetMainClock();
        }

        private void ReseteButton_Click(object sender, RoutedEventArgs e)
        {
            s_bl.MainClock.reset();
            CustomDateTime = s_bl.MainClock.GetMainClock();
        }
    }
}
