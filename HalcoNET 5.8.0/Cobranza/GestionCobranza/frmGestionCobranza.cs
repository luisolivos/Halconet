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
    public partial class AntiguiedadSaldos : Form
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
        //private DataTable Datos = new DataTable();

        public int Rol;
        public int CodVendedor;//slpcode
        public string NombreUsuario;
        public DataTable tablaexcel;
        public DataTable Datos = new DataTable();
        public DataTable JefasCobranza = new DataTable();

        public enum Encabezado
        {
            Comments,Cliente, NombreCliente, CondicionPago, Atradius, Sucursal, Saldo, PorVencer, Col1, Col2, Col3, Col4, Col5, Ct, Boton
        }

        public enum Total
        {
            Saldo, Abono, Col1, Col2, Col3, Col4, Col5
        }
        #endregion        

        public AntiguiedadSaldos(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            InitializeComponent();
            Rol = rolUsuario;
            CodVendedor = codigoVendedor;
            NombreUsuario = nombreUsuario;
            Sucursal = sucursal;
            log = new Clases.Logs(NombreUsuario, this.AccessibleDescription, 0);
        }

        private void Cobranza1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                //if (Rol == (int)Constantes.RolesSistemaSGUV.Administrador || Rol == (int)Constantes.RolesSistemaSGUV.GerenteCobranza)
                //{
                //    clbCobranza.Visible = true;
                //    label2.Visible = true;
                //}
                //else if (Rol == (int)Constantes.RolesSistemaSGUV.JefasCobranza)
                //{
                //    clbCobranza.Visible = false;
                //    label2.Visible = false;
                //    lblSucursal.Visible = false;
                //    clbSucursal.Visible = false;
                //}

                CargarVendedores();
                CargarJefesCobranza();
                CargarSucursales();
                CargarFormasPago();

                gridFacturas.DataSource = null;
                gridFacturas.Columns.Clear();

               // clbCondicion_ItemCheck(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error inesperado. \r\n" + ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public string getMemo(string Sucursal)
        {
            string _memo = "";
            switch (Sucursal)
            {
                case "PUEBLA" : _memo = "01"; break;
                case "MONTERREY" : _memo = "02"; break;
                case "MTY": _memo = "02"; break;
                case "APIZACO" : _memo = "03"; break;
                case "CORDOBA": _memo = "05"; break;
                case "TEPEACA": _memo = "06"; break;
                case "EDOMEX": _memo = "16"; break;
                case "GDL": _memo = "18"; break;
                case "GUADALAJARA": _memo = "18"; break;
                case "SALTILLO": _memo = "23"; break;
            }

            return _memo;
        }
        #region METODOS

        private void CargarFormasPago()
        {
            DataTable table = new DataTable();
            using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_CompromisosCobranza", con))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                    command.Parameters.AddWithValue("@Monto", 0);
                    command.Parameters.AddWithValue("@Comentario", string.Empty);
                    command.Parameters.AddWithValue("@Factura", 0);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Comprometido", 0);
                    command.Parameters.AddWithValue("@NumCompromiso", string.Empty);

                    SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);
                    
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);   
                }
            }

            DataRow row = table.NewRow();
            row["Condicion"] = "TODAS";
            table.Rows.InsertAt(row, 0);

            clbCondicion.DataSource = table;
            clbCondicion.DisplayMember = "Condicion";
            clbCondicion.ValueMember = "Condicion";


        }

        private void CargarSucursales()
        {
         
            SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
            command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.CommandTimeout = 0;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODAS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                DataTable _t = new DataTable();
                string suc = "";
                if (Sucursal.ToUpper() == "MTY")
                    suc = "MONTERREY";


                else if (Sucursal.ToUpper() == "GDL")
                    suc = "GUADALAJARA";
                else
                    suc = Sucursal;

                var query = from item in table.AsEnumerable()
                            where item.Field<string>("Codigo").ToUpper() == suc
                            select item;

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                    clbSucursal.DataSource = _t;
                    clbSucursal.DisplayMember = "Nombre";
                    clbSucursal.ValueMember = "Codigo";
                }
                
            }
            else
                //if (Rol == (int)Constantes.RolesSistemaSGUV.Administrador)
            {
                clbSucursal.DataSource = table;
                clbSucursal.DisplayMember = "Nombre";
                clbSucursal.ValueMember = "Codigo";
            }

        }
        private void CargarVendedores()
        {
            //if (Rol == (int)Constantes.RolesSistemaSGUV.Administrador || Rol == (int)Constantes.RolesSistemaSGUV.GerenteCobranza)
            //{
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
            //}
            //else
            //{
            //    SqlCommand command = new SqlCommand("PJ_Cobrnaza", conection);
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.Parameters.AddWithValue("@TipoConsulta", 4);
            //    command.Parameters.AddWithValue("@Vendedores", string.Empty);
            //    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
            //    command.Parameters.AddWithValue("@Cliente", string.Empty);
            //    command.Parameters.AddWithValue("@Sucursal", string.Empty);
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

            //    clbVendedor.DataSource = table;
            //    clbVendedor.DisplayMember = "Nombre";
            //    clbVendedor.ValueMember = "Codigo";
            //}

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

            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                DataTable _t = new DataTable();
                var query = from item in table.AsEnumerable()
                            where item.Field<string>("Codigo").ToUpper() == getMemo(Sucursal)
                            select item;

                if (query.Count() > 0)
                {
                   
                    _t = query.CopyToDataTable();
                    clbCobranza.DataSource = _t;
                    clbCobranza.DisplayMember = "Nombre";
                    clbCobranza.ValueMember = "Codigo";
                    JefasCobranza = _t.Copy();
                    DataRow row = _t.NewRow();
                    row["Nombre"] = "TODAS";
                    row["Codigo"] = "0";
                    _t.Rows.InsertAt(row, 0);
                }

            }
            else
            {
                clbCobranza.DataSource = table;
                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);
                clbCobranza.DisplayMember = "Nombre";
                clbCobranza.ValueMember = "Codigo";
                JefasCobranza = table.Copy();
            }
        }

        public void Consultar()
        {
            btnExportar.Enabled = false;
            gridFacturas.Columns.Clear();
            gridFacturas.DataSource = null;

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
                        stbSucursales.Append(item["Codigo"].ToString());
                    }
                }
            }

            StringBuilder stbSucursalesName = new StringBuilder();
            foreach (DataRowView item in clbSucursal.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    stbSucursalesName.Append(item["Nombre"].ToString());
                    if (!clbSucursal.ToString().Equals(string.Empty))
                    {
                        stbSucursalesName.Append(",");
                    }  
                }
            }
            if (clbSucursal.CheckedItems.Count == 0 || clbSucursal.CheckedItems.Count == clbSucursal.Items.Count)
            {
                stbSucursalesName.Clear();
                stbSucursalesName.Append("Todas");
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
            
            Cobranza = stbCobranza.ToString();
            Vendedores = stbVendedores.ToString();
            Cliente = string.Empty;
            Factura = dateTimePicker1.Value.ToShortDateString();
            textBox1.Text = "Sucursal: " + stbSucursalesName.ToString();

            SqlCommand command = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 16);
            command.Parameters.AddWithValue("@Vendedores", Vendedores);
            command.Parameters.AddWithValue("@JefaCobranza", Cobranza);
            command.Parameters.AddWithValue("@Sucursal", Sucursales);
            command.Parameters.AddWithValue("@Usuario", NombreUsuario);
            command.Parameters.AddWithValue("@Cliente", Cliente);
            command.Parameters.AddWithValue("@Factura", Factura);
            command.CommandTimeout = 0;

            DataTable _t = new DataTable();
            Datos.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(_t);

            DataGridViewButtonColumn botonComment = new DataGridViewButtonColumn();
            {
                botonComment.Name = "Comment";
                botonComment.HeaderText = string.Empty;
               // botonComment.Text = "Comentarios";
                botonComment.Width = 130;
                botonComment.UseColumnTextForButtonValue = true;
            }
            gridFacturas.Columns.Add(botonComment);

            gridFacturas.DataSource = _t;

            tablaexcel = _t;
            Datos = _t;
            if (tablaexcel.Rows.Count != 0)
            {
                btnExportar.Enabled = true;
            }
        }

        public void FormatoGridEncabezado(DataGridView dgv, bool _check)
        {
            if (_check)
            {
                DataGridViewButtonColumn boton = new DataGridViewButtonColumn();
                {
                    boton.Name = "Detalle";
                    boton.HeaderText = "Detalle";

                    boton.Width = 130;
                    boton.UseColumnTextForButtonValue = true;
                }
                dgv.Columns.Add(boton);

            }

            dgv.Columns[(int)Encabezado.Ct].Visible = false;
            dgv.Columns[(int)Encabezado.Cliente].Width = 55;
            dgv.Columns[(int)Encabezado.NombreCliente].Width = 250;
            dgv.Columns[(int)Encabezado.CondicionPago].Width = 70;
            dgv.Columns[(int)Encabezado.Atradius].Width = 65;
            dgv.Columns[(int)Encabezado.Sucursal].Width = 75;
            dgv.Columns[(int)Encabezado.Saldo].Width = 100;
            dgv.Columns[(int)Encabezado.PorVencer].Width = 100;
            dgv.Columns[(int)Encabezado.Col1].Width = 100;
            dgv.Columns[(int)Encabezado.Col2].Width = 100;
            dgv.Columns[(int)Encabezado.Col3].Width = 100;
            dgv.Columns[(int)Encabezado.Col4].Width = 100;
            dgv.Columns[(int)Encabezado.Col5].Width = 100;
            dgv.Columns[(int)Encabezado.Comments].Width = 85;

            dgv.Columns[(int)Encabezado.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.PorVencer].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Col1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Col2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Col3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Col4].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Encabezado.Col5].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Encabezado.Atradius].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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

        public void FormatoGridTotal(DataGridView dgv)
        {
            dgv.Columns[(int)Total.Saldo].Width = 100;
            dgv.Columns[(int)Total.Abono].Width = 100;
            dgv.Columns[(int)Total.Col1].Width = 100;
            dgv.Columns[(int)Total.Col2].Width = 100;
            dgv.Columns[(int)Total.Col3].Width = 100;
            dgv.Columns[(int)Total.Col4].Width = 100;
            dgv.Columns[(int)Total.Col5].Width = 100;

            dgv.Columns[(int)Total.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Abono].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Col1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Col2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Col3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Col4].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Col5].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Total.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Abono].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Col1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Col2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Col3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Col4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Col5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.ReadOnly = true;
            }
        }

        public DataTable Totales(DataTable datos)
        {
            DataTable _t = new DataTable("Totales");
            _t.Columns.Add("Saldo", typeof(decimal));
            _t.Columns.Add("Abono", typeof(decimal));
            _t.Columns.Add("Col1", typeof(decimal));
            _t.Columns.Add("Col2", typeof(decimal));
            _t.Columns.Add("Col3", typeof(decimal));
            _t.Columns.Add("Col4", typeof(decimal));
            _t.Columns.Add("Col5", typeof(decimal));

            DataRow _r = _t.NewRow();
            _r["Saldo"] = datos.Compute("Sum(Saldo)", string.Empty);
            _r["Abono"] = datos.Compute("Sum([Abono Futuro])", string.Empty);
            _r["Col1"] = datos.Compute("Sum([0-30])", string.Empty);
            _r["Col2"] = datos.Compute("Sum([31-60])", string.Empty);
            _r["Col3"] = datos.Compute("Sum([61-90])", string.Empty);
            _r["Col4"] = datos.Compute("Sum([91-120])", string.Empty);
            _r["Col5"] = datos.Compute("Sum([>120])", string.Empty);
            _t.Rows.Add(_r);

            Porcentajes(_t);
            return _t;
        }

        public void Porcentajes(DataTable datos)
        {
            DataTable _t = new DataTable("Totales");
            _t.Columns.Add("Saldo", typeof(decimal));
            _t.Columns.Add("Abono", typeof(decimal));
            _t.Columns.Add("Col1", typeof(decimal));
            _t.Columns.Add("Col2", typeof(decimal));
            _t.Columns.Add("Col3", typeof(decimal));
            _t.Columns.Add("Col4", typeof(decimal));
            _t.Columns.Add("Col5", typeof(decimal));

            foreach (DataRow r in datos.Rows)
            {
                DataRow _r = _t.NewRow();
                _r["Saldo"] = Convert.ToDecimal(r["Saldo"]) / Convert.ToDecimal(r["Saldo"]);
                _r["Abono"] = Convert.ToDecimal(r["Abono"]) /  Convert.ToDecimal(r["Saldo"]);
                _r["Col1"] = Convert.ToDecimal(r["Col1"]) / Convert.ToDecimal(r["Saldo"]);
                _r["Col2"] = Convert.ToDecimal(r["Col2"]) / Convert.ToDecimal(r["Saldo"]);
                _r["Col3"] = Convert.ToDecimal(r["Col3"]) / Convert.ToDecimal(r["Saldo"]);
                _r["Col4"] = Convert.ToDecimal(r["Col4"]) / Convert.ToDecimal(r["Saldo"]);
                _r["Col5"] = Convert.ToDecimal(r["Col5"]) / Convert.ToDecimal(r["Saldo"]);
                _t.Rows.Add(_r);
            }
            dataGridView2.DataSource = _t;
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
                txtCliente.Clear();
                txtCliente.Enabled = false;
                Datos = new DataTable();
                Datos.Clear();
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
                checkBox5.Checked = true;

                clbCondicion.DataSource = null;
                CargarFormasPago();
                textBox1.Clear();
                Esperar();

                Consultar();

                FormatoGridEncabezado(gridFacturas, true);

                if (gridFacturas.Rows.Count > 0)
                {
                    txtCliente.Enabled = true;
                    dataGridView1.DataSource = Totales((DataTable)gridFacturas.DataSource);
                    Datos = (DataTable)gridFacturas.DataSource;
                    FormatoGridTotal(dataGridView1);
                    groupBox2.Enabled = true;
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
                if(exp.Exportar(gridFacturas, false))
                    MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder stbCondiciones = new StringBuilder();
                foreach (DataRowView item in clbCondicion.CheckedItems)
                {
                    if (item["Condicion"].ToString() != "TODAS")
                    {
                        if (!clbCondicion.ToString().Equals(string.Empty))
                        {
                            stbCondiciones.Append(",");
                        }
                        stbCondiciones.Append(item["Condicion"].ToString());
                    }
                }
                if (clbCondicion.CheckedItems.Count == 0)
                {
                    foreach (DataRowView item in clbCondicion.Items)
                    {
                        if (item["Condicion"].ToString() != "TODAS")
                        {
                            if (!clbCondicion.ToString().Equals(string.Empty))
                            {
                                stbCondiciones.Append(",");
                            }
                            stbCondiciones.Append(item["Condicion"].ToString());
                        }
                    }
                }

                string stringCondiciones = stbCondiciones.ToString();
                string[] CONDICIONES = stringCondiciones.Split(','); 



                DataTable auc = new DataTable();
                
                if (checkBox2.Checked)
                {
                    var colum1 = from item in Datos.AsEnumerable()
                                 where item.Field<decimal>("31-60") > 0
                                 select item;
                    ///////////////////////////////
                    auc.Merge(colum1.CopyToDataTable());
                }
                if (checkBox3.Checked)
                {
                    var colum1 = from item in Datos.AsEnumerable()
                                 where item.Field<decimal>("61-90") > 0
                                 select item;
                    ///////////////////////////////
                    auc.Merge(colum1.CopyToDataTable());
                }
                if (checkBox5.Checked)
                {
                    var colum1 = from item in Datos.AsEnumerable()
                                 where item.Field<decimal>("91-120") > 0
                                 select item;
                    ///////////////////////////////
                    auc.Merge(colum1.CopyToDataTable());
                }
                if (checkBox4.Checked)
                {
                    var colum1 = from item in Datos.AsEnumerable()
                                 where item.Field<decimal>(">120") > 0
                                 select item;
                    ///////////////////////////////
                    auc.Merge(colum1.CopyToDataTable());
                }
                //dataGridView1.DataSource =  Totales(((DataTable)gridFacturas.DataSource));

                DataTable aux = new DataTable();
                if (checkBox1.Checked)
                {
                    var colum1 = from item in Datos.AsEnumerable()
                                 where item.Field<decimal>("0-30") > 0
                                 select item;
                    ///////////////////////////////
                    auc.Merge(colum1.CopyToDataTable());
                }
                gridFacturas.DataSource = null;
                gridFacturas.Columns.Clear();

                DataView vista = new DataView(auc);
                DataTable a = vista.ToTable(true, new string[] { "Código de Cliente", "Nombre de cliente", 
                            "Condición de pago", "Sucursal", "Atradius", "Saldo", "Abono Futuro", "0-30", "31-60", "61-90", "91-120", ">120"});

                DataTable FINAL = a.AsEnumerable().Where(s => CONDICIONES.Contains(s.Field<string>("Condición de pago"))).CopyToDataTable();

                if (FINAL.Columns.Count > 0)
                {
                    gridFacturas.Columns.Clear();
                    gridFacturas.DataSource = FINAL;
                    FormatoGridEncabezado(gridFacturas, true);
                    dataGridView1.DataSource = Totales(FINAL);
                    FormatoGridTotal(dataGridView1);
                   
                }
            }
            catch (Exception)
            {
                gridFacturas.DataSource = null;
                gridFacturas.Columns.Clear();
                //gridFacturas.DataSource = Datos;
                //FormatoGridEncabezado(gridFacturas, true);
            }
        }
        
        DataTable JefasxSucursal = new DataTable();
        private void clbSucursal_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Checked)
                {
                    DataRowView c = clbSucursal.Items[e.Index] as DataRowView;
                    string _memo = getMemo(Convert.ToString(c["Codigo"]).ToUpper());
                    JefasxSucursal.Merge((from item in JefasCobranza.AsEnumerable() where item["Codigo"].ToString() == _memo select item).CopyToDataTable());
                }
                else if (e.NewValue == CheckState.Unchecked)
                {
                    DataRowView c = clbSucursal.Items[e.Index] as DataRowView;
                    string _memo = getMemo(Convert.ToString(c["Codigo"]).ToUpper());
                    JefasxSucursal = ((from item in JefasxSucursal.AsEnumerable() where item["Codigo"].ToString() != _memo select item).CopyToDataTable());
                }
                DataView vista = new DataView(JefasxSucursal);
                DataTable a = vista.ToTable(true, new string[] {"Codigo", "Nombre"});
                clbCobranza.DataSource = a;
                clbCobranza.DisplayMember = "Nombre";
                clbCobranza.ValueMember = "Codigo";


            }
            catch (Exception)
            {
                JefasxSucursal.Clear();
                clbCobranza.DataSource = JefasCobranza;
                clbCobranza.DisplayMember = "Nombre";
                clbCobranza.ValueMember = "Codigo";
            }
        }

        private void gridFacturas_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && this.gridFacturas.Columns[e.ColumnIndex].Name == "Detalle" && e.RowIndex >= 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    DataGridViewButtonCell celBoton = this.gridFacturas.Rows[e.RowIndex].Cells["Detalle"] as DataGridViewButtonCell;
                    Icon icoAtomico;

                    icoAtomico = Properties.Resources.boton_detalle;


                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 5, e.CellBounds.Top + 5);

                    //    this.gridFacturas.Rows[e.RowIndex].Height = icoAtomico.Height + 10;
                    this.gridFacturas.Columns[e.ColumnIndex].Width = icoAtomico.Width + 10;

                    e.Handled = true;
                }
                if (Convert.ToInt32(this.gridFacturas.Rows[e.RowIndex].Cells["Ct"].Value) != 0)
                {
                    if (e.ColumnIndex >= 0 && this.gridFacturas.Columns[e.ColumnIndex].Name == "Comment" && e.RowIndex >= 0)
                    {
                        e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                        DataGridViewButtonCell celBoton = this.gridFacturas.Rows[e.RowIndex].Cells["Comment"] as DataGridViewButtonCell;
                        Icon icoAtomico;

                        if (Convert.ToInt32(this.gridFacturas.Rows[e.RowIndex].Cells["Ct"].Value) == 1)
                            icoAtomico = Properties.Resources.green;
                        else
                            icoAtomico = Properties.Resources.yellow;

                        e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 2, e.CellBounds.Top + 2);

                        this.gridFacturas.Rows[e.RowIndex].Height = icoAtomico.Height + 4;
                        this.gridFacturas.Columns[e.ColumnIndex].Width = icoAtomico.Width + 4;

                        e.Handled = true;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void gridFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)Encabezado.Boton)
                    {
                        if (Convert.ToString(gridFacturas.Rows[e.RowIndex].Cells[(int)Encabezado.Cliente].Value) != "")
                        {
                            string _cliente = Convert.ToString(gridFacturas.Rows[gridFacturas.CurrentRow.Index].Cells[(int)Encabezado.Cliente].Value);
                            string _nombre = Convert.ToString(gridFacturas.Rows[gridFacturas.CurrentRow.Index].Cells[(int)Encabezado.NombreCliente].Value);

                            DetalleAntiguedadSaldos de = new DetalleAntiguedadSaldos(_cliente, _nombre, NombreUsuario);
                            de.MdiParent = this.MdiParent; ;
                            de.Show();
                        }
                    }

                    if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)Encabezado.Comments)
                    {
                        if (Convert.ToString(gridFacturas.Rows[e.RowIndex].Cells[(int)Encabezado.Cliente].Value) != "")
                        {
                            string _cliente = Convert.ToString(gridFacturas.Rows[gridFacturas.CurrentRow.Index].Cells[(int)Encabezado.Cliente].Value);
                            string _nombre = Convert.ToString(gridFacturas.Rows[gridFacturas.CurrentRow.Index].Cells[(int)Encabezado.NombreCliente].Value);

                            GestionCobranza.frmComments frm = new GestionCobranza.frmComments(_cliente, _nombre);
                            frm.MdiParent = this.MdiParent; ;
                            frm.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void clbCondicion_Click(object sender, EventArgs e)
        {

            if (clbCondicion.SelectedIndex == 0)
            {
                if (clbCondicion.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbCondicion.Items.Count; item++)
                    {
                        clbCondicion.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbCondicion.Items.Count; item++)
                    {
                        clbCondicion.SetItemChecked(item, true);
                    }
                }
            }

            checkBox1_Click(sender, e);
           
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            //AntiguedadSaldos.SeguimientoCompromisos seg = new AntiguedadSaldos.SeguimientoCompromisos(Sucursal, Rol);
            //seg.MdiParent = this.MdiParent;
            //seg.Show();
        }

        private void clbCondicion_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
            //List<string> condiciones = new List<string>();

            //if (e.NewValue == CheckState.Checked)
            //{
            //    DataRowView c = clbCondicion.Items[e.Index] as DataRowView;
            //    condiciones.Add(c["Condicion"].ToString());
            //}

            //foreach (DataRowView item in clbCondicion.CheckedItems)
            //{
            //    condiciones.Add(item["Condicion"].ToString());
            //}

            //var results = Datos.AsEnumerable().Where(z => condiciones.Contains(z.Field<string>("Condición de pago")));
            //if (results.Count() > 0)
            //{
            //    DataTable t = results.CopyToDataTable();
            //    gridFacturas.DataSource = null;
            //    gridFacturas.Columns.Clear();
            //    gridFacturas.DataSource = t;
            //    FormatoGridEncabezado(gridFacturas, true);

            //    dataGridView1.DataSource  = this.Totales(t);
            //}
            //else
            //{
            //    gridFacturas.DataSource = null;
            //    gridFacturas.Columns.Clear();
            // //   MessageBox.Show("No se encontraron resultados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                gridFacturas.DataSource = null;
                gridFacturas.Columns.Clear();

                DataTable _t = (from item in Datos.AsEnumerable()
                                where item.Field<string>("Código de cliente").ToLower().Contains(txtCliente.Text.ToLower())
                                // && item.Field<string>("Nombre").ToLower().Contains(textBox2.Text.ToLower())
                                select item).CopyToDataTable();
                
                DataGridViewButtonColumn botonComment = new DataGridViewButtonColumn();
                {
                    botonComment.Name = "Comment";
                    botonComment.HeaderText = string.Empty;
                    // botonComment.Text = "Comentarios";
                    botonComment.Width = 130;
                    botonComment.UseColumnTextForButtonValue = true;
                }
                gridFacturas.Columns.Add(botonComment);
                gridFacturas.DataSource = _t;

                if (_t.Columns.Count > 0)
                {
                    this.FormatoGridEncabezado(gridFacturas, true);
                }
            }
            catch (Exception)
            {
                //  MessageBox.Show(ex.Message);
            }
        }

        private void AntiguiedadSaldos_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void AntiguiedadSaldos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
            }
        }

        private void clbCondicion_SelectedValueChanged(object sender, EventArgs e)
        {
            checkBox1_Click(sender, e);
        }
        #endregion
    }
}
