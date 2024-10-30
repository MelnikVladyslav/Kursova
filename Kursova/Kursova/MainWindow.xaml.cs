using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Kursova
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Метод для збереження симплекс-таблиці у бінарний файл
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Binary Files (*.bin)|*.bin",
                Title = "Save Simplex Table as Binary File"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (BinaryWriter writer = new BinaryWriter(File.Open(saveFileDialog.FileName, FileMode.Create)))
                    {
                        // Записуємо дані з текстового поля у файл
                        writer.Write(InputTextBox.Text);
                    }
                    MessageBox.Show("File saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Метод для завантаження симплекс-таблиці з бінарного файлу
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Binary Files (*.bin)|*.bin",
                Title = "Open Simplex Table Binary File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.Open)))
                    {
                        // Читаємо дані з файлу і завантажуємо їх у текстове поле
                        InputTextBox.Text = reader.ReadString();
                    }
                    MessageBox.Show("File loaded successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Обробник події для пункту "Exit"
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RunSimplexMethod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Перевірка валідності введених даних
                if (!IsValidTableau(InputTextBox.Text))
                {
                    return;
                }

                // Введіть симплекс-таблицю в наступному форматі: 
                // Спочатку кожен рядок матриці, розділений комами і новими рядками.
                var tableau = ParseTableau(InputTextBox.Text);
                var result = SolveSimplex(tableau);
                ResultTextBlock.Text = result;
            }
            catch (Exception ex)
            {
                ResultTextBlock.Text = "Error: " + ex.Message;
            }
        }

        private double[,] ParseTableau(string input)
        {
            var rows = input.Trim().Split('\n');
            var tableau = new double[rows.Length, rows[0].Split(',').Length];

            for (int i = 0; i < rows.Length; i++)
            {
                var columns = rows[i].Trim().Split(',');
                for (int j = 0; j < columns.Length; j++)
                {
                    tableau[i, j] = double.Parse(columns[j]);
                }
            }

            return tableau;
        }

        private string SolveSimplex(double[,] tableau)
        {
            int rows = tableau.GetLength(0);
            int cols = tableau.GetLength(1);

            while (true)
            {
                // Шукаємо ведучий стовпець (стовпець з найменшим коефіцієнтом у рядку Z)
                int pivotCol = -1;
                double minVal = 0;
                for (int j = 0; j < cols - 1; j++)
                {
                    if (tableau[rows - 1, j] < minVal)
                    {
                        minVal = tableau[rows - 1, j];
                        pivotCol = j;
                    }
                }

                // Якщо немає від'ємних елементів, отримуємо оптимальний розв'язок
                if (pivotCol == -1)
                    break;

                // Шукаємо ведучий рядок (рядок з найменшим значенням відношення результату до коефіцієнта)
                int pivotRow = -1;
                double minRatio = double.PositiveInfinity;
                for (int i = 0; i < rows - 1; i++)
                {
                    if (tableau[i, pivotCol] > 0)
                    {
                        double ratio = tableau[i, cols - 1] / tableau[i, pivotCol];
                        if (ratio < minRatio)
                        {
                            minRatio = ratio;
                            pivotRow = i;
                        }
                    }
                }

                if (pivotRow == -1)
                    throw new Exception("Unbounded solution");

                // Нормалізація ведучого елементу до 1
                double pivot = tableau[pivotRow, pivotCol];
                for (int j = 0; j < cols; j++)
                    tableau[pivotRow, j] /= pivot;

                // Обнулення інших елементів у ведучому стовпці
                for (int i = 0; i < rows; i++)
                {
                    if (i != pivotRow)
                    {
                        double factor = tableau[i, pivotCol];
                        for (int j = 0; j < cols; j++)
                        {
                            tableau[i, j] -= factor * tableau[pivotRow, j];
                        }
                    }
                }
            }

            // Отримуємо результат
            return FormatSolution(tableau);
        }

        private string FormatSolution(double[,] tableau)
        {
            int rows = tableau.GetLength(0);
            int cols = tableau.GetLength(1);
            string result = "Optimal Solution:\n";

            for (int j = 0; j < cols - 1; j++)
            {
                bool isBasic = true;
                double value = 0;
                for (int i = 0; i < rows - 1; i++)
                {
                    if (tableau[i, j] == 1)
                    {
                        value = tableau[i, cols - 1];
                    }
                    else if (tableau[i, j] != 0)
                    {
                        isBasic = false;
                        break;
                    }
                }
                if (isBasic)
                    result += $"x{j + 1} = {value}\n";
                else
                    result += $"x{j + 1} = 0\n";
            }

            result += $"Optimal Value Z = {tableau[rows - 1, cols - 1]}";
            return result;
        }

        // Обробник події для скидання
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
            ResultTextBlock.Text = "Solution will be shown here...";
        }

        // Обробник події для пункту "About"
        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Simplex Method Solver v1.0\nDeveloped by Melnik Vladyslav", "About");
        }

        private bool IsValidTableau(string input)
        {
            // Розділимо введені дані на рядки
            var rows = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            if (rows.Length == 0)
            {
                MessageBox.Show("Помилка: Введіть дані для симплекс-таблиці.");
                return false;
            }

            // Перевіримо, чи кожен рядок містить лише числа і має однакову кількість елементів
            int columnCount = rows[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

            foreach (var row in rows)
            {
                var elements = row.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (elements.Length != columnCount)
                {
                    MessageBox.Show("Помилка: Усі рядки повинні мати однакову кількість елементів.");
                    return false;
                }

                foreach (var element in elements)
                {
                    if (!double.TryParse(element, out _))
                    {
                        MessageBox.Show("Помилка: Усі елементи мають бути числовими значеннями.");
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
