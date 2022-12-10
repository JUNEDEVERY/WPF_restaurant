using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TrainingWPF.Classes;
using TrainingWPF.ModelDB;

namespace TrainingWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        PageChange pc = new PageChange();
        List<ZakazIzMenu> zakazs1 = new List<ZakazIzMenu>();
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

            zakazs1 = DataBase.tbE.ZakazIzMenu.ToList();
            pc.CountPage = Zakaz.getZakaz().ToList().Count;
            DataContext = pc;
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

        private void GoPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    pc.CurrentPage--;
                    break;
                case "next":
                    pc.CurrentPage++;
                    break;
                default:
                    pc.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            lVMenu1.ItemsSource = Zakaz.getZakaz().Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();  // оображение записей постранично с определенным количеством на каждой странице
            // Skip(pc.CurrentPage* pc.CountPage - pc.CountPage) - сколько пропускаем записей
            // Take(pc.CountPage) - сколько записей отображаем на странице
        }

        private void txtPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                pc.CountPage = Convert.ToInt32(txtPageCount.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                pc.CountPage = zakazs1.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            pc.Countlist = Zakaz.getZakaz().Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
                lVMenu1.ItemsSource = Zakaz.getZakaz().Skip(0).Take(pc.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            pc.CurrentPage = 1; // текущая страница - это страница 1
        }

        private void txtBackPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pc.CurrentPage = 1;
            lVMenu1.ItemsSource = Zakaz.getZakaz().Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();  // отображаем первые записи в том количестве, которое равно CountPage

        }

        private void txtNextPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            pc.CurrentPage = pc.CountPages;
            lVMenu1.ItemsSource = Zakaz.getZakaz().Skip(pc.CurrentPage * pc.CountPage - pc.CountPage).Take(pc.CountPage).ToList();
        }
    }

   
}



