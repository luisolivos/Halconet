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

namespace Cobranza
{
    public partial class PagosClientes : Form
    {
        public enum Columnas
        {
            Tipo,
            Cliente,
            Enero,
            Febrero,
            Marzo,
            Abril,
            Mayo,
            Junio,
            Julio,
            Agosto,
            Septiembre,
            Octubre,
            Noviembre,
            Diciembre
        }

        public PagosClientes()
        {
            InitializeComponent();
        }

        public DataTable GetTabla(string _opcion, string _cliente)
        {
            DataTable _table  = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_SaldosPendientes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@Vendedores", String.Empty);
                        command.Parameters.AddWithValue("@JefaCobranza", String.Empty);
                        command.Parameters.AddWithValue("@Sucursal", String.Empty);
                        command.Parameters.AddWithValue("@Usuario", _opcion);
                        command.Parameters.AddWithValue("@Cliente", _cliente);
                        command.Parameters.AddWithValue("@Factura", String.Empty);

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;

                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(_table);
                        if (_table.Rows.Count <= 2)
                        {
                            _table.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return _table;
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Tipo].HeaderText = "";
            dgv.Columns[(int)Columnas.Cliente].Visible = false;

            dgv.Columns[(int)Columnas.Tipo].Width = 135;
            dgv.Columns[(int)Columnas.Cliente].Width = 90;

            dgv.Columns[(int)Columnas.Enero].Width = 80;
            dgv.Columns[(int)Columnas.Febrero].Width = 80;
            dgv.Columns[(int)Columnas.Marzo].Width = 80;
            dgv.Columns[(int)Columnas.Abril].Width = 80;
            dgv.Columns[(int)Columnas.Mayo].Width = 80;
            dgv.Columns[(int)Columnas.Junio].Width = 80;
            dgv.Columns[(int)Columnas.Julio].Width = 80;
            dgv.Columns[(int)Columnas.Agosto].Width = 80;
            dgv.Columns[(int)Columnas.Septiembre].Width = 80;
            dgv.Columns[(int)Columnas.Octubre].Width = 80;
            dgv.Columns[(int)Columnas.Noviembre].Width = 80;
            dgv.Columns[(int)Columnas.Diciembre].Width = 80;

            dgv.Columns[(int)Columnas.Enero].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Febrero].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Marzo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Abril].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mayo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Junio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Julio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Agosto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Septiembre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Octubre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Noviembre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Diciembre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            gridVencidos.DataSource = GetTabla("Vencido", txtCliente.Text);
            gridActuales.DataSource = GetTabla("Actual", txtCliente.Text);

   
            Formato(gridActuales);
            Formato(gridVencidos);

        }

        private void gridVencidos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (((DataGridView)sender).Rows.Count > 2)
                {
                    ((DataGridView)sender).Rows[0].DefaultCellStyle.Format = "C0";
                    ((DataGridView)sender).Rows[1].DefaultCellStyle.Format = "C0";
                    ((DataGridView)sender).Rows[2].DefaultCellStyle.Format = "P0";
                    ((DataGridView)sender).Rows[3].DefaultCellStyle.Format = "P0";
                }
            }
            catch(Exception)
            {
            }
        }

        private void PagosClientes_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }
    }
}
