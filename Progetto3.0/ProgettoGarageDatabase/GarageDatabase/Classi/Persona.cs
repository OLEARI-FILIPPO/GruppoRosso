using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageDatabase.Classi
{
    class Persona
    {
        //Campi caratteristiche della classe Persona
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

        private DateTime _dob;
        public DateTime Dob
        {
            get
            {
                return _dob;
            }
            set
            {
                _dob = value;
            }
        }

        //Campi  chiavi Id
        private int _idpersona;
        public int _IdPersona
        {
            get
            {
                return _idpersona;
            }
            set
            {
                _idpersona = value;
            }
        }

        //Costruttore Persona
        public Persona(string Nome, string Cognome, DateTime Dob)
        {
            this.Nome = Nome;
            this.Cognome = Cognome;
            this.Dob = Dob;
        }
    }
}
