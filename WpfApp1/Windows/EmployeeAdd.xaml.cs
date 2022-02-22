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

namespace WpfApp1.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeAdd.xaml
    /// </summary>
    public partial class EmployeeAdd : Window
    {
        bool isEdit = false;
        EF.Employee editEmployee = new EF.Employee();
        public EmployeeAdd()
        {
            InitializeComponent();
            cmbRole.ItemsSource = ClassHelper.Class1.Context.Role.ToList().Select(i => i.NameRole).ToList();
            cmbRole.SelectedIndex = 1;
            isEdit = false;
        }
        public EmployeeAdd(EF.Employee employee)
        {
            InitializeComponent();
            cmbRole.ItemsSource = ClassHelper.Class1.Context.Role.ToList();
            cmbRole.DisplayMemberPath = "NameRole";
            txtLName.Text = employee.LastName;
            txtFName.Text = employee.FirstName;
            txtMName.Text = employee.MiddleName;
            txtPhone.Text = employee.Phone;
            txtEmail.Text = employee.Email;
            txtLogin.Text = employee.Login;
            txtPassword.Password = employee.Password;
            cmbRole.SelectedIndex = employee.IDRole - 1;
            txtAddEditEmployee.Text = "Изменение данных работника";
            btnAddEmployee.Content = "Сохранить";
            isEdit = true;
            editEmployee = employee;
        }
        private void txtPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890+-".IndexOf(e.Text) < 0;
        }
            private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
            {
                //Валидация
                if (string.IsNullOrWhiteSpace(txtLName.Text))
                {
                    MessageBox.Show("Поле ФАМИЛИЯ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtFName.Text))
                {
                    MessageBox.Show("Поле ИМЯ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    MessageBox.Show("Поле ТЕЛЕФОН не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtLogin.Text))
                {
                    MessageBox.Show("Поле ЛОГИН не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Поле ПАРОЛЬ не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (txtPhone.Text.Length > 12)
                {
                    MessageBox.Show("Превышено максимальное количество символов в поле ТЕЛЕФОН", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!Int32.TryParse(txtPhone.Text, out int res))
                {
                    MessageBox.Show("Недопустимые символы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (isEdit) // Изменение пользователя
                {
                    var resClick = MessageBox.Show("Изменить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resClick == MessageBoxResult.No)
                    {
                        return;
                    }
                    try
                    {
                        editEmployee.LastName = txtLName.Text;
                        editEmployee.FirstName = txtFName.Text;
                        editEmployee.MiddleName = txtMName.Text;
                        editEmployee.Phone = txtPhone.Text;
                        editEmployee.Email = txtEmail.Text;
                        editEmployee.IDRole = (cmbRole.SelectedItem as EF.Role).ID;
                        editEmployee.Login = txtLogin.Text;
                        editEmployee.Password = txtPassword.Password;
                        ClassHelper.Class1.Context.SaveChanges();
                        MessageBox.Show("Пользователь изменен");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else    // Добавление пользователя
                {
                    var resClick = MessageBox.Show("Добавить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (resClick == MessageBoxResult.No)
                    {
                        return;
                    }
                    try
                    {
                        EF.Employee newEmployee = new EF.Employee();
                        newEmployee.LastName = txtLName.Text;
                        newEmployee.FirstName = txtFName.Text;
                        newEmployee.MiddleName = txtMName.Text;
                        newEmployee.Phone = txtPhone.Text;
                        newEmployee.Email = txtEmail.Text;
                        newEmployee.IDRole = (cmbRole.SelectedItem as EF.Role).ID;
                        newEmployee.Login = txtLogin.Text;
                        newEmployee.Password = txtPassword.Password;
                        ClassHelper.Class1.Context.Employee.Add(newEmployee);
                        ClassHelper.Class1.Context.SaveChanges();
                        MessageBox.Show("Пользователь добавлен");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
    }
}
