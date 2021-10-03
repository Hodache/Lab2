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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            TaskLabel.Content = "Процент";
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            TaskLabel.Content = "Целевой\nвклад";
        }

        private void DepositTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TaskTextBox.Focus();
            }
        }
        private void TaskTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CalculateButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
