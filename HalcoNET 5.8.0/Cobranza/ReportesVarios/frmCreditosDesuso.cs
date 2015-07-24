using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.ReportesVarios
{
    public partial class frmCreditosDesuso : Form
    {
        public frmCreditosDesuso()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Cliente,
            Nombre,
            Jefa,
            Situacion,
            LinSAP,
            LimAtradius
        }

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", connection))
                    {
                        command.Parameters.AddWithValue("@TipoConsulta", 13);
                        command.Parameters.AddWithValue("@Cliente", numericUpDown1.Value*-1);

                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable table = new DataTable();

                        da.Fill(table);

                        gridDatos.DataSource = table;

                        gridDatos.Columns[(int)Columnas.LimAtradius].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        gridDatos.Columns[(int)Columnas.LinSAP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        gridDatos.Columns[(int)Columnas.LimAtradius].DefaultCellStyle.Format = "C2";
                        gridDatos.Columns[(int)Columnas.LinSAP].DefaultCellStyle.Format = "C2";

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridDatos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void frmCreditosDesuso_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }
    }
}
