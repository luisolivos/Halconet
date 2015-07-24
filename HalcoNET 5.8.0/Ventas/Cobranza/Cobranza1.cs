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
using System.Threading;

namespace Ventas
{
    public partial class Cobranza1 : Form
    {

        #region PARÁMETROS

        Clases.Logs log;
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);
        public string Sucursales;
        public string Cobranza;
        public string Vendedores;
        public string Sucursal;
        public string Cliente;
        public string Factura;

        public int Rol;
        public int CodVendedor;//slpcode
        public string NombreUsuario;
        public DataTable tablaexcel;

        public enum Encabezado
        {
            Cliente, NombreCliente, CondicionPago, Sucursal, ImporteOriginal, Saldo, PorVencer, Col1, Col2, Col3, Col4, Col5
        }

        public enum Detalle
        {
            Factura, Cliente, NombreCliente, Vendedor, Fecha, Vencimiento, ImporteOriginal,  Devolucion, Descuento, Pagos, Saldo, PorVencer, Col1, Col2, Col3, Col4, Col5, Real, PP
        }
        #endregion        
        
        public Cobranza1(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            InitializeComponent();
            Rol = rolUsuario;
            CodVendedor = codigoVendedor;
            NombreUsuario = nombreUsuario;
            Sucursal = sucursal;
        }

        

        private void Cobranza1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

                if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                {
                    clbCobranza.Visible = true;
                    label2.Visible = true;
                }
                else if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
                {
                    clbCobranza.Visible = false;
                    label2.Visible = false;
                    lblSucursal.Visible = false;
                    clbSucursal.Visible = false;
                }

                CargarJefesCobranza();
                CargarSucursales();
                CargarVendedores();

                txtCliente.Clear();
                txtFactura.Clear();
                txtVenta.Clear();
                txtVolumen.Clear();
                txtUtilidad.Clear();
                txtCliente.Focus();

                gridDetalles.DataSource = null;
                gridFacturas.DataSource = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error inesperado. \r\n" + ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        #region METODOS
        private void CargarSucursales()
        {
            //if (Rol == (int)Constantes.RolesSistemaSGUV.Administrador || Rol == (int)Constantes.RolesSistemaSGUV.GerenteCobranza)
            //{
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Sucursales);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@SlpCode", 0);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                clbSucursal.DataSource = table;
                clbSucursal.DisplayMember = "Nombre";
                clbSucursal.ValueMember = "Codigo";
                
            //}
            //else
            //{
            //    SqlCommand command = new SqlCommand("PJ_Cobrnaza", conection);
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.Parameters.AddWithValue("@TipoConsulta", 3);
            //    command.Parameters.AddWithValue("@Vendedores", string.Empty);
            //    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
            //    command.Parameters.AddWithValue("@Cliente", string.Empty);
            //    command.Parameters.AddWithValue("@Sucursal", Sucursal);
            //    command.Parameters.AddWithValue("@Usuario", NombreUsuario);
            //    command.Parameters.AddWithValue("@Factura", string.Empty);
            //    command.CommandTimeout = 0;

            //    DataTable table = new DataTable();
            //    SqlDataAdapter adapter = new SqlDataAdapter();
            //    adapter.SelectCommand = command;
            //    adapter.Fill(table);

            //    DataRow row = table.NewRow();
            //    row["Nombre"] = "TODAS";
            //    row["Codigo"] = "0";
            //    table.Rows.InsertAt(row, 0);

            //    clbSucursal.DataSource = table;
            //    clbSucursal.DisplayMember = "Nombre";
            //    clbSucursal.ValueMember = "Codigo";

            //    int i = 0;
            //    foreach (DataRow r in table.Rows)
            //    {
            //        if (i > 0)
            //            auxsucursales += r.Field<Int32>("Codigo") + ",";
            //        i++;

