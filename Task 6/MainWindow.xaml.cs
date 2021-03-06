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

namespace Task_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Table.Children.Add(matrix);
            Grid.SetColumn(matrix, 1);
            Grid.SetRow(matrix, 1);
            Grid.SetColumnSpan(matrix, 2);
            //matrix.ShowGridLines = true;

            Benchs.Visibility = Visibility.Hidden;
            Works.Visibility = Visibility.Hidden;
        }
        public Grid matrix = new Grid();

        public Grid newMatrix = new Grid();
        public static string letters = "qwertyuiop[]asdgfhjlk;'xzcvbnm/`-|\\_\"=+~./<>(){}:?йцукенгшщзхъфывапролджэячсмитьбюё";
        //Метод проверки вводимых значение в Textbox
        public static string CorrectInput(string text)
        {
            foreach (char item in letters.ToCharArray())
            {
                if (text.ToLower().Contains(item))
                    return "";
                continue;
            }
            return text;
        }

        private void RowCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            ColumnCount.Text = CorrectInput(ColumnCount.Text);
            if (RowCount.Text == "0" || ColumnCount.Text == "0")
                matrix.Children.Clear();
            if (RowCount.Text == "0" || RowCount.Text == "" || RowCount.Text.Contains(" "))
            {
                Benchs.Visibility = Visibility.Hidden;
                matrix.Children.Clear();
            }
            if (ColumnCount.Text == "0" || ColumnCount.Text == "" || ColumnCount.Text.Contains(" "))
            {
                Works.Visibility = Visibility.Hidden;
                matrix.Children.Clear();
            }

            benchs.RowDefinitions.Clear();
            benchs.Children.Clear();

            works.ColumnDefinitions.Clear();
            works.Children.Clear();

            matrix.RowDefinitions.Clear();
            matrix.ColumnDefinitions.Clear();
            matrix.Children.Clear();

            newMatrix.RowDefinitions.Clear();
            newMatrix.ColumnDefinitions.Clear();
            newMatrix.Children.Clear();

            int.TryParse(RowCount.Text, out int count);

            for (int i = 0; i < count; i++)
            {
                var row = new RowDefinition();
                matrix.RowDefinitions.Add(row);

                var new_row = new RowDefinition();
                benchs.RowDefinitions.Add(new_row);
            }
            int.TryParse(ColumnCount.Text, out count);
            for (int i = 0; i < count; i++)
            {
                var col = new ColumnDefinition();
                matrix.ColumnDefinitions.Add(col);

                var new_row = new ColumnDefinition();
                works.ColumnDefinitions.Add(new_row);
            }
            FillWithTextBox();
            if (RowCount.Text != "")
                Benchs.Visibility = Visibility.Visible;

            if (ColumnCount.Text != "")
                Works.Visibility = Visibility.Visible;
        }

        public void FillWithTextBox()
        {


            int rowCount = 0;
            int colCount;
            foreach (var rows in matrix.RowDefinitions)
            {
                rowCount++;
                colCount = 0;
                foreach (var cols in matrix.ColumnDefinitions)
                {
                    colCount++;
                    TextBox textBox = new TextBox();
                    textBox.TextChanged += TextBox_TextChanged;

                    textBox.TextAlignment = TextAlignment.Left;
                    textBox.VerticalContentAlignment = VerticalAlignment.Top;
                    matrix.Children.Add(textBox);
                    Grid.SetColumn(textBox, colCount - 1);
                    Grid.SetRow(textBox, rowCount - 1);
                    try
                    {
                        textBox.FontSize = matrix.ActualHeight / double.Parse(RowCount.Text) / 2.2;
                    }
                    catch (ArgumentException)
                    {
                        textBox.FontSize = 46;
                    }
                }
            }
            rowCount = 0;
            colCount = 0;
            foreach (var rows in benchs.RowDefinitions)
            {
                TextBox label = new TextBox();
                label.IsReadOnly = true;
                label.BorderBrush = null;
                label.TextAlignment = TextAlignment.Right;
                label.VerticalContentAlignment = VerticalAlignment.Center;
                label.FontSize = benchs.ActualHeight / int.Parse(RowCount.Text);
                label.Text = "1";
                label.Style = (Style)Resources["TextBoxStyle1"];
                benchs.Children.Add(label);
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, rowCount);
                rowCount++;
            }
            foreach (var cols in works.ColumnDefinitions)
            {
                TextBox label = new TextBox();
                label.IsReadOnly = true;
                label.BorderBrush = null;
                label.TextAlignment = TextAlignment.Center;
                label.VerticalContentAlignment = VerticalAlignment.Bottom;
                label.FontSize = works.ActualHeight;
                label.Text = "1";
                label.Style = (Style)Resources["TextBoxStyle1"];
                works.Children.Add(label);
                Grid.SetColumn(label, colCount);
                Grid.SetRow(label, 0);
                colCount++;
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var textbox in matrix.Children.OfType<TextBox>())
                textbox.Text = CorrectInput(textbox.Text);

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            foreach (var textbox in matrix.Children.OfType<TextBox>())
                textbox.Clear();
            foreach (var textbox in newMatrix.Children.OfType<TextBox>())
                textbox.Clear();
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            newMatrix.Children.Clear();
            Random random = new Random();
            foreach (var textbox in matrix.Children.OfType<TextBox>())
                textbox.Text = random.Next(1, 10).ToString();
        }

        private void Method_Click(object sender, RoutedEventArgs e)
        {


            if (!matrix.Children.Contains(newMatrix))
                matrix.Children.Add(newMatrix);

            newMatrix.RowDefinitions.Clear();
            newMatrix.ColumnDefinitions.Clear();
            newMatrix.Children.Clear();

            Grid.SetColumn(newMatrix, 0);
            Grid.SetRow(newMatrix, 0);
            Grid.SetColumnSpan(newMatrix, matrix.ColumnDefinitions.Count);
            Grid.SetRowSpan(newMatrix, matrix.RowDefinitions.Count);

            int.TryParse(RowCount.Text, out int count);
            for (int i = 0; i < count; i++)
            {
                var row2 = new RowDefinition();
                var row3 = new RowDefinition();
                newMatrix.RowDefinitions.Add(row2);
                newMatrix.RowDefinitions.Add(row3);
            }
            int.TryParse(ColumnCount.Text, out count);
            for (int i = 0; i < count; i++)
            {
                var col2 = new ColumnDefinition();
                var col3 = new ColumnDefinition();
                newMatrix.ColumnDefinitions.Add(col2);
                newMatrix.ColumnDefinitions.Add(col3);
            }
            int rowCount = 0;
            int colCount;
            foreach (var row in newMatrix.RowDefinitions)
            {
                rowCount++;
                colCount = 0;
                foreach (var col in newMatrix.ColumnDefinitions)
                {
                    colCount++;
                    if (rowCount % 2 == 0 && colCount % 2 == 0)
                    {
                        TextBox newTextBox = new TextBox();
                        newTextBox.TextChanged += TextBox_TextChanged;
                        newTextBox.TextAlignment = TextAlignment.Right;
                        newTextBox.IsReadOnly = true;
                        newTextBox.BorderBrush = null;
                        newTextBox.Style = (Style)Resources["TextBoxStyle1"];
                        newMatrix.Children.Add(newTextBox);
                        Grid.SetColumn(newTextBox, colCount - 1);
                        Grid.SetRow(newTextBox, rowCount - 1);
                        try
                        {
                            newTextBox.FontSize = matrix.ActualHeight / int.Parse(RowCount.Text) / 2.5;
                        }
                        catch (ArgumentException)
                        {
                            newTextBox.FontSize = 46;
                        }
                    }
                }
            }

            int rows = int.Parse(RowCount.Text);
            int cols = int.Parse(ColumnCount.Text);
            int?[,] array = new int?[rows, cols];
            foreach (TextBox textBox in matrix.Children.OfType<TextBox>())
            {
                if (textBox.Text == "")
                {
                    MessageBox.Show("Введите все значения в матрицу");
                    goto end;
                }
                for (int i = 0; i < array.GetLength(0); i++)
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        if (array[i, j] == null)
                        {
                            //Запись значений Textbox'ов в матрицу
                            try
                            {
                                array[i, j] = int.Parse(textBox.Text);
                                goto mark;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Введенны некорректные значения");
                                goto end;
                            }
                        }
                    }
                mark:
                Console.WriteLine();
            }
            int?[,] distribution = new int?[rows, cols];
            foreach (TextBox textBox in newMatrix.Children.OfType<TextBox>())
            {
                for (int i = 0; i < distribution.GetLength(0); i++)
                    for (int j = 0; j < distribution.GetLength(1); j++)
                    {
                        if (distribution[i, j] == null)
                        {
                            //Запись значений Textbox'ов в матрицу

                            distribution[i, j] = 0;
                            goto mark;


                        }
                        else
                            continue;
                    }
                mark:
                Console.WriteLine();
            }
            int minEL = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                int min = 99999999;

                for (int j = 0; j < array.GetLength(1); j++)
                    if (array[i, j] < min && array[i, j] != 0)
                    {

                        if (i == 0)
                            min = (int)array[i, j];
                        else
                            for (int k = 0; k < array.GetLength(0); k++)
                            {
                                if (array[k, j] != distribution[k, j])
                                    min = (int)array[i, j];
                            }

                    }

                for (int j = 0; j < array.GetLength(1); j++)
                    if (array[i, j] == min)
                        minEL = j;
                distribution[i, minEL] = array[i, minEL];
            }
            TextBox[,] textBoxes = new TextBox[int.Parse(RowCount.Text), int.Parse(ColumnCount.Text)];
            TextBox[] textBoxesTMP = newMatrix.Children.OfType<TextBox>().ToArray();

            int counter = 0;
            for (int i = 0; i < textBoxes.GetLength(0); i++)
            {
                for (int j = 0; j < textBoxes.GetLength(1); j++)
                {
                    textBoxes[i, j] = textBoxesTMP[counter];
                    counter++;
                }
            }

            for (int j = 0; j < distribution.GetLength(0); j++)
                for (int k = 0; k < distribution.GetLength(1); k++)
                    if (distribution[j, k] != 0 && textBoxes[j, k].Text != "1")
                    {
                        textBoxes[j, k].Text = "1";
                        break;
                    }

            counter = 0;
            for (int i = 0; i < textBoxes.GetLength(0); i++)
                for (int j = 0; j < textBoxes.GetLength(1); j++)
                {
                    textBoxesTMP[counter].Text = textBoxes[i, j].Text;
                    counter++;
                }
            int sum = 0;
            foreach (var el in distribution)
                if (el != 0)
                    sum += (int)el / (int)el;
            List<int> minInMatrix = MinInMatrix(array, distribution);

            counter = 0;
        //if (!IsNonDegenerate(sum))
        //{
        //    while (!IsNonDegenerate(sum))
        //    {
        //        for (int j = 0; j < distribution.GetLength(0); j++)
        //            for (int k = 0; k < distribution.GetLength(1); k++)
        //                if (array[j, k] == minInMatrix[counter])
        //                {
        //                    textBoxes[j, k].Text = "0";
        //                    sum++;
        //                    counter++;
        //                }

        //    }
        //}
        //counter = 0;
        //for (int i = 0; i < textBoxes.GetLength(0); i++)
        //    for (int j = 0; j < textBoxes.GetLength(1); j++)
        //    {
        //        textBoxesTMP[counter].Text = textBoxes[i, j].Text;
        //        counter++;
        //    }


        end:
            Console.WriteLine();
        }
        public bool IsNonDegenerate(int count)
        {
            if (count == (int.Parse(ColumnCount.Text) * int.Parse(RowCount.Text) - 1))
                return true;
            return false;
        }
        public List<int> MinInMatrix(int?[,] matrix, int?[,] distribution)
        {
            int min = 9999999;
            int sum = 0;
            int rows = int.Parse(RowCount.Text), cols = int.Parse(ColumnCount.Text);
            foreach (var el in distribution)
                if (el != 0)
                    sum += (int)el / (int)el;
            int difference = rows * cols - 1 - sum;
            List<int> minInMatrix = new List<int>();
            for (int i = 0; i < difference; i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                    for (int k = 0; k < matrix.GetLength(1); k++)
                        if (matrix[j, k] == distribution[j, k])
                            matrix[j, k] = 0;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    for (int k = 0; k < matrix.GetLength(1); k++)
                    {
                        try
                        {
                            if (matrix[j, k] < min && matrix[j, k] != 0)
                            {
                                min = (int)matrix[j, k];
                                minInMatrix.Add(min);
                                matrix[j, k] = 0;
                                goto mark;
                            }
                            if (matrix[j + 1, k] < matrix[j, k] && matrix[j + 1, k] != 0)
                            {
                                min = (int)matrix[j + 1, k];
                                minInMatrix.Add(min);
                                matrix[j + 1, k] = 0;
                                goto mark;
                            }
                        }
                        catch (IndexOutOfRangeException) { }

                    }
                }
            mark:
                Console.WriteLine();
            }
            return minInMatrix;
        }
        //public int MinInRow(int?[,] array, int row)
        //{
            
        //            return i;
        //    return 0;
        //}
    }
}
