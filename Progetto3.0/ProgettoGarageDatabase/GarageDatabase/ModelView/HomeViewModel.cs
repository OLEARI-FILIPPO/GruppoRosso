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

namespace GarageDatabase.ModelView
{
    public partial class HomeViewModel : UserControl
    {

       public HomeViewModel()
       {
            InitializeComponent();

            GeneraGrid();
       }

        private void GeneraGrid()
        {

            int Nrow = 7, Ncol = 7;//numero di colonne e numero di righe

            GeneraRigheDinamiche(Nrow);
            GeneraColDinamiche(Ncol);

            GeneraButton();


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

                    panel.Child = B;
                    DynamicGrid.Children.Add(panel);

                }


            }
        }


    }
}

