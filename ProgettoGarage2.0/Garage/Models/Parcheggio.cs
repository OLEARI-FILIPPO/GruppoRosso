using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Classi
{
    class Parcheggio
    {
        public string TargaMacchina { get; set; }
       
        public bool StatoParcheggio { get; set; }

        public string row { get; set; }

        public string col { get; set; }

        public Parcheggio(string row, string col)
        {
            
            StatoParcheggio = false;
            this.row = row;
            this.col = col;
        }

        public string Entra()
        {

            string parking;
            parking = "P" + row + col;
            // StatoParcheggio = true;

            return parking;

           
            
        }

       
    }
}
