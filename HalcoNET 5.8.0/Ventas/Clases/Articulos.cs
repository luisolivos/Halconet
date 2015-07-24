using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ventas.Clases
{
    public class Articulos
    {
        private string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _codArticulo;

        public string CodArticulo
        {
            get { return _codArticulo; }
            set { _codArticulo = value; }
        }

        private string _nombreArticulo;

        public string NombreArticulo
        {
            get { return _nombreArticulo; }
            set { _nombreArticulo = value; }
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
        private decimal? _utilidadEstimada;

        public decimal? UtilidadEstimada
        {
            get { return _utilidadEstimada; }
            set { _utilidadEstimada = value; }
        }

        private decimal? _estimadoActual;

        public decimal? EstimadoActual
        {
            get { return _estimadoActual; }
            set { _estimadoActual = value; }
        }
        
    }
}
