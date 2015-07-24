using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ventas.Clases
{
    public class Lineas
    {
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _claveCliente;

        public string ClaveCliente
        {
            get { return _claveCliente; }
            set { _claveCliente = value; }
        }

        private string _linea;

        public string Linea
        {
            get { return _linea; }
            set { _linea = value; }
        }
        private decimal? _promVentaAtrasada;

        public decimal? PromVentaAtrasada
        {
            get { return _promVentaAtrasada; }
            set { _promVentaAtrasada = value; }
        }
        private decimal? _utilidadAtrasada;

        public decimal? UtilidadAtrasada
        {
            get { return _utilidadAtrasada; }
            set { _utilidadAtrasada = value; }
        }
        private decimal? _ventaActual;

        public decimal? VentaActual
        {
            get { return _ventaActual; }
            set { _ventaActual = value; }
        }
        private decimal? _estimadoActual;

        private decimal? _utilidadEstimada;

        public decimal? UtilidadEstimada
        {
            get { return _utilidadEstimada; }
            set { _utilidadEstimada = value; }
        }

        public decimal? EstimadoActual
        {
            get { return _estimadoActual; }
            set { _estimadoActual = value; }
        }
        
    }
}
