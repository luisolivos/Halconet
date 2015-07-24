using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.GestionCobranza
{
    public partial class frmAuditoriaEdoCta : Form
    {
        public frmAuditoriaEdoCta()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_EstadoCuenta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 5);
                        command.Parameters.AddWithValue("@Desde", dtpDesde.Value);
                        command.Parameters.AddWithValue("@Hasta", dtpHasta.Value);

                        if(ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
                            command.Parameters.AddWithValue("@JefaCobranza", ClasesSGUV.Login.Usuario);

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable tbl = new DataTable();

                        da.SelectCommand = command;
                        da.Fill(tbl);
                        dataGridView2.DataSource = tbl;

                        //dataGridView2.Columns[0].Visible = false;
                        dataGridView2.Columns[0].Width = 200;
                    }
                }
            }catch(Exception)
            {
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_EstadoCuenta", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                       // command.Parameters.AddWithValue("@Usuario", (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value);
                        command.Parameters.AddWithValue("@JefaCobranza", (sender as DataGridView).Rows[e.RowIndex].Cells[0].Value);
                        command.Parameters.AddWithValue("@Desde", dtpDesde.Value);
                        command.Parameters.AddWithValue("@Hasta", dtpHasta.Value);

                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable tbl = new DataTable();

                        da.SelectCommand = command;
                        da.Fill(tbl);
                        dataGridView1.DataSource = tbl;

                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void frmAuditoriaEdoCta_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }
    }
}
