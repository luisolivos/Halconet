using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.Pagos
{
    public partial class frmAuditoria : Form
    {
        public enum Columnas
        {
            ClienteOk, 
            Referencia,
            Abono,
            Cliente,
            Nombre,
            Factura,
            Aplicado,
            Usuario,
            Fecha
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.ClienteOk].Width = 90;
            dgv.Columns[(int)Columnas.Referencia].Width = 180;
            dgv.Columns[(int)Columnas.Abono].Width = 90;
            dgv.Columns[(int)Columnas.Cliente].Width = 90;
            dgv.Columns[(int)Columnas.Nombre].Width = 180;
            dgv.Columns[(int)Columnas.Factura].Width = 90;
            dgv.Columns[(int)Columnas.Aplicado].Width = 90;
            dgv.Columns[(int)Columnas.Usuario].Width = 110;
            dgv.Columns[(int)Columnas.Fecha].Width = 90;

            dgv.Columns[(int)Columnas.Abono].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Aplicado].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Abono].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Aplicado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public frmAuditoria()
        {
            InitializeComponent();
        }

        private void frmAuditoria_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Pagos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 26);
                    command.Parameters.AddWithValue("@FechaDesde", dateTimePicker1.Value);
                    command.Parameters.AddWithValue("@FechaHasta", dateTimePicker2.Value);

                    DataTable tbl = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(tbl);

                    dgvMovimientos.DataSource = tbl;

                    this.Formato(dgvMovimientos);
                }
            }
        }
    }
}
