using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Compras
{
    public partial class frmHistorialABC : Form
    {
        public frmHistorialABC()
        {
            InitializeComponent();
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 23);

                    command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
                    
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand= command;
                    DataTable bl = new DataTable();
                    da.Fill(bl);

                    IList  ds =(bl as IListSource).GetList();
                    chart1.DataBindTable(ds);

                }
            }
        }
    }
}
