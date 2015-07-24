using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presupuesto.Clases.Objetos
{
    public class Proveedor
    {
        string _cardCode;

        public string CardCode
        {
            get { return _cardCode; }
            set { _cardCode = value; }
        }
        string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        string _rRF;

        public string RRF
        {
            get { return _rRF; }
            set { _rRF = value; }
        }

        public Proveedor()
        {
            _cardCode = string.Empty;
            _nombre = string.Empty;
            _rRF = string.Empty;
        }
    }
}
