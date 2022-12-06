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
    /// Логика взаимодействия для WindowSostavZakaza.xaml
    /// </summary>
    public partial class WindowSostavZakaza : Window
    {
        public WindowSostavZakaza(List<ZakazIzMenu> zakazIzMenu, string Status, string SumZaka)
        {
            InitializeComponent();
            lVMenu.ItemsSource = zakazIzMenu;
            SumZakaz.Text = SumZaka;
            StatusZakaz.Text = Status;

        }
    }
}
