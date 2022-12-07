using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using TrainingWPF.ModelDB;

namespace TrainingWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangeFIO.xaml
    /// </summary>
    public partial class ChangeFIO : Window
    {

        Users users;
        public ChangeFIO(Users users)
        {
            InitializeComponent();
            this.users = users;
            tbName.Text = users.Name;
            tbSurname.Text = users.Surname;
            tbPatronymic.Text = users.Patronymic;
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbName.Text) && !String.IsNullOrEmpty(tbSurname.Text) && !String.IsNullOrEmpty(tbPatronymic.Text))
            {
                if (!tbName.Text.Contains(" ") && !tbSurname.Text.Contains(" ") && !tbPatronymic.Text.Contains(" "))
                {
                    if(Regex.IsMatch(tbName.Text, "[^0-9]")&& Regex.IsMatch(tbSurname.Text, "[^0-9]") && Regex.IsMatch(tbPatronymic.Text, "[^0-9]"))
                    {
                        // [^!@#$%^&*()_]
                        users.Name = tbName.Text;
                        users.Surname = tbSurname.Text;
                        users.Patronymic = tbPatronymic.Text;
                        DataBase.tbE.SaveChanges();
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("Проверьте, чтобы поля не содержали цифры", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
                else
                {
                    MessageBox.Show("Проверьте, чтобы поля не содержали пробелы", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            else
            {
                MessageBox.Show("Возможно не заполнено одно или несколько полей, а также не выбраны какие-то элементы", "Возникла какая-то ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
    }
}
