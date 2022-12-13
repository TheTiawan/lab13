using System;
using System.Collections.Generic;
using System.Data.Common;
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
using libmas;
using Microsoft.Win32;

namespace lab13
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public libmas.Matrix _matrix;
        public MainWindow()
        {
            LoginWindow password = new LoginWindow();
            password.ShowDialog();
            try
            {
                using (StreamReader reader = new StreamReader("config.ini"))
                {
                    int i = Convert.ToInt32(reader.ReadLine());
                    int j = Convert.ToInt32(reader.ReadLine());
                    _matrix = new(i,j);
                    _matrix.Fill();
                    InitializeComponent();
                    datagrid.ItemsSource = _matrix.ToDataTable().DefaultView;
                    numrow.Text = $"{i} cтрок";
                    numcol.Text = $"{j} столбцов";
                }
            }
            catch
            {

            }

            InitializeComponent();
        }

        private void aboutprogramclick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дана матрица размера M * N.\nДля каждого столбца матрицы с четным номером (2, 4, …) найти сумму его элементов.\nУсловный оператор не использовать.");
        }

        private void exitclick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            int sum = 0;
            for (int i = 0;i<_matrix.RowLength;i++)
            {
                for (int j = 1;j<_matrix.ColumnLength;j+=2)
                {
                    sum += _matrix[i, j];
                }
            }
            result.Text = sum.ToString();
        }

        private void fillbuttonclick(object sender, RoutedEventArgs e)
        {
            try
            {
                _matrix = new(Convert.ToInt32(rows.Text), Convert.ToInt32(cols.Text));
                _matrix.Fill();
                datagrid.ItemsSource = _matrix.ToDataTable().DefaultView;
                numrow.Text = $"{rows.Text} cтрок";
                numcol.Text = $"{cols.Text} столбцов";
            }
            catch
            {
                MessageBox.Show("Проверьте исходные данные");
            }
        }

        private void saveclick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = ".txt";
            save.Filter = "Текстовые файлы | *.txt";
            save.Title = "Сохранение таблицы";
            if(save.ShowDialog() == true)
            {
                _matrix.Save(save.FileName);
            }
            datagrid.ItemsSource = null;
        }

        private void openclick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new();
            open.DefaultExt = ".txt";
            open.Filter = "Текстовые файлы | *.txt";
            open.Title = "Открытие таблицы";
            if (open.ShowDialog()==true)
            {
                _matrix.Load(open.FileName);
            }
            datagrid.ItemsSource = _matrix.ToDataTable().DefaultView;

        }

        private void indexclick(object sender, MouseButtonEventArgs e)
        {
            index.Text = "[" + (datagrid.Items.IndexOf(datagrid.SelectedItem)+1).ToString() + ";" + (datagrid.CurrentColumn.DisplayIndex + 1).ToString() + "]";
        }

        private void optionsclick(object sender, RoutedEventArgs e)
        {
            Options options = new Options();
            options.ShowDialog();
        }
    }
}
