using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageDatabase.Classi
{
    class Parcheggio
    {

        const decimal tariffa = 2.5M;
        public string IdParcheggio { get; set; }

        public int IdPiano { get; set; }

        public int StatoParcheggio { get; set; }

        public decimal Incasso { get; set; }

        public byte RowPark { get; set; }

        public byte ColPark { get; set; }

        public Parcheggio(string idParcheggio, int idPiano, int statoparcheggio)
        {
            IdParcheggio = idParcheggio;
            IdPiano = idPiano;
            StatoParcheggio = statoparcheggio;
        }

        public string Entra()
        {
            string parking;

            if (StatoParcheggio == 0)
            {
                parking = "P" + RowPark + ColPark;

                /*
                 * 
                 * add sql query here for table storico(DataOrarioIngresso)
                 * 
                 * */

                return parking;
            }
            else
            {
                return null;
            }

        }

        public string Esci()
        {
            if (StatoParcheggio == 1)
            {
                string timeDate = "";
                /*
                 * add sql query here for table storico(DataOrarioUscita)
                 * 
                 * */

                return timeDate;
            }
            else
            {
                return null;
            }
        }

    }
}
