using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace Compras.Clases
{
    public class Ventas
    {
        public DataTable GetVentas(SqlCommand _command)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV) )
            {
                using (_command)
                {
                    _command.Connection = connection;

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = _command;
                    da.SelectCommand.CommandTimeout = 0;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    da.Fill(tbl);
                }
            }
            return tbl;
        }
    }
}
