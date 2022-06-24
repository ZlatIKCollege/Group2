using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using AvtoSalon.Entities;

namespace AvtoSalon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private group_2_is_31Context _dbContext = new group_2_is_31Context();
        private string _currentTable;

        public MainWindow()
        {
            new Autoriz().ShowDialog();
            InitializeComponent();
            CheckUser();

            _currentTable = "Пользователи";
            RefreshTable(_currentTable);
        }

        private void RefreshTable(string tableName)
        {
            switch (tableName)
            {
                case "Пользователи":
                    _dbContext.Users.Load();
                    userDG.ItemsSource = _dbContext.Users.Local.ToObservableCollection();
                    break;
                case "Роли":
                    _dbContext.Roles.Load();
                    rolesDG.ItemsSource = _dbContext.Roles.Local.ToObservableCollection();
                    break;
            }
        }

        private void usersGD_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();

            switch (headerName)
            {
                case "Role":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Name":
                    e.Column.Header = "Имя";
                    break;
                case "Login":
                    e.Column.Header = "Логин";
                    break;
                case "Password":
                    e.Column.Header = "Пароль";
                    break;
                case "RoleNavigation":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Roles.Load();
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("Role");

                    DataGridComboBoxColumn col = new DataGridComboBoxColumn
                    {
                        Header = "Роль",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Roles.ToArray(),
                        SelectedValueBinding = binding
                    };
                    ((DataGrid)sender).Columns.Add(col);
                    break;
            }
        }


        private void rolesGD_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            switch(headerName)
            {
                case "name":
                    e.Column.Header = "Роли";
                    break;
                case "Users":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Tab_GotFocus(object sender, RoutedEventArgs e)
        {
            _currentTable = ((TabItem)sender).Header.ToString();
            RefreshTable(_currentTable);
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _dbContext.SaveChanges();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            switch (_currentTable)
            {
                case "Пользователи":
                    _dbContext.Users.Local.Remove(userDG.SelectedItem as User);
                    break;
            }
        }
        private void CheckUser()
        {
            switch (Autoriz.CurrentUser.Role)
            {
                case 1:
                    break;
                case 2:
                    usersTab.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    break;

            }
        }
        private string GetUserFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV Файлы | *.csv";
            ofd.Title = "Выберите файл для экспорта";
            if (ofd.ShowDialog() == true)
            {
                return ofd.FileName;
            }
            return null;
        }
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            string filepath = GetUserFile();

            if (filepath == null)
                return;

            StreamWriter file = new StreamWriter(filepath, false);

            switch (_currentTable)
            {
                case "Пользователи":
                    ObservableCollection<User> table = _dbContext.Users.Local.ToObservableCollection();

                    file.WriteLine($"ID;Логин;Пароль;РольИД");
                    foreach (User elem in table)
                    {
                        file.WriteLine($"{elem.Id};{elem.Login};{elem.Password};{elem.Role}");
                    }
                    break;
            }
            file.Close();
            MessageBox.Show("Экспорт успешно завершен", "Успешно");
        }

    }
}
