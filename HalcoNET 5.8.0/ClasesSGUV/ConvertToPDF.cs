using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

//using Apitron.PDF.Rasterizer;
//using Apitron.PDF.Rasterizer.Configuration;

namespace ClasesSGUV
{
    public class ConvertToPDF
    {
        Document document = new Document(iTextSharp.text.PageSize.LETTER);

        public void Convert(string _ruta, string _source)
        {
            try
            {
                string[] files = Directory.GetFiles(_source, "*.jpeg");

                
                    document.SetMargins(5f, 0f, 5f, 0f);
                    using (var stream = new FileStream(_ruta, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfWriter.GetInstance(document, stream);
                        document.Open();
                        foreach (string item in files)
                        {

                            using (var imageStream = new FileStream(item, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                var image = iTextSharp.text.Image.GetInstance(imageStream);
                                image.ScaleToFit(iTextSharp.text.PageSize.LETTER.Width, iTextSharp.text.PageSize.LETTER.Height);
                                document.Add(image);
                                document.NewPage();
                            }

                        }
                    }
                
                
            }
            catch (Exception)
            {
            }
            finally
            {
                string[] docs = Directory.GetFiles(@"c:\temp\SGUV-IMG\", "*.jpeg");
                foreach (string f in docs)
                {
                    File.Delete(f);
                }
            }
        }

        public void ConvertToJPEG(string _ruta, string _source, int width, int height)
        {
            Bitmap bitmap =  null;
            Apitron.PDF.Rasterizer.Document document = null;
            try
            {
                
                using (FileStream fs = new FileStream(_ruta, FileMode.Open))
                {
                    document = new Apitron.PDF.Rasterizer.Document(fs);

                    Apitron.PDF.Rasterizer.Configuration.RenderingSettings settings = new Apitron.PDF.Rasterizer.Configuration.RenderingSettings();

                    Apitron.PDF.Rasterizer.Page currentPage = document.Pages[0];

                    using (bitmap = currentPage.Render(width, height, settings))
                    {
                        bitmap.Save(string.Format(@"C:\temp\SGUV-IMG\{0}.png", 0), ImageFormat.Png);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
               document.Dispose(); 
            }
        }

        public void ConvertToImage(string _rutaPDF, int width, int height)
        {
            iTextSharp.text.pdf.PdfReader reader = null;
            int currentPage = 1;
            int pageCount = 0;

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            reader = new iTextSharp.text.pdf.PdfReader(_rutaPDF);
            reader.RemoveUnusedObjects();
            pageCount = reader.NumberOfPages;
            string ext = System.IO.Path.GetExtension(_rutaPDF);
            for (int i = 1; i <= 1; i++)
            {
                iTextSharp.text.pdf.PdfReader reader1 = new iTextSharp.text.pdf.PdfReader(_rutaPDF);
                string outfile = _rutaPDF.Replace((System.IO.Path.GetFileName(_rutaPDF)), (System.IO.Path.GetFileName(_rutaPDF).Replace(".pdf", "") + "_" + i.ToString()) + ext);
                reader1.RemoveUnusedObjects();
                iTextSharp.text.Document doc = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(currentPage));
                iTextSharp.text.pdf.PdfCopy pdfCpy = new iTextSharp.text.pdf.PdfCopy(doc, new System.IO.FileStream(outfile, System.IO.FileMode.Create));
                doc.Open();
                for (int j = 1; j <= 1; j++)
                {
                    iTextSharp.text.pdf.PdfImportedPage page = pdfCpy.GetImportedPage(reader1, currentPage);
                    pdfCpy.SetFullCompression();
                    pdfCpy.AddPage(page);
                    currentPage += 1;
                }
                doc.Close();
                pdfCpy.Close();
                reader1.Close();
                reader.Close();

            }
        }

        public void convertPDF(string _ruta, List<System.Drawing.Image> imagenes)
        {
            if(imagenes.Count > 0)
            {
                Document document = new Document(iTextSharp.text.PageSize.LETTER);
                try
                {

                    // step 2:
                    // we create a writer that listens to the document
                    // and directs a PDF-stream to a file

                    PdfWriter.GetInstance(document, new FileStream(_ruta, FileMode.Create));
                    document.SetMargins(15f, 0f, 10f, 0f);

                    // step 3: we open the document
                    document.Open();

                    foreach (var image in imagenes)
                    {
                        iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                        pic.ScaleToFit(iTextSharp.text.PageSize.LETTER.Width, iTextSharp.text.PageSize.LETTER.Height);
                        document.Add(pic);
                        document.NewPage();
                    }
                }
                catch (DocumentException de)
                {
                    MessageBox.Show("Document Exception: " + de.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (IOException ioe)
                {
                    MessageBox.Show("IOException: " + ioe.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // step 5: we close the document
                document.Close();
            }
        }

        public string CreatePDF(DataGridView dgv)
        {
            string _nombre = string.Empty;

            Document doc = new Document(PageSize.LETTER, 50, 50, 25, 25);
            try
            {
                //doc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                doc.SetPageSize(iTextSharp.text.PageSize.A4);

                _nombre = Path.GetTempFileName() + ".pdf";
                PdfWriter.GetInstance(doc, new FileStream(_nombre, FileMode.OpenOrCreate));

                doc.Open();

                iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL);
                iTextSharp.text.Font fontTitle = FontFactory.GetFont("ARIAL", 15, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font font8BLOD = FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD, new BaseColor(dgv.ColumnHeadersDefaultCellStyle.ForeColor));
                iTextSharp.text.Font texto = FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD);

                PdfPTable PdfTable = new PdfPTable(dgv.Columns.Count);
                PdfPCell PdfPCell = null;
                PdfTable.HorizontalAlignment = 0;
                PdfTable.SpacingBefore = 1;
                PdfTable.SpacingAfter = 1;
                PdfTable.DefaultCell.Border = 0;
                PdfTable.WidthPercentage = 100;


                float[] Columnas = new float[dgv.Columns.Count] ;
                float _total = 0;
                for (int i = 0; i < Columnas.Count(); i++)
                {
                    _total += dgv.Columns[i].Width;
                }

                for (int i = 0; i < Columnas.Count(); i++)
                {
                    Columnas[i] = ((float)dgv.Columns[i].Width / _total) * 100;
                }

                PdfTable.SetWidths(Columnas);
                //Add Header of the pdf table

                foreach (DataGridViewColumn item in dgv.Columns)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(item.Name, font8BLOD))) { BackgroundColor = new BaseColor(dgv.ColumnHeadersDefaultCellStyle.BackColor)};
                    PdfTable.AddCell(PdfPCell);

                }
                
                //DataTable _datos = (DataTable)dgv.DataSource;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.ValueType == typeof(decimal))
                        {
                            if (dgv.Columns[cell.ColumnIndex].DefaultCellStyle.Format != null)
                            {
                                string _value = cell.Value != DBNull.Value ? System.Convert.ToDecimal(cell.Value).ToString(dgv.Columns[cell.ColumnIndex].DefaultCellStyle.Format) : string.Empty;
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(_value, font8))) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = new BaseColor(cell.Style.BackColor) };
                                Color col = row.DefaultCellStyle.BackColor;
                                PdfTable.AddCell(PdfPCell);
                            }
                            else
                            {
                                string _value = cell.Value != DBNull.Value ? System.Convert.ToDecimal(cell.Value).ToString() : string.Empty;

                                PdfPCell = new PdfPCell(new Phrase(new Chunk(_value, font8))) { HorizontalAlignment = Element.ALIGN_RIGHT, BackgroundColor = new BaseColor(cell.Style.BackColor) };
                                PdfTable.AddCell(PdfPCell);
                            }
                        }
                        else if (cell.ValueType == typeof(DateTime))
                        {
                            string _value = cell.Value != DBNull.Value ? System.Convert.ToDateTime(cell.Value).ToShortDateString() : string.Empty;

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(_value, font8))) { HorizontalAlignment = Element.ALIGN_MIDDLE, BackgroundColor = new BaseColor(cell.Style.BackColor) };
                            PdfTable.AddCell(PdfPCell);
                        }
                        else
                        {
                            string _value = cell.Value != DBNull.Value ? cell.Value.ToString() : string.Empty;

                            PdfPCell = new PdfPCell(new Phrase(new Chunk(cell.Value.ToString(), font8))) { BackgroundColor = new BaseColor(cell.Style.BackColor) };
                            PdfTable.AddCell(PdfPCell);
                        }
                    }

                }


                PdfTable.SpacingBefore = 15f;
                doc.Add(PdfTable);

                doc.Close();

            }
            catch (Exception ex)
            {
                _nombre = string.Empty;
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
