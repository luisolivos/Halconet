using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

using System.IO;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;

using System.Text.RegularExpressions;

namespace Cobranza.Clases
{
    public class CrearPDF
    {

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public bool ToPDF(DataGridView dgv, string _jefa)
        {
           // dgv.Columns.Remove("Comentario");
    
            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            bool creado = false;
            try
            {
                doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());

                _nombre = Path.GetTempFileName() + ".pdf";
                PdfWriter.GetInstance(doc, new FileStream(_nombre, FileMode.OpenOrCreate));

                doc.Open();

                iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font texto = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD);

                doc.Add(new Paragraph("\r\nAgenda del día " + DateTime.Now.ToShortDateString()));
                doc.Add(new Paragraph(new Chunk("Jefa de cobranza: " + _jefa, texto)));

                PdfPTable PdfTable = new PdfPTable(dgv.Columns.Count);
                PdfPCell PdfPCell = null;
                PdfTable.HorizontalAlignment = 0;
                PdfTable.SpacingBefore = 1;
                PdfTable.SpacingAfter = 1;
                PdfTable.DefaultCell.Border = 0;
                PdfTable.WidthPercentage = 100;


                float[] Columnas = new float[] { 6, 5, 7, 23, 11, 9, 9, 6, 8, 16};

                PdfTable.SetWidths(Columnas);
                //Add Header of the pdf table

