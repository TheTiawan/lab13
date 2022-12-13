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

namespace lab13
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            myPassword.Focus();
        }
        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if (myPassword.Password == "123")
            {
                Close();
            }
            else
            {
                MessageBox.Show("Неверный пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                myPassword.Focus();
            }
        }
    }
}
