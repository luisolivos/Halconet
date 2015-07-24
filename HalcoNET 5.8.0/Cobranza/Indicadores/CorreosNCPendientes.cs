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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Cobranza.Indicadores
{
    public partial class CorreosNCPendientes : Form
    {
        public CorreosNCPendientes()
        {
            InitializeComponent();
        }

        public void LlenarGrid(int tipo, DataGridView dgv)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_NCPedientes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", tipo);
                        command.Parameters.AddWithValue("@Cliente", string.Empty);
                        command.Parameters.AddWithValue("@Docentry", 0);
                        command.Parameters.AddWithValue("@PrecioCliente", decimal.Zero);
                        command.Parameters.AddWithValue("@FechaEnvio", DateTime.Now);
                        command.Parameters.AddWithValue("@Vendedor", 0);
                        command.Parameters.AddWithValue("@Factura", 0);
                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@LineNum", 0);
                        command.Parameters.AddWithValue("@Comentario", string.Empty);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable table = new DataTable();
                        da.SelectCommand.CommandTimeout = 0;
                        da.Fill(table);

                        dgv.DataSource = table;

                    }

                }
            }
            catch (Exception)
            {

            }
        }

        public string GenerPDFGerente(DataTable _table, string _vendedor)
        {
            string Path = System.IO.Path.GetTempPath() + System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetTempFileName()) + ".pdf";

            this.ToPDF(_table, Path, _vendedor);

            return Path;
        }

        public string GenerPDFJefas()
        {
            string Path = System.IO.Path.GetTempPath() + System.IO.Path.GetTempFileName() + ".pdf";



            return Path;
        }

        public bool ToPDF(DataTable dgv, string _ruta, string _vendedor)
        {

            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            bool creado = false;
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(_ruta, FileMode.OpenOrCreate));

                doc.Open();

                iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font texto = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD);

                doc.Add(new Paragraph("\r\nNotas de crédito pendientes: " + DateTime.Now.ToShortDateString()));
                doc.Add(new Paragraph(new Chunk("Vendedor: " + _vendedor, texto)));

                PdfPTable PdfTable = new PdfPTable(3);
                PdfPCell PdfPCell = null;
                PdfTable.HorizontalAlignment = 0;
                PdfTable.SpacingBefore = 1;
                PdfTable.SpacingAfter = 1;
                PdfTable.DefaultCell.Border = 0;
                PdfTable.WidthPercentage = 100;


                float[] Columnas = new float[] {20, 20, 60};

                PdfTable.SetWidths(Columnas);
                //Add Header of the pdf table

              
                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Factura", font8BLOD)));
                    PdfTable.AddCell(PdfPCell);
                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Cliente", font8BLOD)));
                    PdfTable.AddCell(PdfPCell);
                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Nombre del cliente", font8BLOD)));
                    PdfTable.AddCell(PdfPCell);

                foreach (DataRow item in dgv.Rows)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(Convert.ToString(item.Field<Int32>("Factura")), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Cliente"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Nombre del Cliente"), font8)));
                    PdfTable.AddCell(PdfPCell);
                }


                PdfTable.SpacingBefore = 15f;
                doc.Add(PdfTable);
                doc.AddAuthor("HalcoNET " + ClasesSGUV.Propiedades.Version);
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
            return creado;
        }


        private void CorreosNCPendientes_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                this.LlenarGrid(8, dgvSucursal);
                // this.LlenarGrid(9, dgvJefaCobranza);

                List<string> ListVendedores = new List<string>();
                ListVendedores = (from item in (dgvSucursal.DataSource as DataTable).AsEnumerable()
                                  select item.Field<string>("Vendedor")).Distinct().ToList();
                try
                {
                    foreach (string item in ListVendedores)
                    {


                        DataTable table = (from row in (dgvSucursal.DataSource as DataTable).AsEnumerable()
                                           where row.Field<string>("Vendedor") == item
                                           select row).CopyToDataTable();

                        string _rutaGerentes = this.GenerPDFGerente(table, item);

                        string _mailVendedor = (from row in (dgvSucursal.DataSource as DataTable).AsEnumerable()
                                                where row.Field<string>("Vendedor") == item
                                                select row.Field<string>("MailVendedor")).FirstOrDefault();

                        string _mailGerente = (from row in (dgvSucursal.DataSource as DataTable).AsEnumerable()
                                               where row.Field<string>("Vendedor") == item
                                               select row.Field<string>("MailGerente")).FirstOrDefault();

                        string _mailJefa = (from row in (dgvSucursal.DataSource as DataTable).AsEnumerable()
                                            where row.Field<string>("Vendedor") == item
                                            select row.Field<string>("MailJefa")).FirstOrDefault();

                        SendMail mail = new SendMail();
                        /*******************/
                        if (item.Trim().Equals("Jorge Lopez Aguilar"))
                        {
                            mail.EnviarNC(_rutaGerentes, "lila.perez@pj.com.mx;" + _mailGerente + ";" + _mailJefa, item, _mailVendedor, table.Rows.Count);
                        }
                        else
                        {
                            mail.EnviarNC(_rutaGerentes, _mailGerente + ";" + _mailJefa, item, _mailVendedor, table.Rows.Count);
                        }
                        //mail.EnviarNC(_rutaGerentes, "jose.olivos@pj.com.mx", item, "jose.olivos@pj.com.mx", table.Rows.Count);

                        foreach (DataRow factura in (dgvSucursal.DataSource as DataTable).AsEnumerable())
                        {
                            if (factura.Field<string>("Vendedor") == item)
                            {
                                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                                {
                                    using (SqlCommand command = new SqlCommand("Update [@PreciosCliente] SET U_EnviadoGerente = 'Y'  Where U_DocEntry = @DocEntry", connection))
                                    {
                                        connection.Open();

                                        command.Parameters.AddWithValue("@DocEntry", factura.Field<Int32>("U_Docentry"));

                                        command.ExecuteNonQuery();
                                    }
                                }

                            }
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error inesperado", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception)
            {
            }
            finally
            {
                this.Close();
            }
        }
        

    }
}
