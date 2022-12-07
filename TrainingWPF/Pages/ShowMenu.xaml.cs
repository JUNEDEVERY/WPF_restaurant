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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainingWPF.ModelDB;
using TrainingWPF.Pages;
using Menu = TrainingWPF.ModelDB.Menu;

namespace TrainingWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShowMenu.xaml
    /// </summary>
    public partial class ShowMenu : Page
    {
        List<MyOrder> list = new List<MyOrder>();
        int u_id;
        public struct MyOrder
        {
            public int k;
            public ModelDB.Menu menus;
        }
        public ShowMenu(int u_id)
        {
            InitializeComponent();

            lVMenu.ItemsSource = DataBase.tbE.Menu.ToList();
            this.u_id = u_id;

            //cmbFiltres.Items.Add("Все");
            //cmbFiltres.Items.Add("Все");
            //cmbFiltres.Text = "";
            //sortCmb.Text = "";




        }

        private void addMenu_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Tag);
            ModelDB.Menu menu = DataBase.tbE.Menu.FirstOrDefault(x => x.idMenu == id);
            if (list.Where(x => x.menus.idMenu == id).Count() == 0)
            {
                MyOrder myOrder = new MyOrder();
                myOrder.menus = menu;
                myOrder.k = 1;
                list.Add(myOrder);

            }
            else
            {
                MyOrder order = list.FirstOrDefault(x => x.menus.idMenu == id);
                order.k++;
                list.Remove(list.FirstOrDefault(x => x.menus.idMenu == id));
                list.Add(order);
            }
            Zakaz.Items.Clear();
            foreach (MyOrder myOrder in list)
            {
                Zakaz.Items.Add(myOrder.menus.titile + " в количестве " + myOrder.k + " шт.");
            }
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Zakaz.Items.Count != 0)
                {

                    Zakaz zakaz = new Zakaz()
                    {
                        idStatus = 2,
                        id_client = u_id,
                        datetime = DateTime.Now
                    };

                    DataBase.tbE.Zakaz.Add(zakaz);
                    foreach (MyOrder myOrder in list)
                    {
                        ZakazIzMenu zakazIzMenu = new ZakazIzMenu()
                        {
                            idMenu = myOrder.menus.idMenu,
                            idNapitok = 1,
                            idzakaz = zakaz.idZakaz,
                            quantity = myOrder.k
                        };
                        DataBase.tbE.ZakazIzMenu.Add(zakazIzMenu);
                    }

                    DataBase.tbE.SaveChanges();
                    MessageBox.Show("Ваш заказ успешно оформлен. ");
                    Zakaz.Items.Clear();
                }




                else
                {
                    MessageBox.Show("Ваш заказ пуст. ");
                }






            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Минутку....", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();

        }

        private void DeletePart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Zakaz.SelectedItems.Count != 0)
                {

                    string str = Zakaz.SelectedValue.ToString().Substring(0, Zakaz.SelectedValue.ToString().IndexOf(" в количестве"));

                    list.Remove(list.FirstOrDefault(x => x.menus.titile == str));
                    Zakaz.Items.Clear();
                    foreach (MyOrder myOrder in list)
                    {
                        Zakaz.Items.Add(myOrder.menus.titile + " в количестве " + myOrder.k + " шт.");
                    }
                }
                else
                    MessageBox.Show("Вы не выбрали элемент для удаления", "Возникла ошибка при удалении товара из корзины", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch
            {
                MessageBox.Show(" Вы не выбрали элемент для удаления", "Возникла ошибка при удалении товара из корзины", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        void filtres()
        {
            try
            {
                List<Menu> menus = DataBase.tbE.Menu.ToList();
                if (sortCmb != null)
                if (sortCmb.SelectedIndex != -1)
                {

                    ComboBoxItem comboBoxItem = (ComboBoxItem)sortCmb.SelectedItem;
                    switch (comboBoxItem.Content)
                    {
                        case "По возрастанию цены":
                            {
                                menus = menus.OrderBy(x => x.price).ToList();
                                break;
                            }
                        case "По убыванию цены":
                            {
                                menus = menus.OrderByDescending(x => x.price).ToList();
                                break;
                            }
                        case "По наименованию от А до Я":
                            {
                                menus = menus.OrderBy(x => x.titile).ToList();
                                break;
                            }
                        case "По наименованию от Я до А":
                            {
                                menus = menus.OrderByDescending(x => x.titile).ToList();
                                break;
                            }


                    }
                }

                if (cmbFiltres.SelectedIndex != -1)
                {
                    ComboBoxItem comboBox = (ComboBoxItem)cmbFiltres.SelectedItem;
                    switch (comboBox.Content)
                    {
                        case "Менее 500 Р":
                            {
                                menus = menus.Where(x => x.price <= 500).ToList();
                                break;
                            }
                        case "501 - 1000 Р":
                            {
                                menus = menus.Where(x => x.price >= 501 && x.price <= 1000).ToList();
                                break;
                            }
                        case "1001 - 2000 Р":
                            {
                                menus = menus.Where(x => x.price >= 1001 && x.price <= 2000).ToList();
                                break;
                            }
                        case "2001 - 4000 Р":
                            {
                                menus = menus.Where(x => x.price >= 2001 && x.price <= 4000).ToList();
                                break;
                            }
                        case "4001 - 7000 Р":
                            {
                                menus = menus.Where(x => x.price >= 4001 && x.price <= 7000).ToList();
                                break;
                            }
                        case "Более 7000 Р":
                            {
                                menus = menus.Where(x => x.price >= 7001).ToList();
                                break;
                            }
                        default:
                            menus = menus;
                            break;

                    }
                }
                if (cb1 != null)
                {

                    if (cb1.IsChecked == true)
                    {
                        menus = menus.Where(x => x.Photo != null).ToList();
                    }
                }
                if (tbFiltres != null)
                {

                    if (tbFiltres.Text != "")
                    {
                        menus = menus.Where(x => x.titile.ToLower().Contains(tbFiltres.Text.ToLower()) || x.description.ToLower().Contains(tbFiltres.Text.ToLower())).ToList();
                    }
                }

                lVMenu.ItemsSource = menus;
            }
            catch
            {

            }

            
        }
        private void cb1_Checked(object sender, RoutedEventArgs e)
        {
            filtres();
        }
        private void tbFiltres_TextChanged(object sender, TextChangedEventArgs e)
        {
            filtres();
        }

        private void sortCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filtres();
        }

        private void cmbFiltres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filtres();
        }
    }


}



