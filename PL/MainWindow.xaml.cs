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
            CustomDateTime = s_bl.Clock;
        }

        private DateTime _customDateTime;

        public DateTime CustomDateTime
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            s_bl.addYear(1);
            CustomDateTime = s_bl.Clock;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            s_bl.addDay(1);
            CustomDateTime = s_bl.Clock;
        }
    }
}
