using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading;
using System.IO;

namespace Cobranza
{
    public partial class Form1 : Form
    {
        public string Memo = "";
        public string Usuario;
        public int Rol;
        Clases.Logs log;
        public enum ColumnasGrid
        {
            Enviar,
            DocStatus,
            TicpoDoc,
            DocNum,
            Fecha,
            Cliente,
            Nombre,
            Monto,
            DocEntry,
            EDocnum,
            DocType, 
            Correo,
            Clase
        }

        //public DataTable Datos = new DataTable();

        public Form1(string _usuario, int _rol)
        {
            InitializeComponent();
            Usuario = _usuario;
            Rol = _rol;
        }

        public DataTable CrearTabla(DataTable _t)
        {
            _t.Columns.Add("Enviar", typeof(bool));
            _t.Columns.Add("DocStatus", typeof(string));
            _t.Columns.Add("Tipo de documento", typeof(string));
            _t.Columns.Add("DocNum", typeof(int));
            _t.Columns.Add("Fecha", typeof(DateTime));
            _t.Columns.Add("Cliente", typeof(string));
            _t.Columns.Add("Nombre del cliente", typeof(string));
            _t.Columns.Add("Monto", typeof(decimal));
            _t.Columns.Add("DocEntry", typeof(string));
            _t.Columns.Add("EDocNum", typeof(string));
            _t.Columns.Add("DocType", typeof(string));
            return _t;
        }

        public DataTable CargarDocumentos(DataTable _t, string _cliente, string _tipoDocto, DateTime _inical, DateTime _final, string _jefaCobranza, string _sucursal, string _factura)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
                SqlCommand command = new SqlCommand("PJ_Facturacion", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 1);
                command.Parameters.AddWithValue("@FechaInicial", _inical);
                command.Parameters.AddWithValue("@FechaFinal", _final);
                command.Parameters.AddWithValue("@Cliente", _cliente);
                command.Parameters.AddWithValue("@TipoDocumento", _tipoDocto);
                command.Parameters.AddWithValue("@JefaCobranza", _jefaCobranza);
                command.Parameters.AddWithValue("@Sucursal", _sucursal);
                command.Parameters.AddWithValue("@Factura", _factura);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;

                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(_t);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            return _t;
        }

