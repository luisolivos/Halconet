using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ventas.Ventas.ScoreCard
{
    public partial class frmUtilidadScore : Form
    {
        int Vendedor;
        string Sucursal;
        DateTime Fecha;

        public enum Columnas
        {
            Vendedor,
            Real,
            Objetivo
        }

        private void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Vendedor].Width = 110;
            dgv.Columns[(int)Columnas.Objetivo].Width = 220;
            dgv.Columns[(int)Columnas.Real].Width = 220;

            dgv.Columns[(int)Columnas.Objetivo].HeaderText = "Utilidad\r\nReal";
            dgv.Columns[(int)Columnas.Real].HeaderText = "Utilidad\r\nObjetivo";
        }

        public frmUtilidadScore(int _vendedor, string _sucucrsal, DateTime _fecha)
        {
            InitializeComponent();

            Vendedor = _vendedor;
            Sucursal = _sucucrsal;
            Fecha = _fecha;
        }

        private void frmUtilidadScore_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            workerUtilidad.RunWorkerAsync();
        }

        private void workerUtilidad_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                SqlCommand commandClientes = new SqlCommand("PJ_ScoreCardVentas", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                commandClientes.CommandType = CommandType.StoredProcedure;
                commandClientes.Parameters.AddWithValue("@TipoConsulta", 9);
                commandClientes.Parameters.AddWithValue("@Cliente", string.Empty);
                commandClientes.Parameters.AddWithValue("@Sucursales", Sucursal);
                commandClientes.Parameters.AddWithValue("@Vendedores", string.Empty);
                commandClientes.Parameters.AddWithValue("@Presupuesto", string.Empty);
                commandClientes.Parameters.AddWithValue("@Fecha", Fecha);
                commandClientes.Parameters.AddWithValue("@SlpCode", Vendedor);

                commandClientes.CommandTimeout = 0;
                commandClientes.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = commandClientes;
                DataTable table = new DataTable();
                da.Fill(table);

                gridFacturas.DataSource = table;

                this.Formato(gridFacturas);
            }
            catch (Exception)
            {

            }
        }
    }
}
