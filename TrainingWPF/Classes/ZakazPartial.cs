using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingWPF.ModelDB;

namespace TrainingWPF.ModelDB
{
    public partial class Zakaz
    {
        public double sum { get; set; }
        public string backgroud { get; set; }
        public static List<Zakaz> getZakaz()
        {
            List<Zakaz> zakaz = DataBase.tbE.Zakaz.ToList();
            
            foreach (Zakaz zakaz1 in zakaz)
            {
                zakaz1.sum = 0;
                foreach (ZakazIzMenu zakazIzMenus in DataBase.tbE.ZakazIzMenu.Where(x=>x.idzakaz== zakaz1.idZakaz))
                {
                    Menu menu = DataBase.tbE.Menu.FirstOrDefault(x=>x.idMenu == zakazIzMenus.idMenu);
                    zakaz1.sum += Convert.ToDouble(menu.price) * Convert.ToDouble(zakazIzMenus.quantity);
                }
                if (zakaz1.idStatus == 1)
                {
                    zakaz1.backgroud = "#A0F38E";
                }
                if (zakaz1.idStatus != 1 && zakaz1.datetime < DateTime.Today.AddDays(1))
                {
                    zakaz1.backgroud = "#FC917A";
                }
                if (zakaz1.idStatus != 1 && zakaz1.datetime > DateTime.Today)
                {
                    zakaz1.backgroud = "#FEF790";
                }
                if(zakaz1.idStatus == 2)
                {
                    zakaz1.backgroud = "#fff700";
                }
                if (zakaz1.idStatus == 4)
                {
                    zakaz1.backgroud = "#54ecf5";
                }
            }
            return zakaz;
        }
    }
}
