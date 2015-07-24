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

namespace Ventas.Ventas.ScoreCard
{
    public partial class frmDetalleLineasHalcon : Form
    {
        public int SlpCode;
        public DateTime Fecha;
        public int Bono;

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

        public frmDetalleLineasHalcon(int _slpcode, DateTime _fecha, string _vendedor, int _bono)
        {           
            SlpCode = _slpcode;
            Fecha = _fecha;
            Bono = _bono;
            InitializeComponent();
            this.Text += " " + _vendedor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DetalleLineasHalcon_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            DataTable t = new DataTable();
            SqlCommand command = new SqlCommand("PJ_VariasScoreCard", conection);
            command.Parameters.AddWithValue("@TipoConsulta", 1);
            command.Parameters.AddWithValue("@SlpCode", SlpCode);
            command.Parameters.AddWithValue("@Fecha", Fecha);
            command.Parameters.AddWithValue("@Bono", Bono);
            command.Parameters.AddWithValue("@From", string.Empty);
            command.Parameters.AddWithValue("@Mensaje", string.Empty);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 0;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = command;
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(t);

            dataGridView1.DataSource = t;
            formato();
        }


        private void formato()
        {
            dataGridView1.Columns[0].Visible = false; ;
            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[2].Width = 70;
            dataGridView1.Columns[3].Width = 70;

            dataGridView1.Columns[1].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns[2].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "C0";

            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void DetalleLineasHalcon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
