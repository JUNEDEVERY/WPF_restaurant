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
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        Users users;
        public Order()
        {
            InitializeComponent();
            this.users = users;
            DataBase.tbE = new Entities22();
            lVMenu1.ItemsSource = Zakaz.getZakaz();
            double sum = 0;
            int Stat = 0;
            foreach (Zakaz zakaz in Zakaz.getZakaz())
            {
                sum += zakaz.sum;
                if (zakaz.idStatus == 4)
                {
                    Stat++;
                }
            }
            SumZakazov.Text = sum.ToString();
            ZakazovRabota.Text = Stat.ToString();
        }
        private void SostavZakaz_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Tag);
            List<ZakazIzMenu> zakazIzMenus = DataBase.tbE.ZakazIzMenu.Where(x => x.idzakaz == id).ToList();
            Zakaz zakazs = Zakaz.getZakaz().FirstOrDefault(x => x.idZakaz == id);
            WindowSostavZakaza win = new WindowSostavZakaza(zakazIzMenus, zakazs.Status.statustype, zakazs.sum.ToString());
            if (win.ShowDialog() == true)
            {
            }
        }

        private void btngoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage2(users));
        }


        private void Удалить_Click(object sender, RoutedEventArgs e)
        {
          
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Tag);

            List<ZakazIzMenu> zakazIzMenus = DataBase.tbE.ZakazIzMenu.Where((x) => x.idzakaz == id).ToList();
            foreach (ZakazIzMenu zakazIzMenu in zakazIzMenus)
            {
                DataBase.tbE.ZakazIzMenu.Remove(zakazIzMenu);
                
            }
            Zakaz zakaz1 = DataBase.tbE.Zakaz.FirstOrDefault(x => x.idZakaz == id);
            DataBase.tbE.Zakaz.Remove(zakaz1);
            
            DataBase.tbE.SaveChanges();
            lVMenu1.ItemsSource = Zakaz.getZakaz();
            //this.NavigationService.Refresh();
            


        }

        private void Редактировать_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            int id = Convert.ToInt32(btn.Tag);
            Zakaz zakaz = DataBase.tbE.Zakaz.FirstOrDefault(x => x.idZakaz == id);
            NavigationService.Navigate(new AddOrder(zakaz.idZakaz));
        }
    }
}
