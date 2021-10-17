﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        Dictionary<string, Parcheggio> Parcheggi = new Dictionary<string, Parcheggio>(); //in questo dizionario salvo i parcheggi

        //IdButtone == IdParcheggio

        Dictionary<string, Button> Buttoni = new Dictionary<string, Button>(); //in questo dizionario salvo i buttoni

        public object Arrays { get; private set; }

        private void ConfermaClick(object sender, RoutedEventArgs e) //evento onclick del button conferma
        {
            /*
             * 
             Descrizione Bug: Se l'utente clicca per la seconda volta il button "conferma" con i nuovi i parametri
                              l'app non cancella i vecchi button ne genera nuovi e li aggiunge agli altri.

             Soluzione: Creo un bool checkButtonState che di default è false ed diventa true quando il button viene clickato 
                        quando l'utente clicka il button controllo il suo stato se è vero invoko le invoko la funzione clear() per
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
                CreateParking(row,col); //carico il dizionario
               // printDictionary();

                checkButtonState = true;
                Veicoli_metodi.IsEnabled = true;
            }
          
        }

        private void GeneraButton()
        {

            Buttoni.Clear(); //pulisco ad ogni generazione il vecchio contenuto del dizionario dei buttoni
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

                    Buttoni.Add("P" + iRow.ToString() + jCol.ToString(), B); //carico il dizionario

                    B.Click += ParcheggioClick; //assegno al buttone l'evento parcheggioClick

                    panel.Child = B;

                    DynamicGrid.Children.Add(panel);
                }

            }
        }

        private void CreateParking(int row , int col) //funzione crea parcheggio serve per riempiere il dizionario parcheggio
        {

            Parcheggi.Clear();
            
           // Array.Clear(Buttons, 0, Buttons.Length);
            for (int i = 0; i < row ; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Parcheggi.Add("P"+i.ToString()+j.ToString(), new Parcheggio(i.ToString(), j.ToString())); //carico il dizionario
                }
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


        private void ParcheggioClick(object sender, RoutedEventArgs e) //questa funzione sarà usato per il evento onclick
        {
           
            string info = ((Button)sender).Name; //prelevo il nome del buttone che corrisponde alla chiave/id del dizionario "parcheggi"
            string Targa = Parcheggi[info].TargaMacchina; //prelevo la targa della macchina

            if (Parcheggi[info].StatoParcheggio == false)
            {
                MessageBox.Show("Il parcheggio è libero", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Attenzione Parcheggio occupato dalla macchina: " + Targa, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
           

            //stampo il messaggio con le informazioni
          

        }


        private bool Availability()  //la funzione per controllare la disponibilità del parcheggio
        {
            bool available = false;

            foreach ( KeyValuePair<string, Parcheggio> entry in Parcheggi)
            {
              //  Console.WriteLine(entry.Value);

                if(entry.Value.StatoParcheggio == false) 
                {
                    available = true; //se ci sono i parcheggi disponibile cambio lo stato del buttone
                }

            }

            return available;

        }

        private bool checkNumberPlate() //controlla se la targa non sia stata inserita per la seconda volta
        {

            bool yesOrnot = false;
            foreach (KeyValuePair<string, Parcheggio> entry in Parcheggi)
            {

                if (entry.Value.TargaMacchina == TargaText.Text)
                {

                    yesOrnot = true;
                    break; //appena trova la targa esce dal ciclo
                }

            }

            return yesOrnot;


        }

        private string Park() //la funzione per parcheggiare la macchina
        {
            string RowCol = "";
           // int counter = 0;
            foreach (KeyValuePair<string, Parcheggio> entry in Parcheggi)
            {

                if (entry.Value.StatoParcheggio == false)
                {   

                    RowCol = entry.Value.Entra(); //chiamo il metodo entra() della classe parcheggio che ritorna la riga e colonna
                    entry.Value.StatoParcheggio = true; //cambio lo stato del parcheggio
                    entry.Value.TargaMacchina = TargaText.Text; //prendo la targa del veicolo
                    break; //al primo parcheggio disponibile esco dal ciclo
                }
              
            }
            return RowCol;
        }

  
        private void Button_EntraClick(object sender, RoutedEventArgs e) //Evento del buttone entra
        {
            if (Availability() == true) //prima controllo se c'è il parcheggio disponibile altrimenti genero il messaggio di errore
            {
                if (TargaText.Text == "") //controllo che il textbox sia riempito se no genero il messaggio di errore
                {
                    MessageBox.Show("Per favore inserire la targa del veicolo","Attenzione!",MessageBoxButton.OK,MessageBoxImage.Error);
                    
                }
                else
                {
                    string Parking = Park(); //prendo la riga e colonna 
                    Buttoni[Parking].Style = FindResource("VeicoloClick") as Style; //cambio il colore del parcheggio
                    MessageBox.Show("Il tuo parcheggio: " + Parking); //stampo il messaggio 
                }

            }
            else
            {
                 MessageBox.Show("Non ci sono parcheggi disponibili", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

       
    }
}