            //    }
            //}
        }

        private void CargarVendedores()
        {
            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
                command.Parameters.AddWithValue("@Sucursal", Sucursal);
                command.Parameters.AddWithValue("@SlpCode", 0);

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
            else
            {
                SqlCommand command = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 4);
                command.Parameters.AddWithValue("@Vendedores", string.Empty);
                command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                command.Parameters.AddWithValue("@Cliente", string.Empty);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@Usuario", NombreUsuario);
                command.Parameters.AddWithValue("@Factura", string.Empty);
                command.CommandTimeout = 0;

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

        private void CargarJefesCobranza()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.JefesCobranza);
            command.Parameters.AddWithValue("@Sucursal", Sucursal);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODAS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            clbCobranza.DataSource = table;
            clbCobranza.DisplayMember = "Nombre";
            clbCobranza.ValueMember = "Codigo";
        }

        public void Consultar()
        {
            btnExportar.Enabled = false;
            gridFacturas.Columns.Clear();
            gridFacturas.DataSource = null;
            gridDetalles.Columns.Clear();
            gridDetalles.DataSource = null;

            StringBuilder stbVendedores = new StringBuilder();
            foreach (DataRowView item in clbVendedor.CheckedItems)
            {
                if (clbVendedor.CheckedItems.Count != 0)
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbVendedor.ToString().Equals(string.Empty))
                        {
                            stbVendedores.Append(",");
                        }
                        stbVendedores.Append(item["Codigo"].ToString());
                    }
            }
            //if (clbVendedor.CheckedItems.Count == 0)
            //{
            //    foreach (DataRowView item in clbVendedor.Items)
            //    {
            //        if (item["Codigo"].ToString() != "0")
            //        {
            //            if (!clbVendedor.ToString().Equals(string.Empty))
            //            {
            //                stbVendedores.Append(",");
            //            }
            //            stbVendedores.Append(item["Codigo"].ToString());
            //        }
            //    }
            //}

            StringBuilder stbSucursales = new StringBuilder();
            foreach (DataRowView item in clbSucursal.CheckedItems)
            {
                if (clbSucursal.CheckedItems.Count != 0)
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbSucursal.ToString().Equals(string.Empty))
                        {
                            stbSucursales.Append(",");
                        }
                        stbSucursales.Append(item["Codigo"].ToString());
                    }
            }
            //if (clbSucursal.CheckedItems.Count == 0)
            //{
            //    foreach (DataRowView item in clbSucursal.Items)
            //    {
            //        if (item["Codigo"].ToString() != "0")
            //        {
            //            if (!clbSucursal.ToString().Equals(string.Empty))
            //            {
            //                stbSucursales.Append(",");
            //            }
            //            stbSucursales.Append(item["Codigo"].ToString());
            //        }
            //    }
            //}

            StringBuilder stbCobranza = new StringBuilder();
            foreach (DataRowView item in clbCobranza.CheckedItems)
            {
                if (clbCobranza.CheckedItems.Count != 0)
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbCobranza.ToString().Equals(string.Empty))
                        {
                            stbCobranza.Append("'" + item["Nombre"].ToString()+"',");
                        }
                    }
            }
            //if (clbCobranza.CheckedItems.Count == 0)
            //{
            //    foreach (DataRowView item in clbCobranza.Items)
            //    {
            //        if (item["Codigo"].ToString() != "0")
            //        {
            //            if (!clbCobranza.ToString().Equals(string.Empty))
            //            {
            //                stbCobranza.Append(",");
            //            }
            //            stbCobranza.Append(item["Nombre"].ToString());
            //        }
            //    }
            //}

            Sucursales = stbSucursales.ToString();
            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                Cobranza = stbCobranza.ToString();
            else if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                SqlCommand co = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                co.CommandType = CommandType.StoredProcedure;
                co.Parameters.AddWithValue("@TipoConsulta", 5);
                co.Parameters.AddWithValue("@Vendedores", string.Empty);
                co.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                co.Parameters.AddWithValue("@Cliente", Sucursal);
                co.Parameters.AddWithValue("@Sucursal", string.Empty);
                co.Parameters.AddWithValue("@Usuario", NombreUsuario);
                co.Parameters.AddWithValue("@Factura", string.Empty);
                co.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = co;
                ad.Fill(table);
                string jefa = "";
                foreach (DataRow r in table.Rows)
                {
                    jefa += "'" + r.Field<string>("Nombre") + "',";
                }
                Cobranza = jefa ;
            }
            Vendedores = stbVendedores.ToString();
            Cliente = txtCliente.Text;
            Factura = txtFactura.Text;

            DataSet data = new DataSet();
            BindingSource masterBindingSource = new BindingSource();
            BindingSource detailsBindingSource = new BindingSource();


            SqlCommand command = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 1);
            command.Parameters.AddWithValue("@Vendedores", Vendedores.Trim(','));
            command.Parameters.AddWithValue("@JefaCobranza", Cobranza.Trim(','));
            command.Parameters.AddWithValue("@Sucursal", Sucursales.Trim(','));
            command.Parameters.AddWithValue("@Usuario", NombreUsuario);
            command.Parameters.AddWithValue("@Cliente", Cliente);
            command.Parameters.AddWithValue("@Factura", Factura);

            command.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(data, "TblEncabezado");


            SqlCommand commandDetalles = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            commandDetalles.CommandType = CommandType.StoredProcedure;
            commandDetalles.Parameters.AddWithValue("@TipoConsulta", 2);
            commandDetalles.Parameters.AddWithValue("@Vendedores", Vendedores.Trim(','));
            commandDetalles.Parameters.AddWithValue("@JefaCobranza", Cobranza.Trim(','));
            commandDetalles.Parameters.AddWithValue("@Sucursal", Sucursales.Trim(','));
            commandDetalles.Parameters.AddWithValue("@Usuario", NombreUsuario);
            commandDetalles.Parameters.AddWithValue("@Cliente", Cliente);
            commandDetalles.Parameters.AddWithValue("@Factura", Factura);

            commandDetalles.CommandTimeout = 0;
            SqlDataAdapter adapterDetalles = new SqlDataAdapter();
            adapterDetalles.SelectCommand = commandDetalles;
            adapterDetalles.SelectCommand.CommandTimeout = 0;
            adapterDetalles.Fill(data, "TblDetalles");

            DataRelation relation = new DataRelation("EncabezadoDetalle", data.Tables["TblEncabezado"].Columns["Cliente"], data.Tables["TblDetalles"].Columns["Cliente"]);
            data.Relations.Add(relation);

            masterBindingSource.DataSource = data;
            masterBindingSource.DataMember = "TblEncabezado";
            detailsBindingSource.DataSource = masterBindingSource;
            detailsBindingSource.DataMember = "EncabezadoDetalle";
            gridFacturas.DataSource = masterBindingSource;
            gridDetalles.DataSource = detailsBindingSource;

            tablaexcel =  data.Tables["TblDetalles"];
            gridExcel.DataSource = tablaexcel;
            if (tablaexcel.Rows.Count != 0)
            {
                btnExportar.Enabled = true;
            }
        }

        public void FormatoGridEncabezado(DataGridView dgv)
        {
            dgv.Columns[(int)Encabezado.Cliente].Width = 100;
            dgv.Columns[(int)Encabezado.NombreCliente].Width = 260;
            dgv.Columns[(int)Encabezado.CondicionPago].Width = 90;
            dgv.Columns[(int)Encabezado.CondicionPago].HeaderText = "Condicion de pago";
            dgv.Columns[(int)Encabezado.Sucursal].Width = 90;
            dgv.Columns[(int)Encabezado.ImporteOriginal].Width = 100;
            dgv.Columns[(int)Encabezado.Saldo].Width = 100;
            dgv.Columns[(int)Encabezado.PorVencer].Width = 100;
            dgv.Columns[(int)Encabezado.Col1].Width = 100;
            dgv.Columns[(int)Encabezado.Col2].Width = 100;
            dgv.Columns[(int)Encabezado.Col3].Width = 100;
            dgv.Columns[(int)Encabezado.Col4].Width = 100;
            dgv.Columns[(int)Encabezado.Col5].Width = 100;


            dgv.Columns[(int)Encabezado.ImporteOriginal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.PorVencer].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Col1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Col2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Col3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Col4].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Col5].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Encabezado.ImporteOriginal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Encabezado.Saldo].DefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Encabezado.PorVencer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Encabezado.Col1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Encabezado.Col2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Encabezado.Col3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Encabezado.Col4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Encabezado.Col5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.ReadOnly = true;
            }
        }

        public void FormatoGridDetalle(DataGridView dgv)
        {
            //Factura, Cliente, Vendedor, Fecha, ImporteOriginal, Vencimiento, Devolucion, Descuento, Pagos, Saldo, PorVencer, Col1, Col2, Col3, Col4, Col5
            dgv.Columns[(int)Detalle.Factura].Width = 80;
            dgv.Columns[(int)Detalle.Cliente].Width = 80;
            dgv.Columns[(int)Detalle.NombreCliente].Width = 150;
            dgv.Columns[(int)Detalle.Vendedor].Width = 150;
            dgv.Columns[(int)Detalle.Fecha].Width = 90;
            dgv.Columns[(int)Detalle.ImporteOriginal].Width = 100;
            dgv.Columns[(int)Detalle.Vencimiento].Width = 90;
            dgv.Columns[(int)Detalle.Devolucion].Width = 100;
            dgv.Columns[(int)Detalle.Descuento].Width = 100;
            dgv.Columns[(int)Detalle.Pagos].Width = 100;
            dgv.Columns[(int)Detalle.Saldo].Width = 100;
            dgv.Columns[(int)Detalle.PorVencer].Width = 100;
            dgv.Columns[(int)Detalle.Col1].Width = 100;
            dgv.Columns[(int)Detalle.Col2].Width = 100;
            dgv.Columns[(int)Detalle.Col3].Width = 100;
            dgv.Columns[(int)Detalle.Col4].Width = 100;
            dgv.Columns[(int)Detalle.Col5].Width = 100;

            dgv.Columns[(int)Detalle.ImporteOriginal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.Devolucion].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.Descuento].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.Pagos].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.PorVencer].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.Col1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.Col2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.Col3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.Col4].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.Col5].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.Real].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Detalle.PP].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Detalle.ImporteOriginal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.Devolucion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.Descuento].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.Pagos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.PorVencer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.Col1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.Col2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.Col3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.Col4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.Col5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.Real].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Detalle.PP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.ReadOnly = true;
            }
        }

        private void Esperar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
        }
        private void Continuar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }
        #endregion

        #region EVENTOS
        private void clbSucursal_Click(object sender, EventArgs e)
        {
            if (clbSucursal.SelectedIndex == 0)
            {
                if (clbSucursal.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbSucursal.Items.Count; item++)
                    {
                        clbSucursal.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbSucursal.Items.Count; item++)
                    {
                        clbSucursal.SetItemChecked(item, true);
                    }
                }
            }
        }

        private void clbVendedor_Click(object sender, EventArgs e)
        {
            if (clbVendedor.SelectedIndex == 0)
            {
                if (clbVendedor.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbVendedor.Items.Count; item++)
                    {
                        clbVendedor.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbVendedor.Items.Count; item++)
                    {
                        clbVendedor.SetItemChecked(item, true);
                    }
                }
            }
        }

        private void clbCobranza_Click(object sender, EventArgs e)
        {
            if (clbCobranza.SelectedIndex == 0)
            {
                if (clbCobranza.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbCobranza.Items.Count; item++)
                    {
                        clbCobranza.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbCobranza.Items.Count; item++)
                    {
                        clbCobranza.SetItemChecked(item, true);
                    }
                }
            }
        }

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                Esperar();
                Consultar();
                FormatoGridEncabezado(gridFacturas);
                FormatoGridDetalle(gridDetalles);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Continuar();
            }
        }
        #endregion      

        private void txtCliente_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPresentar_Click(sender, e);
            }
            if (e.KeyCode == Keys.Escape)
            {
                Cobranza1_Load(sender, e);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                //DateTime FechaInicial = DateTime.Now;
                //DateTime FechaFinal = DateTime.Now;

                //Fechas f = new Fechas();
                //if (f.ShowDialog() == DialogResult.OK)
                //{
                //    FechaInicial = DateTime.Parse(f.fi.ToShortDateString());
                //    FechaFinal = DateTime.Parse(f.ff.ToShortDateString());

                //    gridExcel.DataSource = (from item in tablaexcel.AsEnumerable()
                //                            where (item.Field<DateTime>("Fecha") >= FechaInicial && item.Field<DateTime>("Fecha") <= FechaFinal)
                //                            select item).CopyToDataTable();

                Esperar();
                ExportarAExcel ex = new ExportarAExcel();
                if (ex.ExportarSinFormato(gridExcel))
                    MessageBox.Show("El Archivo se creo con exito.");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Inesperado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Continuar();
            }
        }

        private void Cobranza1_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void Cobranza1_FormClosing(object sender, FormClosingEventArgs e)
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
