using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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
using static System.Net.Mime.MediaTypeNames;

namespace TrainingWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccount.xaml
    /// </summary>
    public partial class PersonalAccount : Page
    {
        Users users;

    

        List<Country> countryList = DataBase.tbE.Country.ToList();
        List<City> cityList = DataBase.tbE.City.ToList();
        List<GenderTable> genderList = DataBase.tbE.GenderTable.ToList();
        List<UserPhoto> userPhotos = DataBase.tbE.UserPhoto.ToList();
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
        public PersonalAccount(Users user)
        {
            InitializeComponent();
            this.users = user;
         

            Surname.Text = user.Surname;
            Name.Text = user.Name;
            Patronymic.Text = user.Patronymic;
            cmbCity.ItemsSource = cityList;
            cmbCity.SelectedValuePath = "idCity";
            cmbCity.DisplayMemberPath = "nameCity";
            cmbCountry.ItemsSource = countryList;
            cmbCountry.SelectedValuePath = "idCountry";
            cmbCountry.DisplayMemberPath = "nameCountry";
            cmbGender.ItemsSource = genderList;
            cmbGender.SelectedValuePath = "IdGender";
            cmbGender.DisplayMemberPath = "gender";

            cmbCity.SelectedValue = DataBase.tbE.City.FirstOrDefault(x => x.idCity == user.idCity).idCity;
            cmbCountry.SelectedValue = DataBase.tbE.Country.FirstOrDefault(x => x.idCountry == user.idCountry).idCountry;
            cmbGender.SelectedValue = DataBase.tbE.GenderTable.FirstOrDefault(x => x.IdGender == user.IdGender).IdGender;
            if (user.photo != null)
            {

                byte[] Bar = user.photo;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
                showIMG(Bar, nullPhoto);  // отображаем картинку
            }
            if(user.idRole == 1)
            {
                btnMenu.Visibility = Visibility.Collapsed;
            }
            if (user.idRole == 2)
            {
                btnBack.Visibility = Visibility.Collapsed;
            }


        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            List<UserPhoto> users1 = DataBase.tbE.UserPhoto.Where(x => x.id_client == users.id_client).ToList();
            byte[] Bar = users1[count].photoBinary;
            showIMG(Bar, nullPhoto);
            users.MainPhotoid = users1[count].idPhoto;
            DataBase.tbE.SaveChanges();

            MessageBox.Show("Фото изменено");
            showIMG(Bar, nullPhoto);
            backPicture.Visibility = Visibility.Collapsed;
            goPicture.Visibility = Visibility.Collapsed;
            btnChange.Visibility = Visibility.Collapsed;

        }

        private void openGallery_Click(object sender, RoutedEventArgs e)
        {
            //userId = user.id_client;
            WindowChangePhotoGallery window = new WindowChangePhotoGallery(users);
            window.Show();

            window.Closing += (obj, args) =>
            {
                if (users.photo != null)
                {
                    byte[] Barr = users.photo;
                    BitmapImage Bim = new BitmapImage();
                    using (MemoryStream MS = new MemoryStream(Barr))
                    {
                        Bim.BeginInit();
                        Bim.StreamSource = MS;
                        Bim.CacheOption = BitmapCacheOption.OnLoad;
                        Bim.EndInit();
                    }
                    nullPhoto.Source = Bim;
                    nullPhoto.Stretch = Stretch.Uniform;
                }
                else
                {
                    string path = Environment.CurrentDirectory;
                    path = path.Replace("bin\\Debug", "Resources\\nullphoto.jpg");
                    nullPhoto.Source = BitmapFrame.Create(new Uri(path));
                }
            };


        }


        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ChangeFIO change = new ChangeFIO(users);
            change.ShowDialog();
            NavigationService.Navigate(new PersonalAccount(users));
        }

        private void changePhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserPhoto userPhoto = new UserPhoto();
                userPhoto.id_client = users.id_client;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.ShowDialog();
                string path = openFileDialog.FileName;
                System.Drawing.Image SDI = System.Drawing.Image.FromFile(path);
                ImageConverter IC = new ImageConverter();
                byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));
                userPhoto.photoBinary = Barray;  // заполяем поле photoBinary полученным байтовым массивом
                DataBase.tbE.UserPhoto.Add(userPhoto);  // добавляем объект в таблицу БД
                users.MainPhotoid = userPhoto.idPhoto;
                DataBase.tbE.SaveChanges();  // созраняем изменения в БД
                MessageBox.Show("Фото добавлено");
                showIMG(Barray, nullPhoto);
                this.NavigationService.Refresh();
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Минутку....", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                if (openFileDialog.ShowDialog() == true)
                {
                    foreach (string file in openFileDialog.FileNames)
                    {
                        UserPhoto userPhoto = new UserPhoto();
                        userPhoto.id_client = users.id_client;
                        System.Drawing.Image SDI = System.Drawing.Image.FromFile(file);  // создаем объект для загрузки изображения в базу
                        ImageConverter IC = new ImageConverter();  // создаем конвертер для перевода картинки в двоичный формат
                        byte[] Barray = (byte[])IC.ConvertTo(SDI, typeof(byte[]));  // создаем байтовый массив для хранения картинки
                        userPhoto.photoBinary = Barray;  // заполяем поле photoBinary полученным байтовым массивом
                        DataBase.tbE.UserPhoto.Add(userPhoto);  // добавляем объект в таблицу БД
                    }
                    DataBase.tbE.SaveChanges();
                    MessageBox.Show("Фото добавлены");
                    this.NavigationService.Refresh();
                }

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Минутку....", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        int count = 0;

        private void backPicture_Click(object sender, RoutedEventArgs e)
        {
            List<UserPhoto> users1 = DataBase.tbE.UserPhoto.Where(x => x.id_client == users.id_client).ToList();
            count--;
            if (goPicture.IsEnabled == false)
            {
                goPicture.IsEnabled = true;
            }
            if (users1.Count != 0)  // если объект не пустой, начинает переводить байтовый массив в изображение
            {

                byte[] Bar = users1[count].photoBinary;
                BitmapImage BI = new BitmapImage();  // создаем объект для загрузки изображения
                showIMG(Bar, nullPhoto);
            }
            if (count == 0)
            {
                backPicture.IsEnabled = false;
            }

        }

        private void goPicture_Click(object sender, RoutedEventArgs e)
        {
            List<UserPhoto> users1 = DataBase.tbE.UserPhoto.Where(x => x.id_client == users.id_client).ToList();
            count++;
            if (backPicture.IsEnabled == false)
            {
                backPicture.IsEnabled = true;
            }
            if (users1.Count != 0 && users1.Count > count)
            {

                byte[] Bar = users1[count].photoBinary;
                showIMG(Bar, nullPhoto);
            }
            if (count == users1.Count - 1)
            {
                goPicture.IsEnabled = false;
            }
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                WindowChangePasswordLogin windowChangePasswordLogin = new WindowChangePasswordLogin(users);
                windowChangePasswordLogin.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowChangePhotoGallery windowChangePhotoGallery = new WindowChangePhotoGallery(users);
            windowChangePhotoGallery.Show();

            //if (userPhotos.Count != 0)
            //{

            //    byte[] Bar = userPhotos.FirstOrDefault().photoBinary;   // считываем изображение из базы (считываем байтовый массив двоичных данных) - выбираем последнее добавленное изображение
            //    showIMG(Bar, nullPhoto);  // отображаем картинку
            //}
            windowChangePhotoGallery.Closed += (obj, args) =>
            {
                if (users.photo != null)
                {
                    byte[] bytes = users.photo;
                    BitmapImage bitmapImage = new BitmapImage();
                    using (MemoryStream memory = new MemoryStream(bytes))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memory;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                    }
                    nullPhoto.Source = bitmapImage;
                    nullPhoto.Stretch = Stretch.Uniform;

                }
                else
                {
                    string path = Environment.CurrentDirectory;
                    path = path.Replace("bin\\Debug", "Resources\\nullphoto.png");
                    nullPhoto.Source = BitmapFrame.Create(new Uri(path));

                }

            };

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {


            users.IdGender = cmbGender.SelectedIndex + 1;
            users.idCity = cmbCity.SelectedIndex + 1;
            DataBase.tbE.SaveChanges();
            MessageBox.Show("Изменения внесены!");



        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

            if (users.idRole == 1)
            {
                NavigationService.Navigate(new AdminPage2(users));

            }
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new ShowMenu(users.id_client));

        }
    }
}