        public void EnviarDocumentos(DataTable _t, string _crystal, string _tipoDoc, string _tipo, string _rutaDoc, string _mailJefa)
        {
            var _facturas = (from item in _t.AsEnumerable()
                             where item.Field<string>("Tipo de documento") == _tipoDoc
                                 && item.Field<string>("DocType") == _tipo
                                 && item.Field<bool>("Enviar") == true
                             select item);
            if (_facturas.Count() > 0)
            {
                string _numfactura = "";
                string _mailCliente = "";
                try
                {
                    DataTable _facturasI = _facturas.CopyToDataTable();

                    ReportDocument docFacturasI;
                    Tables CrTables;
                    TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
                    TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
                    ConnectionInfo crConnectionInfo = new ConnectionInfo();

                    docFacturasI = new ReportDocument();
                    docFacturasI.Load(_crystal);
                    crConnectionInfo.ServerName = "192.168.2.100";
                    crConnectionInfo.DatabaseName = "SBO-DistPJ";
                    crConnectionInfo.UserID = "sa";
                    crConnectionInfo.Password = "SAP-PJ1";
                    CrTables = docFacturasI.Database.Tables;

                    foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                    {
                        crtableLogoninfo = CrTable.LogOnInfo;
                        crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                        CrTable.ApplyLogOnInfo(crtableLogoninfo);
                    }
                    
                    foreach (DataRow item in _facturasI.Rows)
                    {
                        docFacturasI.SetParameterValue("DocKey@", item.Field<string>("DocEntry"));
                        _numfactura = item.Field<Int32>("DocNum").ToString();
                        string _rutaPDF = _rutaDoc + item.Field<string>("EDocNum") + ".pdf";
                        _mailCliente = item.Field<string>("U_Correo");
                        docFacturasI.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, _rutaPDF);
                        string _rutaXML = "";
                        if (item.Field<DateTime>("Fecha").Year != DateTime.Now.Year
                                || (item.Field<DateTime>("Fecha").Month != DateTime.Now.Month && item.Field<DateTime>("Fecha").Year == DateTime.Now.Year))
                        {
                            _rutaXML = "\\\\192.168.2.100\\xml\\" + item.Field<DateTime>("Fecha").Month + "-" + item.Field<DateTime>("Fecha").Year + "\\" + item.Field<string>("EDocNum") + ".xml";
                        }
                        else if (item.Field<DateTime>("Fecha").Month == DateTime.Now.Month && item.Field<DateTime>("Fecha").Year == DateTime.Now.Year)
                        {
                            _rutaXML = "\\\\192.168.2.100\\xml\\" + item.Field<string>("EDocNum") + ".xml";
                        }
                        if (!string.IsNullOrEmpty(_rutaXML))
                        {
                            if (!string.IsNullOrEmpty(_rutaPDF))
                            {
                                SendMail co = new SendMail();
                                int acum = 0;
                                if (co.Enviar(_mailJefa, _mailCliente, _rutaPDF, _rutaXML)) /// retornar bol para saber estatus de envío 
                                {
                                    acum = ActualizarContador(); // actualizar solo si se envio ---- si es false Error al enviar doc "cancelar proceso"
                                    this.RegistraEnviado(_numfactura, "Enviado", Usuario, _tipoDoc, "");
                                    toolMensajes.Text = "Correos enviados el día de hoy: " + acum;
                                    if (acum > 180)
                                    {
                                        toolMensajes.BackColor = Color.Red;
                                        toolMensajes.ForeColor = Color.White;
                                    }
                                    /// guargar indicador de que la factura se envio
                                }
                                else
                                {
                                    // MessageBox.Show("Error: No se envió el correo.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    GetContador();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Error: No se encontro el archivo PDF.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error: No se encontro el archivo XML.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    docFacturasI.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al enviar factura: " + _numfactura + "\r\nE-Mail: " + _mailCliente + "\r\nDescripción del error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    string[] docs = Directory.GetFiles(_rutaDoc, "*.pdf");
                    foreach (string f in docs)
                    {
                        File.Delete(f);
                    }
                }
            }
        }

        public void GetMemo()
        {
            if (Rol != 1)
            {
                SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
                SqlCommand command = new SqlCommand("PJ_Facturacion", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 5);
                command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
                command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                command.Parameters.AddWithValue("@Cliente", string.Empty);
                command.Parameters.AddWithValue("@TipoDocumento", string.Empty);
                command.Parameters.AddWithValue("@JefaCobranza", Usuario);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@Factura", string.Empty);

                try
                {
                    connection.Open();
                    Memo = Convert.ToString(command.ExecuteScalar());
                }
                catch (Exception)
                {
                }
                finally { connection.Close(); }
            }
            else
            {
                Memo = "";
            }
        }

        public void CargarJefas()
        {
            try
            {
                SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
                SqlCommand command = new SqlCommand("PJ_Facturacion", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 2);
                command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
                command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                command.Parameters.AddWithValue("@Cliente", string.Empty);
                command.Parameters.AddWithValue("@TipoDocumento", string.Empty);
                command.Parameters.AddWithValue("@JefaCobranza", Memo);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@Factura", string.Empty);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                DataTable _t = new DataTable();
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(_t);

                DataRow R = _t.NewRow();
                R["U_Cobranza"] = "Todas";
                _t.Rows.InsertAt(R, 0);

                cbCobranza.DataSource = _t;
                cbCobranza.DisplayMember = "U_Cobranza";
                cbCobranza.ValueMember = "U_Cobranza";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetCorreo()
        {
            string _correo = "";
            SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            SqlCommand command = new SqlCommand("PJ_Facturacion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 3);
            command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
            command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
            command.Parameters.AddWithValue("@Cliente", string.Empty);
            command.Parameters.AddWithValue("@TipoDocumento", string.Empty);
            command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
            command.Parameters.AddWithValue("@Sucursal", Memo);
            command.Parameters.AddWithValue("@Factura", string.Empty);

            try
            {
                connection.Open();
                _correo = Convert.ToString(command.ExecuteScalar());
            }
            catch (Exception)
            {
                _correo = "";
            }
            finally { connection.Close(); }

            return _correo;
        }

        public int ActualizarContador()
        {
            int acum = 0;
            SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            SqlCommand command = new SqlCommand("PJ_Facturacion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 4);
            command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
            command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
            command.Parameters.AddWithValue("@Cliente", string.Empty);
            command.Parameters.AddWithValue("@TipoDocumento", string.Empty);
            command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
            command.Parameters.AddWithValue("@Sucursal", Memo);
            command.Parameters.AddWithValue("@Factura", string.Empty);

            try
            {
                connection.Open();
                acum = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception)
            {
                acum = 0;
            }
            finally { connection.Close(); }

            return acum;
        }

        public void  GetContador()
        {
            int acum = 0;
            SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            SqlCommand command = new SqlCommand("PJ_Facturacion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 6);
            command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
            command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
            command.Parameters.AddWithValue("@Cliente", string.Empty);
            command.Parameters.AddWithValue("@TipoDocumento", string.Empty);
            command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
            command.Parameters.AddWithValue("@Sucursal", Memo);
            command.Parameters.AddWithValue("@Factura", string.Empty);

            try
            {
                connection.Open();
                acum = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception)
            {
                acum = 0;
            }
            finally { connection.Close(); }
            if (acum > 180)
            {
                toolMensajes.BackColor = Color.Red;
                toolMensajes.ForeColor = Color.White;
            }
            toolMensajes.Text = "Correos enviados el día de hoy: " +acum;
        }

        public void FormatoGrid()
        {
             gridDocumentos.Columns[(int)ColumnasGrid.DocEntry].Visible  = false;
             gridDocumentos.Columns[(int)ColumnasGrid.DocType].Visible = false;

             gridDocumentos.Columns[(int)ColumnasGrid.Enviar].Width = 40;
             gridDocumentos.Columns[(int)ColumnasGrid.DocStatus].Width = 75;
             gridDocumentos.Columns[(int)ColumnasGrid.TicpoDoc].Width = 75;
             gridDocumentos.Columns[(int)ColumnasGrid.Clase].Width = 75;
             gridDocumentos.Columns[(int)ColumnasGrid.DocNum].Width = 90;
             gridDocumentos.Columns[(int)ColumnasGrid.Fecha].Width = 90;
             gridDocumentos.Columns[(int)ColumnasGrid.Cliente].Width = 70;
             gridDocumentos.Columns[(int)ColumnasGrid.Nombre].Width = 150;
             gridDocumentos.Columns[(int)ColumnasGrid.Monto].Width = 90;
             gridDocumentos.Columns[(int)ColumnasGrid.DocEntry].Width = 100;
             gridDocumentos.Columns[(int)ColumnasGrid.EDocnum].Width = 100;
             gridDocumentos.Columns[(int)ColumnasGrid.DocType].Width = 90;
             gridDocumentos.Columns[(int)ColumnasGrid.Correo].Width = 120;

            gridDocumentos.Columns[(int)ColumnasGrid.Enviar].DisplayIndex = 0;
            gridDocumentos.Columns[(int)ColumnasGrid.DocStatus].DisplayIndex = 1;
            gridDocumentos.Columns[(int)ColumnasGrid.TicpoDoc].DisplayIndex = 2;
            gridDocumentos.Columns[(int)ColumnasGrid.Clase].DisplayIndex = 3;
            gridDocumentos.Columns[(int)ColumnasGrid.DocNum].DisplayIndex = 4;
            gridDocumentos.Columns[(int)ColumnasGrid.EDocnum].DisplayIndex = 5;
            gridDocumentos.Columns[(int)ColumnasGrid.Fecha].DisplayIndex = 6;
            gridDocumentos.Columns[(int)ColumnasGrid.Cliente].DisplayIndex = 7;
            gridDocumentos.Columns[(int)ColumnasGrid.Nombre].DisplayIndex = 8;
            gridDocumentos.Columns[(int)ColumnasGrid.Monto].DisplayIndex = 10;
            gridDocumentos.Columns[(int)ColumnasGrid.Correo].DisplayIndex = 11;

            gridDocumentos.Columns[(int)ColumnasGrid.Enviar].HeaderText = "Enviar";
            gridDocumentos.Columns[(int)ColumnasGrid.DocStatus].HeaderText = "Situación";
            gridDocumentos.Columns[(int)ColumnasGrid.TicpoDoc].HeaderText = "Tipo de documento";
            gridDocumentos.Columns[(int)ColumnasGrid.Clase].HeaderText = "Clase de documento";
            gridDocumentos.Columns[(int)ColumnasGrid.DocNum].HeaderText = "Número de documento";
            gridDocumentos.Columns[(int)ColumnasGrid.EDocnum].HeaderText = "Número de documento electonico";
            gridDocumentos.Columns[(int)ColumnasGrid.Fecha].HeaderText = "Fecha";
            gridDocumentos.Columns[(int)ColumnasGrid.Cliente].HeaderText = "Cliente";
            gridDocumentos.Columns[(int)ColumnasGrid.Nombre].HeaderText = "Nombre del cliente";
            gridDocumentos.Columns[(int)ColumnasGrid.Monto].HeaderText = "Total factura";
            gridDocumentos.Columns[(int)ColumnasGrid.Correo].HeaderText = "Correo";


            gridDocumentos.Columns[(int)ColumnasGrid.DocStatus].ReadOnly = true;
            gridDocumentos.Columns[(int)ColumnasGrid.TicpoDoc].ReadOnly = true;
            gridDocumentos.Columns[(int)ColumnasGrid.DocNum].ReadOnly = true;
            gridDocumentos.Columns[(int)ColumnasGrid.Fecha].ReadOnly = true;
            gridDocumentos.Columns[(int)ColumnasGrid.Cliente].ReadOnly = true;
            gridDocumentos.Columns[(int)ColumnasGrid.Nombre].ReadOnly = true; ;
            gridDocumentos.Columns[(int)ColumnasGrid.Monto].ReadOnly = true;
            gridDocumentos.Columns[(int)ColumnasGrid.DocEntry].ReadOnly = true;
            gridDocumentos.Columns[(int)ColumnasGrid.EDocnum].ReadOnly = true;
            gridDocumentos.Columns[(int)ColumnasGrid.DocType].ReadOnly = true;
            gridDocumentos.Columns[(int)ColumnasGrid.Correo].ReadOnly = true;
            gridDocumentos.Columns[(int)ColumnasGrid.Clase].ReadOnly = true;

            gridDocumentos.Columns[(int)ColumnasGrid.Monto].DefaultCellStyle.Format = "C2";

            gridDocumentos.Columns[(int)ColumnasGrid.DocStatus].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridDocumentos.Columns[(int)ColumnasGrid.TicpoDoc].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridDocumentos.Columns[(int)ColumnasGrid.Nombre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridDocumentos.Columns[(int)ColumnasGrid.Cliente].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridDocumentos.Columns[(int)ColumnasGrid.EDocnum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridDocumentos.Columns[(int)ColumnasGrid.Correo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridDocumentos.Columns[(int)ColumnasGrid.DocEntry].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridDocumentos.Columns[(int)ColumnasGrid.DocNum].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gridDocumentos.Columns[(int)ColumnasGrid.Monto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridDocumentos.Columns[(int)ColumnasGrid.Fecha].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void RegistraEnviado(string _factura, string status, string _usuario, string _documento, string _coment)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Facturacion", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 7);
                        command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
                        command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
                        command.Parameters.AddWithValue("@Cliente", status);
                        command.Parameters.AddWithValue("@TipoDocumento", _documento);
                        command.Parameters.AddWithValue("@JefaCobranza", _coment);
                        command.Parameters.AddWithValue("@Sucursal", _usuario);
                        command.Parameters.AddWithValue("@Factura", _factura);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
                cbTipoDocto.SelectedIndex = 0;
                GetMemo();
                CargarJefas();
                GetContador();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: "+ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btnCorreos.Enabled = false;
            string _cliente = textBox1.Text;
            DateTime _inical = DateTime.Parse(dateTimePicker1.Value.ToShortDateString());
            DateTime _final = DateTime.Parse(dateTimePicker2.Value.ToShortDateString());
            string _tipoDocto = cbTipoDocto.Text;
            string _jefa = cbCobranza.Text;
            string _factura = txtFactura.Text;
            DataTable _documentos = new DataTable();
            CrearTabla(_documentos);
            if (string.IsNullOrEmpty(_factura))
            {
                gridDocumentos.DataSource = CargarDocumentos(_documentos, _cliente, _tipoDocto, _inical, _final, _jefa, Memo, _factura);
                FormatoGrid();
                if (_documentos.Rows.Count > 0)
                {
                    btnCorreos.Enabled = true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(_factura) && _tipoDocto != "Todo")
                {
                    gridDocumentos.DataSource = CargarDocumentos(_documentos, _cliente, _tipoDocto, _inical, _final, _jefa, Memo, _factura);
                    FormatoGrid();
                    if (_documentos.Rows.Count > 0)
                    {
                        btnCorreos.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Debe especificar el tipo de documento.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCorreos_Click(object sender, EventArgs e)
        {
            string _correoSucursal = GetCorreo();
            if (!String.IsNullOrEmpty(_correoSucursal))
            {
                var _elementosEnviar = (from item in ((DataTable)gridDocumentos.DataSource).AsEnumerable()
                                 where item.Field<bool>("Enviar") == true
                                 select item);
                int elementos = _elementosEnviar.Count();
                if (elementos > 0)
                {
                    DialogResult d = MessageBox.Show("¿Desea enviar " + elementos + " correos?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (d == DialogResult.Yes)
                    {
                        toolMensajes.Visible = false;
                        toolStatus.Text = "Enviando.....";
                        EnviarDocumentos((DataTable)gridDocumentos.DataSource, @"\\192.168.2.100\HalcoNET\Crystal\OINV_DocType_I.rpt", "Factura", "I", @"FacturasI\", _correoSucursal);
                        EnviarDocumentos((DataTable)gridDocumentos.DataSource, @"\\192.168.2.100\HalcoNET\Crystal\OINV_DocType_S.rpt", "Factura", "S", @"FacturasS\", _correoSucursal);
                        EnviarDocumentos((DataTable)gridDocumentos.DataSource, @"\\192.168.2.100\HalcoNET\Crystal\ORIN_DocType_I.rpt", "Nota de crédito", "I", @"CreditMemoI\", _correoSucursal);
                        EnviarDocumentos((DataTable)gridDocumentos.DataSource, @"\\192.168.2.100\HalcoNET\Crystal\ORIN_DocType_S.rpt", "Nota de crédito", "S", @"CreditMemoS\", _correoSucursal);
                        toolStatus.Text = "Listo";
                        toolMensajes.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar al menos un documento.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                GetContador();
            }
        }

        private void gridDocumentos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex == 0)
            //    if (e.ColumnIndex == (int)ColumnasGrid.EDocnum)
            //    {
            //        DataTable _t = (from item in Datos.AsEnumerable()
            //                        select item).CopyToDataTable();
            //    }
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
