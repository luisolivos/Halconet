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
    public partial class frmPPC : Form
    {
        DataSet data1 = new DataSet();
        private int SlpCode;
        private string Folio;
        private string Solicitante;
        private string UnidadVenta;
        private string Type;
        private string Cliente;
        private string Comprador;
        private string Justificacion;
        private string Proveedor;
        private string Otro;
        private string Articulo;
        private string Descripcion;
        private DateTime FechaSolicitud;
        private DateTime FechaCompromiso;
        private int Linea;
        private int Cantidad;
        private int Pronostico;
        private bool Validar = false;
        private string Existe;
        Clases.Logs log;

        public enum TipoConulta
        {
            CargarSolicitantes = 1,
            CargarClientes = 2,
            CargarLineas = 3,
            CargarSucursales = 4,
            CargarProveedores = 5,
            CargarArticulos = 6,
            GuardarSolicitud = 7,
            GetMail = 8
        }

        public frmPPC(int _slpCode)
        {
            InitializeComponent();
            SlpCode = _slpCode;
        }

        private void CargarVendedores()
        {
            using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConulta.CargarSolicitantes);
                    command.Parameters.AddWithValue("@Folio", string.Empty);
                    command.Parameters.AddWithValue("@Solicitante", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Justificacion", string.Empty);
                    command.Parameters.AddWithValue("@TipoSolicitud", string.Empty);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", 0);
                    command.Parameters.AddWithValue("@Pronostico", 0);

                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = "";
                    row["Codigo"] = 0;
                    row["Memo"] = "00";
                    table.Rows.InsertAt(row, 0);
                    
                    table.TableName = "Vendedores";
                    data1.Tables.Add(table);
                }
            }
        }

        private void CargarSucursales()
        {
            using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConulta.CargarSucursales);
                    command.Parameters.AddWithValue("@Folio", string.Empty);
                    command.Parameters.AddWithValue("@Solicitante", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Justificacion", string.Empty);
                    command.Parameters.AddWithValue("@TipoSolicitud", string.Empty);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", 0);
                    command.Parameters.AddWithValue("@Pronostico", 0);

                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = "----";
                    row["Codigo"] = 0; ;
                    table.Rows.InsertAt(row, 0);

                    table.TableName = "Sucursales";
                    data1.Tables.Add(table);
                }
            }
        }

        private void CargarClientes()
        {
            using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConulta.CargarClientes);
                    command.Parameters.AddWithValue("@Folio", string.Empty);
                    command.Parameters.AddWithValue("@Solicitante", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Justificacion", string.Empty);
                    command.Parameters.AddWithValue("@TipoSolicitud", string.Empty);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", 0);
                    command.Parameters.AddWithValue("@Pronostico", 0);

                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = "";
                    row["Codigo"] = 0; 
                    table.Rows.InsertAt(row, 0);

                    table.TableName = "Clientes";
                    data1.Tables.Add(table);
                }
            }
        }

        private void CargarProveedores()
        {
            using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConulta.CargarProveedores);
                    command.Parameters.AddWithValue("@Folio", string.Empty);
                    command.Parameters.AddWithValue("@Solicitante", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Justificacion", string.Empty);
                    command.Parameters.AddWithValue("@TipoSolicitud", string.Empty);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", 0);
                    command.Parameters.AddWithValue("@Pronostico", 0);

                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = "Otro";
                    row["Codigo"] = "0";

                    table.Rows.InsertAt(row, 0);

                    table.TableName = "Proveedores";
                    data1.Tables.Add(table);
                }
            }
        }

        private void CargarLineas()
        {
            using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConulta.CargarLineas);
                    command.Parameters.AddWithValue("@Folio", string.Empty);
                    command.Parameters.AddWithValue("@Solicitante", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Justificacion", string.Empty);
                    command.Parameters.AddWithValue("@TipoSolicitud", string.Empty);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", 0);
                    command.Parameters.AddWithValue("@Pronostico", 0);

                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = "";
                    row["Codigo"] = 0;

                    table.Rows.InsertAt(row, 0);

                    table.TableName = "Lineas";
                    data1.Tables.Add(table);
                }
            }
        }

        private void CargarCompradores()
        {
            using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 10);
                    command.Parameters.AddWithValue("@Folio", string.Empty);
                    command.Parameters.AddWithValue("@Solicitante", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Justificacion", string.Empty);
                    command.Parameters.AddWithValue("@TipoSolicitud", string.Empty);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", 0);
                    command.Parameters.AddWithValue("@Pronostico", 0);

                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    table.TableName = "Compradores";
                    data1.Tables.Add(table);
                }
            }
        }

        private void CargarArticulos()
        {
            using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConulta.CargarArticulos);
                    command.Parameters.AddWithValue("@Folio", string.Empty);
                    command.Parameters.AddWithValue("@Solicitante", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Justificacion", string.Empty);
                    command.Parameters.AddWithValue("@TipoSolicitud", string.Empty);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", 0);
                    command.Parameters.AddWithValue("@Pronostico", 0);

                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    table.TableName = "Articulos";
                    data1.Tables.Add(table);
                }
            }
        }

        private string GetMail()
        {
            string mail ="";
            using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                {
                    Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConulta.GetMail);
                    command.Parameters.AddWithValue("@Folio", string.Empty);
                    command.Parameters.AddWithValue("@Solicitante", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Justificacion", string.Empty);
                    command.Parameters.AddWithValue("@TipoSolicitud", Type);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Linea", Linea);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", 0);
                    command.Parameters.AddWithValue("@Pronostico", 0);

                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        mail = reader.GetString(0);
                    }

                }
            }
            return mail;
        }

        private string GetMailAdmin()
        {//Cambiar correor en el procedimiento almacenado en caso de que el correo del programador cambie
            string mail = "";
            using (SqlConnection Connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", Connection))
                {
                    Connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 12);
                    command.Parameters.AddWithValue("@Folio", string.Empty);
                    command.Parameters.AddWithValue("@Solicitante", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Justificacion", string.Empty);
                    command.Parameters.AddWithValue("@TipoSolicitud", Type);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Linea", Linea);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", 0);
                    command.Parameters.AddWithValue("@Pronostico", 0);

                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        mail = reader.GetString(0);
                    }

                }
            }
            return mail;
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
                    command.Parameters.AddWithValue("@Folio", string.Empty);
                    command.Parameters.AddWithValue("@Solicitante", string.Empty);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaCompromiso", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Justificacion", string.Empty);
                    command.Parameters.AddWithValue("@TipoSolicitud", Type);
                    command.Parameters.AddWithValue("@Proveedor", string.Empty);
                    command.Parameters.AddWithValue("@Otro", string.Empty);
                    command.Parameters.AddWithValue("@Linea", Linea);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", SlpCode);
                    command.Parameters.AddWithValue("@Pronostico", 0);

                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        mail = reader.GetString(0);
                    }

                }
            }
            return mail;
        }

        private void CrearPDF()
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            string path = System.IO.Path.GetTempPath();
            bool creado = false;
            string _mail = string.Empty;
            string _mailVendedor = string.Empty;
            try
            {
                _mail = GetMail();
                _mailVendedor = this.GetMailVendedor();

                

                PdfWriter.GetInstance(doc, new FileStream(path + "Solicitud_" + Folio + ".pdf", FileMode.OpenOrCreate));

                doc.Open();

                iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 10);
                iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD);
                string title = "";
                if (Type == "CE")
                    title = "Compra especial";
                if (Type == "LIN")
                    title = "Producto de Linea";
                if (Type == "PPC")
                    title = "PPC";

                doc.Add(new Paragraph("\r\nSolicitud de " + title, fontTitle));

                doc.Add(new Phrase("\r\nFolio: ", font8BLOD));
                doc.Add(new Phrase(Folio + "\r\n", font8));

                doc.Add(new Phrase("Fecha de solicitud: ", font8BLOD));
                doc.Add(new Phrase(FechaSolicitud.ToShortDateString() + "\r\n", font8));

                doc.Add(new Phrase("Solicitante: ", font8BLOD));
                doc.Add(new Phrase(Solicitante + "\r\n", font8));

                doc.Add(new Phrase("Unidad de venta: ", font8BLOD));
                doc.Add(new Phrase(UnidadVenta + "\r\n", font8));

                doc.Add(new Phrase("Fecha de compromiso de venta: ", font8BLOD));
                doc.Add(new Phrase(FechaCompromiso.ToShortDateString() + "\r\n", font8));

                doc.Add(new Phrase("Cliente: ", font8BLOD));
                doc.Add(new Phrase(Cliente + "\r\n", font8));

                doc.Add(new Phrase("Nombre del cliente: ", font8BLOD));
                doc.Add(new Phrase(txtNombreCliente.Text + "\r\n", font8));

                doc.Add(new Phrase("Justificación: ", font8BLOD));
                doc.Add(new Phrase(Justificacion, font8));

                doc.Add(new Phrase("Comprador: ", font8BLOD));
                doc.Add(new Phrase(Comprador, font8));

                if (rbLinea.Checked)
                {
                    doc.Add(new Phrase("\r\nPronostico de venta mensual: ", font8BLOD));
                    doc.Add(new Phrase(Pronostico.ToString(), font8));
                }

                doc.Add(new Paragraph("\r\n\r\nItems solicitados\r\n"));


                PdfPTable PdfTable = new PdfPTable(5);
                PdfPCell PdfPCell = null;
                PdfTable.HorizontalAlignment = 0;
                PdfTable.SpacingBefore = 10;
                PdfTable.SpacingAfter = 10;
                PdfTable.DefaultCell.Border = 0;
                // PdfTable.SetWidths(new int[] { 1, 4 });

                //Add Header of the pdf table
                PdfPCell = new PdfPCell(new Phrase(new Chunk("Item", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Descripcion", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Linea", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Proveedor", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Cantidad", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk(Articulo.ToString(), font8)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk(Descripcion, font8)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk(cbLineas.Text, font8)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk(cbProveedor.Text + " " + txtOtro.Text, font8)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk(Cantidad.ToString(), font8)));
                PdfTable.AddCell(PdfPCell);

                PdfTable.SpacingBefore = 15f;
                doc.Add(PdfTable);

                doc.Close();
                creado = true;
            }
            catch (Exception ex)
            {
                creado = false;
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();
            }
            try
            {
                if (creado)
                {
                    if (!string.IsNullOrEmpty(_mail))
                    {
                        if (!string.IsNullOrEmpty(_mailVendedor))
                        //Enviar por correo
                        {
                            Cobranza.SendMail m = new Cobranza.SendMail();
                            m.Enviar(path + "Solicitud_" + Folio + ".pdf", _mail, _mailVendedor, cbSolicitante.Text, true);
                        }
                        else
                        {
                            MessageBox.Show("Error: No se encontro correo [Vendedor]", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error: No se encontro correo [Destinatario]", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    }
                    //string mailAdmin = GetMailAdmin();
                    //Cobranza.SendMail Admin = new Cobranza.SendMail();
                    //Admin.Enviar(path + "Solicitud_" + Folio + ".pdf", mailAdmin, mailAdmin, cbSolicitante.Text, true);
                }
                else
                {
                    MessageBox.Show("Error: Al crear PDF", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
            finally
            {
                //string[] files = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "*.pdf");

                //foreach (string item in files)
                //{
                //    System.IO.File.Delete(item);
                //}
            }

        }

        private bool ValidatItem()
        {
            bool v = false;
            try
            {
                if (Type == "CE" || Type == "LIN")
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand comm = new SqlCommand("PJ_SolicitudProducto", connection))
                        {
                            comm.CommandType = CommandType.StoredProcedure;
                            comm.Parameters.AddWithValue("@TipoConsulta", 9);
                            comm.Parameters.AddWithValue("@Folio", string.Empty);
                            comm.Parameters.AddWithValue("@Solicitante", Solicitante);
                            comm.Parameters.AddWithValue("@Sucursal", UnidadVenta);
                            comm.Parameters.AddWithValue("@TipoSolicitud", Type);
                            comm.Parameters.AddWithValue("@FechaCompromiso", FechaCompromiso);
                            comm.Parameters.AddWithValue("@Cliente", Cliente);
                            comm.Parameters.AddWithValue("@Justificacion", Justificacion);
                            comm.Parameters.AddWithValue("@Proveedor", Proveedor);
                            comm.Parameters.AddWithValue("@Otro", Otro);
                            comm.Parameters.AddWithValue("@Linea", Linea);
                            comm.Parameters.AddWithValue("@Articulo", Articulo);
                            comm.Parameters.AddWithValue("@Cantidad", Cantidad);
                            comm.Parameters.AddWithValue("@Pronostico", Pronostico);

                            SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                            parameter.Direction = ParameterDirection.Output;
                            comm.Parameters.Add(parameter);

                            connection.Open();

                            comm.ExecuteNonQuery();

                            string mensaje = Convert.ToString(comm.Parameters["@Mensaje"].Value);
                            if (mensaje != "")
                            {
                                v = false;
                                Existe = mensaje;
                            }
                            else
                            {
                                v = true;
                                Existe = "";
                            }
                        }
                    }
                }
                else if (Type == "PPC")
                {
                    v = true;
                }
            }
            catch (Exception)
            {
            }
            return v;
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

        private void PPC_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

                BindingSource BS = new BindingSource();

                this.CargarVendedores();
                this.CargarSucursales();
                this.CargarClientes();
                this.CargarProveedores();
                this.CargarLineas();
                this.CargarArticulos();
                this.CargarCompradores();

                cbSolicitante.DataSource = data1.Tables["Vendedores"];
                cbSolicitante.DisplayMember = "Nombre";
                cbSolicitante.ValueMember = "Codigo";

                cbProveedor.DataSource = data1.Tables["Proveedores"];
                cbProveedor.DisplayMember = "Nombre";
                cbProveedor.ValueMember = "Codigo";

                cbLineas.DataSource = data1.Tables["Lineas"];
                cbLineas.DisplayMember = "Nombre";
                cbLineas.ValueMember = "Codigo";

                txtCliente.AutoCompleteCustomSource = Autocomplete(data1.Tables["Clientes"], "Codigo");
                txtCliente.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;

                if (SlpCode > 0)
                {
                    cbSolicitante.SelectedValue = SlpCode;
                    cbSolicitante_SelectionChangeCommitted(sender, e);

                    cbSolicitante.Enabled = false;
                    cbUnidadVenta.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbSolicitante_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int slpcode = Convert.ToInt32(cbSolicitante.SelectedValue);
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

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (rbPPC.Checked)
            {
                ep.Clear();
                lblPronostico.Visible = false;
                mtPronostico.Visible = false;
                txtNombreArticulo.ReadOnly = true;
                pictureBox1.Visible = true;
            }
            if (rbLinea.Checked)
            {
                ep.Clear();
                lblPronostico.Visible = true;
                mtPronostico.Visible = true;
                txtNombreArticulo.ReadOnly = false;
            }

            if (rbCES.Checked)
            {
                ep.Clear();
                lblPronostico.Visible = false;
                mtPronostico.Visible = false;
                txtNombreArticulo.ReadOnly = false;
                mtPronostico.Text = "0";
            }
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (!rbLinea.Checked && !rbPPC.Checked && !rbCES.Checked)
            {
                epTipoSolicitud.SetError(rbLinea, "Debe eligir una opción.");
            }
            else
            {
                epTipoSolicitud.Clear();
                if (rbPPC.Checked)
                {
                    if (dtCompromiso.Value >= DateTime.Now.AddDays(7))
                    {
                        toolTip.IsBalloon = true;
                        //toolTip.Show("Para un PPC la fecha máxima de compromiso debe ser menor a 7 días\r\n a partr de la fecha de solicitud.", dtCompromiso, 5000);
                        epFechaCompromiso.SetError(dtCompromiso, "Para un PPC la fecha máxima de compromiso debe ser menor a 7 días\r\n a partr de la fecha de solicitud.");
                       // dtCompromiso.Value = DateTime.Now.AddDays(7);
                        Validar = false;
                    }
                    else
                    {
                        epFechaCompromiso.Clear();
                        Validar = true;
                    }
                }
                if (rbLinea.Checked || rbCES.Checked)
                {
                    if (dtCompromiso.Value >= DateTime.Now.AddDays(30))
                    {
                        //toolTip.Show("Para una Compra especial la fecha máxima de compromiso debe ser menor a 30 días\r\n a partr de la fecha de solicitud.", dtCompromiso, 5000);
                        epFechaCompromiso.SetError(dtCompromiso, "Para una Compra especial la fecha máxima de compromiso debe ser menor a 30 días\r\n a partr de la fecha de solicitud.");
                       // dtCompromiso.Value = DateTime.Now.AddDays(30);
                        Validar = false;
                    }
                    else
                    {
                        epFechaCompromiso.Clear();
                        Validar = true;
                    }
                }
            }
        }

        private void txtCliente_Leave(object sender, EventArgs e)
        {
            try
            {
                epCliente.Clear();
                if (!string.IsNullOrEmpty(txtCliente.Text))
                {
                    string _nombreCliente = (from item in data1.Tables["Clientes"].AsEnumerable()
                                             where item.Field<string>("Codigo") == txtCliente.Text
                                             select item.Field<string>("Nombre")).FirstOrDefault();

                    txtNombreCliente.Text = _nombreCliente.ToString();
                    Validar = true;
                }
            }
            catch (Exception)
            {
                txtNombreCliente.Clear();
                epCliente.SetError(txtCliente, "El Cliente no existe en la base de datos.");
                Validar = false;
            }
        }

        private void cbProveedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToString(cbProveedor.SelectedValue) == "0")
            {
                txtOtro.Enabled = true;
            }
            else
            {
                txtOtro.Clear();
                txtOtro.Enabled = false;
            }
        }

        private void cbLineas_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                txtItem.AutoCompleteMode = AutoCompleteMode.None;
                int _itmsgrcod = Convert.ToInt32(cbLineas.SelectedValue);
                if (rbPPC.Checked)
                {

                    var _articulo = (from item in data1.Tables["Articulos"].AsEnumerable()
                                     where item.Field<Int16>("ItmsGrpCod") == _itmsgrcod
                                     select item).CopyToDataTable();

                    txtItem.AutoCompleteCustomSource = Autocomplete(_articulo, "Codigo");
                    txtItem.AutoCompleteMode = AutoCompleteMode.Suggest;
                    txtItem.AutoCompleteSource = AutoCompleteSource.CustomSource;


                }
                var _comprador = (from item in data1.Tables["Compradores"].AsEnumerable()
                                  where item.Field<Int16>("Codigo") == _itmsgrcod
                                  select item.Field<string>("Nombre")).FirstOrDefault();

                txtComprador.Text = _comprador;
            }
            catch (Exception)
            {
            }
        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            try
            {
                epItem.Clear();
                if (!string.IsNullOrEmpty(txtItem.Text) && rbPPC.Checked)
                {
                    string _nombreArticulo = (from item in data1.Tables["Articulos"].AsEnumerable()
                                              where item.Field<string>("Codigo") == txtItem.Text
                                              select item.Field<string>("Nombre")).FirstOrDefault();

                    txtNombreArticulo.Text = _nombreArticulo.ToString();
                    Validar = true;
                }
            }
            catch (Exception)
            {
                txtNombreArticulo.Clear();
                epItem.SetError(txtItem, "El Articulo no existe en la base de datos.");
                Validar = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ep.Clear();
            if (cbSolicitante.Text != "")
            {
                Solicitante = cbSolicitante.SelectedValue.ToString() + " - " +cbSolicitante.Text;
                SlpCode = Convert.ToInt32(cbSolicitante.SelectedValue);
               // cbSolicitante.BackColor = Color.FromName("Window");
                if (cbUnidadVenta.Text != "")
                {
                    UnidadVenta = cbUnidadVenta.SelectedValue.ToString();
                    cbUnidadVenta.BackColor = Color.FromName("Window");
                    if (rbLinea.Checked || rbPPC.Checked || rbCES.Checked)
                    {
                        if (rbPPC.Checked)
                        {
                            Type = "PPC";
                        }
                        else if (rbLinea.Checked)
                        {
                            Type = "LIN";
                        }
                        else if (rbCES.Checked)
                        {
                            Type = "CE";
                        }

                        if (DateTime.Parse(dtCompromiso.Value.ToShortDateString()) >= DateTime.Parse(DateTime.Now.ToShortDateString()))
                        {
                            dtCompromiso.BackColor = Color.FromName("Window");
                            FechaCompromiso = dtCompromiso.Value;
                            if (!string.IsNullOrEmpty(txtCliente.Text))
                            {
                                txtCliente.BackColor = Color.FromName("Window");
                                Cliente = txtCliente.Text;
                                if (!string.IsNullOrEmpty(txtJustificacion.Text))
                                {
                                    txtJustificacion.BackColor = Color.FromName("Window");
                                    Justificacion = txtJustificacion.Text;
                                    if ((cbProveedor.Text == "Otro" && txtOtro.Text != "") || cbProveedor.Text != "Otro" && txtOtro.Text == "")
                                    {
                                        txtOtro.BackColor = Color.FromName("Window");
                                        Proveedor = cbProveedor.SelectedValue.ToString();
                                        Otro = txtOtro.Text;
                                        if (cbLineas.Text != "")
                                        {
                                            Linea = Convert.ToInt32(cbLineas.SelectedValue);
                                            if (txtItem.Text != "")
                                            {
                                                Articulo = txtItem.Text;
                                                if (txtNombreArticulo.Text != "")
                                                {
                                                    Descripcion = txtNombreArticulo.Text;
                                                    if (mtCantidad.Text != "" && convertCantidad)
                                                    {
                                                        Cantidad = int.Parse(mtCantidad.Text);
                                                        if ((mtPronostico.Text != "" && rbLinea.Checked && convertPronostico) || (mtPronostico.Text == "" && rbPPC.Checked) || (mtPronostico.Text == "0" && rbCES.Checked))
                                                        {
                                                            if (mtPronostico.Text != "")
                                                                Pronostico = int.Parse(mtPronostico.Text);
                                                            else
                                                                Pronostico = 0;
                                                            FechaSolicitud = DateTime.Now;
                                                            /**Validar**/
                                                            dateTimePicker1_ValueChanged(sender, e);
                                                            if(Validar)
                                                                radioButton1_Click(sender, e);
                                                            if (Validar)
                                                                cbSolicitante_SelectionChangeCommitted(sender, e);
                                                            if (Validar)
                                                                txtCliente_Leave(sender, e);
                                                            if (Validar)
                                                                cbProveedor_SelectionChangeCommitted(sender, e);
                                                            if (Validar)
                                                                cbLineas_SelectionChangeCommitted(sender, e);
                                                            if (Validar)
                                                                txtItem_Leave(sender, e);
                                                            bool val = this.ValidatItem();
                                                            if ((rbPPC.Checked && val) || (val && rbLinea.Checked) || (val && rbCES.Checked))// validar que el articulo no exista en la BD
                                                            {
                                                                if (Validar)
                                                                {
                                                                    Comprador = txtComprador.Text;
                                                                    try
                                                                    {
                                                                        string _mailVendedor = this.GetMailVendedor();
                                                                        if (!string.IsNullOrEmpty(_mailVendedor))
                                                                        {
                                                                            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                                                                            {
                                                                                using (SqlCommand comm = new SqlCommand("PJ_SolicitudProducto", connection))
                                                                                {
                                                                                    comm.CommandType = CommandType.StoredProcedure;
                                                                                    comm.Parameters.AddWithValue("@TipoConsulta", 7);
                                                                                    comm.Parameters.AddWithValue("@Folio", string.Empty);
                                                                                    comm.Parameters.AddWithValue("@Solicitante", Solicitante);
                                                                                    comm.Parameters.AddWithValue("@Sucursal", UnidadVenta);
                                                                                    comm.Parameters.AddWithValue("@TipoSolicitud", Type);
                                                                                    comm.Parameters.AddWithValue("@FechaCompromiso", FechaCompromiso);
                                                                                    comm.Parameters.AddWithValue("@Cliente", Cliente);
                                                                                    comm.Parameters.AddWithValue("@Justificacion", Justificacion);
                                                                                    comm.Parameters.AddWithValue("@Proveedor", Proveedor);
                                                                                    comm.Parameters.AddWithValue("@Otro", Otro);
                                                                                    comm.Parameters.AddWithValue("@Linea", Linea);
                                                                                    comm.Parameters.AddWithValue("@Articulo", Articulo);
                                                                                    comm.Parameters.AddWithValue("@Cantidad", Cantidad);
                                                                                    comm.Parameters.AddWithValue("@Pronostico", Pronostico);

                                                                                    SqlParameter parameter = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 9);
                                                                                    parameter.Direction = ParameterDirection.Output;
                                                                                    comm.Parameters.Add(parameter);

                                                                                    connection.Open();

                                                                                    comm.ExecuteNonQuery();

                                                                                    string mensaje = Convert.ToString(comm.Parameters["@Mensaje"].Value);
                                                                                    if (mensaje != "")
                                                                                    {
                                                                                        txtFolio.Text = mensaje;
                                                                                        Folio = mensaje;
                                                                                        this.CrearPDF();

                                                                                        MessageBox.Show("Registro exitoso!");
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            MessageBox.Show("Error: El vendedor no tiene asigngada una cuenta de correo.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ep.SetError(txtItem, "El artículo " + Existe + " ya existe en la Base de datos.");
                                                                //MessageBox.Show();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ep.SetError(mtPronostico, "El campo 'Pronostico de venta mensual' es obligatorio");
                                                            //MessageBox.Show();
                                                        }

                                                    }
                                                    else
                                                    {
                                                        ep.SetError(mtCantidad, "El campo 'Cantidad requerida' es obligatorio");
                                                        //MessageBox.Show();
                                                    }
                                                }
                                                else
                                                {
                                                    ep.SetError(txtNombreArticulo, "El campo 'Descripción' es obligatorio");
                                                }
                                            }
                                            else
                                            {
                                                ep.SetError(txtItem, "El campo 'Artículo' es obligatorio");
                                                //MessageBox.Show();
                                            }

                                        }
                                        else
                                        {
                                            ep.SetError(cbLineas, "El campo 'Linea' es obligatorio");
                                            //MessageBox.Show();
                                        }

                                    }
                                    else
                                    {
                                        txtOtro.BackColor = Color.FromName("Info");
                                        ep.SetError(txtOtro, "El campo 'Especifique' es obligatorio");
                                        //MessageBox.Show();
                                    }

                                }
                                else
                                {
                                    txtJustificacion.BackColor = Color.FromName("Info");
                                    ep.SetError(txtJustificacion, "El campo 'Justificación' es obligatorio");
                                    //MessageBox.Show();
                                }
                            }
                            else
                            {
                                txtCliente.BackColor = Color.FromName("Info");
                                ep.SetError(txtCliente, "El campo 'Cliente' es obligatorio");
                                //MessageBox.Show();
                            }
                        }
                        else
                        {
                            dtCompromiso.BackColor = Color.FromName("Info");
                            ep.SetError(dtCompromiso, "El campo 'Fecha de compromiso de venta' es obligatorio");
                            //MessageBox.Show();
                        }

                    }
                    else
                    {
                        ep.SetError(rbLinea, "El campo 'Unidad de venta' es obligatorio");
                        //MessageBox.Show();
                    }
                }
                else
                {
                    cbUnidadVenta.BackColor = Color.FromName("Info");
                    ep.SetError(cbUnidadVenta, "El campo 'Unidad de venta' es obligatorio");
                    //MessageBox.Show("El campo 'Unidad de venta' es obligatorio");
                }
            }
            else
            {
                cbSolicitante.BackColor = Color.FromName("Info");
                ep.SetError(cbSolicitante, "El campo 'Solicitante' es obligatorio");
                //MessageBox.Show("El campo 'Solicitante' es obligatorio");
            }
        }

        bool convertCantidad = false;
        private void mtCantidad_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int i = Int32.Parse(mtCantidad.Text.ToString());
                convertCantidad = true;
            }
            catch (Exception)
            {
                convertCantidad = false;
            }
            
        }

        bool convertPronostico = false;
        private void mtPronostico_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (mtPronostico.Text != "" && rbLinea.Checked)
                {
                    int i = Int32.Parse(mtPronostico.Text.ToString());
                    convertPronostico = true;
                }
                if (rbPPC.Checked)
                    convertPronostico = true;
            }
            catch (Exception)
            {
                convertPronostico = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtFolio.Clear();
            //if (SlpCode > 0)
            //{
            //    cbSolicitante.SelectedIndex = 0;
            //    cbUnidadVenta.Text = "";
            //}
            rbLinea.Checked = false;
            rbPPC.Checked = false;
            dtCompromiso.Value = DateTime.Now;
            txtCliente.Clear();
            txtNombreCliente.Clear();
            txtJustificacion.Clear();
            cbProveedor.SelectedIndex = 0;
            txtOtro.Clear();
            txtItem.Clear();
            txtNombreArticulo.Clear();
            mtCantidad.Clear();
            mtPronostico.Clear();
            ep.Clear();
            epTipoSolicitud.Clear();
            epCliente.Clear();
            epItem.Clear();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Solicitudes.frmCatalogoArticulos catalogo = new frmCatalogoArticulos(Convert.ToInt32(cbLineas.SelectedValue), cbLineas.SelectedText, data1.Tables["Articulos"]);
            if (catalogo.ShowDialog() == DialogResult.OK)
            {
                txtItem.Text = catalogo.Item;
                cbLineas.SelectedValue = catalogo.ItmsGrpCod;
                cbLineas_SelectionChangeCommitted(sender, e);
                txtItem_Leave(sender, e);
            }
        }

        private void PPC_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void PPC_FormClosing(object sender, FormClosingEventArgs e)
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
