using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageDatabase.Classi
{
    class Storico
    {
        //Campi  chiavi Id
        private int _idStorico;
        public int IdStorico
        {
            get
            {
                return _idStorico;
            }
            set
            {
                _idStorico = value;
            }
        }

        private int _idAuto;
        public int IdAuto
        {
            get
            {
                return _idAuto;
            }
            set
            {
                _idAuto = value;
            }
        }

        private int _idparcheggio = 0;
        public int IdParcheggio
        {
            get
            {
                return _idparcheggio;
            }
            set
            {
                _idAuto = value;
            }
        }


        //Campi caratteristiche della classe Storico
        private DateTime _dataOrarioIngresso;
        public DateTime DataOrarioIngresso
        {
            get
            {
                return _dataOrarioIngresso;
            }
            set
            {
                _dataOrarioIngresso = value;
            }
        }

        private DateTime _dataOraUscita;
        public DateTime DataOraUscita
        {
            get
            {
                return _dataOraUscita;
            }
            set
            {
                _dataOraUscita = value;
            }
        }

        //Costruttore Storico
        public Storico(DateTime DataOrarioIngresso, DateTime DataOraUscita)
        {
            this.DataOrarioIngresso = DataOrarioIngresso;
            this.DataOraUscita = DataOraUscita;
        }
    }
}
