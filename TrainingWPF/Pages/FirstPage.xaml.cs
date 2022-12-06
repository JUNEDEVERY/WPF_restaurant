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

namespace TrainingWPF
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    /// 
    public partial class FirstPage : Page
    {
        //List<Role> RT = DataBase.tbE.Role.ToList();

        public FirstPage()
        {
            InitializeComponent();
            //tbAdmin.Text = RT[0].roleName;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            FrameWork.MainFrame.Navigate(new Autorization());
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            FrameWork.MainFrame.Navigate(new Registration());

        }
    }
}
