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

        List<string> targhe = new List<string>();



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

                    B.MouseMove += MoveMouse;

                    B.MouseLeave += LeaveMouse;

                    panel.Child = B;

                    DynamicGrid.Children.Add(panel);
                }

            }
        }

        private void LeaveMouse(object sender, MouseEventArgs e)
        {
        
            ((Button)sender).Content = ((Button)sender).Name;
            
        }

        private void MoveMouse(object sender, MouseEventArgs e)
        {
            if(Parcheggi[((Button)sender).Name].StatoParcheggio == false)
            {
                ((Button)sender).Content = "Vuoto";
            }
            else
            {
                ((Button)sender).Content = Parcheggi[((Button)sender).Name].TargaMacchina;
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


        string oldKeyButton = "";
        int counter = 0;
        private void ParcheggioClick(object sender, RoutedEventArgs e) //questa funzione sarà usata per l' evento onclick
        {
            
            IdButton = ((Button)sender).Name; //prelevo il nome del buttone che corrisponde alla chiave/id del dizionario "parcheggi"

            IdOut = ((Button)sender).Name;
            counter++;

            if (counter == 1)
            {
                oldKeyButton = ((Button)sender).Name;
            }

            if(oldKeyButton != ((Button)sender).Name)
            {

                  Buttoni[oldKeyButton].Style = FindResource("StileVeicolo") as Style;
              
            }

            Enterclicked = true;

            ((Button)sender).Style = FindResource("VeicoloClick2") as Style;

            //  
            oldKeyButton = ((Button)sender).Name;
            if (Parcheggi[IdButton].StatoParcheggio == false)
            {
                Buttoni[IdButton].Content = "Vuoto";
            }
            else
            {
                Buttoni[IdButton].Content = Parcheggi[IdButton].TargaMacchina;
            }
        }



        private bool checkNumberPlate()
        {
            string Targa = TargaText.Text;

            bool check = false;

            foreach (KeyValuePair<string, Parcheggio> entry in Parcheggi)
            {
                //  Console.WriteLine(entry.Value);

                if (entry.Value.TargaMacchina == Targa)
                {
                    check = true; 
                    break;
                }

            }

            return check;


        }

        private void Button_EntraClick(object sender, RoutedEventArgs e)
        {

            string targa = TargaText.Text;
            string enterRis  = Parcheggi[IdButton].Entra();

            if (targa == "" || targa.Length != 7 || checkNumberPlate() == true)
            {
                MessageBox.Show("La targa già stata inserita oppure non è valida (Contralle se sette caratteri)", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (Enterclicked == false)
            {
                MessageBox.Show("Non hai selezionato un parcheggio", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (InAvailability() == true && enterRis != null && Enterclicked == true)
            {
                //MessageBox.Show("Il parcheggio è libero", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                Parcheggi[IdButton].TargaMacchina = TargaText.Text;
                Parcheggi[IdButton].StatoParcheggio = true;
                Buttoni[IdButton].Style = FindResource("VeicoloClick") as Style;

                MessageBox.Show("Il tuo parcheggio: " + enterRis, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                targhe.Add(targa);
                

                // TargaText.Text = "";
                Enterclicked = false;

            }
            else if (enterRis == null)
            {
                MessageBox.Show("Parcheggio occupato dal " + Parcheggi[IdButton].TargaMacchina, "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
                Buttoni[IdButton].Style = FindResource("VeicoloClick") as Style;
            }
            else if (InAvailability() == false)
            {

                MessageBox.Show("Non ci sono parcheggi disponibili", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            listaAuto.ItemsSource = targhe;
            listaAuto.Items.Refresh();
        }

        /*private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                item.Style = FindResource("VeicoloClick2") as Style;

            }
        }*/




        private void Button_EsciClick(object sender, RoutedEventArgs e)
        {
            string time = Parcheggi[IdOut].Esci();


            if (Enterclicked == true && time == null)
            {
                MessageBox.Show("Selezionare un parcheggio oppure il parcheggio è occupato", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                Parcheggi[IdOut].StatoParcheggio = false;
                Parcheggi[IdOut].TargaMacchina = "";
                Buttoni[IdOut].Style = FindResource("StileVeicolo") as Style;

                targhe.Remove(Parcheggi[IdOut].TargaMacchina);
                

                MessageBox.Show("La macchina è rimasta per " + time,"Info",MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                MessageBoxResult res = MessageBox.Show("Vuoi cancellare la macchina della posizione " + Buttoni[IdOut].Name, "Conferma", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (res == MessageBoxResult.Yes)
                {
                    Parcheggi[IdOut].StatoParcheggio = false;
                    Parcheggi[IdOut].TargaMacchina = "";
                    Buttoni[IdOut].Style = FindResource("StileVeicolo") as Style;
                    MessageBox.Show("La macchina è rimasta per " + time, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Non è stato effettuato nessun cambiamento", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            listaAuto.ItemsSource = targhe;
            listaAuto.Items.Refresh();
        }

    }
}
