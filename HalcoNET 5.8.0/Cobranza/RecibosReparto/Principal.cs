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
    public partial class Principal : Form
    {
        #region PARAMETROS
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);
        public SqlConnection conectionSGUV = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        DSReparto Ds = new DSReparto();
        ComboBox cmb;
        public string Jefa;
        public string Sucursal;
        public string JefaCobranza;
        public int Rol;
        public DataTable TablaRecibo = new DataTable();
        public string Usuario;
        Clases.Logs log;

        private enum Columnas
        {
            Enviar,
            Clave,
            Cliente,
            FechaFactura,
            FechaVto,
            Actividad,
            Factura,
            Facturado,
            Saldo
        }
        private enum ColumnasGeneral
        {
            Clave,
            Cliente,
            Factura,
            FechaFacura,
            FechaVto,
            Facturado,
            Saldo,
        }
        private enum ColumnasConsultar
        {
            LineNum,
            Folio,
            Clave,
            Cliente,
            Actividad,
            Factura,
            FechaFacura,
            FechaVto,
            Facturado,
            Saldo,
            Confirmacion
        }

        #endregion

        public Principal(string _sucursal, string _jefa, int _rol, string _usuario)
        {
            InitializeComponent();

            Sucursal = _sucursal;
            JefaCobranza = _jefa;
            Rol = _rol;
            Usuario = _usuario;
            log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                CargarJefasCobranza(clbCobranza2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region METODOS
        private void CargarJefasCobranza(ComboBox cb)
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
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
                DataTable cob = new DataTable();
                cob.Columns.Add("Codigo");
                cob.Columns.Add("Nombre");

                DataRow row1 = cob.NewRow();
                row1["Nombre"] = JefaCobranza;
                row1["Codigo"] = JefaCobranza;
                cob.Rows.InsertAt(row1, 0);

                cb.DataSource = cob;
                cb.DisplayMember = "Nombre";
                cb.ValueMember = "Codigo";

            }
            else
            {
                cb.DataSource = table;
                cb.DisplayMember = "Nombre";
                cb.ValueMember = "Codigo";
            }
        }

        private void FormatoGrid()
        {
            gridRecibo.Columns[(int)Columnas.Clave].Width = 70;
            gridRecibo.Columns[(int)Columnas.Actividad].Width = 70;
            gridRecibo.Columns[(int)Columnas.Cliente].Width = 250;
            gridRecibo.Columns[(int)Columnas.Factura].Width = 100;
            gridRecibo.Columns[(int)Columnas.FechaFactura].Width = 100;
            gridRecibo.Columns[(int)Columnas.FechaVto].Width = 100;
            gridRecibo.Columns[(int)Columnas.Facturado].Width = 100;
            gridRecibo.Columns[(int)Columnas.Saldo].Width = 100;
            gridRecibo.Columns[(int)Columnas.Enviar].Width = 60;

            gridRecibo.Columns[(int)Columnas.Clave].ReadOnly = true;
            gridRecibo.Columns[(int)Columnas.Cliente].ReadOnly = true;
            gridRecibo.Columns[(int)Columnas.Factura].ReadOnly = true;
            gridRecibo.Columns[(int)Columnas.FechaFactura].ReadOnly = true;
            gridRecibo.Columns[(int)Columnas.FechaVto].ReadOnly = true;
            gridRecibo.Columns[(int)Columnas.Facturado].ReadOnly = true;
            gridRecibo.Columns[(int)Columnas.Saldo].ReadOnly = true;

            gridRecibo.Columns[(int)Columnas.Clave].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridRecibo.Columns[(int)Columnas.Cliente].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; 
            gridRecibo.Columns[(int)Columnas.Factura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridRecibo.Columns[(int)Columnas.FechaFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridRecibo.Columns[(int)Columnas.FechaVto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridRecibo.Columns[(int)Columnas.Facturado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridRecibo.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            gridRecibo.Columns[(int)Columnas.Facturado].DefaultCellStyle.Format = "C2";
            gridRecibo.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
        }
      
        private void FormatoCGridConsulta()
        {
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.LineNum].Visible = false;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Folio].Visible = false;

            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Clave].Width = 70;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Cliente].Width = 250;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Actividad].Width = 100;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Factura].Width = 100;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.FechaFacura].Width = 100;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.FechaVto].Width = 100;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Facturado].Width = 100;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Saldo].Width = 100;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Confirmacion].Width = 90;

            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Clave].ReadOnly = true;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Cliente].ReadOnly = true;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Actividad].ReadOnly = true;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Factura].ReadOnly = true;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.FechaFacura].ReadOnly = true;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.FechaVto].ReadOnly = true;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Facturado].ReadOnly = true;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Saldo].ReadOnly = true;


            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Clave].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Cliente].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Factura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Actividad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.FechaFacura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.FechaVto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Facturado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Facturado].DefaultCellStyle.Format = "C2";
            gridConsultaRecibo.Columns[(int)ColumnasConsultar.Saldo].DefaultCellStyle.Format = "C2";
        }

        private string GetCadenaTipoClientes()
        {
            string aux = "";
            if (chbLunes.Checked)
                aux += "24";
            if (chbMartes.Checked)
                aux += ",25";
            if (chbMiercoles.Checked)
                aux += ",26";
            if (chbJueves.Checked)
                aux += ",27";
            if (chbViernes.Checked)
                aux += ",28";
            if (chbSabado.Checked)
                aux += ",29";
            if (chbEspeciales.Checked)
                aux += ",30";

                return aux;
        }

        private int GetFolio()
        {
            int f = 0;
            using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                SqlCommand command = new SqlCommand("SELECT MAX(Folio) FROM [@TablaRecibo] ", con);
                try
                {
                    con.Open();
                    f = int.Parse(command.ExecuteScalar().ToString());
                    f += 1;
                   // reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return f;

        }

        public bool GuardarRecibo(string _folio, DataTable _table)
        {
            int rows = 0;
            using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                try
                {
                    con.Open();
                    foreach (DataRow item in _table.Rows)
                    {
                        SqlCommand command = new SqlCommand("Insert into [@TablaRecibo] values(@Folio, @Fecha, @LineNum, @Jefa, @Clave, @Cliente, @Actividad, @Factura, @FechaFactura, @FechaVencimiento, @Facturado, @Saldo, NULL, NULL, NULL)", con);
                        command.Parameters.AddWithValue("@Folio", _folio);
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now);
                        command.Parameters.AddWithValue("@LineNum", (rows + 1));
                        command.Parameters.AddWithValue("@Jefa", Jefa);
                        command.Parameters.AddWithValue("@Clave", item.Field<string>("Clave"));
                        command.Parameters.AddWithValue("@Cliente", item.Field<string>("Cliente"));

                        string act = item.Field<string>("Actividad");
                        if (string.IsNullOrEmpty(act))
                        {
                            act = null;
                        }
                        txtFolio.Text = _folio;
                        command.Parameters.AddWithValue("@Actividad", act);
                        command.Parameters.AddWithValue("@Factura", item.Field<string>("Factura"));
                        command.Parameters.AddWithValue("@FechaFactura", item.Field<DateTime>("Fecha de factura"));
                        command.Parameters.AddWithValue("@FechaVencimiento", item.Field<DateTime>("Fecha de vencimiento"));
                        command.Parameters.AddWithValue("@Facturado", item.Field<decimal>("Facturado"));
                        command.Parameters.AddWithValue("@Saldo", item.Field<decimal>("Saldo"));
                        command.Parameters.AddWithValue("@Estatus", false);
                        command.Parameters.AddWithValue("@Actualizacion", dateTimePicker1.MinDate);

                        command.ExecuteNonQuery();
                        rows++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return rows > 0;
        }

        public bool ActualizarRecibo(string _folio)
        {
            int rows = 0;
            using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                try
                {
                    con.Open();
                    foreach (DataRow item in TblRecibo.Rows)
                    {
                        
                        if (item.Field<bool>("Confirmación") || item.Field<bool>("Recibido"))
                        {
                            SqlCommand command = new SqlCommand("Update [@TablaRecibo] SET Estatus =  @Estatus, FechaActualizacion = @Fecha, Recibido = @Recibido WHERE Factura = @Factura AND Folio = @Folio", con);

                            command.Parameters.AddWithValue("@Estatus", item.Field<bool>("Confirmación"));
                            command.Parameters.AddWithValue("@Recibido", item.Field<bool>("Recibido"));

                            command.Parameters.AddWithValue("@Fecha", DateTime.Now);

                            command.Parameters.AddWithValue("@Factura", item.Field<string>("Factura"));
                            command.Parameters.AddWithValue("@Folio", item.Field<string>("Folio"));
                            command.ExecuteNonQuery();
                        }


                        rows++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return rows == TblRecibo.Rows.Count;
           
        }
        #endregion

        #region EVENTOS
        //no vencimiento
        private void clbCobranza2_SelectedValueChanged(object sender, EventArgs e)
        {
            chbVencimiento.Checked = false;
            TablaRecibo.Clear();
            errorP.Clear();
            try
            {
                if (clbCobranza2.Text != "")
                {
                    if (cbCobro.Checked || cbRevision.Checked)
                    {
                        string DiasRevision = "";
                        string DiasCobro = "";
                        txtFolio.Text = "";
                        if (cbRevision.Checked)
                        {
                            if (chbLunes.Checked)
                                DiasRevision += ",RevLunes";
                            if (chbMartes.Checked)
                                DiasRevision += ",RevMartes";
                            if (chbMiercoles.Checked)
                                DiasRevision += ",RevMiercoles";
                            if (chbJueves.Checked)
                                DiasRevision += ",RevJueves";
                            if (chbViernes.Checked)
                                DiasRevision += ",RevViernes";
                            if (chbSabado.Checked)
                                DiasRevision += ",RevSabado";
                            if (chbEspeciales.Checked)
                                DiasRevision += ",RevEspeciales";
                        }

                        if (cbCobro.Checked)
                        {
                            if (chbLunes.Checked)
                                DiasCobro += ",CobLunes";
                            if (chbMartes.Checked)
                                DiasCobro += ",CobMartes";
                            if (chbMiercoles.Checked)
                                DiasCobro += ",CobMiercoles";
                            if (chbJueves.Checked)
                                DiasCobro += ",CobJueves";
                            if (chbViernes.Checked)
                                DiasCobro += ",CobViernes";
                            if (chbSabado.Checked)
                                DiasCobro += ",CobSabado";
                            if (chbEspeciales.Checked)
                                DiasCobro += ",CobEspeciales";
                            if (chbVencimiento.Checked)
                                DiasCobro += ",Vencimiento";
                        }

                        string TipoCliente = "";
                        Ds.Tables["Listado"].Clear();
                        Ds.Tables["Jefa Cobranza"].Clear();
                        Ds.Tables["Vendedor"].Clear();

                        TipoCliente = GetCadenaTipoClientes();

                        Jefa = clbCobranza2.Text;

                        gridRecibo.DataSource = null;
                        gridRecibo.Columns.Clear();
                        if (!string.IsNullOrEmpty(Jefa) && !string.IsNullOrEmpty(TipoCliente))
                        {
                            SqlCommand command = new SqlCommand("PJ_ReciboReparto", conection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", 11);
                            command.Parameters.AddWithValue("@JefaCobranza", Jefa);
                            command.Parameters.AddWithValue("@Cliente", TipoCliente);
                            command.Parameters.AddWithValue("@Vendedor", string.Empty);
                            command.Parameters.AddWithValue("@Revision", DiasRevision);
                            command.Parameters.AddWithValue("@Cobro", DiasCobro);

                            SqlDataAdapter adapter = new SqlDataAdapter();
                            adapter.SelectCommand = command;
                            adapter.SelectCommand.CommandTimeout = 0;
                            adapter.Fill(TablaRecibo);

                            gridRecibo.DataSource = TablaRecibo;
                     
                            FormatoGrid();
                        }
                    }
                    else
                    {
                        errorP.SetError(groupBox2, "Debe seleecionar una activdad.");
                    }
                }
                else
                {
                    errorP.SetError(clbCobranza2, "Debe seleecionar una Jefa de Cobranza");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //clbCobranza1.SelectedIndex = 0;
            gridRecibo.DataSource = null;
            gridRecibo.Columns.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cbRevision.Checked = true;
            txtRecibo.Clear();
            clbCobranza2.SelectedIndex = 0;
            foreach (Control item in groupBox1.Controls)
            {
                if(item is CheckBox)
                {
                    CheckBox ch = item as CheckBox;
                    ch.Checked = false;
                }
            }
            gridRecibo.DataSource = null;
            gridRecibo.Columns.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string Folio = GetFolio().ToString("000000");
            
            DSReparto _ds = new DSReparto();
            _ds.Tables["Listado"].Clear();
            _ds.Tables["Jefa Cobranza"].Clear();
            _ds.Tables["Vendedor"].Clear();

            DataTable dt = (DataTable)gridRecibo.DataSource;

            foreach (DataGridViewRow item in gridRecibo.Rows)
            {
                if (Convert.ToBoolean(item.Cells[(int)Columnas.Enviar].Value))
                {
                    DataRow row = _ds.Tables["Listado"].NewRow();
                    row["Clave"] = Convert.ToString(item.Cells[(int)Columnas.Clave].Value);
                    row["Cliente"] = Convert.ToString(item.Cells[(int)Columnas.Cliente].Value);
                    row["Actividad"] = Convert.ToString(item.Cells[(int)Columnas.Actividad].Value);
                    row["Fecha de factura"] = Convert.ToDateTime(item.Cells[(int)Columnas.FechaFactura].Value);
                    row["Factura"] = Convert.ToString(item.Cells[(int)Columnas.Factura].Value);
                    row["Facturado"] = Convert.ToDecimal(item.Cells[(int)Columnas.Facturado].Value);
                    row["Saldo"] = Convert.ToDecimal(item.Cells[(int)Columnas.Saldo].Value);
                    row["Fecha de vencimiento"] = Convert.ToDateTime(item.Cells[(int)Columnas.FechaVto].Value);
                    row["FechaCreacion"] = DateTime.Now;
                    _ds.Tables["Listado"].Rows.Add(row);
                }
            }

            DataRow rowc = _ds.Tables["Jefa Cobranza"].NewRow();
            rowc["Nombre jefa"] = Jefa;
            _ds.Tables["Jefa Cobranza"].Rows.Add(rowc);

            DataRow rowv = _ds.Tables["Vendedor"].NewRow();
            rowv["Nombre vendedor"] = Folio;
            _ds.Tables["Vendedor"].Rows.Add(rowv);

            if (_ds.Tables["Listado"].Rows.Count > 0)
            {
                if (GuardarRecibo(Folio, _ds.Tables["Listado"]))
                {
                    ReporteCrystal f = new ReporteCrystal(_ds, "\\\\192.168.2.100\\HalcoNET\\Crystal\\ReporteRecibo.rpt", "Listado de cobranza");
                    f.MdiParent = this.MdiParent;
                    f.Show();
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar al menos una factura", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridRecibo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gridRecibo.Columns[gridRecibo.CurrentCell.ColumnIndex].Name.Equals("SeleccionaActividad"))
            {
                cmb = e.Control as ComboBox;
                cmb.SelectedIndexChanged -= new EventHandler(dvgCombo_SelectedIndexChanged);
                cmb.SelectedIndexChanged += new EventHandler(dvgCombo_SelectedIndexChanged);
            }
        }

        private void dvgCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmb = (ComboBox)sender;
                if (cmb.SelectedItem != null)
                {
                    gridRecibo.Rows[gridRecibo.CurrentRow.Index].Cells["Actividad"].Value = cmb.SelectedItem;
                    cmb.SelectedIndexChanged -= new EventHandler(dvgCombo_SelectedIndexChanged);
                }
            }
            catch (StackOverflowException) { }
        }

        private void gridRecibo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (cmb != null)
                cmb.SelectedIndexChanged -= new EventHandler(dvgCombo_SelectedIndexChanged);
        }

        DataTable TblRecibo = new DataTable();
        String FolioActualizar = "";
        private void button7_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRecibo.Text))
            {
                TblRecibo.Clear();
                
                FolioActualizar = txtRecibo.Text;
                try
                {
                    if (FolioActualizar.Length < 6)
                    {
                        FolioActualizar = int.Parse(txtRecibo.Text).ToString("000000");
                        txtRecibo.Text = FolioActualizar;
                    }
                }
                catch (Exception)
                {
                }

                SqlCommand command = new SqlCommand("PJ_ReciboReparto", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 7);
                command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                command.Parameters.AddWithValue("@Cliente", FolioActualizar);
                command.Parameters.AddWithValue("@Vendedor", 0);
                command.Parameters.AddWithValue("@Revision", string.Empty);
                command.Parameters.AddWithValue("@Cobro", string.Empty);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(TblRecibo);


                gridConsultaRecibo.DataSource = null;
                gridConsultaRecibo.Columns.Clear();

                gridConsultaRecibo.DataSource = TblRecibo;
                FormatoCGridConsulta();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (ActualizarRecibo(FolioActualizar))
                MessageBox.Show("La información se guardo con éxito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_ReciboReparto", conection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 8);
                command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                command.Parameters.AddWithValue("@Cliente", FolioActualizar);
                command.Parameters.AddWithValue("@Vendedor", 0);
                command.Parameters.AddWithValue("@Revision", string.Empty);
                command.Parameters.AddWithValue("@Cobro", string.Empty);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DSReparto _dsImprimir = new DSReparto();

                string _jefa = "";
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow item in table.Rows)
                    {
                        DataRow row = _dsImprimir.Tables["Listado"].NewRow();
                        row["Clave"] = item.Field<string>("Clave");
                        row["Cliente"] = item.Field<string>("Cliente");
                        row["Actividad"] = item.Field<string>("Actividad");
                        row["Fecha de factura"] = item.Field<DateTime>("FechaFactura");
                        row["Factura"] = item.Field<string>("Factura");
                        row["Facturado"] = item.Field<decimal>("Facturado");
                        row["Saldo"] = item.Field<decimal>("Saldo");
                        row["Fecha de vencimiento"] = item.Field<DateTime>("FechaVencimiento");
                        row["FechaCreacion"] = item.Field<DateTime>("Docdate");
                        _jefa = item.Field<string>("JefaCobranza");
                        _dsImprimir.Tables["Listado"].Rows.Add(row);
                    }

                    DataRow rowc = _dsImprimir.Tables["Jefa Cobranza"].NewRow();
                    rowc["Nombre jefa"] = _jefa;
                    _dsImprimir.Tables["Jefa Cobranza"].Rows.Add(rowc);

                    DataRow rowv = _dsImprimir.Tables["Vendedor"].NewRow();
                    rowv["Nombre vendedor"] = FolioActualizar;
                    _dsImprimir.Tables["Vendedor"].Rows.Add(rowv);

                    ReporteCrystal f = new ReporteCrystal(_dsImprimir, @"\\192.168.2.100\HalcoNET\Crystal\ReporteRecibo.rpt", "Listado de cobranza");
                    f.MdiParent = this.MdiParent;
                    f.WindowState = FormWindowState.Maximized;
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " +ex.Message);
            }
        }

        private void txtRecibo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button7_Click(sender, e);
            }
        }

        #endregion

        private void gridRecibo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
           
        }

        private void chbVencimiento_Click(object sender, EventArgs e)
        {
            if (chbVencimiento.Checked)
            {
                errorP.Clear();
                  
                cbCobro.Checked = true;

                chbLunes.Checked = false;
                chbMartes.Checked = false;
                chbMiercoles.Checked = false;
                chbJueves.Checked = false;
                chbMiercoles.Checked = false;
                chbJueves.Checked = false;
                chbViernes.Checked = false;
                chbSabado.Checked = false;
                chbEspeciales.Checked = false;

                errorP.Clear();
                try
                {
                    if (clbCobranza2.Text != "" )
                    {
                        if (cbCobro.Checked || cbRevision.Checked)
                        {
                            string DiasRevision = "";
                            string DiasCobro = "";
                            txtFolio.Text = "";
                            if (cbRevision.Checked)
                            {
                                if (chbLunes.Checked)
                                    DiasRevision += ",RevLunes";
                                if (chbMartes.Checked)
                                    DiasRevision += ",RevMartes";
                                if (chbMiercoles.Checked)
                                    DiasRevision += ",RevMiercoles";
                                if (chbJueves.Checked)
                                    DiasRevision += ",RevJueves";
                                if (chbViernes.Checked)
                                    DiasRevision += ",RevViernes";
                                if (chbSabado.Checked)
                                    DiasRevision += ",RevSabado";
                                if (chbEspeciales.Checked)
                                    DiasRevision += ",RevEspeciales";
                            }

                            if (cbCobro.Checked)
                            {
                                if (chbLunes.Checked)
                                    DiasCobro += ",CobLunes";
                                if (chbMartes.Checked)
                                    DiasCobro += ",CobMartes";
                                if (chbMiercoles.Checked)
                                    DiasCobro += ",CobMiercoles";
                                if (chbJueves.Checked)
                                    DiasCobro += ",CobJueves";
                                if (chbViernes.Checked)
                                    DiasCobro += ",CobViernes";
                                if (chbSabado.Checked)
                                    DiasCobro += ",CobSabado";
                                if (chbEspeciales.Checked)
                                    DiasCobro += ",CobEspeciales";
                                if (chbVencimiento.Checked)
                                    DiasCobro += ",Vencimiento";
                            }

                            string TipoCliente = "";
                            Ds.Tables["Listado"].Clear();
                            Ds.Tables["Jefa Cobranza"].Clear();
                            Ds.Tables["Vendedor"].Clear();

                            TipoCliente = GetCadenaTipoClientes();

                            Jefa = clbCobranza2.Text;

                            gridRecibo.DataSource = null;
                            gridRecibo.Columns.Clear();
                            if (!string.IsNullOrEmpty(Jefa))
                            {
                                SqlCommand command = new SqlCommand("PJ_ReciboReparto", conection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@TipoConsulta", 9);
                                command.Parameters.AddWithValue("@JefaCobranza", Jefa);
                                command.Parameters.AddWithValue("@Cliente", TipoCliente);
                                command.Parameters.AddWithValue("@Vendedor", string.Empty);
                                command.Parameters.AddWithValue("@Revision", DiasRevision);
                                command.Parameters.AddWithValue("@Cobro", DiasCobro);

                                DataTable table = new DataTable();
                                SqlDataAdapter adapter = new SqlDataAdapter();
                                adapter.SelectCommand = command;
                                adapter.SelectCommand.CommandTimeout = 0;
                                adapter.Fill(TablaRecibo);

                                gridRecibo.DataSource = TablaRecibo;

                                FormatoGrid();
             
                            }
                        }
                        else
                        {
                            errorP.SetError(groupBox2, "Debe seleecionar una activdad.");
                        }
                    }
                    else
                    {
                        errorP.SetError(clbCobranza2, "Debe seleecionar una Jefa de Cobranza");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (chbVencimiento.Checked)
            {
                chbVencimiento_Click(sender, e);
            }
            else
            {
                clbCobranza2_SelectedValueChanged(sender, e);
            }
        }

        private void Principal_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void gridRecibo_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void gridRecibo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F5)
            {
                RecibosReparto.Facturas fact = new RecibosReparto.Facturas(TablaRecibo);
                DialogResult res = fact.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    string factura = fact.Factura;

                    if (!string.IsNullOrEmpty(factura))
                    {
                        foreach (DataRow item in TablaRecibo.Rows)
                        {
                            if (item.Field<string>("Factura").Equals(factura))
                            {
                                item.SetField("A recibo", true);
                            }
                        }
                    }
                }
            }
        }
    }
}
