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

            DynamicGrid.ShowGridLines = true;
        }

        bool checkButtonState = false; //stato di buttone

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

            if (Row.Text == "" || Col.Text == "") //se utente non inserisce uno dei due dati(row o col) stampa il message di errore
            {
               MessageBox.Show("Non hai fornito uno dei dati richiesti(Row o Col)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                  
               row = Convert.ToInt32(Row.Text); //Converto Row.Text (String) in un intero
               col = Convert.ToInt32(Col.Text); //Converto Col.Text (String) in un intero

               GeneraRigheDinamiche(row); //con questo metodo genero le righe del grid
               GeneraColDinamiche(col);   //con questo metodo genero le colonne del grid

               GeneraButton(); //con questo metodo genero i button

               checkButtonState = true;

            }
          
        }

        private void GeneraButton()
        {
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

                    Border panel = new Border(); //Il border fungerà da panel dato che avrà al suo interno un button
                    Grid.SetColumn(panel, jCol); //setto le colonne
                    Grid.SetRow(panel, iRow); //setto le righe

                    //genero il button
                    Button B = new Button();
                    B.Content = "P" + iRow.ToString() + jCol.ToString(); //assegno come il content il numero della cella che corrisponde alla righa e colonna in cui si trova
                    B.Width = 50;
                    B.Height = 30;
                    B.HorizontalAlignment = HorizontalAlignment.Center;
                    B.VerticalAlignment = VerticalAlignment.Center;
                    panel.Child = B;
                    panel.Margin = new Thickness(1);

                    DynamicGrid.Children.Add(panel);
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

  
    }
}
