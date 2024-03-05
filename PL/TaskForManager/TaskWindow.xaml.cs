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

namespace PL.Task
{
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
                    CurrentTask =  s_bl.Task.Read(TheID)!; // Update mode
                }
                catch (Exception)
                {
                    Console.WriteLine("There is no task in the system with such an ID card");
                }
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



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
