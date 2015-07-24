using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Cobranza.GestionCobranza
{
    public partial class frmLlamadas : Form
    {
        string __Cliente;
        public frmLlamadas(string _cliente)
        {
            InitializeComponent();
            __Cliente = _cliente;
        }
           
        
        private void frmLlamadas_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            dtInicial.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            button1_Click(sender, e);

            gridDatos.Columns[0].Width = 70;
            gridDatos.Columns[1].Width = 300;
            gridDatos.Columns[2].Width = 110;

            gridDatos.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 9);
                        command.Parameters.AddWithValue("@Fecha", dtInicial.Value);
                        command.Parameters.AddWithValue("@FechaFinal", dtFinal.Value);
                        command.Parameters.AddWithValue("@NumCompromiso", __Cliente);

                        SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                        parameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parameter);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(table);

                        gridDatos.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }
    }
}
