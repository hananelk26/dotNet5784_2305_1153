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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        public EngineerWindow(int TheID = 0)
        {
            InitializeComponent();
            if (TheID == 0)
            {
                CurrentEngineer = new BO.Engineer();
            }
            else
            {
                try
                {
                    CurrentEngineer = s_bl.Engineer.Read(TheID)!;
                }
                catch (Exception)
                {
                    Console.WriteLine("There is no engineer in the system with such an ID card");
                }
            }
        }

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();



        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentEngineer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));



        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0) || (e.Text == "." && ((TextBox)sender).Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        private void LetterTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allowing only letters
            if (!char.IsLetter(e.Text, 0))
            {
                e.Handled = true; // Prevent the non-letter character from being added
            }
        }

        private void AddOrUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (sender as Button)!;
            string buttonText = clickedButton.Content.ToString()!;
            if (buttonText == "Add")
            {
                try
                {
                    s_bl.Engineer.Create(CurrentEngineer);
                    MessageBox.Show("The engineer was successfully added",
                                        "Adding an engineer",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception me)
                {
                    MessageBox.Show(me.Message, "Error adding an engineer", MessageBoxButton.OK, MessageBoxImage.Error);
                   
                }
            }
            else
            {
                try
                {
                    s_bl.Engineer.Update(CurrentEngineer);
                    MessageBox.Show("The engineer updated successfully",
                                        "Engineer update",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("This engineer does not appear in the system", "Error adding an engineer", MessageBoxButton.OK, MessageBoxImage.Error);
                    Console.WriteLine("This engineer does not appear in the system.");
                }
            }
        }
    }

}
