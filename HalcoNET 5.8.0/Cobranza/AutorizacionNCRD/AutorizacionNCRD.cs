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

namespace Cobranza
{
    public partial class Cobranza1 : Form
    {

        #region PARÁMETROS

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);
        public string Sucursales;
        public string Cobranza;
        public string Vendedores;
        public string Sucursal;
        public string Cliente;
        public string Factura;
        Clases.Logs log;
        public int IDLog;

        public int Rol;
        public int CodVendedor;//slpcode
        public string NombreUsuario;
        public DataTable tablaexcel;
        public DataTable JefasCobranza = new DataTable();
        public DataTable TBLVendedores = new DataTable();

        public enum Columnas
        {
            Factura, Cliente, NombreCliente, Sucursal, Fecha, Vencimiento, CostoBase, 
            TotalFactura,TotalPE, PagoAplicado, Devolucion, ProntoPago, NCPPSGUV, DiferenciaPP, PrecioEspecial, NCPESGUV, DiferenciaPE, Saldo, 
            UtilidadOriginal, UtilidadReal, Autorizado, Boton, BotonDetalle
        }
        #endregion        

        DataTable _Encabezado = new DataTable();
        public Cobranza1(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            InitializeComponent();
            Rol = rolUsuario;
            CodVendedor = codigoVendedor;
            NombreUsuario = nombreUsuario;
            Sucursal = sucursal;
            log = new Clases.Logs(NombreUsuario, this.AccessibleDescription, IDLog);
        }

        private void Cobranza1_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            _Encabezado.Columns.Add("Factura");
            _Encabezado.Columns.Add("Cliente");
            _Encabezado.Columns.Add("Nombre del cliente");
            _Encabezado.Columns.Add("Sucursal");
            _Encabezado.Columns.Add("Fecha factura");
            _Encabezado.Columns.Add("Fecha de vencimiento");
            _Encabezado.Columns.Add("Costo Base");
            _Encabezado.Columns.Add("Total Factura");
            _Encabezado.Columns.Add("Pagos aplicados");
            _Encabezado.Columns.Add("NC Dev");
            _Encabezado.Columns.Add("NC PP");
            _Encabezado.Columns.Add("NC PE");
            _Encabezado.Columns.Add("Saldo");
            _Encabezado.Columns.Add("Utilidad original");
            _Encabezado.Columns.Add("Utilidad real");
            _Encabezado.Columns.Add("Autorizado", typeof(bool));
            try
            {
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

                    lblVendedor.Location = new Point(lblSucursal.Location.X, lblSucursal.Location.Y);
                    clbVendedor.Location = new Point(clbSucursal.Location.X, clbSucursal.Location.Y);
                }

                CargarJefesCobranza();
                CargarSucursales();
                CargarVendedores();

