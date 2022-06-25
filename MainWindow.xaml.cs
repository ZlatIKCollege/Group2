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
                case "Автомобили":
                    _dbContext.Cars.Load();
                    carsDG.ItemsSource = _dbContext.Cars.Local.ToObservableCollection();
                    break;
                case "Страны":
                    _dbContext.Countries.Load();
                    countriesDG.ItemsSource = _dbContext.Countries.Local.ToObservableCollection();
                    break;
                case "Профессии":
                    _dbContext.Professions.Load();
                    professionDG.ItemsSource = _dbContext.Professions.Local.ToObservableCollection();
                    break;
                case "Клиенты":
                    _dbContext.Clients.Load();
                    clientsDG.ItemsSource = _dbContext.Clients.Local.ToObservableCollection();
                    break;
                case "Сотрудники":
                    _dbContext.Workers.Load();
                    workersDG.ItemsSource = _dbContext.Workers.Local.ToObservableCollection();
                    break;
                case "Продажи":
                    _dbContext.Contracts.Load();
                    contractsDG.ItemsSource = _dbContext.Contracts.Local.ToObservableCollection();
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

        private void clientsGD_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            switch (headerName)
            {
                case "name":
                    e.Column.Header = "Клиент";
                    break;
                case "Contracts":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void professionGD_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            switch (headerName)
            {
                case "Name":
                    e.Column.Header = "Професия";
                    break;
                case "Workers":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;

            }
        }

        private void workersGD_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            switch (headerName)
            {
                case "Name":
                    e.Column.Header = "Имя";
                    break;
                case "Profession":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Professions.Load();
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("Profession");

                    DataGridComboBoxColumn col = new DataGridComboBoxColumn
                    {
                        Header = "Профессия",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Professions.ToArray(),
                        SelectedValueBinding = binding
                    };
                    ((DataGrid)sender).Columns.Add(col);
                    break;
                case "Salary":
                    e.Column.Header = "Зарплата";
                    break;
                case "Workers":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "ProfessionNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Contracts":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void сountriesGD_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            switch (headerName)
            {
                case "Countries":
                    e.Column.Header = "Страны";
                    break;
                case "Cars":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void carsGD_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            switch (headerName)
            {
                case "CarBrand":
                    e.Column.Header = "Марка";
                    break;
                case "Country":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Countries.Load();
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("Country");

                    DataGridComboBoxColumn col = new DataGridComboBoxColumn
                    {
                        Header = "Страны",
                        DisplayMemberPath = "Countries",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Countries.ToArray(),
                        SelectedValueBinding = binding
                    };
                    ((DataGrid)sender).Columns.Add(col);
                    break;


                case "Year":
                    e.Column.Header = "Дата";
                    break;
                case "Contracts":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "CountryNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;

            }
        }

        private void contractsGD_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            switch (headerName)
            {
                case "Client":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Clients.Load();
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("Client");

                    DataGridComboBoxColumn col = new DataGridComboBoxColumn
                    {
                        Header = "Клиент",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Clients.ToArray(),
                        SelectedValueBinding = binding
                    };
                    ((DataGrid)sender).Columns.Add(col);
                    break;


                case "Worker":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Workers.Load();
                    Binding binding1 = new Binding();
                    binding1.Path = new PropertyPath("Worker");

                    DataGridComboBoxColumn col1 = new DataGridComboBoxColumn
                    {
                        Header = "Работник",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Workers.ToArray(),
                        SelectedValueBinding = binding1
                    };
                    ((DataGrid)sender).Columns.Add(col1);
                    break;


                case "Car":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Cars.Load();
                    Binding binding2 = new Binding();
                    binding2.Path = new PropertyPath("Car");

                    DataGridComboBoxColumn col2 = new DataGridComboBoxColumn
                    {
                        Header = "Машина",
                        DisplayMemberPath = "CarBrand",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Cars.ToArray(),
                        SelectedValueBinding = binding2
                    };
                    ((DataGrid)sender).Columns.Add(col2);
                    break;
                case "Price":
                    e.Column.Header = "Цена";
                    break;
                case "Date":
                    e.Column.Header = "Дата";
                    break;
                case "CarNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "ClientNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "WorkerNavigation":
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
            _dbContext.SaveChanges();//офрошаемся к БД и сох
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            switch (_currentTable)//случий тек табл
            {
                case "Пользователи":// в какой ты  в таб 
                    _dbContext.Users.Local.Remove(userDG.SelectedItem as User);// оброш к базе к таб узер , и удаляем в лок БД и потом сох (выброный элемента строку = юзер)
                    break;
                case "авто":
                    _dbContext.Cars.Local.Remove(carsDG.SelectedItem as Car);
                    break;
                case "Клиент":
                    _dbContext.Clients.Local.Remove(clientsDG.SelectedItem as Client);
                    break;
                case "Работники":
                    _dbContext.Workers.Local.Remove(workersDG.SelectedItem as Worker);
                    break;
                case "Контракт":
                    _dbContext.Contracts.Local.Remove(contractsDG.SelectedItem as Contract);
                    break;
                case "Професия":
                    _dbContext.Professions.Local.Remove(professionDG.SelectedItem as Profession);
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
                    _dbContext.Users.Load();
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
        private void ReportSalesMounthButton_Click(object sender, RoutedEventArgs e)
        {
            string reportName = ((Button)sender).Content.ToString();

            switch (reportName)
            {
                case "Продажи за текущий месяц":
                    ObservableCollection<Contract> contractCurMounth = new ObservableCollection<Contract>();
                    _dbContext.Contracts.Load();
                    foreach (Contract contract in _dbContext.Contracts.Local.ToObservableCollection())
                    {
                        if (contract.Date.Month == DateTime.Now.Month)
                            contractCurMounth.Add(contract);
                    }

                    reportDG.ItemsSource = contractCurMounth;

                    break;
            }
        }

        private void reportDG_AutoGenertingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headerName = e.Column.Header.ToString();
            e.Column.IsReadOnly = true;

            switch (headerName)
            {
                case "Worker":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Car":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Date":
                    e.Column.Header = "Дата";
                    break;
                case "WorkerNavigation":
                    e.Column.Visibility = Visibility.Collapsed;

                    _dbContext.Workers.Load();
                    Binding binding = new Binding();
                    binding.Path = new PropertyPath("Worker");

                    DataGridComboBoxColumn col = new DataGridComboBoxColumn
                    {
                        Header = "Работник",
                        DisplayMemberPath = "Name",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Workers.ToArray(),
                        SelectedValueBinding = binding,
                        IsReadOnly = true
                    };
                    ((DataGrid)sender).Columns.Add(col);
                    break;
                case "CarNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    _dbContext.Cars.Load();

                    Binding binding1 = new Binding();
                    binding1.Path = new PropertyPath("Car");

                    DataGridComboBoxColumn col1 = new DataGridComboBoxColumn { 
                    Header = "Машина",
                        DisplayMemberPath = "CarBrand",
                        SelectedValuePath = "Id",
                        ItemsSource = _dbContext.Cars.ToArray(),
                        SelectedValueBinding = binding1,
                        IsReadOnly = true
                    };
                    ((DataGrid)sender).Columns.Add(col1);
                    break;
                case "ClientNavigation":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;
                case "Client":
                    e.Column.Visibility = Visibility.Collapsed;
                    break;

            }
        }

    }
}
