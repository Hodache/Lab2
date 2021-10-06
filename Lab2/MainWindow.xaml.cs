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

            // Восстановление информации о радио кнопках 
            percentRadioBtn.IsChecked = Properties.Settings.Default.percentRadioBtn;
            depositSumRadioBtn.IsChecked = Properties.Settings.Default.depositSumRadioBtn;

            // Восстановление информации в полях ввода
            depositTextBox.Text = Properties.Settings.Default.depositTextBox;
            taskTextBox.Text = Properties.Settings.Default.taskTextBox;
        }

        // Изменение текста в taskLabel в зависимости от радио кнопок и сохранение нажатой
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            taskLabel.Content = "Процент";
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            taskLabel.Content = "Целевой\nвклад";
        }

        // Реакция на нажатия клавиши Enter в полях ввода
        private void DepositTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                taskTextBox.Focus();
            }
        }
        private void TaskTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CalculateButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        // Реакция на нажатие кнопки "вычислить месяц"
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)percentRadioBtn.IsChecked)
            {
                float deposit, percent;

                try
                {
                    deposit = float.Parse(depositTextBox.Text);
                    percent = float.Parse(taskTextBox.Text);
                }
                catch (FormatException) // тип ошибки, которую перехватываем
                {
                    MessageBox.Show("Поля ввода должны содержать числа!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; // прерываем обработчик клика, если ввели какую-то ерунду
                }

                if (deposit <= 0 || percent <= 0)
                {
                    MessageBox.Show("Размер вклада и процент должны быть больше нуля!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int month = Logic.FindPercentMonth(deposit, percent);

                    answerTextBlock.Text = $"Месяцев до превышения процента ({percent}): {month}";
                }
            }
            else if ((bool)depositSumRadioBtn.IsChecked) {
                float deposit, depositSum;

                try
                {
                    deposit = float.Parse(depositTextBox.Text);
                    depositSum = float.Parse(taskTextBox.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Поля ввода должны содержать числа!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (deposit <= 0 || depositSum <= deposit)
                {
                    MessageBox.Show("Изначальный размер вклада должен быть больше нуля, а целевой размер вклада больше чем изначальный!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int month = Logic.FindDepositSumMonth(deposit, depositSum);

                    answerTextBlock.Text = $"Месяцев до превышения размера вклада ({depositSum}): {month}";
                }
            }
            else
            {
                MessageBox.Show("Ошибка выбора!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Вывод задания
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Гражданин 1 марта открыл счет в банке, вложив A руб. " +
                "Через каждый месяц размер вклада увеличивается на 2% от имеющейся суммы. " +
                "Определить: а) за какой месяц величина ежемесячного увеличения вклада превысит B руб.; " +
                "б) через сколько месяцев размер вклада превысит C руб", "Задание");
        }

        // Очистка полей ввода
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            depositTextBox.Text = "";
            taskTextBox.Text = "";
        }

        // При закрытии окна
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Изменение стандартных значений полей ввода (для восстановления)
            Properties.Settings.Default.depositTextBox = depositTextBox.Text;
            Properties.Settings.Default.taskTextBox = taskTextBox.Text;

            // Изменение стандартных значений радио кнопок
            Properties.Settings.Default.percentRadioBtn = (bool)percentRadioBtn.IsChecked;
            Properties.Settings.Default.depositSumRadioBtn = (bool)depositSumRadioBtn.IsChecked;

            // Сохранение изменений
            Properties.Settings.Default.Save();
        }
    }

    public class Logic
    {
        // Находим месяц превышения процента
        public static int FindPercentMonth(float deposit, float percent)
        {
            float increase = 0; // Ежемесячное увеличение
            int percentMonth = 0; // Счетчик месяцев

            // Высчитываем очередное увеличене
            while (increase <= percent)
            {
                increase = deposit / 50;
                deposit += increase;
                percentMonth++;
            }

            return percentMonth;
        }

        // Находим месяц превышения вклада
        public static int FindDepositSumMonth(float deposit, float depositSum)
        {
            int depostiSumMonth = 0; // Счетчик месяцев

            while (deposit <= depositSum)
            {
                depostiSumMonth++;
                deposit += deposit / 50;
            }

            return depostiSumMonth;
        }
    }
}