                txtCliente.Clear();
                txtFactura.Clear();
                txtCliente.Focus();
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
                TBLVendedores = table.Copy();

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
                TBLVendedores = table.Copy();

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
            JefasCobranza = table.Copy();
            clbCobranza.DataSource = table;
            clbCobranza.DisplayMember = "Nombre";
            clbCobranza.ValueMember = "Codigo";
        }

        public void Consultar()
        {
            btnExportar.Enabled = false;
            button1.Enabled = false;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();

            StringBuilder stbVendedores = new StringBuilder();
            foreach (DataRowView item in clbVendedor.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clbVendedor.ToString().Equals(string.Empty))
                    {
                        stbVendedores.Append(",");
                    }
                    stbVendedores.Append(item["Codigo"].ToString());
                }
            }
            if (clbVendedor.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clbVendedor.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbVendedor.ToString().Equals(string.Empty))
                        {
                            stbVendedores.Append(",");
                        }
                        stbVendedores.Append(item["Codigo"].ToString());
                    }
                }
            }

            StringBuilder stbSucursales = new StringBuilder();
            foreach (DataRowView item in clbSucursal.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clbSucursal.ToString().Equals(string.Empty))
                    {
                        stbSucursales.Append(",");
                    }
                    //stbSucursales.Append(this.GetSucursal(item["Codigo"].ToString()));
                    stbSucursales.Append(item["Codigo"].ToString());
                }
            }
            if (clbSucursal.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clbSucursal.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbSucursal.ToString().Equals(string.Empty))
                        {
                            stbSucursales.Append(",");
                        }
                        //stbSucursales.Append(this.GetSucursal(item["Codigo"].ToString()));
                        stbSucursales.Append(item["Codigo"].ToString());
                    }
                }
            }

            StringBuilder stbCobranza = new StringBuilder();
            foreach (DataRowView item in clbCobranza.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clbCobranza.ToString().Equals(string.Empty))
                    {
                        stbCobranza.Append(",");
                    }
                    stbCobranza.Append(item["Nombre"].ToString());
                }
            }
            if (clbCobranza.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clbCobranza.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbCobranza.ToString().Equals(string.Empty))
                        {
                            stbCobranza.Append(",");
                        }
                        stbCobranza.Append(item["Nombre"].ToString());
                    }
                }
            }

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
                    jefa += r.Field<string>("Nombre") + ",";
                }
                Cobranza = "," + jefa;
            }
            Vendedores = stbVendedores.ToString();
            Cliente = txtCliente.Text;
            Factura = txtFactura.Text;

            SqlCommand command = new SqlCommand("PJ_SaldosPendientes", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 1);
            command.Parameters.AddWithValue("@Vendedores", Vendedores);
            command.Parameters.AddWithValue("@JefaCobranza", Cobranza);
            command.Parameters.AddWithValue("@Sucursal", Sucursales);
            command.Parameters.AddWithValue("@Usuario", NombreUsuario);
            command.Parameters.AddWithValue("@Cliente", Cliente);
            command.Parameters.AddWithValue("@Factura", Factura);

            command.CommandTimeout = 0;
            DataTable _table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(_table);
            dataGridView1.DataSource = _table;

            if (_table.Rows.Count > 0)
            {
                btnExportar.Enabled = true;
                button1.Enabled = true;
            }
        }

        public void Formato(DataGridView dgv)
        {
            DataGridViewButtonColumn buttonComent = new DataGridViewButtonColumn();
            {
                buttonComent.Name = "btnPagos";
                buttonComent.HeaderText = "";
                buttonComent.Width = 30;
                buttonComent.UseColumnTextForButtonValue = true;
                buttonComent.FlatStyle = FlatStyle.Popup;
                //buttonComent.DisplayIndex = (int)ColumnasGrid.BtnComentarios;
            }
            dgv.Columns.Add(buttonComent);

            DataGridViewButtonColumn btnDetalle = new DataGridViewButtonColumn();
            {
                btnDetalle.Name = "Detalle";
                btnDetalle.HeaderText = "Detalle";
                btnDetalle.Text = "Detalle";
                btnDetalle.Width = 80;
                btnDetalle.UseColumnTextForButtonValue = true;
                btnDetalle.FlatStyle = FlatStyle.Popup;
                //buttonComent.DisplayIndex = (int)ColumnasGrid.BtnComentarios;
            }
            dgv.Columns.Add(btnDetalle);

            dgv.Columns[(int)Columnas.Factura].DisplayIndex = 0;
            dgv.Columns[(int)Columnas.Cliente].DisplayIndex = 1;
            dgv.Columns[(int)Columnas.NombreCliente].DisplayIndex = 2;
            dgv.Columns[(int)Columnas.Sucursal].DisplayIndex = 3;
            dgv.Columns[(int)Columnas.Fecha].DisplayIndex = 4;
            dgv.Columns[(int)Columnas.Vencimiento].DisplayIndex = 5;
            dgv.Columns[(int)Columnas.CostoBase].DisplayIndex = 6;
            dgv.Columns[(int)Columnas.TotalFactura].DisplayIndex = 7;
            dgv.Columns[(int)Columnas.TotalPE].DisplayIndex = 8;
            dgv.Columns[(int)Columnas.PagoAplicado].DisplayIndex = 9;
            dgv.Columns[(int)Columnas.Boton].DisplayIndex = 10;
            dgv.Columns[(int)Columnas.Devolucion].DisplayIndex = 11;
            dgv.Columns[(int)Columnas.ProntoPago].DisplayIndex = 12;
            dgv.Columns[(int)Columnas.NCPPSGUV].DisplayIndex = 13;
            dgv.Columns[(int)Columnas.DiferenciaPP].DisplayIndex = 14;
            dgv.Columns[(int)Columnas.PrecioEspecial].DisplayIndex = 15;
            dgv.Columns[(int)Columnas.NCPESGUV].DisplayIndex = 16;
            dgv.Columns[(int)Columnas.DiferenciaPE].DisplayIndex = 17;
            dgv.Columns[(int)Columnas.Saldo].DisplayIndex = 18;
            dgv.Columns[(int)Columnas.UtilidadOriginal].DisplayIndex = 19;
            dgv.Columns[(int)Columnas.UtilidadReal].DisplayIndex = 20;
            dgv.Columns[(int)Columnas.Autorizado].DisplayIndex = 21;
            dgv.Columns[(int)Columnas.BotonDetalle].DisplayIndex = 22;

             //, , , , , , , , , , , , , , , 
            dgv.Columns[(int)Columnas.Boton].Width = 20;
            dgv.Columns[(int)Columnas.Factura].Width = 90;
            dgv.Columns[(int)Columnas.Cliente].Width = 70;
            dgv.Columns[(int)Columnas.NombreCliente].Width = 200;
            dgv.Columns[(int)Columnas.Sucursal].Width = 70;
            dgv.Columns[(int)Columnas.Fecha].Width = 85;
            dgv.Columns[(int)Columnas.Vencimiento].Width = 85;
            dgv.Columns[(int)Columnas.Saldo].Width = 100;
            dgv.Columns[(int)Columnas.CostoBase].Width = 100;
            dgv.Columns[(int)Columnas.TotalFactura].Width = 100;
            dgv.Columns[(int)Columnas.PagoAplicado].Width = 100;
            dgv.Columns[(int)Columnas.Devolucion].Width = 100;
            dgv.Columns[(int)Columnas.ProntoPago].Width = 100;
            dgv.Columns[(int)Columnas.NCPPSGUV].Width = 100;
            dgv.Columns[(int)Columnas.DiferenciaPP].Width = 100;
            dgv.Columns[(int)Columnas.PrecioEspecial].Width = 100;
            dgv.Columns[(int)Columnas.NCPESGUV].Width = 100;
            dgv.Columns[(int)Columnas.DiferenciaPE].Width = 100;
            dgv.Columns[(int)Columnas.Saldo].Width = 100;
            dgv.Columns[(int)Columnas.UtilidadOriginal].Width = 80;
            dgv.Columns[(int)Columnas.UtilidadReal].Width = 80;
            dgv.Columns[(int)Columnas.Autorizado].Width = 80;

            dgv.Columns[(int)Columnas.Factura].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cliente].ReadOnly = true;
            dgv.Columns[(int)Columnas.NombreCliente].ReadOnly = true;
            dgv.Columns[(int)Columnas.Sucursal].ReadOnly = true;
            dgv.Columns[(int)Columnas.Fecha].ReadOnly = true;
            dgv.Columns[(int)Columnas.Vencimiento].ReadOnly = true;
            dgv.Columns[(int)Columnas.Saldo].ReadOnly = true;
            dgv.Columns[(int)Columnas.CostoBase].ReadOnly = true;
            dgv.Columns[(int)Columnas.TotalFactura].ReadOnly = true;
            dgv.Columns[(int)Columnas.TotalPE].ReadOnly = true;
            dgv.Columns[(int)Columnas.PagoAplicado].ReadOnly = true;
            dgv.Columns[(int)Columnas.Devolucion].ReadOnly = true;
            dgv.Columns[(int)Columnas.ProntoPago].ReadOnly = true;
            dgv.Columns[(int)Columnas.NCPPSGUV].ReadOnly = true;
            dgv.Columns[(int)Columnas.DiferenciaPP].ReadOnly = true;
            dgv.Columns[(int)Columnas.PrecioEspecial].ReadOnly = true;
            dgv.Columns[(int)Columnas.NCPESGUV].ReadOnly = true;
            dgv.Columns[(int)Columnas.DiferenciaPE].ReadOnly = true;
            dgv.Columns[(int)Columnas.Saldo].ReadOnly = true;
            dgv.Columns[(int)Columnas.UtilidadOriginal].ReadOnly = true;
            dgv.Columns[(int)Columnas.UtilidadReal].ReadOnly = true;

            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
            {
                dgv.Columns[(int)Columnas.Autorizado].ReadOnly = false;
            }
            else
            {
                dgv.Columns[(int)Columnas.Autorizado].ReadOnly = true;
            }

            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.CostoBase].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.TotalFactura].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.TotalPE].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.PagoAplicado].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Devolucion].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.ProntoPago].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NCPPSGUV].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.DiferenciaPP].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.PrecioEspecial].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NCPESGUV].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.DiferenciaPE].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.UtilidadOriginal].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)Columnas.UtilidadReal].DefaultCellStyle.Format = "P2";

            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.CostoBase].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.TotalFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.TotalPE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PagoAplicado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Devolucion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.ProntoPago].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NCPPSGUV].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PrecioEspecial].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NCPESGUV].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.UtilidadOriginal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.UtilidadReal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.DiferenciaPE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.DiferenciaPP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            dgv.Columns[(int)Columnas.Sucursal].Visible = false;
            dgv.Columns[(int)Columnas.Fecha].Visible = false;
            dgv.Columns[(int)Columnas.Vencimiento].Visible = false;
            dgv.Columns[(int)Columnas.CostoBase].Visible = false;
            dgv.Columns[(int)Columnas.TotalPE].Visible = false;
            dgv.Columns[(int)Columnas.Sucursal].Visible = false;
            dgv.Columns[(int)Columnas.DiferenciaPE].Visible = false;
            dgv.Columns[(int)Columnas.DiferenciaPP].Visible = false;
        }

        public string GetSucursal(string _cod)
        {
            switch (_cod)
            {
                case "107": return "PUEBLA";
                case "105": return "MONTERREY";
                case "106": return "MONTERREY";
                case "100": return "APIZACO";
                case "102": return "CORDIBA";
                case "108": return "TEPEACA";
                case "103": return "EDOMEX";
                case "104": return "GUADALAJARA";
                case "121": return "SALTILLO";
                default: return "";
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
                Formato(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                ExportarAExcel exp = new ExportarAExcel();
                if (exp.Exportar(dataGridView1, false))
                    MessageBox.Show("El archivo se creo con exíto.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].Name == "btnPagos" && e.RowIndex >= 0)
                    //if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)Columnas.Boton)
                    {
                        string _Factura = dataGridView1.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells["Factura"].Value.ToString();

                        DetallePagos frmDetalle = new DetallePagos(_Factura);
                        frmDetalle.ShowDialog();
                    }

                    if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].Name == "Detalle" && e.RowIndex >= 0)
                    //if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)Columnas.Boton)
                    {
                        int _Factura = Convert.ToInt32(dataGridView1.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells["Factura"].Value);

                        AutorizacionNCRD.DetalleFacts frmDetalle = new AutorizacionNCRD.DetalleFacts(_Factura);
                        frmDetalle.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ActuazaFactura(string _factura, string _valor)
        {
            try
            {
                Esperar();
                using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("PJ_SaldosPendientes", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Vendedores", string.Empty);
                    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@Usuario", _valor);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Factura", _factura);

                    command.ExecuteNonQuery();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Continuar();
            }

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {//  if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].Name == "btnPagos" && e.RowIndex >= 0)
                    if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].Name == "Autorizado" && e.RowIndex >= 0)
                    {
                        string _Factura = dataGridView1.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells["Factura"].Value.ToString();
                        Boolean _Valor = Convert.ToBoolean(dataGridView1.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells["Autorizado"].Value);
                        string _autorizado = "0";
                        if (_Valor)
                            _autorizado = "1";
                        ActuazaFactura(_Factura, _autorizado);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        DataTable JefasxSucursal = new DataTable();
        DataTable VendedorxSucursal = new DataTable();
        private void clbSucursal_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Checked)
                {
                    DataRowView c = clbSucursal.Items[e.Index] as DataRowView;
                    string _memo = getMemo(Convert.ToInt32(c["Codigo"]));
                    JefasxSucursal.Merge((from item in JefasCobranza.AsEnumerable() where item["Codigo"].ToString() == _memo select item).CopyToDataTable());

                    DataRowView v = clbSucursal.Items[e.Index] as DataRowView;
                    string _memoV = getMemo(Convert.ToInt32(c["Codigo"]));
                    VendedorxSucursal.Merge((from item in TBLVendedores.AsEnumerable() where item["Memo"].ToString() == _memo select item).CopyToDataTable());
                }
                else if (e.NewValue == CheckState.Unchecked)
                {
                    DataRowView c = clbSucursal.Items[e.Index] as DataRowView;
                    string _memo = getMemo(Convert.ToInt32(c["Codigo"]));
                    JefasxSucursal = ((from item in JefasxSucursal.AsEnumerable() where item["Codigo"].ToString() != _memo select item).CopyToDataTable());

                    DataRowView v = clbSucursal.Items[e.Index] as DataRowView;
                    string _memoV = getMemo(Convert.ToInt32(c["Codigo"]));
                    VendedorxSucursal = ((from item in TBLVendedores.AsEnumerable() where item["Memo"].ToString() != _memo select item).CopyToDataTable());
                }
                
                DataView vista = new DataView(JefasxSucursal);
                DataTable a = vista.ToTable(true, new string[] { "Codigo", "Nombre" });
                clbCobranza.DataSource = a;
                clbCobranza.DisplayMember = "Nombre";
                clbCobranza.ValueMember = "Codigo";

                DataView vistaV = new DataView(VendedorxSucursal);
                DataTable aV = vistaV.ToTable(true, new string[] { "Codigo", "Nombre" });
                clbVendedor.DataSource = aV;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
            catch (Exception)
            {
                JefasxSucursal.Clear();
                clbCobranza.DataSource = JefasCobranza;
                clbCobranza.DisplayMember = "Nombre";
                clbCobranza.ValueMember = "Codigo";

                VendedorxSucursal.Clear();
                clbVendedor.DataSource = TBLVendedores;
                clbVendedor.DisplayMember = "Nombre";
                clbVendedor.ValueMember = "Codigo";
            }
        }

        public string getMemo(int GroupCode)
        {
            string _memo = "";
            switch (GroupCode)
            {
                case 107: _memo = "01"; break;
                case 105: _memo = "02"; break;
                case 106: _memo = "02"; break;
                case 100: _memo = "03"; break;
                case 102: _memo = "05"; break;
                case 108: _memo = "06"; break;
                case 103: _memo = "16"; break;
                case 104: _memo = "18"; break;
            }

            return _memo;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)Columnas.UtilidadReal].Value) < (decimal)0.15)
                    {
                        item.Cells[(int)Columnas.UtilidadReal].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.UtilidadReal].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.UtilidadReal].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.UtilidadReal].Style.ForeColor = Color.Black;
                    }


                    item.Cells[(int)Columnas.NCPESGUV].Style.BackColor = Color.FromArgb(235,241,222);
                    item.Cells[(int)Columnas.PrecioEspecial].Style.BackColor = Color.FromArgb(235, 241, 222);

                    item.Cells[(int)Columnas.TotalFactura].Style.BackColor = Color.FromArgb(218, 238, 243);
                    item.Cells[(int)Columnas.TotalPE].Style.BackColor = Color.FromArgb(218, 238, 243);

                    item.Cells[(int)Columnas.NCPPSGUV].Style.BackColor = Color.FromArgb(242, 242, 242);
                    item.Cells[(int)Columnas.ProntoPago].Style.BackColor = Color.FromArgb(242, 242, 242);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var query = from item in ((DataTable)dataGridView1.DataSource).AsEnumerable()
                        where item.Field<bool>("Autorizado") == true
                        select item;
            if (query.Count() > 0)
                dataGridView1.DataSource = query.CopyToDataTable();
            else
                dataGridView1.DataSource = _Encabezado;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dataGridView1.Columns[e.ColumnIndex].Name == "btnPagos" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.dataGridView1.Rows[e.RowIndex].Cells["btnPagos"] as DataGridViewButtonCell;
                Icon icoAtomicoDetalle;
                icoAtomicoDetalle = Properties.Resources.liqpay;
                e.Graphics.DrawIcon(icoAtomicoDetalle, e.CellBounds.Left + 5, e.CellBounds.Top + 3);
                this.dataGridView1.Rows[e.RowIndex].Height = icoAtomicoDetalle.Height + 10;
                this.dataGridView1.Columns[e.ColumnIndex].Width = icoAtomicoDetalle.Width + 10;
                e.Handled = true;
            }
        }

        private void Cobranza1_Shown(object sender, EventArgs e)
        {
            log.ID = log.Inicio();
        }

        private void Cobranza1_FormClosing(object sender, FormClosingEventArgs e)
        {
            log.Fin();
        }
    }
}
