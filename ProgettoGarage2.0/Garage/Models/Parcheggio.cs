using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Garage.Classi
{
    class Parcheggio
    {
        public string TargaMacchina { get; set; }

        public Stopwatch TotTime { get; set; }

        public bool StatoParcheggio { get; set; }

        public string row { get; set; }

        public string col { get; set; }

        public Parcheggio(string row, string col)
        {

            StatoParcheggio = false;
            this.row = row;
            this.col = col;
            TotTime = new Stopwatch();
        }

        public string Entra()
        {

            string parking;

            if (StatoParcheggio == false)
            {
                parking = "P" + row + col;
                // StatoParcheggio = true;

                TotTime.Start();
                //Thread.Sleep(10000);

                return parking;

            }
            else
            {
                return null;
            }



        }

        public string Esci()
        {


            if (StatoParcheggio == true)
            {
                TotTime.Stop();

                TimeSpan ts = TotTime.Elapsed;


                return ts.ToString();
            }
            else
            {
                return null;
            }

            // StatoParcheggio = true;



        }


    }
}
