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

namespace Cobranza.Indicadores
{
    public partial class IndicadorReferencias : Form
    {
        string Sucursal;

        public IndicadorReferencias(string _sucursal)
        {
            Sucursal = _sucursal;
            InitializeComponent();
        }

        private enum Columnas
        {
            Cliente,
            Nombre, 
            Sucursal,
            Saldo

        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Cliente].Width = 90;
            dgv.Columns[(int)Columnas.Nombre].Width = 150;
            dgv.Columns[(int)Columnas.Sucursal].Width = 90;
            dgv.Columns[(int)Columnas.Saldo].Width = 90;

            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void CargarSucursales()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_IndicadoresSucursalCob", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    cbSucursal.DataSource = tbl;
                    cbSucursal.ValueMember = "Codigo";
                    cbSucursal.DisplayMember = "Nombre";
                }
            }
        }

        private void IndicadorReferencias_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                try
                {
                    this.CargarSucursales();
                    cbSucursal.SelectedValue = Sucursal;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_IndicadoresSucursalCob", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 5);
                        command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now);

                        DataTable tbl = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        da.Fill(tbl);

                        dgvDatos.DataSource = tbl;

                        this.Formato(dgvDatos);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cbSucursal_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_IndicadoresSucursalCob", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 5);
                        command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now);

                        DataTable tbl = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        da.Fill(tbl);

                        dgvDatos.DataSource = tbl;

                        this.Formato(dgvDatos);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDatos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = (sender as DataGridView);
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {

                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }
    }
}
