using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageDatabase.Classi
{
    class Piano
    {
        //Campi  chiavi Id
        private int _idpiano;
        public int _IdPiano
        {
            get
            {
                return _idpiano;
            }
            set
            {
                _idpiano = value;
            }
        }

        private byte _nPiano;
        public byte NPiano
        {
            get
            {
                return _nPiano;
            }
            set
            {
                _nPiano = value;
            }
        }

        //Campi caratteristiche della classe Piano
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

        //Costruttore Piano
        public Piano(byte NPiano, string Nome)
        {
            this.NPiano = NPiano;
            this.Nome = Nome;
        }





    }
}
