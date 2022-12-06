using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
using TrainingWPF.ModelDB;

namespace TrainingWPF
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {

        List<Country> countryList = DataBase.tbE.Country.ToList();
        List<City> cityList = DataBase.tbE.City.ToList();
        List<GenderTable> genderList = DataBase.tbE.GenderTable.ToList();
        List<Users> users = DataBase.tbE.Users.ToList();

        public Registration()
        {

            InitializeComponent();
            DataBase.tbE = new Entities22();
            cmb2.ItemsSource = cityList;
            cmb2.SelectedValuePath = "idCity";
            cmb2.DisplayMemberPath = "nameCity";
            cmb.ItemsSource = countryList;
            cmb.SelectedValuePath = "idCountry";
            cmb.DisplayMemberPath = "nameCountry";




        }
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (checkPassword.IsChecked == true)
                {
                    tbPassword.Password = tbPasswordTB.Text;
                }
                if (checkPassword.IsChecked == false)
                {
                    tbPasswordTB.Text = tbPassword.Password;
                }
                if (checkPassword2.IsChecked == true)
                {
                    tbPassword2.Password = tbPasswordTB1.Text;
                }
                if (checkPassword2.IsChecked == false)
                {
                    tbPasswordTB1.Text = tbPassword2.Password;
                }

                int genderList = 0;
                if (rb1.IsChecked == true) { genderList = 1; }
                else if (rb2.IsChecked == true) { genderList = 2; }

                /// <summary>
                /// Проверка на заполненнсть
                /// </summary>
                if (!String.IsNullOrEmpty(tbName.Text)
                    && !String.IsNullOrEmpty(tbSurname.Text)
                    && !String.IsNullOrEmpty(tbPatronymic.Text)
                    && !String.IsNullOrEmpty(tbLogin.Text)
                    && !String.IsNullOrEmpty(tbPassword.Password)
                    && !String.IsNullOrEmpty(tbPassword2.Password)
                    && (cmb.SelectedItem != null)
                    && (cmb2.SelectedItem != null)
                    && (rb1.IsChecked == true || rb2.IsChecked == true))

                    /// <summary>
                    /// Проверка на пробелы
                    /// </summary>
                    /// 
                    if (users.Where(x => x.Login.ToString() == tbLogin.Text).Count() == 0)
                        if (!tbName.Text.Contains(" ")
                         && !tbSurname.Text.Contains(" ")
                         && !tbPatronymic.Text.Contains(" ")
                         && !tbLogin.Text.Contains(" ")
                         && !tbPassword.Password.Contains(" ")
                         && !tbPassword2.Password.Contains(" "))
                        {
                            // два рбаочих варианта регулярки на две цифры
                            //if (Regex.IsMatch(tbPassword.Password, @"(?=.[0-9]){2,}"))
                            if (Regex.IsMatch(tbPassword.Password, @"(?=.*[0-9].*[0-9])"))
                            {
                                if (Regex.IsMatch(tbPassword.Password, @"(?=.[A-Z]){1,}"))
                                {
                                    if (Regex.IsMatch(tbPassword.Password, @"[a-z]+.*[a-z]+.*[a-z]"))
                                    {
                                        if (Regex.IsMatch(tbPassword.Password, @"\W"))
                                        {

                                            if (Regex.IsMatch(tbPassword.Password, @"(?=.*[^\w\s]).{8,}"))
                                            {

                                                if (tbPassword.Password.ToString() == tbPassword2.Password.ToString())
                                                {
                                                    Users users = new Users()
                                                    {

                                                        Name = tbName.Text,
                                                        Surname = tbSurname.Text,
                                                        Patronymic = tbPatronymic.Text,
                                                        Login = tbLogin.Text,
                                                        Password = tbPassword.Password.GetHashCode().ToString(),
                                                        idCountry = cmb.SelectedIndex + 1,
                                                        idCity = cmb2.SelectedIndex + 1,
                                                        //Country = (Country)cmb.SelectedItem,
                                                        //City = (City)cmb2.SelectedItem,


                                                        IdGender = genderList,
                                                        idRole = 2
                                                    };


                                                    DataBase.tbE.Users.Add(users);
                                                    DataBase.tbE.SaveChanges();
                                                    MessageBox.Show("Успешная регистрация");
                                                    NavigationService.Navigate(new Autorization());
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Пароли не совпадают", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                                }



                                            }
                                            else
                                            {
                                                MessageBox.Show("Пароль должен состоять не менее восьми символов", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);



                                            }


                                        }
                                        else
                                        {
                                            MessageBox.Show("Пароль должен содержать не менее одного спец.символа", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);


                                        }



                                    }
                                    else
                                    {

                                        MessageBox.Show("В пароле должно находится не менее 3 строчных латинских символов", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);


                                    }

                                }
                                else
                                {
                                    MessageBox.Show("В пароле должно находится не менее одного 1 заглавного символа", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                                }
                            }

                            else
                            {
                                MessageBox.Show("В пароле содержатся менее двух цифр", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                            }
                        }

                        else
                        {
                            MessageBox.Show("Проверьте, чтобы поля не содержали пробелы", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    else
                    {
                        MessageBox.Show("Такой логин уже существует", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                else
                {
                    MessageBox.Show("Возможно не заполнено одно или несколько полей, а также не выбраны какие-то элементы", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка, попробуйте еще раз", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void checkPassword_Checked(object sender, RoutedEventArgs e)
        {
            if (checkPassword.IsChecked == true)
            {
                tbPasswordTB.Text = tbPassword.Password;
                tbPasswordTB.Visibility = Visibility.Visible;
                tbPassword.Visibility = Visibility.Collapsed;
            }
        }

        private void checkPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            if (checkPassword.IsChecked == false)
            {
                tbPassword.Password = tbPasswordTB.Text;
                tbPasswordTB.Visibility = Visibility.Collapsed;
                tbPassword.Visibility = Visibility.Visible;
            }
        }



        private void checkPassword2_Checked_1(object sender, RoutedEventArgs e)
        {
            if (checkPassword2.IsChecked == true)
            {
                tbPasswordTB1.Text = tbPassword2.Password;
                tbPasswordTB1.Visibility = Visibility.Visible;
                tbPassword2.Visibility = Visibility.Collapsed;
            }
        }

        private void checkPassword2_Unchecked_1(object sender, RoutedEventArgs e)
        {
            if (checkPassword2.IsChecked == false)
            {
                tbPassword2.Password = tbPasswordTB1.Text;
                tbPasswordTB1.Visibility = Visibility.Collapsed;
                tbPassword2.Visibility = Visibility.Visible;
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }
    }
}