using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using System.Xml.Linq;
using TrainingWPF.ModelDB;

namespace TrainingWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для WindowChangePasswordLogin.xaml
    /// </summary>
    public partial class WindowChangePasswordLogin : Window
    {
        Users users;
        List<Users> user = DataBase.tbE.Users.ToList();
        public WindowChangePasswordLogin(Users users)
        {
            InitializeComponent();
            this.users = users;
            tbLogin.Text = users.Login;

        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string a = tbOldPassword.Password.GetHashCode().ToString();
                Users users1 = DataBase.tbE.Users.FirstOrDefault(x => x.Login == users.Login && x.Password == a);
                if(users1 == null)
                {
                    MessageBox.Show("Старый пароль не соответствует действительности", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                /// <summary>
                /// Проверка на заполненнсть
                /// </summary>
                if (!String.IsNullOrEmpty(tbLogin.Text)
                    && !String.IsNullOrEmpty(tbOldPassword.Password)
                    && !String.IsNullOrEmpty(tbNewPassword.Password)
                    && !String.IsNullOrEmpty(tbNewNewPas.Password))


                    /// <summary>
                    /// Проверка на пробелы
                    /// </summary>
                    /// 
                 
                    if (user.Where(x => x.Login.ToString() == tbLogin.Text).Count() == 0)
                        if (!tbLogin.Text.Contains(" ")
                         && !tbOldPassword.Password.Contains(" ")
                         && !tbNewPassword.Password.Contains(" ")
                         && !tbNewNewPas.Password.Contains(" ") )
                        {
                            // два рбаочих варианта регулярки на две цифры
                            //if (Regex.IsMatch(tbPassword.Password, @"(?=.[0-9]){2,}"))
                            if (Regex.IsMatch(tbNewNewPas.Password, @"(?=.*[0-9].*[0-9])") && Regex.IsMatch(tbNewPassword.Password, @"(?=.*[0-9].*[0-9])"))
                            {
                                if (Regex.IsMatch(tbNewNewPas.Password, @"(?=.[A-Z]){1,}") && Regex.IsMatch(tbNewPassword.Password, @"(?=.[A-Z]){1,}"))
                                {
                                    if (Regex.IsMatch(tbNewNewPas.Password, @"[a-z]+.*[a-z]+.*[a-z]") && Regex.IsMatch(tbNewPassword.Password, @"[a-z]+.*[a-z]+.*[a-z]"))
                                    {
                                        if (Regex.IsMatch(tbNewNewPas.Password, @"\W") && Regex.IsMatch(tbNewPassword.Password, @"\W"))
                                        {

                                            if (Regex.IsMatch(tbNewNewPas.Password, @"(?=.*[^\w\s]).{8,}") && Regex.IsMatch(tbNewPassword.Password, @"(?=.*[^\w\s]).{8,}"))
                                            {

                                                if (tbNewNewPas.Password.ToString() == tbNewPassword.Password.ToString())
                                                {



                                                    users.Login = tbLogin.Text;
                                                    users.Password = tbNewNewPas.GetHashCode().ToString();
                                                    DataBase.tbE.SaveChanges();
                                                    MessageBox.Show("Успешно");
                                                    this.Close();
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
    }
}
