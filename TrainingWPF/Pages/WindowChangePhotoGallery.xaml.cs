using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для WindowChangePhotoGallery.xaml
    /// </summary>
    public partial class WindowChangePhotoGallery : Window
    {
        Users user;
        public WindowChangePhotoGallery(Users user)
        {
            InitializeComponent();
            this.user = user;
            lvGallery.ItemsSource = DataBase.tbE.UserPhoto.Where(x => x.id_client == user.id_client).ToList();
            lvGallery.SelectedValuePath = "idPhoto";


        }
        void showIMG(byte[] bArray, System.Windows.Controls.Image image)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(bArray))
            {
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }
            image.Source = bitmapImage;
            image.Stretch = Stretch.Uniform;
        }
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            try
            {
              
                int index = Convert.ToInt32(lvGallery.SelectedValue);
                UserPhoto photo = DataBase.tbE.UserPhoto.FirstOrDefault(x => x.id_client == user.id_client  && x.idPhoto == index);
                user.photo = photo.photoBinary;

                DataBase.tbE.SaveChanges();
                MessageBox.Show("Фото изменено");
                this.Close();
                 
            }
            catch
            {

            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                
                UserPhoto userPhoto = new UserPhoto();
                userPhoto.id_client = user.id_client;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                string path = openFileDialog.FileName;
                System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);
                ImageConverter IC = new ImageConverter();
                byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                userPhoto.photoBinary = Barray;  // заполяем поле photoBinary полученным байтовым массивом
                DataBase.tbE.UserPhoto.Add(userPhoto);  // добавляем объект в таблицу БД
                DataBase.tbE.SaveChanges();  
                MessageBox.Show("Фото добавлено");
                lvGallery.ItemsSource = DataBase.tbE.UserPhoto.Where(x => x.id_client == user.id_client).ToList();
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Минутку....", MessageBoxButton.OK, MessageBoxImage.Error);
            }
         
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            

            
            int index = Convert.ToInt32(lvGallery.SelectedValue);
            UserPhoto userPhoto = DataBase.tbE.UserPhoto.FirstOrDefault(x => x.idPhoto == index);
            DataBase.tbE.UserPhoto.Remove(userPhoto);
            DataBase.tbE.SaveChanges();
            lvGallery.ItemsSource = DataBase.tbE.UserPhoto.Where(x => x.id_client == user.id_client).ToList();
            MessageBox.Show("Фото удалено");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void img_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Controls.Image img = (System.Windows.Controls.Image)sender;
            int index = Convert.ToInt32(img.Uid);
            UserPhoto userPhoto = DataBase.tbE.UserPhoto.FirstOrDefault(x => x.idPhoto == index);
            byte[] Barr = userPhoto.photoBinary;
            BitmapImage Bim = new BitmapImage();
            using (MemoryStream MS = new MemoryStream(Barr))
            {
                Bim.BeginInit();
                Bim.StreamSource = MS;
                Bim.CacheOption = BitmapCacheOption.OnLoad;
                Bim.EndInit();
            }
            img.Source = Bim;
            img.Stretch = Stretch.Uniform;

        }
    }
}
