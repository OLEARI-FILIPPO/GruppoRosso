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
using GarageDatabase.Classi;

namespace GarageDatabase.ModelView
{
    public partial class HomeViewModel : UserControl
    {

       
       public HomeViewModel()
       {
            InitializeComponent();
            btn_entra.Visibility = Visibility.Hidden;
            btn_esci.Visibility = Visibility.Hidden;

            connection.Open();
             //MessageBox.Show(id);
            string commandoInserimento = "DELETE FROM Parcheggio";
            SqlCommand command = new SqlCommand(commandoInserimento, connection);
            command.ExecuteNonQuery();
            connection.Close();
            //  GeneraGrid();

        }

        public static Dictionary<string, Button> Bottoni = new Dictionary<string, Button>(); //in dizionario salviami le auto parcheggiate
        Dictionary<string, Parcheggio> Parcheggi = new Dictionary<string, Parcheggio>();

        //string constring = "Server = DESKTOP-R6PQGP4\\SQLEXPRESSNEW; Database=Test; User Id=sa; Password = Danishveer&17012001";
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-R6PQGP4\SQLEXPRESSNEW;Initial Catalog=GestioneParcheggio;User ID=sa;Password=Danishveer&17012001");

        int Nrow = 7, Ncol = 7; //numero di colonne e numero di righe

        private void GeneraGrid()
        {
     
            GeneraRigheDinamiche(Nrow);
            GeneraColDinamiche(Ncol);

            GeneraButton();
            CreatParking();

        }

        private void GeneraRigheDinamiche(int Nrow) //funzione per generare le righe in modo dinamico
        {
            for (int i = 0; i < Nrow; i++) //ciclo for che genera le righe
            {
                DynamicGrid.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void GeneraColDinamiche(int Ncol)
        {

            for (int i = 0; i < Ncol; i++)
            {
                DynamicGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

        }

        private void GeneraButton()
        {
            int iRow = -1;
            foreach (RowDefinition row in DynamicGrid.RowDefinitions)
            {
                iRow++;
                int jCol = -1;

                //I pannelli sono uno dei tipi di controllo più importanti di WPF. Fungono da contenitori per altri controlli
                //e controllano il layout delle finestre/pagine.


                foreach (ColumnDefinition column in DynamicGrid.ColumnDefinitions)
                {
                    jCol++;

                    Border panel = new Border();
                    {
                        BorderThickness = new Thickness(1);
                    }

                    Grid.SetColumn(panel, jCol);
                    Grid.SetRow(panel, iRow);

                    //genero il button
                    Button B = new Button()
                    {
                        Margin = new Thickness(3),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,

                        Style = FindResource("StileVeicolo") as Style,
                        Name = "P" + iRow.ToString() + jCol.ToString(),     //nome del bottone ho messo nome del button uguale al contenuto 

                        Content = "P" + iRow.ToString() + jCol.ToString(), //assegno come il content il numero della cella che corrisponde alla righa e colonna in cui si trova
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch,
                        Background = new SolidColorBrush(Colors.White),

                    };
                    B.Click += ParhceggioClick;

                    Bottoni.Add("P" + iRow.ToString() + jCol.ToString(), B);
                    

                    panel.Child = B;
                    DynamicGrid.Children.Add(panel);

                }


            }
        }

        string oldKey = "P00";
        Button cliccato;
        bool clickOrNot = false;
        public static string Selected = "";
        private void ParhceggioClick(object sender, RoutedEventArgs e)
        {
            
            clickOrNot = true;
            Selected = ((Button)sender).Name;
            Bottoni[oldKey].Style = FindResource("StileVeicolo") as Style;
            ((Button)sender).Style = FindResource("VeicoloClick") as Style;
            cliccato = ((Button)sender);
            oldKey = ((Button)sender).Name;
        }
        
        private void CreatParking()
        {

  
           // int counter = 0;
            string id = " ";
         //   int mol = 2 * 2;
          //  SqlCommand command;

            //string commandoInserimento = "Insert INTO PARCHEGGIO"
            for (int i = 0; i < Nrow; i++)
            {
                for (int j = 0; j < Ncol; j++)
                {

                    id = "P" + i.ToString() + j.ToString();

                    openConnect(id);

                  //  counter++;

                    Parcheggi.Add("P" + i.ToString() + j.ToString(), new Parcheggio(id));


                }
            }

        }

        private void openConnect(string id)
        {
            connection.Open();
          //  MessageBox.Show(id);
            string commandoInserimento = "INSERT INTO Parcheggio VALUES ('" + id + "', 0, NULL, NULL,NULL)";
            SqlCommand command = new SqlCommand(commandoInserimento, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }

        //Parte la message box personalizzata

        private string Entra(string id)
        {
            int bit = -1;
            connection.Open();
            //  MessageBox.Show(id);
            string commandoSelect = "SELECT Stato FROM Parcheggio WHERE IdParcheggio='"+id+"'";
            SqlCommand command = new SqlCommand(commandoSelect, connection);
            bit = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            MessageBox.Show(bit.ToString());

            if (bit == 0)
            {
                connection.Open();
                MessageBox.Show(id);
                string commandoUpdate = "UPDATE Parcheggio set Stato = 1 WHERE IdParcheggio = '"+id+"'";
                SqlCommand UPcommand = new SqlCommand(commandoUpdate, connection);
                UPcommand.ExecuteNonQuery();
                connection.Close();
                return id;
            }
            else
            {
                return null;
            }

          
        }

        private string Esci(string id, Button b)
        {
            connection.Open();
            MessageBox.Show(id);
            string commandoUpdate = "UPDATE Parcheggio set Stato = 0 WHERE IdParcheggio = '" + id + "'";
            SqlCommand UPcommand = new SqlCommand(commandoUpdate, connection);

            b.Content = id;
            b.Style = FindResource("StileVeicolo") as Style;
            UPcommand.ExecuteNonQuery();
            connection.Close();
            return "Tempo";
        }

        private void Btn_genera_Click(object sender, RoutedEventArgs e)
        {
            Nrow = Convert.ToInt32(SelezionaRow.Value);
            Ncol = Convert.ToInt32(SelezionaCol.Value);
            Bottoni.Clear();
            Parcheggi.Clear();
            btn_genera.Visibility = Visibility.Hidden;
            btn_entra.Visibility = Visibility.Visible;
            btn_esci.Visibility = Visibility.Visible;

            GeneraGrid();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputViewModel InputVm = new InputViewModel();

            if(clickOrNot == false)
            {
                MessageBox.Show("Selezionare un parcheggio");
            }
            else
            {
                //null se occupato
                
                Entra(oldKey);
                InputVm.Show();
                //  InputVm.targa;

            }
            

          //  GeneraGrid();
        }

        private void Btn_esci_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < Bottoni.Count; i++)
            {
                if (Bottoni.Values.Contains(cliccato))
                {
                    MessageBox.Show(cliccato.Name);
                    //Esci(cliccato.Name);
                    MessageBox.Show(Esci(cliccato.Name,cliccato));
                    break;
                }
            }
            
        }



      

    }
}
