using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.Classi
{
    class Persona
    {
        private string IdPersona { get; set; }
        private string _nome;
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;
            }
        }

        private string _cognome;
        public string Cognome
        {
            get
            {
                return _cognome;
            }
            set
            {
                _cognome = value;
            }
        }

        private int _eta;
        public int Eta
        {
            get
            {
                return _eta;
            }
            set
            {
                _eta = value;
            }
        }

        private DateTime _dataNascita;
        public DateTime DataNascita
        {
            get
            {
                return _dataNascita;
            }
            set
            {
                _dataNascita = value;
            }
        }

        public Persona(string IdPersona)
        {
            this.IdPersona = IdPersona;
        }
        public void Anagrafica(string Nome, string Cognome, int Eta, DateTime DataNascita)
        {
            this.Nome = Nome;
            this.Cognome = Cognome;
            this.Eta = Eta;
            this.DataNascita = DataNascita;
        }

    }
}
