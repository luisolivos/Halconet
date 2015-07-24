using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas.AnalisisClientes.Controles.Gerencia
{
    public partial class SeguimientoCompromisos : UserControl
    {
        int RolUsuario;
        string Sucursal;
        int CodigoVendedor;

        public enum Columnas
        {
            Linea,
            Cliente,
            Nombre,
            Mes1,
            Mes2,
            Mes3,
            Mes4,
            Mes5,
            Mes6,
            Compromisos
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Linea].Width = 60;
            dgv.Columns[(int)Columnas.Cliente].Width = 60;
            dgv.Columns[(int)Columnas.Nombre].Width = 150;
            dgv.Columns[(int)Columnas.Mes1].Width = 70;
            dgv.Columns[(int)Columnas.Mes2].Width = 70;
            dgv.Columns[(int)Columnas.Mes3].Width = 70;
            dgv.Columns[(int)Columnas.Mes4].Width = 70;
            dgv.Columns[(int)Columnas.Mes5].Width = 70;
            dgv.Columns[(int)Columnas.Mes6].Width = 70;
            dgv.Columns[(int)Columnas.Compromisos].Visible = false;

            dgv.Columns[(int)Columnas.Mes1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mes2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mes3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mes4].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mes5].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mes6].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Mes1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mes6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public SeguimientoCompromisos(int _rol, string _sucursal, int _codVendedor)
        {
            InitializeComponent();

            RolUsuario = _rol;
            Sucursal = _sucursal;
            CodigoVendedor = _codVendedor;
        }

        private void CargarVendedores()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 11);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", CodigoVendedor);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbVendedor.DataSource = table;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
        }

        private string Cadena(CheckedListBox clb)
        {
            StringBuilder stb = new StringBuilder();
            foreach (DataRowView item in clb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clb.ToString().Equals(string.Empty))
                    {
                        stb.Append(",");
                    }
                    stb.Append(item["Codigo"].ToString());
                }
            }
            if (clb.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clbVendedor.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clb.ToString().Equals(string.Empty))
                        {
                            stb.Append(",");
                        }
                        stb.Append(item["Codigo"].ToString());
                    }
                }
            }

            return stb.ToString();
        }

        private void SeguimientoCompromisos_Load(object sender, EventArgs e)
        {
            try
            {
                this.CargarVendedores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_AnalisisVentas";

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 13);
                    command.Parameters.AddWithValue("@Pregunta", 0);
                    command.Parameters.AddWithValue("@Clasificacion", string.Empty);
                    command.Parameters.AddWithValue("@Letra", string.Empty);
                    command.Parameters.AddWithValue("@Especificacion", this.Cadena(clbVendedor));
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);

                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                    command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                    command.Parameters.AddWithValue("@Nombre", string.Empty);

                    command.CommandTimeout = 0;

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);

                    dgvRanking.DataSource = table;
                    this.Formato(dgvRanking);
                }
            }
        }

        private void dgvRanking_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value) != decimal.Zero)
                    {
                        ////mes 1
                        if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes1].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)0.80)
                        {
                            item.Cells[(int)Columnas.Mes1].Style.BackColor = Color.Red;
                            item.Cells[(int)Columnas.Mes1].Style.ForeColor = Color.White;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes1].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) >= (decimal)0.80
                             && (Convert.ToDecimal(item.Cells[(int)Columnas.Mes1].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes1].Style.BackColor = Color.Yellow;
                            item.Cells[(int)Columnas.Mes1].Style.ForeColor = Color.Black;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes1].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) > (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes1].Style.BackColor = Color.Green;
                            item.Cells[(int)Columnas.Mes1].Style.ForeColor = Color.Black;
                        }
                        ///////////////
                        if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes2].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)0.80)
                        {
                            item.Cells[(int)Columnas.Mes2].Style.BackColor = Color.Red;
                            item.Cells[(int)Columnas.Mes2].Style.ForeColor = Color.White;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes2].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) >= (decimal)0.80
                             && (Convert.ToDecimal(item.Cells[(int)Columnas.Mes2].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes2].Style.BackColor = Color.Yellow;
                            item.Cells[(int)Columnas.Mes2].Style.ForeColor = Color.Black;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes2].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) > (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes2].Style.BackColor = Color.Green;
                            item.Cells[(int)Columnas.Mes2].Style.ForeColor = Color.Black;
                        }
                        ///////////////
                        if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes3].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)0.80)
                        {
                            item.Cells[(int)Columnas.Mes3].Style.BackColor = Color.Red;
                            item.Cells[(int)Columnas.Mes3].Style.ForeColor = Color.White;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes3].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) >= (decimal)0.80
                             && (Convert.ToDecimal(item.Cells[(int)Columnas.Mes3].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes3].Style.BackColor = Color.Yellow;
                            item.Cells[(int)Columnas.Mes3].Style.ForeColor = Color.Black;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes3].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) > (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes3].Style.BackColor = Color.Green;
                            item.Cells[(int)Columnas.Mes3].Style.ForeColor = Color.Black;
                        }
                        ///////////////
                        if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes4].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)0.80)
                        {
                            item.Cells[(int)Columnas.Mes4].Style.BackColor = Color.Red;
                            item.Cells[(int)Columnas.Mes4].Style.ForeColor = Color.White;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes4].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) >= (decimal)0.80
                             && (Convert.ToDecimal(item.Cells[(int)Columnas.Mes4].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes4].Style.BackColor = Color.Yellow;
                            item.Cells[(int)Columnas.Mes4].Style.ForeColor = Color.Black;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes4].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) > (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes4].Style.BackColor = Color.Green;
                            item.Cells[(int)Columnas.Mes4].Style.ForeColor = Color.Black;
                        }
                        ///////////////
                        if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes5].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)0.80)
                        {
                            item.Cells[(int)Columnas.Mes5].Style.BackColor = Color.Red;
                            item.Cells[(int)Columnas.Mes5].Style.ForeColor = Color.White;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes5].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) >= (decimal)0.80
                             && (Convert.ToDecimal(item.Cells[(int)Columnas.Mes5].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes5].Style.BackColor = Color.Yellow;
                            item.Cells[(int)Columnas.Mes5].Style.ForeColor = Color.Black;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes5]) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos])) > (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes5].Style.BackColor = Color.Green;
                            item.Cells[(int)Columnas.Mes5].Style.ForeColor = Color.Black;
                        }
                        ///////////////
                        if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes6].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)0.80)
                        {
                            item.Cells[(int)Columnas.Mes6].Style.BackColor = Color.Red;
                            item.Cells[(int)Columnas.Mes6].Style.ForeColor = Color.White;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes6].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) >= (decimal)0.80
                             && (Convert.ToDecimal(item.Cells[(int)Columnas.Mes6].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) < (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes6].Style.BackColor = Color.Yellow;
                            item.Cells[(int)Columnas.Mes6].Style.ForeColor = Color.Black;
                        }
                        else if ((Convert.ToDecimal(item.Cells[(int)Columnas.Mes6].Value) / Convert.ToDecimal(item.Cells[(int)Columnas.Compromisos].Value)) > (decimal)1)
                        {
                            item.Cells[(int)Columnas.Mes6].Style.BackColor = Color.Green;
                            item.Cells[(int)Columnas.Mes6].Style.ForeColor = Color.Black;
                        }
                        ///////////////
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
