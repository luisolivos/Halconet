using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;

namespace Presupuesto.Clases
{
    public class Requisicion
    {
        static string _cuenta;
        public static string Cuenta
        {
            get { return Requisicion._cuenta; }
            set { Requisicion._cuenta = value; }
        }

        static decimal _iD;

        public static decimal ID
        {
            get { return Requisicion._iD; }
            set { Requisicion._iD = value; }
        }
        static string _estatus;

        public static string Estatus
        {
            get { return Requisicion._estatus; }
            set { Requisicion._estatus = value; }
        }
        static DateTime _fecha;

        public static DateTime Fecha
        {
            get { return Requisicion._fecha; }
            set { Requisicion._fecha = value; }
        }
        static decimal _total;

        public static decimal Total
        {
            get { return Requisicion._total; }
            set { Requisicion._total = value; }
        }
        static decimal _iva;

        public static decimal Iva
        {
            get { return Requisicion._iva; }
            set { Requisicion._iva = value; }
        }
        static string _nR;

        public static string NR
        {
            get { return Requisicion._nR; }
            set { Requisicion._nR = value; }
        }
        static string _proyecto;

        public static string Proyecto
        {
            get { return Requisicion._proyecto; }
            set { Requisicion._proyecto = value; }
        }
        static string _tipo;

        public static string Tipo
        {
            get { return Requisicion._tipo; }
            set { Requisicion._tipo = value; }
        }
        static DateTime modificacion;

        public static DateTime Modificacion
        {
            get { return Requisicion.modificacion; }
            set { Requisicion.modificacion = value; }
        }

        public static void ExecuteReader()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ_Pres))
            {
                using (SqlCommand command = new SqlCommand("sp_requisicion", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@Estatus", Estatus);
                    command.Parameters.AddWithValue("@Fecha", Fecha);
                    command.Parameters.AddWithValue("@Total", Total);
                    command.Parameters.AddWithValue("@Iva", Iva);
                    command.Parameters.AddWithValue("@NR", NR);
                    command.Parameters.AddWithValue("@Cuenta", Cuenta);
                    command.Parameters.AddWithValue("@Proyecto", Proyecto);
                    command.Parameters.AddWithValue("@Tipo", Tipo);
                    command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);
                    command.Parameters.AddWithValue("@Modificacion", Modificacion);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        ID = reader.GetDecimal(0);
                    }
                }
            }
        }

    }
}
