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

namespace TrainingWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage2.xaml
    /// </summary>
    public partial class AdminPage2 : Page
    {
        Users users;
       

        public AdminPage2(Users users)
        {
            InitializeComponent();
            this.users = users;
            dg.ItemsSource = DataBase.tbE.Users.ToList();
            cmbCity.Items.Add("Все");
            foreach (City city in DataBase.tbE.City.ToList())
            {
                cmbCity.Items.Add(city.nameCity);
            }
        }

  
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

            dg.Visibility = Visibility.Visible;
            tbShowUser.Visibility = Visibility.Collapsed;
            tbSleepUser.Visibility = Visibility.Visible;

        }

        private void tbSleepUser_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dg.Visibility = Visibility.Collapsed;
            tbShowUser.Visibility = Visibility.Visible;
            tbSleepUser.Visibility = Visibility.Collapsed;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SortCombobox.SelectedIndex = -1;

            cmbGender.SelectedIndex = -1;
            cmbCity.SelectedIndex = -1;
            tbFiltres.Clear();
            dg.ItemsSource = DataBase.tbE.Users.ToList();


        }
        private void cmbDesc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dg.ItemsSource = DataBase.tbE.Users.OrderByDescending(x => x.Surname).ToList();
        }
        private void SortCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortCombobox.SelectedItem != null)
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)SortCombobox.SelectedItem;
                switch (comboBoxItem.Content)
                {
                    case "По возрастанию":
                        {
                            dg.ItemsSource = DataBase.tbE.Users.OrderBy(x => x.Surname).ToList();
                            break;
                        }
                    case "По убыванию":
                        {
                            dg.ItemsSource = DataBase.tbE.Users.OrderByDescending(x => x.Surname).ToList();
                            break;
                        }
                }

            }
        }

        private void cmbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbGender.SelectedItem != null)
            {   
                ComboBoxItem comboBoxItem = (ComboBoxItem)cmbGender.SelectedItem;
                switch (comboBoxItem.Content)
                {
                    case "Мужской":
                        {
                            dg.ItemsSource = DataBase.tbE.Users.Where(x => x.GenderTable.IdGender == 1).ToList();
                            break;
                        }
                    case "Женский":
                        {
                            dg.ItemsSource = DataBase.tbE.Users.Where(x => x.GenderTable.IdGender == 2).ToList();
                            break;
                        }
                }

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            dg.ItemsSource = DataBase.tbE.Users.ToList();
            if (tbFiltres.Text != "")
            {
                dg.ItemsSource = 
                    DataBase.tbE.Users.Where(x => x.Surname.Contains(tbFiltres.Text) || x.Name.Contains(tbFiltres.Text) || x.Login.Contains(tbFiltres.Text)).ToList();
            }
        }

        private void cmbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCity.SelectedItem != null)
            {
                if (cmbCity.SelectedItem.ToString() != "Все")
                {
                    dg.ItemsSource = DataBase.tbE.Users.Where(x => x.City.nameCity == cmbCity.SelectedItem.ToString()).ToList();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Order());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Autorization());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PersonalAccount(users));
        }
    }
}