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
            users.Name = tbName.Text;
            users.Surname = tbSurname.Text;
            users.Patronymic = tbPatronymic.Text;
            DataBase.tbE.SaveChanges();
            this.Close();


        }
    }
}
