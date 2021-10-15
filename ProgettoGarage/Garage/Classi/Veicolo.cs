using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Classi
{
    class Veicolo
    {
        private Persona P { get; set; }

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

        public Veicolo(string Targa)
        {
            this.Targa = Targa;
        }

        private string _colore;
        public string Colore
        {
            get
            {
                return _colore;
            }
            set
            {
                _colore = value;
            }
        }

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

        public void Descrizione(string Colore, string Marca, string Modello)
        {
            this.Colore = Colore;
            this.Marca = Marca;
            this.Modello = Modello;
        }

    }
}
