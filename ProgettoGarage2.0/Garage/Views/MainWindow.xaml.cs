using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using Garage.Classi;
using Garage.Models;

namespace Garage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
   
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DynamicGrid.ShowGridLines = false;
            Targa_label.Content = "";
        }

        bool checkButtonState = false; //stato di buttone

        bool Enterclicked = false;
        string IdButton, IdOut;

        Dictionary<string, Parcheggio> Parcheggi = new Dictionary<string, Parcheggio>();

        Dictionary<string, Button> Buttoni = new Dictionary<string, Button>(); //in dizionario salviami le auto parcheggiate


        private void ConfermaClick(object sender, RoutedEventArgs e) //evento onclick del button conferma
        {
            /*
             * 
             Descrizione Bug: Se l'utente clicca per la seconda volta il button "conferma" con i nuovi parametri
                              l'app non cancella i vecchi button ne genera nuovi e gli aggiunge agli altri.

             Soluzione: Creo un bool checkButtonState, che di default è false, e diventa true quando il button viene cliccato. 
                        Quando l'utente clicca il button controllo il suo stato, se è vero invoco la funzione clear() per
                        cancellare tutto.

             */
            

            if (checkButtonState == false) //controllo lo stato 
            {
                GeneraGrid();
                
            }
            else
            {
                DynamicGrid.RowDefinitions.Clear();    //cancello le righe
                DynamicGrid.ColumnDefinitions.Clear(); //cancello le colonne
                DynamicGrid.Children.Clear(); //cancello i componenti UI del grid

                GeneraGrid(); //genero la nuova grid
            }

        }

        private void GeneraGrid() //questa funzione mi genera il grid
        {
            int row, col;
            int.TryParse(Row.Text, out row); //Converto Row.Text (String) in un intero
            int.TryParse(Col.Text, out col); //Converto Col.Text (String) in un intero

            /*
             * Il "TryParse" fa in modo che il programma non generi eccezioni
             * in caso l'utente inserisca qualsiasi valore nelle textbox
             * e ritorna 0 se è stato inserito un valore diverso da un intero
             */

            if (row <= 0 || col <= 0) //se utente non inserisce uno dei due dati(row o col) stampa il message di errore
            {
               MessageBox.Show("Non hai fornito uno dei dati richiesti(Row o Col)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                GeneraRigheDinamiche(row); //con questo metodo genero le righe del grid
                GeneraColDinamiche(col);   //con questo metodo genero le colonne del grid

                GeneraButton(); //con questo metodo genero i button
                CreateParking(row,col);
               // printDictionary();

                checkButtonState = true;
                Veicoli_metodi.IsEnabled = true;
            }
          
        }

        private void GeneraButton()
        {

            Buttoni.Clear();
            int iRow = -1;
            foreach (RowDefinition righe in DynamicGrid.RowDefinitions)
            {
                iRow++;
                int jCol = -1;

                foreach (ColumnDefinition colonne in DynamicGrid.ColumnDefinitions)
                {

                    jCol++;

                    //I pannelli sono uno dei tipi di controllo più importanti di WPF. Fungono da contenitori per altri controlli
                    //e controllano il layout delle finestre/pagine.

                    Border panel = new Border();
                    {
                        BorderThickness = new Thickness(1);
                        //BorderBrush = new SolidColorBrush(Colors.Red);
                        
                    }

                    Grid.SetColumn(panel, jCol); //setto le colonne
                    Grid.SetRow(panel, iRow); //setto le righe


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

                    // ListButton.Add(B);

                    Buttoni.Add("P" + iRow.ToString() + jCol.ToString(), B);

                    B.Click += ParcheggioClick; //assegno al buttone l'evento parcheggioClick

                    panel.Child = B;

                    DynamicGrid.Children.Add(panel);
                }

            }
        }

        private void CreateParking(int row , int col)
        {

            Parcheggi.Clear();
            
           // Array.Clear(Buttons, 0, Buttons.Length);
            for (int i = 0; i < row ; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Parcheggi.Add("P"+i.ToString()+j.ToString(), new Parcheggio(i.ToString(), j.ToString()));
                }
            }
        }

        private void printDictionary()
        {
            foreach (KeyValuePair<string, Parcheggio> entry in Parcheggi)
            {
                //  Console.WriteLine(entry.Value);

                Console.WriteLine("{0}:\t{1}", entry.Key, entry.Value);
            }
        }

        private void GeneraRigheDinamiche(int row) //Genera le righe in modo dinamiche
        {
            for (int i = 0; i < row; i++) //Con questo ciclo genero le righe
            {
                DynamicGrid.RowDefinitions.Add(new RowDefinition());

            }
        }

        private void GeneraColDinamiche(int col) //Genera le colonne in modo dinamiche
        {
            for (int i = 0; i < col; i++) //Con questo ciclo genere le colonne
            {
                DynamicGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private bool InAvailability()
        {
            bool available = false;

            foreach (KeyValuePair<string, Parcheggio> entry in Parcheggi)
            {
                //  Console.WriteLine(entry.Value);

                if (entry.Value.StatoParcheggio == false)
                {
                    available = true; //se ci sono i parcheggi disponibile cambio lo stato del buttone
                }

            }

            return available;

        }

        

        private void ParcheggioClick(object sender, RoutedEventArgs e) //questa funzione sarà usata per l' evento onclick
        {
           
            IdButton = ((Button)sender).Name; //prelevo il nome del buttone che corrisponde alla chiave/id del dizionario "parcheggi"

            IdOut = ((Button)sender).Name;
           
           
            Enterclicked = true;

            ((Button)sender).Style = FindResource("VeicoloClick2") as Style;


        }

  


        private bool checkNumberPlate()
        {
            string Targa = Parcheggi[IdButton].TargaMacchina;

            bool check = false;
            foreach (KeyValuePair<string, Parcheggio> entry in Parcheggi)
            {
                //  Console.WriteLine(entry.Value);

                if (entry.Value.TargaMacchina == Targa)
                {
                    check= true; //se ci sono i parcheggi disponibile cambio lo stato del buttone
                    break;
                }

            }

            return check;


        }

        private void Button_EntraClick(object sender, RoutedEventArgs e)
        {

            //   string Targa = Parcheggi[IdButton].TargaMacchina;

            if (TargaText.Text == "")
            {
                MessageBox.Show("Inserire la targa", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (checkNumberPlate()==true)
            {
                MessageBox.Show("Targa già inserita, inserire una nuova targa" , "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if(Enterclicked == false)
            {
                MessageBox.Show("Non hai selezionato un parcheggio", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (InAvailability() == true && Parcheggi[IdButton].StatoParcheggio == false && Enterclicked == true)
            {
                //MessageBox.Show("Il parcheggio è libero", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

                Parcheggi[IdButton].StatoParcheggio = true;
                Parcheggi[IdButton].TargaMacchina = TargaText.Text;
                string pos = parkIN();
                Buttoni[IdButton].Style = FindResource("VeicoloClick") as Style;
              //  oldNumerPlate = TargaText.Text; //server per controllare che l'utente non abbia inserito lo stessa della prima
                MessageBox.Show("Il tuo parcheggio: " + pos, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                Enterclicked = false;

            }
            else if (Parcheggi[IdButton].StatoParcheggio == true)
            {
                MessageBox.Show("Parcheggio occupato dal " + Parcheggi[IdButton].TargaMacchina, "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                Buttoni[IdButton].Style = FindResource("VeicoloClick") as Style;
            }
            else if (InAvailability() == false)
            {

                MessageBox.Show("Non ci sono parcheggi disponibili", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          


        }

        private string parkIN()
        {
            string Pos = Parcheggi[IdButton].Entra();
            return Pos;
        }

      
        
        private void Button_EsciClick(object sender, RoutedEventArgs e)
        {
            if (Enterclicked == true)
            {
                Parcheggi[IdOut].StatoParcheggio = false;
                Parcheggi[IdOut].TargaMacchina = "";
                Buttoni[IdOut].Style = FindResource("StileVeicolo") as Style;
                MessageBox.Show("La macchina è rimasta per " + Parcheggi[IdOut].Esci(),"Info",MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

       
    }
}
