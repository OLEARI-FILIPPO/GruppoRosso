using GarageDatabase.ModelView;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Haley.MVVM.Services;
using Haley.Abstractions;

namespace GarageDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IDialogService ds;
        public MainWindow()
        {
            InitializeComponent();
            ds = new DialogService();
            DataContext = new HomeViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var result = ds.ShowDialog("Attenzione!", "Terminare l'esecuzione?", 
                                        Haley.Enums.NotificationIcon.Warning, 
                                        Haley.Enums.DialogMode.Confirmation, blurWindows: true);

            var res = result.DialogResult;
            if (res.HasValue  && res.Value)
            {
                Close();
            }
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
