using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ventas.Clases
{
    public class Clientes
    {
        private string _claveCliente;

        public string ClaveCliente
        {
            get { return _claveCliente; }
            set { _claveCliente = value; }
        }
        private string _cliente;

        public string Cliente
        {
            get { return _cliente; }
            set { _cliente = value; }
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
        

        private decimal? _estimadoActualCompra;

        public decimal? EstimadoActualCompra
        {
            get { return _estimadoActualCompra; }
            set { _estimadoActualCompra = value; }
        }

        

        
    }
}
