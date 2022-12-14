using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrainingWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAdvertisement.xaml
    /// </summary>
    public partial class PageAdvertisement : Page
    {
        public PageAdvertisement()
        {
            InitializeComponent();

            DoubleAnimation doubleAnimationImage = new DoubleAnimation();
            doubleAnimationImage.From = 300;
            doubleAnimationImage.To = 290;
            doubleAnimationImage.Duration = TimeSpan.FromSeconds(0.5);
            doubleAnimationImage.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimationImage.AutoReverse = true;
            doubleAnimationImage.AccelerationRatio = 1; 
            logoSource.BeginAnimation(WidthProperty, doubleAnimationImage);

            DoubleAnimation doubleAnimationImageBurger = new DoubleAnimation();
            doubleAnimationImageBurger.From = 450;
            doubleAnimationImageBurger.To = 300;
            doubleAnimationImageBurger.Duration = TimeSpan.FromSeconds(3);
            doubleAnimationImage.AccelerationRatio = 1;
            doubleAnimationImageBurger.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimationImageBurger.AutoReverse = true;
            imgBurger.BeginAnimation(WidthProperty, doubleAnimationImageBurger);
            // btnGoMenu

          


            DoubleAnimation buttonHeightAnimation = new DoubleAnimation();
            buttonHeightAnimation.From = 70;
            buttonHeightAnimation.To = 150;
            buttonHeightAnimation.Duration = TimeSpan.FromSeconds(1);
            buttonHeightAnimation.RepeatBehavior = RepeatBehavior.Forever;
            buttonHeightAnimation.AutoReverse = true;
            btnGoMenu.BeginAnimation(WidthProperty, buttonHeightAnimation);

            DoubleAnimation buttonWidthAnimation = new DoubleAnimation();
            buttonWidthAnimation.From = 100;
            buttonWidthAnimation.To = 150;
            buttonWidthAnimation.Duration = TimeSpan.FromSeconds(1);
            buttonWidthAnimation.RepeatBehavior = RepeatBehavior.Forever;
            buttonWidthAnimation.AutoReverse = true;
            btnGoMenu.BeginAnimation(HeightProperty, buttonWidthAnimation);

            ThicknessAnimation buttonThicknessAnimation = new ThicknessAnimation();
            buttonThicknessAnimation.From = new Thickness(0, 0, 80, 0);
            buttonThicknessAnimation.To = new Thickness(80, 0, 0, 0);
            buttonThicknessAnimation.Duration = TimeSpan.FromSeconds(4);
            buttonThicknessAnimation.RepeatBehavior = RepeatBehavior.Forever;
            buttonThicknessAnimation.AutoReverse = true;
            btnGoMenu.BeginAnimation(MarginProperty, buttonThicknessAnimation);

            DoubleAnimation animationChangebtnFont = new DoubleAnimation(); 
            animationChangebtnFont.From = 16;
            animationChangebtnFont.To = 25;
            animationChangebtnFont.Duration = TimeSpan.FromSeconds(0.33);
            animationChangebtnFont.RepeatBehavior = RepeatBehavior.Forever;
            animationChangebtnFont.AutoReverse = true;
            btnGoMenu.BeginAnimation(FontSizeProperty, animationChangebtnFont);

            ColorAnimation btnBackgroundAnimation = new ColorAnimation();
            Color colorStart1 = Color.FromRgb(176, 163, 116);
            btnGoMenu.Background = new SolidColorBrush(colorStart1);
            btnBackgroundAnimation.From = Color.FromRgb(255, 229, 204);
            btnBackgroundAnimation.To = Color.FromRgb(236, 191, 83);
            btnBackgroundAnimation.Duration = TimeSpan.FromSeconds(0.5);
            btnBackgroundAnimation.RepeatBehavior = RepeatBehavior.Forever;
            btnBackgroundAnimation.AutoReverse = true;
            btnGoMenu.Background.BeginAnimation(SolidColorBrush.ColorProperty, btnBackgroundAnimation);


            ColorAnimation buttonAnimationColor = new ColorAnimation(); 
            ColorConverter colorConverter = new ColorConverter();
            Color colorStart2 = (Color)colorConverter.ConvertFrom("#ffa812");
            btnGoMenu.Background = new SolidColorBrush(colorStart2);
            buttonAnimationColor.From = colorStart1;
            buttonAnimationColor.To = (Color)colorConverter.ConvertFrom("#ffba01");
            buttonAnimationColor.Duration = TimeSpan.FromSeconds(2);
            buttonAnimationColor.RepeatBehavior = RepeatBehavior.Forever;
            animationChangebtnFont.AutoReverse = true;
            btnGoMenu.Background.BeginAnimation(SolidColorBrush.ColorProperty, buttonAnimationColor);


            DoubleAnimation tbAnimationFont = new DoubleAnimation();
            tbAnimationFont.From = 15;
            tbAnimationFont.To = 10;
            tbAnimationFont.Duration = TimeSpan.FromSeconds(0.5);
            tbAnimationFont.RepeatBehavior = RepeatBehavior.Forever;
            tbAnimationFont.AutoReverse = true;
            tbAnimationFont.AccelerationRatio = 1;
            tbText.BeginAnimation(FontSizeProperty, tbAnimationFont);


            ColorAnimation tbAnimationColor = new ColorAnimation(); 
            ColorConverter colorConverter2 = new ColorConverter();
            Color colorStart3 = (Color)colorConverter2.ConvertFrom("#FFFFFF");
            tbText.Background = new SolidColorBrush(colorStart3);
            tbAnimationColor.From = colorStart3;
            tbAnimationColor.From = Color.FromRgb(252, 237, 237);
            tbAnimationColor.To = Color.FromRgb(242, 188, 94);
            tbAnimationColor.Duration = TimeSpan.FromSeconds(1);
            tbAnimationColor.RepeatBehavior = RepeatBehavior.Forever;
            tbAnimationFont.AutoReverse = true;
            tbText.Background.BeginAnimation(SolidColorBrush.ColorProperty, tbAnimationColor);

        }

        private void btnGoMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FirstPage());
        }
    }
}
