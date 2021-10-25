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

        const decimal tariffa = 1.8M;

       
       public HomeViewModel()
       {
            InitializeComponent();

            connection.Open();
             //MessageBox.Show(id);
            //string commandoInserimento = "DELETE FROM Parcheggio";
           // SqlCommand command = new SqlCommand(commandoInserimento, connection);
            //command.ExecuteNonQuery();
            connection.Close();
            //  GeneraGrid();

        }

        public static Dictionary<string, Button> Bottoni = new Dictionary<string, Button>(); //in dizionario salviami le auto parcheggiate
        Dictionary<string, Parcheggio> Parcheggi = new Dictionary<string, Parcheggio>();

        //string constring = "Server = DESKTOP-R6PQGP4\\SQLEXPRESSNEW; Database=Test; User Id=sa; Password = Danishveer&17012001";
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-M63NC6P\SQLEXPRESSNEW;Initial Catalog=database;User ID=sa;Password=Fillo-fous05");

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


        public static string LisPlate = "";
        private void openConnect(string id)
        {
            connection.Open();
          //  MessageBox.Show(id);
            //string commandoInserimento = "INSERT INTO Parcheggio VALUES ('" + id + "', 0, NULL, NULL,NULL)";
            //SqlCommand command = new SqlCommand(commandoInserimento, connection);
            //command.ExecuteNonQuery();

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
               // MessageBox.Show(id);
                string commandoUpdate = "UPDATE Parcheggio set Stato = 1 WHERE IdParcheggio = '"+id+"'";
                SqlCommand UPcommand = new SqlCommand(commandoUpdate, connection);
                UPcommand.ExecuteNonQuery();


              /* string storico = "INSERT INTO Storico (IdParcheggio, DataOrarioIngresso, Targa) VALUES ('" + id + "', '" + Convert.ToString(DateTime.Now) + "', '"+Bottoni[Selected].Content+"')";
                SqlCommand storicoComando = new SqlCommand(storico, connection);
                storicoComando.ExecuteNonQuery();*/


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
           // MessageBox.Show(id);
            string commandoUpdate = "UPDATE Parcheggio set Stato = 0 WHERE IdParcheggio = '" + id + "'";
            SqlCommand UPcommand = new SqlCommand(commandoUpdate, connection);

            string commandoSelect = "SELECT DataOrarioIngresso FROM Storico";
            SqlCommand selectIngresso = new SqlCommand(commandoSelect, connection);

            commandoSelect = "SELECT DataOraUscita FROM Storico";
            SqlCommand selectUscita = new SqlCommand(commandoSelect, connection);

            //TimeSpan invterval = 

            b.Content = id;
            b.Style = FindResource("StileVeicolo") as Style;
            UPcommand.ExecuteNonQuery();
            DateTime date1 = Convert.ToDateTime(selectIngresso.ExecuteScalar());
            DateTime date2 = Convert.ToDateTime(selectUscita.ExecuteScalar());


            /*double interval = Math.Abs(date1.Subtract(date2).TotalSeconds);
            decimal incasso = (decimal)interval * tariffa;

            commandoUpdate = "UPDATE Parcheggio set Incasso = " + incasso + " WHERE IdParcheggio = '" + id + "'";
            SqlCommand updateCommand = new SqlCommand(commandoUpdate, connection);

            updateCommand.ExecuteNonQuery();
            */
            //MessageBox.Show("intervallo= " + date1.ToString() + "   "+ date2.ToString() + " tempo: " + interval);

            //MessageBox.Show((selectIngresso));
            ///Console.WriteLine("   {0,-35} {1,20}", "Total Number of Minutes:", interval.TotalMinutes);
            connection.Close();
            return "Tempo";
        }

        private void HomeView_Loaded(object sender, RoutedEventArgs e)
        {
            connection.Open();
            string commandoSelect = "SELECT COUNT(*) FROM Piano";
            SqlCommand command = new SqlCommand(commandoSelect, connection);

            int numero = Convert.ToInt32(command.ExecuteScalar());
            //MessageBox.Show(Convert.ToString(command.ExecuteScalar()));
            connection.Close();

            if (numero != 0)
            {
                connection.Open();
                string prendiRC = "SELECT Nrighe FROM Piano";
                SqlCommand comando = new SqlCommand(prendiRC, connection);

                Nrow = Convert.ToInt32(comando.ExecuteScalar());


                prendiRC = "SELECT Ncol FROM Piano";
                comando = new SqlCommand(prendiRC, connection);

                Ncol = Convert.ToInt32(comando.ExecuteScalar());

                string veicoliEntrati = "select Parcheggio.IdParcheggio, Storico.Targa  from Parcheggio   join Storico on Storico.IdParcheggio = Parcheggio.IdParcheggio      WHERE Stato = 1";
              

                //GeneraButton();
                

                



                
                Bottoni.Clear();
                Parcheggi.Clear();
                btn_genera.IsEnabled = false;
                SelezionaCol.IsEnabled = false;
                SelezionaRow.IsEnabled = false;
                btn_entra.Visibility = Visibility.Visible;
                btn_esci.Visibility = Visibility.Visible;
                connection.Close();
                GeneraGrid();

                Dictionary<string, string> valori = new Dictionary<string, string>();
                connection.Open();
                comando = new SqlCommand(veicoliEntrati, connection);

                SqlDataReader entrati = comando.ExecuteReader();
                if (entrati.HasRows)
                {
                    while (entrati.Read())
                    {
                        string s = entrati["IdParcheggio"] as string;
                        string targa = entrati["Targa"] as string;
                        valori.Add(s, targa);
                        //MessageBox.Show("id: " + Bottoni["P01"].Name);
                        Bottoni[s].Content = targa;

                    }
                }
                connection.Close();
            }
        }

        private void Btn_genera_Click(object sender, RoutedEventArgs e)
        {
            connection.Open();

            



            Nrow = Convert.ToInt32(SelezionaRow.Value);
            Ncol = Convert.ToInt32(SelezionaCol.Value);
            Bottoni.Clear();
            Parcheggi.Clear();
            btn_entra.IsEnabled = true;
            btn_esci.IsEnabled = true;
            btn_genera.IsEnabled = false;
            btn_entra.Visibility = Visibility.Visible;
            btn_esci.Visibility = Visibility.Visible;
            SelezionaCol.IsEnabled = false;
            SelezionaRow.IsEnabled = false;


            string commandoInsert = "INSERT INTO Piano (Piano, Ncol, Nrighe, Nome) Values (0, " + Ncol + ", " + Nrow + ", 'Primo') ";
            SqlCommand cmd = new SqlCommand(commandoInsert, connection);

            cmd.ExecuteNonQuery();

            connection.Close();



            //int righe = Convert.ToInt32(command.ExecuteScalar());

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

                connection.Open();

                string commandoUpdate = "UPDATE Storico SET DataOrarioIngresso = '"+DateTime.Now+"' WHERE IdParcheggio = '" + cliccato.Name + "'";
                SqlCommand UPcommand = new SqlCommand(commandoUpdate, connection);
                UPcommand.ExecuteNonQuery();


                connection.Close();
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
                    // MessageBox.Show(cliccato.Name);
                    //Esci(cliccato.Name);

                    connection.Open();
                    string commandoUpdate = "UPDATE Storico SET DataOraUscita = '" + DateTime.Now + "' WHERE IdParcheggio = '" + cliccato.Name + "'";
                    SqlCommand UPcommand = new SqlCommand(commandoUpdate, connection);
                    UPcommand.ExecuteNonQuery();
                    connection.Close();



                    //   string commandoUpdate = "UPDATE Storico set DataOraUscita WHERE IdParcheggio = '" + cliccato.Name + "'";
                    //   SqlCommand UPcommand = new SqlCommand(commandoUpdate, connection);
                    //  UPcommand.ExecuteNonQuery();

                    MessageBox.Show(Esci(cliccato.Name,cliccato));
                    break;
                }
            }
            
        }



      

    }
}
