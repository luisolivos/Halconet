using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

using System.IO;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;

using System.Text.RegularExpressions;

namespace Pagos
{
    public class CrearPDF
    {

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }


        public bool ToPDF(DataTable _facturas)
        {
            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            bool creado = false;
            try
            {
                _nombre = Path.GetTempFileName() + ".pdf";
                PdfWriter.GetInstance(doc, new FileStream(_nombre, FileMode.OpenOrCreate));

                doc.Open();

                iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 5.5f, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 6f, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLODTitle = FontFactory.GetFont("ARIAL", 5.5f, iTextSharp.text.Font.BOLD, new BaseColor(System.Drawing.Color.Black));
                iTextSharp.text.Font font8BLODTotal = FontFactory.GetFont("ARIAL", 5.5f, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font texto = FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font textoMini = FontFactory.GetFont("ARIAL", 3.5f, iTextSharp.text.Font.BOLD);

                doc.Add(new Paragraph(new Phrase(new Chunk("Facturas autorizadas para pago  " + DateTime.Now.ToShortDateString(), font8BLODTitle))) { });

                //----------------------------------------------------------------------------------------------------------------------------------
                List<string> Condiciones = new List<string>();
                Condiciones = (from item in _facturas.AsEnumerable()
                               where !item.Field<string>("Moneda").ToLower().Contains("total")
                              select item.Field<string>("Moneda")).Distinct().ToList();

                foreach (string condicion in Condiciones)
                {
                    PdfPTable PdfTable = new PdfPTable(4);
                    PdfPCell PdfPCell = null;
                    PdfTable.HorizontalAlignment = 0;
                    PdfTable.SpacingBefore = 1;
                    PdfTable.SpacingAfter = 1;
                    PdfTable.DefaultCell.Border = 0;
                    PdfTable.WidthPercentage = 90;


                    float[] Columnas = new float[] {10f, 50f, 10f, 20f};

                    PdfTable.SetWidths(Columnas);
                    //Add Header of the pdf table
                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Factura", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(217, 217, 217) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Proveedor", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(217, 217, 217) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Autorizado", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(217, 217, 217) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("Cta. Bancaria", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(217, 217, 217) };
                    PdfTable.AddCell(PdfPCell);

                    DataTable _datos = (from item in _facturas.AsEnumerable()
                                        where !item.Field<string>("Moneda").ToLower().Contains("total")
                                            && item.Field<string>("Moneda").Equals(condicion)
                                        select item).CopyToDataTable();

                    

                    ///////////////////////
                    List<string> Proveedores = new List<string>();
                    Proveedores = (from item1 in _facturas.AsEnumerable()
                                   where !item1.Field<string>("Proveedor").ToLower().Contains("total")
                                   select item1.Field<string>("Proveedor")).Distinct().ToList();

                    foreach (string item in Proveedores)
                    {
                        DataRow row = _datos.NewRow();

                        row["Factura"] = string.Empty;
                        row["Proveedor"] = item + " TOTAL";
                        row["Autorizado"] = (from acum in _datos.AsEnumerable()
                                             where acum.Field<string>("Moneda") == condicion
                                                && acum.Field<string>("Proveedor") == item
                                             select acum.Field<decimal>("Autorizado")).Sum();

                        _datos.Rows.Add(row);
                    }
                    
                    _datos = (from tv in _datos.AsEnumerable()
                              orderby tv.Field<string>("Proveedor")
                              select tv).CopyToDataTable();
                    ///////////////////////

                    foreach (DataRow item in _datos.Rows)
                    {
                        if (item.Field<string>("Proveedor").ToLower().Contains("total"))
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Factura"), font8BLOD)));
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Proveedor"), font8BLOD)));
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Autorizado").ToString("C2"), font8BLOD))) { HorizontalAlignment = Element.ALIGN_CENTER };
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Cuenta"), font8BLOD)));
                            PdfTable.AddCell(PdfPCell);
                        }
                        else
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Factura"), font8)));
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Proveedor"), font8)));
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<decimal>("Autorizado").ToString("C2"), font8))) { HorizontalAlignment = Element.ALIGN_CENTER };
                            PdfTable.AddCell(PdfPCell);

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Field<string>("Cuenta"), font8)));
                            PdfTable.AddCell(PdfPCell);
                        }
                    }

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(string.Empty))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(217, 217, 217) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("TOTAL " + condicion, font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(217, 217, 217) };
                    PdfTable.AddCell(PdfPCell);

                    decimal total = Convert.ToDecimal(_datos.Compute("SUM([Autorizado])", string.Empty));

                    PdfPCell = new PdfPCell(new Phrase(new Chunk(total.ToString("C2"), font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(217, 217, 217) };
                    PdfTable.AddCell(PdfPCell);

                    PdfPCell = new PdfPCell(new Phrase(new Chunk("", font8BLODTitle))) { HorizontalAlignment = Element.ALIGN_CENTER, BackgroundColor = new BaseColor(217, 217, 217) };
                    PdfTable.AddCell(PdfPCell);

                    PdfTable.SpacingBefore = 15f;
                    doc.Add(PdfTable);
                }
                //------------------------------------------------------------------------------------
                //doc.Add(new Phrase(new Chunk("HalcoNET", textoMini)));
                //PdfTable.AddCell(PdfPCell);

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

     
    }
}
