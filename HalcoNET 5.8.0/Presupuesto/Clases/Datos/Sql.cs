using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;

namespace Presupuesto.Clases.Datos
{
    public class Sql
    {
        public static DataTable Fill(SqlCommand command)
        {
            using (command)
            {
                DataTable tbl = new DataTable();
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = command;
                    da.SelectCommand.CommandTimeout = 0;

                    da.Fill(tbl);

                    return tbl;
                }
            }
        }

    }
}
