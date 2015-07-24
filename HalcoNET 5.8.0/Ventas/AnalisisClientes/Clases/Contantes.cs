using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ventas.AnalisisClientes.Clases
{
    public class Contantes
    {
        private static string _cliente;

        public static string Cliente
        {
            get { return Contantes._cliente; }
            set { Contantes._cliente = value; }
        }
        private static string _nombre;

        public static string Nombre
        {
            get { return Contantes._nombre; }
            set { Contantes._nombre = value; }
        }
        private static int _vendedor;

        public static int Vendedor
        {
            get { return Contantes._vendedor; }
            set { Contantes._vendedor = value; }
        }
        private static string _linea;

        public static string Linea
        {
            get { return Contantes._linea; }
            set { Contantes._linea = value; }
        }
    }
}
