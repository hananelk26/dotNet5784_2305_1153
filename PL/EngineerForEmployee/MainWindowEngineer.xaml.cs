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

namespace PL.EngineerForEmployee
{
    /// <summary>
    /// Interaction logic for MainWindowEngineer.xaml
    /// </summary>
    public partial class MainWindowEngineer : Window
    {
        
        public MainWindowEngineer(int idOfEngineer)
        {
            InitializeComponent();
            IdOfCurrentEngineer = idOfEngineer;
        }
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();


        public int IdOfCurrentEngineer
        {
            get { return (int)GetValue(IdOfCurrentEngineerProperty); }
            set { SetValue(IdOfCurrentEngineerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IdOfCurrentEngineer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IdOfCurrentEngineerProperty =
            DependencyProperty.Register("IdOfCurrentEngineer", typeof(int), typeof(MainWindowEngineer), new PropertyMetadata(null));



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var engineer = s_bl.Engineer.Read(IdOfCurrentEngineer);
                if (engineer!.Task == null)
                    throw new Exception("There is no task for the engineer ");
                var idOfTask = engineer.Task.Id;
                new TaskWindow(idOfTask,true).ShowDialog();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            new EngineerForEmployee.CheckTheEngineer().ShowDialog();
        }
    }
}
