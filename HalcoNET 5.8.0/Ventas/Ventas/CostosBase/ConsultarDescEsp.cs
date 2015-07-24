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


namespace Ventas
{
    public partial class ConsultarDescEsp : Form
    {
        #region PARAMETROS
        Clases.Logs log;
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        public string Articulo;
        public string Mes;
        public string Año;
        public decimal Precio;

        public enum ColumnasGrid
        {
            Articulo, NombreArticulo, Linea, Descuento, Cliente, NombreCliente
        }
        #endregion

        public ConsultarDescEsp()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(clbMeses.Text))
            {
                Mes = GetMes(clbMeses.Text);
                if (!string.IsNullOrEmpty(txtAño.Text))
                {
                    Año = txtAño.Text;

                    SqlCommand command = new SqlCommand("PJ_Ventas", conection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 21);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@Lineas", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@FechaInicial", string.Empty);
                    command.Parameters.AddWithValue("@FechaFinal", string.Empty);
                    command.Parameters.AddWithValue("@Factura", string.Empty);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@GranCanales", string.Empty);
                    command.Parameters.AddWithValue("@Canales", string.Empty);
                    command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Mes", Mes);
                    command.Parameters.AddWithValue("@Anio", Año);
                    command.Parameters.AddWithValue("@NombreArticulo", string.Empty);
                    command.Parameters.AddWithValue("@Precio", ClasesSGUV.Propiedades.RolesHalcoNET.Administrador);
                    command.Parameters.AddWithValue("@Moneda", string.Empty);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    dgvDescuentos.DataSource = table;
                    if (dgvDescuentos.Rows.Count > 0)
                    {
                       FormatoGrid();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron descuentos.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        clbMeses.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Ingese un año.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtAño.Focus();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un mes.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                clbMeses.Focus();
            }
        }

        private string GetMes(string mes)
        {
            switch (mes)
            {
                case "Enero":
                    Mes = "1";
                    break;
                case "Febrero":
                    Mes = "2";
                    break;
                case "Marzo":
                    Mes = "3";
                    break;
                case "Abril":
                    Mes = "4";
                    break;
                case "Mayo":
                    Mes = "5";
                    break;
                case "Junio":
                    Mes = "6";
                    break;
                case "Julio":
                    Mes = "7";
                    break;
                case "Agosto":
                    Mes = "8";
                    break;
                case "Septiembre":
                    Mes = "9";
                    break;
                case "Octubre":
                    Mes = "10";
                    break;
                case "Noviembre":
                    Mes = "11";
                    break;
                case "Diciembre":
                    Mes = "12";
                    break;
            }
            return Mes;
        }

        private void FormatoGrid()
        {
            dgvDescuentos.Columns[(int)ColumnasGrid.Articulo].Width = 100;
            dgvDescuentos.Columns[(int)ColumnasGrid.NombreArticulo].Width = 190;
            dgvDescuentos.Columns[(int)ColumnasGrid.Linea].Width = 80;
            dgvDescuentos.Columns[(int)ColumnasGrid.Descuento].Width = 77;
            dgvDescuentos.Columns[(int)ColumnasGrid.Cliente].Width = 68;
            dgvDescuentos.Columns[(int)ColumnasGrid.NombreCliente].Width = 200;


            dgvDescuentos.Columns[(int)ColumnasGrid.Descuento].DefaultCellStyle.Format = "P2";
            dgvDescuentos.Columns[(int)ColumnasGrid.Descuento].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void ConsultarDescEsp_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
        }

        private void ConsultarDescEsp_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void ConsultarDescEsp_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
            }
        }


    }
}
