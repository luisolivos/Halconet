using System;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Presupuesto.Clases
{
    public class FillControl
    {
        public FillControl()
        {
        }

        public void FillDataSource(Control control, string _encabezado, int _tipo)
        {
            ComboBox cb;

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ_Pres))
            {
                using (SqlCommand command = new SqlCommand("PRES_Catalogos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", _tipo);
                    command.Parameters.AddWithValue("@Usuario", string.Empty);
                    command.Parameters.AddWithValue("@Rol", 0);
                    command.Parameters.AddWithValue("@Condition", string.Empty);

                    DataTable table = new DataTable();

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = _encabezado;
                    row["Codigo"] = "0";
                    table.Rows.InsertAt(row, 0);

                    if (control is ComboBox)
                    {
                        cb = control as ComboBox;
                        cb.DataSource = table;
                        cb.DisplayMember = "Nombre";
                        cb.ValueMember = "Codigo";
                    }
               
                }
            }

        }
    }
}
