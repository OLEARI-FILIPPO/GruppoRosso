using Haley.Abstractions;
using Haley.MVVM.Services;
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
using System.Data.SqlClient;


namespace GarageDatabase.ModelView
{
    public partial class InputViewModel: Window
    {

        public static string targa { get; set; }
        public InputViewModel()
        {
            InitializeComponent();



        }

        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-M63NC6P\SQLEXPRESSNEW;Initial Catalog=database;User ID=sa;Password=Fillo-fous05");

        private void ok(object sender, RoutedEventArgs e)
        {

            connection.Open();

            string storico = "INSERT INTO Storico (IdParcheggio, DataOrarioIngresso, Targa) VALUES ('" + HomeViewModel.Bottoni[HomeViewModel.Selected].Name + "', '" + Convert.ToString(DateTime.Now) + "', '" + txtTarga.Text + "')";
            SqlCommand storicoComando = new SqlCommand(storico, connection);
            storicoComando.ExecuteNonQuery();

            connection.Close();


            //HomeViewModel.LisPlate = txtTarga.Text;
            HomeViewModel.Bottoni[HomeViewModel.Selected].Content = txtTarga.Text;


          
            //  targa = txtTarga.Text;
            //SqlConnection connection = new SqlConnection();
            this.Close();

        }
    }
}
