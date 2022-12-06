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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrainingWPF.ModelDB;


namespace TrainingWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>Grid.Row="1"
    public partial class AddOrder : Page
    {

        List<MyOrder> list = new List<MyOrder>();
        int u_id;
        public struct MyOrder
        {
            public int k;
            public Zakaz zakaz;
            public ModelDB.Menu menus;
        }
        List<ModelDB.Menu> menus = DataBase.tbE.Menu.ToList();
        List<Status> status = DataBase.tbE.Status.ToList();
        List<string> str = new List<string>();
        int id_zakaz;
        public AddOrder(int id_zakaz)
        {
            InitializeComponent();
            this.id_zakaz = id_zakaz;
            List<ZakazIzMenu> zakazIzMenus = DataBase.tbE.ZakazIzMenu.Where(x => x.idzakaz == id_zakaz).ToList();
            foreach (ZakazIzMenu zakazIzMenu in zakazIzMenus)
            {
                MyOrder order = new MyOrder()
                {
                    k = zakazIzMenu.quantity,
                    menus = zakazIzMenu.Menu

                };
                list.Add(order);
            }

            foreach (MyOrder myOrder in list)
            {
                lVMenu.Items.Add(myOrder.menus.titile + " в количестве " + myOrder.k + " шт.");
            }

            //lVMenu.ItemsSource = str;
            cmbBludo.ItemsSource = menus;
            cmbBludo.SelectedValuePath = "idMenu";
            cmbBludo.DisplayMemberPath = "titile";
            STATUS.ItemsSource = status;
            STATUS.SelectedValuePath = "idStatus";
            STATUS.DisplayMemberPath = "statustype";

            STATUS.SelectedValue = DataBase.tbE.Zakaz.FirstOrDefault(x => x.idZakaz == id_zakaz).idStatus;

        }

        private void cmbBludo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbcount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            tb1.Text = rx.Replace(tb1.Text, "");
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (list.Where(x => x.menus.titile == cmbBludo.Text).Count() == 0)
                {
                    MyOrder myOrder = new MyOrder();
                    myOrder.menus = DataBase.tbE.Menu.FirstOrDefault(x => x.titile == cmbBludo.Text);

                    myOrder.k += Convert.ToInt32(tb1.Text);
                    //myOrder.k = 1;
                    list.Add(myOrder);

                }
                else
                {
                    MyOrder order = list.FirstOrDefault(x => x.menus.titile == cmbBludo.Text);
                    order.k += Convert.ToInt32(tb1.Text);
                    //order.k++;
                  
                    list.Remove(list.FirstOrDefault(x => x.menus.titile == cmbBludo.Text));
                    
                    list.Add(order);
                }
                lVMenu.Items.Clear();
                foreach (MyOrder myOrder in list)
                {
                    lVMenu.Items.Add(myOrder.menus.titile + " в количестве " + myOrder.k + " шт.");
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Минутку....", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            

        }
        Regex rx = new Regex(@"\D", RegexOptions.IgnoreCase);
        private void tb1_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb1.Text = rx.Replace(tb1.Text, "");
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (lVMenu.SelectedItems.Count != 0)
                {
                    string str = lVMenu.SelectedValue.ToString().Substring(0, lVMenu.SelectedValue.ToString().IndexOf(" в количестве"));

                    list.Remove(list.FirstOrDefault(x => x.menus.titile == str));
                    lVMenu.Items.Clear();
                    foreach (MyOrder myOrder in list)
                    {
                        lVMenu.Items.Add(myOrder.menus.titile + " в количестве " + myOrder.k + " шт.");
                    }
                }
                else
                {
                    MessageBox.Show("Вы не выбрали элемент для удаления", "Возникла ошибка при удалении товара из корзины", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Вы не выбрали элемент для удаления", "Возникла ошибка при удалении товара из корзины", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //str.Remove(lVMenu.SelectedValue.ToString());
            //lVMenu.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Zakaz zakaz = DataBase.tbE.Zakaz.FirstOrDefault(x => x.idZakaz == id_zakaz);
                zakaz.idStatus = Convert.ToInt32(STATUS.SelectedValue);
                List<ZakazIzMenu> zakazIzMenus = DataBase.tbE.ZakazIzMenu.Where((x) => x.idzakaz == id_zakaz).ToList();
                foreach (ZakazIzMenu zakazIzMenu in zakazIzMenus)
                {
                    DataBase.tbE.ZakazIzMenu.Remove(zakazIzMenu);

                }
                foreach (MyOrder myOrder in list)
                {
                    ZakazIzMenu zakazIzMenu = new ZakazIzMenu()
                    {
                        idMenu = myOrder.menus.idMenu,
                        idNapitok = 1,
                        idzakaz = id_zakaz,
                        quantity = myOrder.k
                    };
                    DataBase.tbE.ZakazIzMenu.Add(zakazIzMenu);
                }
                DataBase.tbE.SaveChanges();
                MessageBox.Show("Заказ успешно изменен", "Готово!", MessageBoxButton.OK, MessageBoxImage.Information);
                
                NavigationService.Navigate(new Order());
                

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Минутку....", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}