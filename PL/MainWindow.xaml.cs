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



        public DateTime? CustomDateTime
        {
            get { return (DateTime?)GetValue(CustomDateTimeProperty); }
            set { SetValue(CustomDateTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomDateTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CustomDateTimeProperty =
            DependencyProperty.Register("CustomDateTime", typeof(DateTime?), typeof(MainWindow), new PropertyMetadata(null));



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
            int y = 1;
            s_bl.MainClock.addYear(y);
            CustomDateTime = s_bl.MainClock.GetMainClock();
        }

        private void AddDayButton_Click(object sender, RoutedEventArgs e)
        {
            int d=1;
            s_bl.MainClock.addDay(d);
            CustomDateTime = s_bl.MainClock.GetMainClock();
        }

        private void ReseteButton_Click(object sender, RoutedEventArgs e)
        {
            s_bl.MainClock.reset();
            CustomDateTime = s_bl.MainClock.GetMainClock();
        }

        private void addMonth_Click(object sender, RoutedEventArgs e)
        {
            s_bl.MainClock.addMonth(1);
            CustomDateTime = s_bl.MainClock.GetMainClock();
        }
        
    }
}