                foreach (DataGridViewColumn item in dgv.Columns)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Name, font8BLOD)));
                    PdfTable.AddCell(PdfPCell);

                }

                DataTable _datos = (DataTable)dgv.DataSource;

                foreach (DataRow item in _datos.Rows)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Folio"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Tipo de compromiso"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Cliente"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Nombre del cliente"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    if (item.Field<string>("Tipo de compromiso") == "Llamada")
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<DateTime>("Fecha de vencimiento de compromiso").ToString(), font8)));
                        PdfTable.AddCell(PdfPCell);
                    }
                    else
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<DateTime>("Fecha de vencimiento de compromiso").ToShortDateString(), font8)));
                        PdfTable.AddCell(PdfPCell);
                    }
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Cantidad comprometida").ToString("C2"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Pagado comprometido").ToString("C2"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Efectividad").ToString("P2"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Estatus"), font8)));
                    PdfTable.AddCell(PdfPCell);
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Comentario"), font8)));
                    PdfTable.AddCell(PdfPCell);
                }
                

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
            return creado;
        }

        public bool ToPDF(DataTable _facturas, string _vendedor, string _jefa)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            bool creado = false;
            try
            {
                _nombre = Path.GetRandomFileName() + ".pdf";
                PdfWriter.GetInstance(doc, new FileStream("PDF\\" + _nombre, FileMode.OpenOrCreate));

                doc.Open();

                iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font texto = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font textoMini = FontFactory.GetFont("ARIAL", 4, iTextSharp.text.Font.BOLD);

                doc.Add(new Paragraph("\r\nNCRD Pendientes al " + DateTime.Now.ToShortDateString()));
                doc.Add(new Paragraph(new Chunk("Jefa de cobranza: " + _jefa, texto)));
                doc.Add(new Paragraph(new Chunk("Vendedor: " + _vendedor, texto)));

                PdfPTable PdfTable = new PdfPTable(8);
                PdfPCell PdfPCell = null;
                PdfTable.HorizontalAlignment = 0;
                PdfTable.SpacingBefore = 1;
                PdfTable.SpacingAfter = 1;
                PdfTable.DefaultCell.Border = 0;
                PdfTable.WidthPercentage = 100;


                float[] Columnas = new float[] {9f, 10f, 10f, 10f, 28f, 11f, 11f, 11f };

                PdfTable.SetWidths(Columnas);
                //Add Header of the pdf table


                PdfPCell = new PdfPCell(new Phrase(new Chunk("Factura", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Fecha factura", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Fecha Vto", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Cliente", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Nombre del cliente", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Total factura", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Saldo", font8BLOD)));
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Días desde ultimo pago ", font8BLOD)));
                PdfTable.AddCell(PdfPCell);


                DataTable _datos = _facturas.Copy();

                foreach (DataRow item in _datos.Rows)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32>("Factura").ToString(), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<DateTime>("Fecha factura").ToShortDateString(), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<DateTime>("Fecha Vto").ToShortDateString(), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Cliente"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Nombre del cliente"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Total factura").ToString("C2"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Saldo").ToString("C2"), font8)));
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32>("Días desde ultimo pago").ToString(), font8)));
                    PdfTable.AddCell(PdfPCell);
                }


                PdfTable.SpacingBefore = 15f;
                doc.Add(PdfTable);

                doc.Add(new Phrase(new Chunk("HalcoNET", textoMini)));
                PdfTable.AddCell(PdfPCell);

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

        //PDF Reporte Corcho<<facturacion pendiente de entregar a credito y cobranza>>
        public bool ToPDFCorcho(DataTable _facturas)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            bool creado = false;
            try
            {
                _nombre = Path.GetTempFileName() + ".pdf";
                PdfWriter.GetInstance(doc, new FileStream(_nombre, FileMode.OpenOrCreate));

                doc.Open();

                iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLODTitle = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                iTextSharp.text.Font font8BLODTotal = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font texto = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font textoMini = FontFactory.GetFont("ARIAL", 4, iTextSharp.text.Font.BOLD);

                doc.Add(new Paragraph("\r\nCorcho " + DateTime.Now.ToShortDateString()));

               //CrearPDF
               // PDFWriter.PageEvent = PageEventHandler;

                PdfPTable PdfTable = new PdfPTable(11);
                PdfPCell PdfPCell = null;
                PdfTable.HorizontalAlignment = 0;
                PdfTable.SpacingBefore = 1;
                PdfTable.SpacingAfter = 1;
                PdfTable.DefaultCell.Border = 0;
                PdfTable.WidthPercentage = 100;

                float[] Columnas = new float[] {6f, 7f, 7f, 21f, 11f, 7f, 6f, 7f, 7f, 4f, 17f};

                PdfTable.SetWidths(Columnas);
                //Add Header of the pdf table

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Tipo de doc.", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Factura", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Fecha", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Cliente", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Responsable", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };//asesor de ventas
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Cond. de pago", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Estatus", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Total del doc", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Total recibido", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Días trans", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                PdfTable.AddCell(PdfPCell);

                PdfPCell = new PdfPCell(new Phrase(new Chunk("Causas", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                PdfTable.AddCell(PdfPCell);


                DataTable _datos = _facturas.Copy();

                foreach (DataRow item in _datos.Rows)
                {
                    if (item.Field<string>("Asesor de ventas").Contains("Total"))
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(string.Empty, font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32?>("Folio SAP").ToString(), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<DateTime?>("Fecha").ToString(), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Cliente"), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk("TOTAL", font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Condicion de pago"), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Estatus"), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Total del documento").ToString("C2"), font8BLODTotal))) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Total recibido").ToString("C2"), font8BLODTotal))) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32?>("Días trans").ToString(), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Causas"), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                        PdfTable.AddCell(PdfPCell);
                    }
                    else
                    {

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Tipo").ToString(), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32?>("Folio SAP").ToString(), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<DateTime>("Fecha").ToShortDateString(), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Cliente"), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Asesor de ventas"), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Condicion de pago"), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Estatus"), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Total del documento").ToString("C2"), font8))) { HorizontalAlignment = Element.ALIGN_RIGHT };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Total recibido").ToString("C2"), font8))) { HorizontalAlignment = Element.ALIGN_RIGHT };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32?>("Días trans").ToString(), font8))) {HorizontalAlignment = Element.ALIGN_CENTER};
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Causas"), font8))) {HorizontalAlignment = Element.ALIGN_LEFT};
                        PdfTable.AddCell(PdfPCell);
                    }
                }


                PdfTable.SpacingBefore = 15f;
                doc.Add(PdfTable);

                doc.Add(new Phrase(new Chunk("HalcoNET", textoMini)));
                PdfTable.AddCell(PdfPCell);

                doc.Close();

                System.Diagnostics.Process.Start(_nombre);
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

        //PDF Corcho Reporte de facturacion diaria
        public string ToPDFCorchoxCP(DataTable _facturas, DateTime _fecha)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            string _Path = string.Empty;

            try
            {
                _nombre = Path.GetTempFileName() + ".pdf";
                PdfWriter.GetInstance(doc, new FileStream(_nombre, FileMode.OpenOrCreate));

                doc.Open();

                iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLODTitle = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                iTextSharp.text.Font font8BLODTotal = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font texto = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font textoMini = FontFactory.GetFont("ARIAL", 4, iTextSharp.text.Font.BOLD);

                doc.Add(new Paragraph("Facturas pendientes por entregar a Crédito y Cobranza " + DateTime.Now.ToShortDateString()));

                //----------------------------------------------------------------------------------------------------------------------------------
                List<string> Condiciones = new List<string>();
                Condiciones = (from item in _facturas.AsEnumerable()
                              where !item.Field<string>("Condicion de pago").Contains("Total")
                              select item.Field<string>("Condicion de pago")).Distinct().ToList();

                foreach (string condicion in Condiciones)
                {

                    doc.Add(new Paragraph("\r\nCondición de pago: " + condicion, font8BLOD));
                    PdfPTable PdfTable = new PdfPTable(10);
                    PdfPCell PdfPCell = null;
                    PdfTable.HorizontalAlignment = 0;
                    PdfTable.SpacingBefore = 1;
                    PdfTable.SpacingAfter = 1;
                    PdfTable.DefaultCell.Border = 0;
                    PdfTable.WidthPercentage = 100;


                    float[] Columnas = new float[] {6f, 7f, 7f, 21f, 12f, 6f, 8f, 8f, 5f, 20};

                    PdfTable.SetWidths(Columnas);
                    //Add Header of the pdf table

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Tipo de doc", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Folio SAP", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Fecha", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Cliente", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Responsable de entregar", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Estatus", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Total del doc", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Total recibido", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Días trans", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Causas", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);


                    DataTable _datos = (from item in _facturas.AsEnumerable()
                                        where// !item.Field<string>("Condicion de pago").Contains("Total")
                                              item.Field<string>("Condicion de pago").Contains(condicion)
                                        select item).CopyToDataTable();

                  

                    //_datos = (from tv in _datos.AsEnumerable()
                    //          orderby tv.Field<string>("Asesor de ventas")
                    //          select tv).CopyToDataTable();


                    foreach (DataRow item in _datos.Rows)
                    {
                        if (item.Field<string>("Condicion de pago").Contains("Total"))
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(string.Empty, font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfTable.AddCell(PdfPCell);

                            //PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32?>("Folio SAP").ToString(), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfTable.AddCell(PdfPCell);

                            //PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<DateTime?>("Fecha").ToString(), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };                            
                            PdfTable.AddCell(PdfPCell);

                            //PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Cliente"), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            //PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };

                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk("TOTAL", font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfTable.AddCell(PdfPCell);

                            //PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Estatus"), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfTable.AddCell(PdfPCell);

                           
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Total del documento").ToString("C2"), font8BLODTotal))) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Total recibido").ToString("C2"), font8BLODTotal))) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfTable.AddCell(PdfPCell);

                            //PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32?>("Días trans").ToString(), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfTable.AddCell(PdfPCell);

                            //PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Causas"), font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTotal))) { BackgroundColor = new BaseColor(217, 217, 217) };
                            PdfTable.AddCell(PdfPCell);
                        }
                        else
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Tipo"), font8)));
                            PdfTable.AddCell(PdfPCell);

                            if (item.Field<DateTime>("Fecha").Date != _fecha.Date)
                            {
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32?>("Folio SAP").ToString(), font8))) { BackgroundColor = new BaseColor(217, 217, 217) };
                                PdfTable.AddCell(PdfPCell);
                            }
                            else
                            {
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32?>("Folio SAP").ToString(), font8))) { BackgroundColor = new BaseColor(255, 255, 255) };
                                PdfTable.AddCell(PdfPCell);
                            }

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<DateTime>("Fecha").ToShortDateString(), font8)));
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Cliente"), font8)));
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Asesor de ventas"), font8)));
                            PdfTable.AddCell(PdfPCell);

                            //PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Condicion de pago"), font8)));
                            //PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Estatus"), font8)));
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell.HorizontalAlignment = Element.ALIGN_RIGHT;

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Total del documento").ToString("C2"), font8))) { HorizontalAlignment = Element.ALIGN_RIGHT };
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Total recibido").ToString("C2"), font8))) { HorizontalAlignment = Element.ALIGN_RIGHT };
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<Int32?>("Días trans").ToString(), font8))) { HorizontalAlignment = Element.ALIGN_CENTER };
                            PdfTable.AddCell(PdfPCell);



                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Causas") + " " +item.Field<DateTime>("U_Cierre").ToShortDateString(), font8))) { HorizontalAlignment = Element.ALIGN_LEFT };
                            PdfTable.AddCell(PdfPCell);
                        }
                    }

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    //PdfPCell = new PdfPCell(new Phrase(new Chunk("TOTAL", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    //PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("TOTAL", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    decimal total = Convert.ToDecimal(_datos.Compute("SUM([Total del documento])", string.Empty));

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(total.ToString("C2"), font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    //PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    //PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfTable.SpacingBefore = 15f;
                    doc.Add(PdfTable);
                }
                //------------------------------------------------------------------------------------
                doc.Add(new Phrase(new Chunk("HalcoNET", textoMini)));
                //PdfTable.AddCell(PdfPCell);

                doc.Close();

                
                _Path = _nombre;
            }
            catch (Exception ex)
            {
                _Path = string.Empty;
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();


            }
            return _Path;
        }

        /////////////////********pdf atradius************///////////////
        public string ToPDFAtradius(string _cardCode, string _texto)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            try
            {
                DataTable _datos = Source(10, _cardCode);
                if (_datos.Rows.Count > 0)
                {
                    _nombre = Path.GetTempFileName() + ".pdf";
                    PdfWriter.GetInstance(doc, new FileStream(_nombre, FileMode.OpenOrCreate));

                    doc.Open();

                    iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
                    iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font font8BLODTitle = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                    iTextSharp.text.Font font8BLODTotal = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font texto = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font textoMini = FontFactory.GetFont("ARIAL", 4, iTextSharp.text.Font.BOLD);

                    doc.Add(new Paragraph(new Chunk(_texto, texto)));

                    PdfPTable PdfTable = new PdfPTable(_datos.Columns.Count);
                    PdfPCell PdfPCell = null;
                    PdfTable.HorizontalAlignment = 0;
                    PdfTable.SpacingBefore = 1;
                    PdfTable.SpacingAfter = 1;
                    PdfTable.DefaultCell.Border = 0;
                    PdfTable.WidthPercentage = 80;


                    float[] Columnas = new float[] { 11, 40, 11, 12, 13, 13 };

                    PdfTable.SetWidths(Columnas);
                    //Add Header of the pdf table
                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Cliente", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Nombre", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Factura", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Fecha Cont.", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Fecha Vto.", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Saldo", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);



                    foreach (DataRow item in _datos.Rows)
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("CardCode"), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("CardName"), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<int>("DocNum").ToString(), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<DateTime>("DocDate").ToShortDateString(), font8))) { HorizontalAlignment = Element.ALIGN_CENTER };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<DateTime>("DocDueDate").ToShortDateString(), font8))) { HorizontalAlignment = Element.ALIGN_CENTER };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Saldo").ToString("C2"), font8))) { HorizontalAlignment = Element.ALIGN_RIGHT };
                        PdfTable.AddCell(PdfPCell);
                    }


                    PdfTable.SpacingBefore = 15f;
                    doc.Add(PdfTable);

                    doc.Close();
                }
                else
                    _nombre = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();


            }
            return _nombre;
        }

        public string ToPDFAtradius(DataGridView dgv, string _cardCode, string _texto)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            try
            {
                DataTable _datos = (DataTable)(dgv.DataSource);
                if (_datos.Rows.Count > 0)
                {
                    _nombre = Path.GetTempFileName() + ".pdf";
                    PdfWriter.GetInstance(doc, new FileStream(_nombre, FileMode.OpenOrCreate));

                    doc.Open();

                    iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
                    iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font font8BLODTitle = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                    iTextSharp.text.Font font8BLODTotal = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font texto = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font textoMini = FontFactory.GetFont("ARIAL", 4, iTextSharp.text.Font.BOLD);

                    doc.Add(new Paragraph(new Chunk(_texto, texto)));

                    PdfPTable PdfTable = new PdfPTable(_datos.Columns.Count);
                    PdfPCell PdfPCell = null;
                    PdfTable.HorizontalAlignment = 0;
                    PdfTable.SpacingBefore = 1;
                    PdfTable.SpacingAfter = 1;
                    PdfTable.DefaultCell.Border = 0;
                    PdfTable.WidthPercentage = 50;


                    float[] Columnas = new float[] { 25, 25, 25, 25 };

                    PdfTable.SetWidths(Columnas);
                    //Add Header of the pdf table
                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Factura", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Saldo en prorroga", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Pagos", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Saldo actual", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);



                    foreach (DataRow item in _datos.Rows)
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<int>("Factura").ToString(), font8)));
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Saldo en prorroga").ToString("C2"), font8))) { HorizontalAlignment = Element.ALIGN_RIGHT };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Pagos").ToString("C2"), font8))) { HorizontalAlignment = Element.ALIGN_RIGHT };
                        PdfTable.AddCell(PdfPCell);

                        PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Saldo Actual").ToString("C2"), font8))) { HorizontalAlignment = Element.ALIGN_RIGHT };
                        PdfTable.AddCell(PdfPCell);
                    }


                    PdfTable.SpacingBefore = 15f;
                    doc.Add(PdfTable);

                    doc.Close();
                }
                else
                    _nombre = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();


            }
            return _nombre;
        }

        public DataTable Source(int _tipoConsulta, string _CardCode)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_AtradiusP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", _tipoConsulta);
                    command.Parameters.AddWithValue("@Desde", DateTime.Now);
                    command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                    command.Parameters.AddWithValue("@CardCode", _CardCode);
                    command.Parameters.AddWithValue("@CardName", string.Empty);
                    command.Parameters.AddWithValue("@DocEntry", string.Empty);
                    command.Parameters.AddWithValue("@DocNum", string.Empty);

                    SqlParameter ValidaUsuario = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                    ValidaUsuario.Direction = ParameterDirection.Output;
                    command.Parameters.Add(ValidaUsuario);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.SelectCommand.CommandTimeout = 0;

                    da.Fill(table);
                }
            }

            return table;
        }

        //*///////pdf depositos
        public string ToPDFDepositos(DataTable __Datos, string _texto)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            try
            {
                DataTable _datos = __Datos;
                if (_datos.Rows.Count > 0)
                {
                    _nombre = Path.GetTempFileName() + ".pdf";
                    PdfWriter.GetInstance(doc, new FileStream(_nombre, FileMode.OpenOrCreate));

                    doc.Open();

                    iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
                    iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font font8BLODTitle = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.White));
                    iTextSharp.text.Font font8BLODTotal = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font texto = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
                    iTextSharp.text.Font textoMini = FontFactory.GetFont("ARIAL", 4, iTextSharp.text.Font.BOLD);

                    doc.Add(new Paragraph(new Chunk(_texto, texto)));

                    PdfPTable PdfTable = new PdfPTable(_datos.Columns.Count);
                    PdfPCell PdfPCell = null;
                    PdfTable.HorizontalAlignment = 0;
                    PdfTable.SpacingBefore = 1;
                    PdfTable.SpacingAfter = 1;
                    PdfTable.DefaultCell.Border = 0;
                    PdfTable.WidthPercentage = 70;


                    float[] Columnas = new float[] { 10, 30, 25, 15 };

                    PdfTable.SetWidths(Columnas);
                    //Add Header of the pdf table
                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Cliente", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Nombre", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Referencia", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Monto", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(System.Drawing.Color.Black) };
                    PdfTable.AddCell(PdfPCell);

                    foreach (DataRow item in _datos.Rows)
                    {
                        if (item.Field<decimal>("Monto") != decimal.Zero)
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Cliente"), font8)));
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Nombre"), font8)));
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Referencia").ToString(), font8)));
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Monto").ToString("C2"), font8))) { HorizontalAlignment = Element.ALIGN_RIGHT };
                            PdfTable.AddCell(PdfPCell);
                        }
                    }


                    PdfTable.SpacingBefore = 15f;
                    doc.Add(PdfTable);

                    doc.Close();
                }
                else
                    _nombre = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                doc.Close();


            }
            return _nombre;
        }
    }
}
