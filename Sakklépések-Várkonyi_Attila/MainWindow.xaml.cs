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

namespace Sakklépések_Várkonyi_Attila
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Rectangle[,] mezo;
        public MainWindow()
        {
            InitializeComponent();
            TablaBetolt();
        }

        private void TablaBetolt()
        {
            for (int i = 0; i < 8; i++)
            {
                tabla.RowDefinitions.Add(new RowDefinition());
                tabla.ColumnDefinitions.Add(new ColumnDefinition());
            }

            mezo = new Rectangle[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Rectangle ujMezo = new Rectangle();
                    ujMezo.Stroke = Brushes.Black;
                    ujMezo.StrokeThickness = 1;
                    if ((i+j) % 2 == 0)
                    {
                        ujMezo.Fill = Brushes.White;
                    }
                    else
                    {
                        ujMezo.Fill = Brushes.Black;
                        ujMezo.Stroke = Brushes.White;
                    }
                    ujMezo.PreviewMouseLeftButtonDown += Kivalasztas;
                    mezo[i, j] = ujMezo;
                    tabla.Children.Add(mezo[i, j]);
                    Grid.SetRow(mezo[i, j], i);
                    Grid.SetColumn(mezo[i, j], j);

                }
            }

        }

        private void Kivalasztas(object sender, MouseButtonEventArgs e)
        {
            Rectangle kivalasztott = sender as Rectangle;
            // Button kival = sender as Button v image helyette és vhogy igy.
            int x = -1;
            int y = -1;
            //Vegigmegyunk a táblán és ha a kiválasztott kockát megtalálta kapjuk meg a koordinátáit
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    
                    if(mezo[i, j].Equals(kivalasztott))
                    {
                        x = i;
                        y = j;
                    }
                    //A régebbi kiválasztott elemek visszaszínezése
                    if ((i + j) % 2 == 0)
                    {
                        mezo[i, j].Fill = Brushes.White;
                    }
                    else
                    {
                        mezo[i, j].Fill = Brushes.Black;
                        mezo[i, j].Stroke = Brushes.White;
                    }
                }
            }
            string betu = "a";
            if (y == 0)
            {
                betu = "H";
            }
            if (y == 1)
            {
                betu = "G";
            }
            if (y == 2)
            {
                betu = "F";
            }
            if (y == 3)
            {
                betu = "E";
            }
            if (y == 4)
            {
                betu = "D";
            }
            if (y == 5)
            {
                betu = "C";
            }
            if (y == 6)
            {
                betu = "B";
            }
            if (y == 7)
            {
                betu = "A";
            }

            if (sakkBabuk.SelectedItem == kiraly)
            {
            kivalasztott.Fill = new ImageBrush(new BitmapImage(
                new Uri(@"kiraly.png", UriKind.Relative)));
                pozicio.Visibility = Visibility.Visible;
                pozicio.Content = "Mező pozíciója: " + betu + $"{x}";
            }
            if (sakkBabuk.SelectedItem == kiralyNo)
            {
                kivalasztott.Fill = new ImageBrush(new BitmapImage(
                    new Uri(@"kiralyno.png", UriKind.Relative)));
                pozicio.Visibility = Visibility.Visible;
                pozicio.Content = "Mező pozíciója: " + betu + $"{x}";
            }
            if(sakkBabuk.SelectedItem == futo)
            {
                kivalasztott.Fill = new ImageBrush(new BitmapImage(
                    new Uri(@"futo.png", UriKind.Relative)));
                pozicio.Visibility = Visibility.Visible;
                pozicio.Content = "Mező pozíciója: " + betu + $"{x}";
            }
            if (sakkBabuk.SelectedItem == huszar)
            {
                
                kivalasztott.Fill = new ImageBrush(new BitmapImage(
                    new Uri(@"huszar.png", UriKind.Relative)));
                pozicio.Visibility = Visibility.Visible;
                pozicio.Content = "Mező pozíciója: " + betu + $"{x}";
            }
            if (sakkBabuk.SelectedItem == bastya)
            {
                for (int i = x; i < 8; i++)
                {
                    mezo[i, y].Fill = Brushes.Red;
                }
                for (int i = x; i > 0; i--)
                {
                    mezo[i-1, y].Fill = Brushes.Red;
                }
                for (int i = y; i < 8; i++)
                {
                    mezo[x, i].Fill = Brushes.Red;
                }
                for (int i = y; i > 0; i--)
                {
                    mezo[x, i - 1].Fill = Brushes.Red;
                }
                kivalasztott.Fill = new ImageBrush(new BitmapImage(
                    new Uri(@"bastya.png", UriKind.Relative)));
                pozicio.Visibility = Visibility.Visible;
                pozicio.Content = "Mező pozíciója: " + betu + $"{x}";
            }
            if (sakkBabuk.SelectedItem == feherGy)
            {
                if (x >= 1)
                {
                    mezo[x - 1, y - 1].Fill = Brushes.Red;
                    mezo[x - 1, y + 1].Fill = Brushes.Red;
                }
                else
                    MessageBox.Show("A fehér gyalog nem tud tovább lépni!");
                
                kivalasztott.Fill = new ImageBrush(new BitmapImage(
                    new Uri(@"feherGy.png", UriKind.Relative)));
                pozicio.Visibility = Visibility.Visible;
                pozicio.Content = "Mező pozíciója: " + betu + $"{x}";
            }
            if (sakkBabuk.SelectedItem == feketeGy)
            {
                if (x <= 6)
                {
                    mezo[x + 1, y - 1].Fill = Brushes.Red;
                    mezo[x + 1, y + 1].Fill = Brushes.Red;
                }
                else
                    MessageBox.Show("A fekete gyalog nem tud tovább lépni!");
                
                kivalasztott.Fill = new ImageBrush(new BitmapImage(
                    new Uri(@"feketeGy.png", UriKind.Relative)));
                pozicio.Visibility = Visibility.Visible;
                pozicio.Content = "Mező pozíciója: " + betu + $"{x}";
            }


        }
    }
}
