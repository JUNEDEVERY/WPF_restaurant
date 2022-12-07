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
using TrainingWPF.ModelDB;
using TrainingWPF.Pages;

namespace TrainingWPF
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Page
    {
     
        public Autorization()
        {
            InitializeComponent();
            DataBase.tbE = new Entities22();
           

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameWork.MainFrame.Navigate(new FirstPage());
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {

            string p = Convert.ToString(tbpass.Password.GetHashCode());
            Users users = DataBase.tbE.Users.FirstOrDefault(x => x.Login == tblog.Text && x.Password == p);

            if (users != null)
            {
                if (users.idRole == 1)
                {
                    // admin - gerasimov 22!Aaaaa
                    NavigationService.Navigate(new AdminPage2(users));
                }
                else
                {
                    MessageBox.Show($"Привет, {users.Name}!", "Успешная авторизация", MessageBoxButton.OK, MessageBoxImage.Information); 
                   NavigationService.Navigate(new ShowMenu(users.id_client));
                   NavigationService.Navigate(new PersonalAccount(users));
                    
                }

            }       
            else
            {
                MessageBox.Show($"Введенный логин и/или пароль не существуют в системе", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error); ;

            }

        }

        private void btngoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FirstPage());
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }
    }
}
