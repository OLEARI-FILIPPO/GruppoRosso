using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Models
{
    class Cronometro
    {
        public Stopwatch tempoTrascorso { get; set; }

        public Cronometro(Stopwatch tempoTrascorso)
        {
            this.tempoTrascorso = tempoTrascorso;
        }

        public TimeSpan Esci(int row, int col)
        {
            TimeSpan IntervalloTempo = this.tempoTrascorso.Elapsed;
            this.tempoTrascorso.Stop();
            return IntervalloTempo;
        }

    }
}
