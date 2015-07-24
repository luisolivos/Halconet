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

using System.IO;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Compras.Solicitudes
{
    public partial class frmSolicitudProducto : Form
    {
        Clases.Logs log;

        string Ruta = string.Empty;

        DataSet data1 = new DataSet();

        DataTable tblDetalle = new DataTable();
        private enum Columnas
        {
            Articulo,
            Descripcion,
            Linea,
            Proveedor,
            Especifique,
            Cantidad,
            Pronostico,
            Comprador,
            ItmsGrpCod,
            CardCode
        }

        public frmSolicitudProducto()
        {
            InitializeComponent();

            tblDetalle.Columns.Add("Articulo", typeof(string));
            tblDetalle.Columns.Add("Descripcion", typeof(string));
            tblDetalle.Columns.Add("Linea", typeof(string));
            tblDetalle.Columns.Add("Proverdor", typeof(string));
            tblDetalle.Columns.Add("Especifique Proveedor", typeof(string));
            tblDetalle.Columns.Add("Cantidad", typeof(decimal));
            tblDetalle.Columns.Add("Pronostico", typeof(decimal));
            tblDetalle.Columns.Add("Comprador", typeof(string));

            tblDetalle.Columns.Add("ItmsGrpCod", typeof(int));
            tblDetalle.Columns.Add("CardCode", typeof(string));

        }

        public void Formato(DataGridView dgv, bool descripcion, bool pronostico, bool especifique)
        {
            dgv.Columns[(int)Columnas.Articulo].Width = 100;
            dgv.Columns[(int)Columnas.Descripcion].Width = 250;
            dgv.Columns[(int)Columnas.Linea].Width = 100;
            dgv.Columns[(int)Columnas.Proveedor].Width = 250;
            dgv.Columns[(int)Columnas.Cantidad].Width = 90;
            dgv.Columns[(int)Columnas.Pronostico].Width = 90;
            dgv.Columns[(int)Columnas.Comprador].Width = 90;
            dgv.Columns[(int)Columnas.Comprador].ReadOnly = true;

            dgv.Columns[(int)Columnas.Pronostico].Visible = pronostico;
            dgv.Columns[(int)Columnas.Descripcion].ReadOnly = descripcion;
            dgv.Columns[(int)Columnas.Especifique].Visible = true;

            dgv.Columns[(int)Columnas.ItmsGrpCod].Visible = false;
            dgv.Columns[(int)Columnas.CardCode].Visible = false;
        }
        
        public DataTable Fill(int _tipoConsulta, ComboBox _cb, string _name)
        {
            DataTable tbl = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", connection))
                {
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", _tipoConsulta);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(tbl);

                    DataRow row = tbl.NewRow();
                    row["Codigo"] = 0;
                    if (_name.Equals("Provedores"))
                        row["Nombre"] = "Otro";

                    tbl.Rows.InsertAt(row, 0);
                    tbl.TableName = _name;

                    data1.Tables.Add(tbl);

                    _cb.DataSource = data1.Tables[_name];
                    _cb.DisplayMember = "Nombre";
                    _cb.ValueMember = "Codigo";

                }
            }


            return tbl;
        }

        public bool ValidarArticulo(string item, string tipo)
        {
            try
            {
                if (tipo == "CE" || tipo == "LIN")
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand comm = new SqlCommand("PJ_SolicitudProducto", connection))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.AddWithValue("@TipoConsulta", 9);
                            comm.Parameters.AddWithValue("@TipoSolicitud", tipo);
                            comm.Parameters.AddWithValue("@Articulo", item);

                            SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                            parameter.Direction = ParameterDirection.Output;
                            comm.Parameters.Add(parameter);

                            connection.Open();

                            comm.ExecuteNonQuery();

                            string mensaje = Convert.ToString(comm.Parameters["@Mensaje"].Value);
                            if (mensaje != "")
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ValidaEncabezado()
        {
            errorProvider.Clear();
            if (Convert.ToInt32(cbVendedor.SelectedValue) == 0)
            {
                errorProvider.SetError(cbVendedor, "Seleccione un vendedor");
                return false;
            }
            if (Convert.ToInt32(cbUnidadVenta.SelectedValue) == 0)
            {
                errorProvider.SetError(cbUnidadVenta, "Seleccione una unidad de venta");
                return false;
            }
            if (string.IsNullOrEmpty(txtCliente.Text))
            {
                errorProvider.SetError(txtCliente, "Ingrese un código de cliente");
                return false;
            }
            if (string.IsNullOrEmpty(txtJustificar.Text))
            {
                errorProvider.SetError(label2, "Justifique el motivo de la solicitud");
                return false;
            }

            return true;
        }

        public bool ValidaDetalle()
        {
            string tipo = string.Empty;
            if (rbCES.Checked)
                tipo = "CE";
            if (rbLinea.Checked)
                tipo = "LIN";
            if (rbPPC.Checked)
                tipo = "PPC";

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.ErrorText = string.Empty;

                    if (string.IsNullOrEmpty(row.Cells[(int)Columnas.Articulo].Value.ToString()))
                    {
                        row.ErrorText = "El campo [Articulo] es obligatorio";
                        return false;
                    }
                    else
                    {
                        if (!this.ValidarArticulo(row.Cells[(int)Columnas.Articulo].Value.ToString(), tipo))
                        {
                            row.ErrorText = "El articulo ya existe en la base de datos";
                            return false;
                        }
                    }
                    if (string.IsNullOrEmpty(row.Cells[(int)Columnas.Descripcion].Value.ToString()))
                    {
                        row.ErrorText = "El campo [Descripción] es obligatorio";
                        return false;
                    }
                    if (Convert.ToInt16(row.Cells[(int)Columnas.ItmsGrpCod].Value) <= 0)
                    {
                        row.ErrorText = "El campo [Linea] es obligatorio";
                        return false;
                    }
                    if (string.IsNullOrEmpty(row.Cells[(int)Columnas.CardCode].Value.ToString()) )
                    {
                        row.ErrorText = "El campo [Proveedor] es obligatorio";
                        return false;
                    }
                    else
                    {
                        if (!tipo.Equals("PPC"))
                        {
                            if (row.Cells[(int)Columnas.CardCode].Value.ToString().Equals("0") & string.IsNullOrEmpty(row.Cells[(int)Columnas.Especifique].Value.ToString()))
                            {
                                row.ErrorText = "Debe esecificar el nombre del Proveedor";
                                return false;
                            }
                        }
                    }
                    if (Convert.ToDecimal(row.Cells[(int)Columnas.Cantidad].Value) <= 0)
                    {
                        row.ErrorText = "El campo [Cantidad] es obligatorio";
                        return false;
                    }
                    if (rbLinea.Checked)
                    {
                        if (Convert.ToDecimal(row.Cells[(int)Columnas.Pronostico].Value) <= 0)
                        {
                            row.ErrorText = "El campo [Pronostico] es obligatorio";
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private bool CrearPDF(decimal Folio, string Type)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            string path = System.IO.Path.GetTempPath();

            string _mailVendedor = string.Empty;
            try
            {
                //*_mailVendedor = this.GetMailVendedor();



                PdfWriter.GetInstance(doc, new FileStream(path + "Solicitud_" + Folio.ToString("00000000") + ".pdf", FileMode.OpenOrCreate));

                doc.Open();

                iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 8);
                iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
                string title = "";
                if (Type == "CE")
                    title = "Compra especial";
                if (Type == "LIN")
                    title = "Producto de Linea";
                if (Type == "PPC")
                    title = "PPC";

                doc.Add(new Paragraph("Solicitud de " + title, fontTitle));

                doc.Add(new Phrase("\r\nFolio: ", font8BLOD));
                doc.Add(new Phrase(Folio + "\r\n", font8));

                doc.Add(new Phrase("Fecha de solicitud: ", font8BLOD));
                doc.Add(new Phrase(DateTime.Now.ToShortDateString() + "\r\n", font8));

                doc.Add(new Phrase("Solicitante: ", font8BLOD));
                doc.Add(new Phrase(cbVendedor.Text + "\r\n", font8));

                doc.Add(new Phrase("Unidad de venta: ", font8BLOD));
                doc.Add(new Phrase(cbUnidadVenta.SelectedValue + "-" + cbUnidadVenta.Text + "\r\n", font8));

                doc.Add(new Phrase("Fecha de compromiso de venta: ", font8BLOD));
                doc.Add(new Phrase(dtCompromiso.Value.ToShortDateString() + "\r\n", font8));

                doc.Add(new Phrase("Cliente: ", font8BLOD));
                doc.Add(new Phrase(txtCliente.Text + "\r\n", font8));

                doc.Add(new Phrase("Nombre del cliente: ", font8BLOD));
                doc.Add(new Phrase(txtNombreCliente.Text + "\r\n", font8));

                doc.Add(new Phrase("Justificación: ", font8BLOD));
                doc.Add(new Phrase(txtJustificar.Text, font8));

                doc.Add(new Paragraph("\r\nArtículos solicitados\r\n"));


                PdfPTable PdfTable = new PdfPTable(7);
                PdfPCell PdfPCell = null;
                PdfTable.HorizontalAlignment = 0;
                PdfTable.SpacingBefore = 5;
                PdfTable.SpacingAfter = 5;
                PdfTable.DefaultCell.Border = 0;
                PdfTable.WidthPercentage = 100;

                float[] Columnas = new float[] { 10, 25, 10, 25, 10, 10, 10 };
                PdfTable.SetWidths(Columnas);
                //Add Header of the pdf table
                PdfPCell = new PdfPCell(new Phrase(new Chunk("Artículo", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Descripción", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Línea", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Proveedor", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Cantidad", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Pronostico", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Comprador", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                foreach (DataRow item in tblDetalle.Rows)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Articulo"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Descripcion"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Linea"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Proverdor") + " " + item.Field<string>("Especifique Proveedor"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Cantidad").ToString(), font8)));
                    PdfTable.AddCell(PdfPCell);

                    if (Type == "LIN")
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Pronostico").ToString(), font8)));
                        PdfTable.AddCell(PdfPCell);
                    }
                    else
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(string.Empty, font8)));
                        PdfTable.AddCell(PdfPCell);
                    }

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Comprador"), font8)));
                    PdfTable.AddCell(PdfPCell);
                }

                PdfTable.SpacingBefore = 15f;
                doc.Add(PdfTable);

                doc.Close();

                Ruta = path + "Solicitud_" + Folio.ToString("00000000") + ".pdf";
                //System.Diagnostics.Process.Start(path + "Solicitud_" + Folio.ToString("00000000") + ".pdf");

                return true;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                doc.Close();
            }
        }

        private string GetMailVendedor()
        {
            string mail = "";
            using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                {
                    Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 11);
                    command.Parameters.AddWithValue("@Cantidad", cbVendedor.SelectedValue);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        mail = reader.GetString(0);
                    }

                }
            }
            return mail;
        }

        private string GetMailCompradores(string tipo)
        {
            List<string> mail = new List<string>();

            var qry = (from item in tblDetalle.AsEnumerable()
                       select item.Field<int>("ItmsGrpCod")).Distinct();

            foreach (var item in qry)
            {
                using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                    {
                        Connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 8);
                        command.Parameters.AddWithValue("@TipoSolicitud", tipo);
                        command.Parameters.AddWithValue("@Linea", item);

                        SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                        parameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parameter);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            mail.Add(reader.GetString(0));
                        }

                    }
                }

            }
            string _correos = string.Empty ;
            List<string> mails = mail.Distinct().ToList();
            foreach (string item in mails)
            {
                _correos += string.Concat(item, ";");
            }

            return _correos;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal Folio;
                string tipo = string.Empty;
                if (rbCES.Checked)
                    tipo = "CE";
                if (rbLinea.Checked)
                    tipo = "LIN";
                if (rbPPC.Checked)
                    tipo = "PPC";
                toolStripStatus.Text = string.Empty;
                toolStripStatus.BackColor = Color.FromName("Control");
                toolStripStatus.ForeColor = Color.Black;

                if (this.ValidaEncabezado())
                {
                    if (this.ValidaDetalle())
                    {
                        if (tblDetalle.Rows.Count > 0)
                        {
                            string _mailVendeor = this.GetMailVendedor();
                            if (!string.IsNullOrEmpty(_mailVendeor))
                            {
                                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                                {
                                    using (SqlCommand comm = new SqlCommand("PJ_SolicitudProducto", connection))
                                    {
                                        comm.CommandType = CommandType.StoredProcedure;
                                        comm.Parameters.AddWithValue("@TipoConsulta", 13);
                                        comm.Parameters.AddWithValue("@Folio", string.Empty);
                                        comm.Parameters.AddWithValue("@Solicitante", cbVendedor.SelectedValue);
                                        comm.Parameters.AddWithValue("@Sucursal", cbUnidadVenta.SelectedValue);
                                        comm.Parameters.AddWithValue("@TipoSolicitud", tipo);
                                        comm.Parameters.AddWithValue("@FechaCompromiso", dtCompromiso.Value);
                                        comm.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                                        comm.Parameters.AddWithValue("@Justificacion", txtJustificar.Text);
                                        comm.Parameters.AddWithValue("@Vendedor", cbVendedor.Text);
                                        comm.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);

                                        connection.Open();

                                        Folio = Convert.ToDecimal(comm.ExecuteScalar());

                                        if (Folio > 0)
                                        {
                                            txtFolio.Text = Folio.ToString("00000000");
                                            int line = 0;
                                            foreach (DataRow item in tblDetalle.Rows)
                                            {
                                                using (SqlConnection connectionDetalle = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                                                {
                                                    using (SqlCommand commDetalle = new SqlCommand("PJ_SolicitudProducto", connection))
                                                    {
                                                        commDetalle.CommandType = CommandType.StoredProcedure;
                                                        commDetalle.Parameters.AddWithValue("@TipoConsulta", 14);
                                                        commDetalle.Parameters.AddWithValue("@Folio1", Folio);
                                                        commDetalle.Parameters.AddWithValue("@Line", line);
                                                        commDetalle.Parameters.AddWithValue("@Articulo", item.Field<string>("Articulo"));
                                                        commDetalle.Parameters.AddWithValue("@Descripcion", item.Field<string>("Descripcion"));
                                                        commDetalle.Parameters.AddWithValue("@Proveedor", item.Field<string>("CardCode"));
                                                        commDetalle.Parameters.AddWithValue("@Otro", item.Field<string>("Especifique Proveedor"));
                                                        commDetalle.Parameters.AddWithValue("@Linea", item.Field<int>("ItmsGrpCod"));
                                                        commDetalle.Parameters.AddWithValue("@Comprador", item.Field<string>("Comprador"));
                                                        commDetalle.Parameters.AddWithValue("@Cantidad", item.Field<decimal>("Cantidad"));
                                                        commDetalle.Parameters.AddWithValue("@Pronostico", item.Field<decimal>("Pronostico"));

                                                        connectionDetalle.Open();
                                                        commDetalle.ExecuteNonQuery();
                                                    }
                                                }
                                                line++;
                                            }

                                            if (this.CrearPDF(Folio, tipo))
                                            {
                                                // public bool Enviar(string _file, string _mailDestinatario, string _mailVendedor, string _vendedor, bool _solicitud)
                                                Cobranza.SendMail mail = new Cobranza.SendMail();
                                                string _mailComprador = this.GetMailCompradores(tipo);
                                                mail.Enviar(Ruta, _mailComprador, _mailVendeor, cbVendedor.Text, true);
                                            }

                                        }
                                        else
                                        {
                                            toolStripStatus.Text = "No se ha podido enviar la solicitud.";
                                            toolStripStatus.BackColor = Color.Red;
                                            toolStripStatus.ForeColor = Color.White;
                                        }
                                    }

                                    toolStripStatus.Text = "Su solicitud ha sido enviada.";
                                    toolStripStatus.BackColor = Color.Green;
                                    toolStripStatus.ForeColor = Color.Black;
                                    btnGuardar.Enabled = false;
                                }

                            }
                            else
                            {
                                toolStripStatus.Text = "El vendedor no tiene asignada una cuenta de correo";
                                toolStripStatus.BackColor = Color.Red;
                                toolStripStatus.ForeColor = Color.White;
                            }
                        }
                        else
                        {
                            toolStripStatus.Text = "No ha ingresado artículos.";
                            toolStripStatus.BackColor = Color.Red;
                            toolStripStatus.ForeColor = Color.White;
                        }
                    }
                    else
                    {
                        toolStripStatus.Text = "No se ha podido enviar la solicitud.";
                        toolStripStatus.BackColor = Color.Red;
                        toolStripStatus.ForeColor = Color.White;
                    }
                }
                else
                {
                    toolStripStatus.Text = "No se ha podido enviar la solicitud.";
                    toolStripStatus.BackColor = Color.Red;
                    toolStripStatus.ForeColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                toolStripStatus.Text = ex.Message;
                toolStripStatus.BackColor = Color.Red;
                toolStripStatus.ForeColor = Color.White;
            }
        }

        public static AutoCompleteStringCollection Autocomplete(DataTable _t, string _column)
        {
            DataTable dt = _t;

            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

            foreach (DataRow row in dt.Rows)
            {
                coleccion.Add(Convert.ToString(row[_column]));
            }

            return coleccion;
        }

        private void frmSolicitudProducto_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                this.Fill(1, cbVendedor, "Vendedores");
                this.Fill(4, cbUnidadVenta, "Sucursales");
                this.Fill(2, new ComboBox(), "Clientes");
                this.Fill(6, new ComboBox(), "Articulos");
                this.Fill(3, new ComboBox(), "Lineas");
                this.Fill(5, new ComboBox(), "Provedores");

                txtCliente.AutoCompleteCustomSource = Autocomplete(data1.Tables["Clientes"], "Codigo");
                txtCliente.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;

                if (ClasesSGUV.Login.Vendedor1 > 0)
                {
                    cbVendedor.SelectedValue = ClasesSGUV.Login.Vendedor1;
                    cbVendedor_SelectionChangeCommitted(sender, e);

                    cbVendedor.Enabled = false;
                    cbUnidadVenta.Enabled = false;
                }

                dgvItems.DataSource = tblDetalle;

                this.Formato(dgvItems, true, false, false);

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            }
            catch (Exception)
            {

            }
        }

        private void cbVendedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int slpcode = Convert.ToInt32(cbVendedor.SelectedValue);

                string memo = (from item in data1.Tables["Vendedores"].AsEnumerable()
                               where item.Field<Int32>("Codigo") == slpcode
                               select item.Field<string>("Memo")).FirstOrDefault();

                var _sucursal = from item in data1.Tables["Sucursales"].AsEnumerable()
                                where item.Field<string>("Codigo") == memo
                                select item;

                cbUnidadVenta.DataSource = _sucursal.CopyToDataTable();
                cbUnidadVenta.DisplayMember = "Nombre";
                cbUnidadVenta.ValueMember = "Codigo";
            }
            catch (Exception)
            {
            }
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                errorProvider.Clear();
                if (!string.IsNullOrEmpty(txtCliente.Text))
                {
                    string _nombreCliente = (from item in data1.Tables["Clientes"].AsEnumerable()
                                             where item.Field<string>("Codigo") == txtCliente.Text
                                             select item.Field<string>("Nombre")).FirstOrDefault();

                    txtNombreCliente.Text = _nombreCliente.ToString();
                   // Validar = true;
                }
            }
            catch (Exception)
            {
                txtNombreCliente.Clear();
                errorProvider.SetError(txtCliente, "El Cliente no existe en la base de datos.");
                //Validar = false;
            }
        }

        private void rb_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbCES.Checked)
                {
                    if (tblDetalle.Rows.Count > 0)
                    {
                        DialogResult result = MessageBox.Show("La solicitud solo puede ser de un tipo PPC, Compra especial o Linea.\r\nLos artículos capturados se enviarán como Compra especial\r\n ¿Desea continuar?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        this.Formato(dgvItems, false, false, true);
                        //tblDetalle.Rows.Clear();
                    }
                    else
                    {
                        this.Formato(dgvItems, false, false, true);
                        tblDetalle.Rows.Clear();
                    }
                }
                if (rbPPC.Checked)
                {
                    if (tblDetalle.Rows.Count > 0)
                    {
                        DialogResult result = MessageBox.Show("La solicitud solo puede ser de un tipo PPC, Compra especial o Linea.\r\nLos artículos capturados se enviarán como PPC\r\n ¿Desea continuar?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        this.Formato(dgvItems, true, false, false);
                        //tblDetalle.Rows.Clear();
                    }
                    else
                    {
                        this.Formato(dgvItems, true, false, false);
                        tblDetalle.Rows.Clear();
                    }
                }
                if (rbLinea.Checked)
                {
                    if (tblDetalle.Rows.Count > 0)
                    {
                        DialogResult result = MessageBox.Show("La solicitud solo puede ser de un tipo PPC, Compra especial o Linea.\r\nLos artículos capturados se enviarán como Solicitud de Producto de Linea\r\n ¿Desea continuar?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        this.Formato(dgvItems, false, true, true);
                       // tblDetalle.Rows.Clear();
                    }
                    else
                    {
                        this.Formato(dgvItems, false, true, true);
                        tblDetalle.Rows.Clear();
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void dgvItems_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {

                if ((sender as DataGridView).CurrentCell.ColumnIndex == (int)Columnas.Articulo & rbPPC.Checked)
                {
                    var source = new AutoCompleteStringCollection();


                    string[] stringArray = Array.ConvertAll<DataRow, String>(data1.Tables["Articulos"].Select(), delegate(DataRow row) { return (String)row["Codigo"]; });

                    source.AddRange(stringArray);

                    TextBox prodCode = e.Control as TextBox;
                    if (prodCode != null)
                    {
                        prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode.AutoCompleteCustomSource = source;
                        prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
                else
                {
                    TextBox prodCode = e.Control as TextBox;
                    if (prodCode != null)
                    {
                        prodCode.AutoCompleteMode = AutoCompleteMode.None;
                        prodCode.AutoCompleteSource = AutoCompleteSource.None;
                    }
                }
            }
            catch (Exception) { }
        }

        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((sender as DataGridView).CurrentCell.ColumnIndex == (int)Columnas.Articulo & rbPPC.Checked)
                {
                    string item = (sender as DataGridView).CurrentCell.Value.ToString();

                    var qry = (from itemvar in data1.Tables["Articulos"].AsEnumerable()
                               where itemvar.Field<string>("Codigo").ToLower().Equals(item.ToLower())
                               select itemvar).CopyToDataTable();

                    if (qry.Rows.Count > 0)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row = (sender as DataGridView).CurrentRow;

                        row.Cells[(int)Columnas.Descripcion].Value = qry.Rows[0].Field<string>("Nombre");
                        row.Cells[(int)Columnas.Linea].Value = qry.Rows[0].Field<string>("ItmsGrpNam");
                        row.Cells[(int)Columnas.ItmsGrpCod].Value = qry.Rows[0].Field<Int16>("ItmsGrpCod");
                        row.Cells[(int)Columnas.CardCode].Value = qry.Rows[0].Field<string>("CardCode");
                        row.Cells[(int)Columnas.Proveedor].Value = qry.Rows[0].Field<string>("CardName");
                        row.Cells[(int)Columnas.Comprador].Value = qry.Rows[0].Field<string>("Comprador");

                    }
                }

                if ((sender as DataGridView).CurrentCell.ColumnIndex == (int)Columnas.Linea & !rbPPC.Checked)
                {
                    string item = (sender as DataGridView).CurrentCell.Value.ToString();

                    var qry = (from itemvar in data1.Tables["Lineas"].AsEnumerable()
                               where (itemvar.Field<string>("Nombre") == null ? string.Empty : itemvar.Field<string>("Nombre")).ToLower().Equals(item.ToLower())
                               select itemvar).CopyToDataTable();
                    if (qry.Rows.Count > 0)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row = (sender as DataGridView).CurrentRow;

                        row.Cells[(int)Columnas.Comprador].Value = qry.Rows[0].Field<string>("Comprador");
                        row.Cells[(int)Columnas.ItmsGrpCod].Value = qry.Rows[0].Field<Int16>("Codigo");
                    }
                }

                if ((sender as DataGridView).CurrentCell.ColumnIndex == (int)Columnas.Proveedor)
                {
                    string item = (sender as DataGridView).CurrentCell.Value.ToString();

                    var qry = (from itemvar in data1.Tables["Provedores"].AsEnumerable()
                               where (itemvar.Field<string>("Nombre") == null ? string.Empty : itemvar.Field<string>("Nombre")).ToLower().Equals(item.ToLower())
                               select itemvar).CopyToDataTable();
                    if (qry.Rows.Count > 0)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row = (sender as DataGridView).CurrentRow;

                        row.Cells[(int)Columnas.CardCode].Value = qry.Rows[0].Field<string>("Codigo");
                    }
                }
            }
            catch (Exception) { }
        }

        private void dgvItems_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if (!rbPPC.Checked)
                {
                    DataGridView dgv = (sender as DataGridView);
                    if (dgv == null) return;

                    int filaEdit = e.RowIndex;
                    int columnaEdit = e.ColumnIndex;

                    if (filaEdit == -1) return;
                    if (columnaEdit == (int)Columnas.Linea)
                    {
                        object objValCell = dgv.Rows[filaEdit].Cells[columnaEdit].Value;
                        if (objValCell == null)
                            return;
                        string valCell = objValCell.ToString();
                        DataGridViewComboBoxCell celcombo = new DataGridViewComboBoxCell();
                        object objTipoInci = dgv.Rows[filaEdit].Cells[(int)Columnas.Linea].Value;

                        if (objTipoInci == null) return;

                        string tipoInci = objTipoInci.ToString();
                        celcombo.DataSource = data1.Tables["Lineas"];
                        celcombo.ValueMember = "Nombre";
                        celcombo.DisplayMember = "Nombre";
                        
                        if (valCell == string.Empty)
                        {
                            dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                        }
                        else
                        {
                            celcombo.Value = valCell.Trim();
                            dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                        }
                    }
                }

                if (true)
                {
                    DataGridView dgv = (sender as DataGridView);
                    if (dgv == null) return;

                    int filaEdit = e.RowIndex;
                    int columnaEdit = e.ColumnIndex;

                    if (filaEdit == -1) return;
                    if (columnaEdit == (int)Columnas.Proveedor)
                    {
                        object objValCell = dgv.Rows[filaEdit].Cells[columnaEdit].Value;
                        if (objValCell == null)
                            return;
                        string valCell = objValCell.ToString();
                        DataGridViewComboBoxCell celcombo = new DataGridViewComboBoxCell();
                        object objTipoInci = dgv.Rows[filaEdit].Cells[(int)Columnas.Proveedor].Value;

                        if (objTipoInci == null) return;

                        string tipoInci = objTipoInci.ToString();
                        celcombo.DataSource = data1.Tables["Provedores"];
                        celcombo.ValueMember = "Nombre";
                        celcombo.DisplayMember = "Nombre";
                        
                        if (valCell == string.Empty)
                        {
                            dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                        }
                        else
                        {
                            celcombo.Value = valCell.Trim();
                            dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.ProductName, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItems_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[(int)Columnas.Cantidad].Value = 0;
                e.Row.Cells[(int)Columnas.Pronostico].Value = 0;
                e.Row.Cells[(int)Columnas.ItmsGrpCod].Value = 0;

                e.Row.Cells[(int)Columnas.Articulo].Value = string.Empty;
                e.Row.Cells[(int)Columnas.Descripcion].Value = string.Empty;
                e.Row.Cells[(int)Columnas.Proveedor].Value = string.Empty;
                e.Row.Cells[(int)Columnas.Comprador].Value = string.Empty;
                e.Row.Cells[(int)Columnas.CardCode].Value = string.Empty;
                e.Row.Cells[(int)Columnas.Especifique].Value = string.Empty;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void nuevoToolStripButton_Click(object sender, EventArgs e)
        {
            txtFolio.Clear();
            dtCompromiso.Value = DateTime.Now;
            cbVendedor.SelectedIndex = 0;
            cbUnidadVenta.SelectedIndex = 0;
            txtCliente.Clear();
            txtNombreCliente.Clear();
            rbPPC.Checked = true;
            txtJustificar.Clear();
            tblDetalle.Rows.Clear();
            this.Formato(dgvItems, false, false, false);
            btnGuardar.Enabled = true;
            Ruta = string.Empty;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            nuevoToolStripButton_Click(sender, e);
        }

        private void frmSolicitudProducto_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void frmSolicitudProducto_FormClosing(object sender, FormClosingEventArgs e)
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
