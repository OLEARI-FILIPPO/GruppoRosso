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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

 

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)(ToggleButton.IsChecked == true))
            {
                Titolo.Text = "GARAGE";
                Aggiungi_Piano.Content = "AGGIUNGI PIANO";
                Rimuovi_Piano.Content = "RIMUOVI PIANO";
                ToggleButton.Margin = new Thickness() { Left = 50 };
                ColonnaMenu.Width = new GridLength(250);
            }
            else
            {
                Titolo.Text = "";
                Aggiungi_Piano.Content = "";
                Rimuovi_Piano.Content = "";
                ToggleButton.Margin = new Thickness() { Left = 5, Right = 5, Top = 0, Bottom = 0 } ;
                ColonnaMenu.Width = new GridLength(75);
            }
        }
    }
}
