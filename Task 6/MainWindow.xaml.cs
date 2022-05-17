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
        }
        public Grid matrix = new Grid();
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
            benchs.RowDefinitions.Clear();
            matrix.RowDefinitions.Clear();
            int.TryParse(CorrectInput(RowCount.Text), out int count);

            for (int i = 0; i < count; i++)
            {
                var row = new RowDefinition();
                matrix.RowDefinitions.Add(row);

                var new_row = new RowDefinition();
                benchs.RowDefinitions.Add(new_row);
            }
            FillWithTextBox();
        }

        private void ColumnCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            works.ColumnDefinitions.Clear();
            matrix.ColumnDefinitions.Clear();
            int.TryParse(CorrectInput(ColumnCount.Text), out int count);

            for (int i = 0; i < count; i++)
            {
                var col = new ColumnDefinition();
                matrix.ColumnDefinitions.Add(col);

                var new_row = new ColumnDefinition();
                works.ColumnDefinitions.Add(new_row);
            }
            FillWithTextBox();
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
                    textBox.FontSize = 26;
                    textBox.TextAlignment = TextAlignment.Center;
                    matrix.Children.Add(textBox);
                    Grid.SetColumn(textBox,colCount - 1);
                    Grid.SetRow(textBox, rowCount - 1);
                }
            }
            rowCount = 0;
            colCount = 0;
            foreach (var rows in benchs.RowDefinitions)
            { 
                Label label = new Label();
                label.FontSize = FontSizeCalc();
                label.Content = "1";
                benchs.Children.Add(label);
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, rowCount);
                rowCount++;
            }
            foreach (var cols in works.ColumnDefinitions)
            {
                Label label = new Label();
                label.FontSize = FontSizeCalc();
                label.Content = "1";
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

        public int FontSizeCalc()
        {
            try
            {
                int rowCount = matrix.RowDefinitions.Count();
                int colCount = matrix.ColumnDefinitions.Count();
                return 144 / (rowCount * colCount);
            }
            catch (DivideByZeroException) { }
            return 20;
        }
    }
}
