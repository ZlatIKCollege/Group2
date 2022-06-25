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
using AvtoSalon.Entities;

namespace AvtoSalon
{
    /// <summary>
    /// Логика взаимодействия для Autoriz.xaml
    /// </summary>
    public partial class Autoriz : Window
    {
        private group_2_is_31Context _dbContext = new group_2_is_31Context();
        private bool _isLogin = false;
        public static User CurrentUser;
        public Autoriz()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = _dbContext.Users.Where(
                    (usr) => usr.Login == loginTextBox.Text && usr.Password == passwordTextBox.Text
                    ).Single();
                MessageBox.Show($"Привет,{user.Login}!", "Успешно!");

                _isLogin = true;
                CurrentUser = user;
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Неверный логин или пароль!");
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (!_isLogin)
                App.Current.Shutdown();
        }
    }
}
