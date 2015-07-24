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

namespace Ventas.Ventas
{
    public partial class frmVentaEfectiva : Form
    {
        int RolUsuario = ClasesSGUV.Login.Rol;
        string Sucursal = ClasesSGUV.Login.Sucursal;
        int CodigoVendedor = ClasesSGUV.Login.Vendedor1;
        Clases.Logs log;

        public frmVentaEfectiva()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Vendedor,
            Sucursal,
            Vta,
            NC,
            VtaEfectiva
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Vendedor].Width = 200;
            dgv.Columns[(int)Columnas.Sucursal].Width = 100;
            dgv.Columns[(int)Columnas.Vta].Width = 100;
            dgv.Columns[(int)Columnas.NC].Width = 100;
            dgv.Columns[(int)Columnas.VtaEfectiva].Width = 100;

            dgv.Columns[(int)Columnas.Vta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NC].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.VtaEfectiva].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Vta].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NC].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.VtaEfectiva].DefaultCellStyle.Format = "C2";
        }

        private void CargarVendedores()
        {
            SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);

            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
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
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal )
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
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

        private void CargarSucursales()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Sucursales);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "Todas";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            //if(RolUsuario != (int)Constantes.RolesSistemaSGUV.Administrador)
                //foreach (DataRow item in table.Rows)
                //{
                //    if (item.Field<string>("Nombre").Equals("Racsa"))
                //        table.Rows.Remove(item);
                //}

            clbSucursal.DataSource = table;
            clbSucursal.DisplayMember = "Nombre";
            clbSucursal.ValueMember = "Codigo";
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
                foreach (DataRowView item in clb.Items)
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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_VtaEfectiva", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Desde", dtDesde.Value.Date);
                        command.Parameters.AddWithValue("@Hasta", dtHasta.Value.Date);
                        command.Parameters.AddWithValue("@Vendedores", this.Cadena(clbVendedor));
                        command.Parameters.AddWithValue("@Sucursales", this.Cadena(clbSucursal));
                        command.Parameters.AddWithValue("@Lineas", string.Empty);
                        command.Parameters.AddWithValue("@Articulo", string.Empty);

                        DataTable table = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(table);

                        lblTotal.Text =  string.Empty;
                        if(table.Rows.Count > 0)
                        {
                            decimal venta = Convert.ToDecimal( table.Compute("SUM([Monto de venta])", string.Empty));
                            decimal nc = Convert.ToDecimal(table.Compute("SUM([Monto de NC])", string.Empty));
                            decimal vtaEfectiva = Convert.ToDecimal(table.Compute("SUM([Venta efectiva])", string.Empty));

                            lblTotal.Text = "Monto de venta:\t" + venta.ToString("C0") + "\r\nMonto de NC:\t" + nc.ToString("C0") + "\r\nVenta Efectiva:\t" + vtaEfectiva.ToString("C0");
                        }


                        if (checkBox1.Checked)
                        {
                            if (RolUsuario != (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
                            {
                                var query = (from item in table.AsEnumerable()
                                             select item.Field<string>("Sucursal")).Distinct();

                                foreach (var item in query.ToList())
                                {
                                    DataRow r = table.NewRow();
                                    r["Sucursal"] = item + " Total";

                                    r["Monto de venta"] = (from acum in table.AsEnumerable()
                                                           where acum.Field<string>("Sucursal") == item
                                                           select acum.Field<decimal>("Monto de venta")).Sum();
                                    r["Monto de NC"] = (from acum in table.AsEnumerable()
                                                        where acum.Field<string>("Sucursal") == item
                                                        select acum.Field<decimal>("Monto de NC")).Sum();
                                    r["Venta efectiva"] = (from acum in table.AsEnumerable()
                                                           where acum.Field<string>("Sucursal") == item
                                                           select acum.Field<decimal>("Venta efectiva")).Sum();
                                    table.Rows.Add(r);
                                }

                                table = (from tv in table.AsEnumerable()
                                         orderby tv.Field<string>("Sucursal")
                                         select tv).CopyToDataTable();

                            }

                        }
                        dgvVenta.DataSource = table;
                        this.Formato(dgvVenta);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VentaEfectiva_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                this.CargarVendedores();
                this.CargarSucursales();

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clbVendedor_Click(object sender, EventArgs e)
        {
            if ((sender as CheckedListBox).SelectedIndex == 0)
            {
                if ((sender as CheckedListBox).CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < (sender as CheckedListBox).Items.Count; item++)
                    {
                        (sender as CheckedListBox).SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < (sender as CheckedListBox).Items.Count; item++)
                    {
                        (sender as CheckedListBox).SetItemChecked(item, true);
                    }
                }
            }
        }

        private void dgvVenta_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void VentaEfectiva_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void VentaEfectiva_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void dgvVenta_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells[(int)Columnas.Sucursal].Value.ToString().Contains("Total"))
                        item.DefaultCellStyle.Font = new Font("Arial", 9f, FontStyle.Bold);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            btnConsultar_Click(sender, e);
        }
    }
}
