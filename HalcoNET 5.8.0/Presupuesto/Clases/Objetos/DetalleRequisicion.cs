using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace Presupuesto.Clases.Objetos
{
    public class DetalleRequisicion
    {
        decimal _iD;

        public decimal ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        private int _numProv;

        public int NumProv
        {
            get { return _numProv; }
            set { _numProv = value; }
        }

        string _proveedor;

        public string Proveedor
        {
            get { return _proveedor; }
            set { _proveedor = value; }
        }
        int _linea;

        public int Linea
        {
            get { return _linea; }
            set { _linea = value; }
        }
        string _articulo;

        public string Articulo
        {
            get { return _articulo; }
            set { _articulo = value; }
        }
        string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        decimal _cantidad;

        public decimal Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        decimal _precio;

        public decimal Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }
        decimal _lineTotal;

        public decimal LineTotal
        {
            get { return _lineTotal; }
            set { _lineTotal = value; }
        }
        bool _sugerencia;

        public bool Sugerencia
        {
            get { return _sugerencia; }
            set { _sugerencia = value; }
        }

        private string _comentarios;

        public string Comentarios
        {
            get { return _comentarios; }
            set { _comentarios = value; }
        }

        public DetalleRequisicion()
        {
            _iD = decimal.Zero;
            _proveedor = string.Empty;
            _linea = 0;
            _articulo = string.Empty;
            _descripcion = string.Empty;
            _cantidad = decimal.Zero;
            _precio = decimal.Zero;
            _lineTotal = decimal.Zero;
            _sugerencia = false;
        }

        public void ExecuteNonQuery(DetalleRequisicion dreq)
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ_Pres))
            {
                using (SqlCommand command = new SqlCommand("sp_requisicion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@ID", dreq.ID);
                    command.Parameters.AddWithValue("@NumProv", dreq.NumProv);
                    command.Parameters.AddWithValue("@Proveedor", dreq.Proveedor);
                    command.Parameters.AddWithValue("@Linea", dreq.Linea);
                    command.Parameters.AddWithValue("@Articulo", dreq.Articulo);
                    command.Parameters.AddWithValue("@Descripcion", dreq.Descripcion);
                    command.Parameters.AddWithValue("@Cantidad", dreq.Cantidad);
                    command.Parameters.AddWithValue("@Precio", dreq.Precio);
                    command.Parameters.AddWithValue("@LineTotal", dreq.LineTotal);
                    command.Parameters.AddWithValue("@Sugerencia", dreq.Sugerencia);
                    command.Parameters.AddWithValue("@Comentarios", dreq.Comentarios);
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
