using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage
{
    class Persona
    {
        private string _cognome;
        private string _nome;

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
    }
}
