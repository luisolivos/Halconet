using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Presupuesto.Clases
{
    public class Datos_Presupuesto
    {
        public DataTable GetData(Comun_Presupuesto obj)
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ_Pres))
            {
                using (SqlCommand command = new SqlCommand("PRES_Mantenimiento", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", obj.TipoConsulta);
                    command.Parameters.AddWithValue("@Cuenta", obj.Cuenta);
                    command.Parameters.AddWithValue("@NR", obj.NR);
                    command.Parameters.AddWithValue("@Proyecto", obj.Proyecto);
                    command.Parameters.AddWithValue("@Mes", obj.Mes);
                    command.Parameters.AddWithValue("@Año", obj.Año);
                    command.Parameters.AddWithValue("@CvePres", obj.CvePres);
                    command.Parameters.AddWithValue("@Original", obj.Original);
                    command.Parameters.AddWithValue("@Presupuesto", obj.Presupuesto);
                    command.Parameters.AddWithValue("@Creacion", obj.Creacion);
                    command.Parameters.AddWithValue("@Modificacion", obj.Modificacion);
                    command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Usuario == null ? "Pruebas" : ClasesSGUV.Login.Usuario);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);
                }
                
            }

            return table;
        }

        public int ExecuteNonQuery(Comun_Presupuesto obj)
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ_Pres))
            {
                using (SqlCommand command = new SqlCommand("PRES_Mantenimiento", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", obj.TipoConsulta);
                    command.Parameters.AddWithValue("@Cuenta", obj.Cuenta);
                    command.Parameters.AddWithValue("@NR", obj.NR);
                    command.Parameters.AddWithValue("@Proyecto", obj.Proyecto);
                    command.Parameters.AddWithValue("@Mes", obj.Mes);
                    command.Parameters.AddWithValue("@Año", obj.Año);
                    command.Parameters.AddWithValue("@CvePres", obj.CvePres);
                    command.Parameters.AddWithValue("@Original", obj.Original);
                    command.Parameters.AddWithValue("@Presupuesto", obj.Presupuesto);
                    command.Parameters.AddWithValue("@Creacion", obj.Creacion);
                    command.Parameters.AddWithValue("@Modificacion", obj.Modificacion);
                    command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Usuario == null ? "Pruebas" : ClasesSGUV.Login.Usuario);

                    connection.Open();

                    return command.ExecuteNonQuery();
                }

            }
        }
    }
}
