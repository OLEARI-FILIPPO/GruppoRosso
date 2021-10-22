using GarageDatabase.ModelView;
using GarageDatabase.View;
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

namespace GarageDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new HomeViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)(ToggleButton.IsChecked == true))
            {
                menuToggleIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Close; ;
                Titolo.Text = "GARAGE";
                GestionePiani.Content = "Gestione Piani";
                Home.Content = "Home";
                ToggleButton.Margin = new Thickness() { Left = 50 };
                ColonnaMenu.Width = new GridLength(250);
            }
            else
            {
                menuToggleIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Bars; ;

                Home.Content = "";
                Titolo.Text = "";
                GestionePiani.Content = "";
                ToggleButton.Margin = new Thickness() { Left = 5, Right = 5, Top = 0, Bottom = 0 };
                ColonnaMenu.Width = new GridLength(75);
            }
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            GestionePianiIcon.Foreground = new SolidColorBrush(Color.FromRgb(0, 233, 225));
            GestionePiani.Foreground = new SolidColorBrush(Color.FromRgb(0, 233, 225));
            GestioneSP.Cursor = Cursors.Hand;
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            GestionePianiIcon.Foreground = Brushes.White;
            GestionePiani.Foreground = Brushes.White;
            GestioneSP.Cursor = Cursors.Arrow;
        }

        private void GestioneSP_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void GestioneButton_Checked(object sender, RoutedEventArgs e)
        {
            GestionePiani1.Visibility = Visibility.Visible;
            HomeParcheggio.Visibility = Visibility.Hidden;

        }

        private void HomeButton_Checked(object sender, RoutedEventArgs e)
        {
            GestionePiani1.Visibility = Visibility.Hidden;
            HomeParcheggio.Visibility = Visibility.Visible;

        }

        private void ToggleButton_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void ToggleButton_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}
