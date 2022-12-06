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

                if(Zakaz.Items.Count != 0)
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
    }


}



