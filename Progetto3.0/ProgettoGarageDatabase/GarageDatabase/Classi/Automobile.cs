using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageDatabase.Classi
{
    class Automobile
    {
        //Campi caratteristiche della classe Auomobile
        private string _marca;
        public string Marca
        {
            get
            {
                return _marca;
            }
            set
            {
                _marca = value;
            }
        }

        private string _modello;
        public string Modello
        {
            get
            {
                return _modello;
            }
            set
            {
                _modello = value;
            }
        }

        private string _targa;
        public string Targa
        {
            get
            {
                return _targa;
            }
            set
            {
                _targa = value;
            }
        }

        //Campi  chiavi Id
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

        private int _idPersona;
        public int IdPersona
        {
            get
            {
                return _idPersona;
            }
            set
            {
                _idPersona = value;
            }
        }


        //Costruttore Automobile
        public Automobile(string Marca, string Modello, string Targa)
        {
            this.Modello = Modello;
            this.Marca = Marca;
            this.Targa = Targa;
        }






    }
}
