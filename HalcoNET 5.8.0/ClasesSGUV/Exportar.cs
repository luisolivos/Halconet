using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace ClasesSGUV
{
    public class Exportar
    {
        public bool ExportarSinFormato(DataGridView grd)
        {
            try
            {
                SaveFileDialog fichero = new SaveFileDialog();
                string[] encabezado;
                fichero.Filter = "Libro de Excel(.xls)|*.xls";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    encabezado = new string[grd.Rows.Count + 2];
                    int x = 0;
                    foreach (DataGridViewColumn item in grd.Columns)
                    {
                        //x++;
                        //encabezado[0] += "["+x.ToString() + "]\t";
                        encabezado[0] += item.HeaderText + "\t";
                    }
                    x = 1;
                    foreach (DataGridViewRow row in grd.Rows)
                    {
                        for (int i = 0; i < grd.Columns.Count; i++)
                        {
                            encabezado[x] += row.Cells[i].Value == null ? string.Empty : row.Cells[i].Value.ToString().Replace('\t', ' ') + "\t";
                        }
                        x++;
                    }

                    //System.IO.File.WriteAllLines(System.IO.Path.GetDirectoryName(fichero.FileName) + @"\" + System.IO.Path.GetFileNameWithoutExtension(fichero.FileName) + ".xls", encabezado, Encoding.GetEncoding("iso-8859-15"));
                    System.IO.File.WriteAllLines(fichero.FileName, encabezado, Encoding.GetEncoding("iso-8859-15"));

                    MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            catch (Exception ex)
            {
                var dialogTypeName = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg";
                var dialogType = typeof(Form).Assembly.GetType(dialogTypeName);

                var dialog = (Form)Activator.CreateInstance(dialogType, new PropertyGrid());

                dialog.Text = "Error inesperado: ";
                dialogType.GetProperty("Details").SetValue(dialog, ex.Message, null);
                dialogType.GetProperty("Message").SetValue(dialog, "Error inesperado", null);

                var result = dialog.ShowDialog();

                return false;
            }
            finally
            {
            }
        }

        public bool ExportarColores(DataGridView grd)
        {
            try
            {
                bool okFile = false;
                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xls)|*.xls";
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application aplicacion;
                    Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                    Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo;
                    aplicacion = new Microsoft.Office.Interop.Excel.Application();
                    libros_trabajo = aplicacion.Workbooks.Add();
                    hoja_trabajo =
                        (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

                    int ColumnIndex = 0;

                    foreach (DataGridViewColumn col in grd.Columns)
                    {
                        if (col.Visible)
                        {
                            ColumnIndex++;

                            hoja_trabajo.Cells[1, ColumnIndex] = col.HeaderText;
                        }
                    }

                    //ColumnIndex = grd.ColumnCount;

                    string LetraColumna = string.Empty;
                    switch (ColumnIndex)
                    {
                        case 1:
                            LetraColumna = "A";
                            break;
                        case 2:
                            LetraColumna = "B";
                            break;
                        case 3:
                            LetraColumna = "C";
                            break;
                        case 4:
                            LetraColumna = "D";
                            break;
                        case 5:
                            LetraColumna = "E";
                            break;
                        case 6:
                            LetraColumna = "F";
                            break;
                        case 7:
                            LetraColumna = "G";
                            break;
                        case 8:
                            LetraColumna = "H";
                            break;
                        case 9:
                            LetraColumna = "I";
                            break;
                        case 10:
                            LetraColumna = "J";
                            break;
                        case 11:
                            LetraColumna = "K";
                            break;
                        case 12:
                            LetraColumna = "L";
                            break;
                        case 13:
                            LetraColumna = "M";
                            break;
                        case 14:
                            LetraColumna = "N";
                            break;
                        case 15:
                            LetraColumna = "O";
                            break;
                        case 16:
                            LetraColumna = "P";
                            break;
                        case 17:
                            LetraColumna = "Q";
                            break;
                        case 18:
                            LetraColumna = "R";
                            break;
                        case 19:
                            LetraColumna = "S";
                            break;
                        case 20:
                            LetraColumna = "T";
                            break;
                        case 21:
                            LetraColumna = "U";
                            break;
                        case 22:
                            LetraColumna = "V";
                            break;
                        case 23:
                            LetraColumna = "W";
                            break;
                    }

                    Microsoft.Office.Interop.Excel.Range rango = hoja_trabajo.get_Range("A1", LetraColumna + "1");
                    rango.Font.Bold = true;
                    rango.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rango.Interior.ColorIndex = 15;

                    int rowIndex = 1;

                    foreach (DataGridViewRow row in grd.Rows)
                    {
                        if (row.Visible)
                        {
                            int Column = 0;
                            rowIndex++;
                            for (int j = 0; j < row.Cells.Count; j++)
                            {
                                if (grd.Columns[j].Visible)
                                {
                                    if (grd.Columns[Column].DefaultCellStyle.Format == "C" || grd.Columns[Column].DefaultCellStyle.Format == "C2")
                                        hoja_trabajo.Cells[rowIndex, Column + 1].NumberFormat = "$#,##0.00";
                                    else if (grd.Columns[Column].DefaultCellStyle.Format == "P0" || grd.Columns[Column].DefaultCellStyle.Format == "P2")
                                        hoja_trabajo.Cells[rowIndex, j + 1].NumberFormat = "0.00%";
                                    else
                                    {
                                        hoja_trabajo.Cells[rowIndex, Column + 1].NumberFormat = "@";

                                    }
                                    if (row.Cells[j].Style.BackColor.Name == "0")
                                        hoja_trabajo.Cells[rowIndex, Column + 1].Interior.Color = Color.White;
                                    else
                                        hoja_trabajo.Cells[rowIndex, Column + 1].Interior.Color = row.Cells[j].Style.BackColor;//ColorTranslator.ToOle(row.Cells[rowIndex - 1].Style.BackColor);
                                    if (row.Cells[j].Style.BackColor == Color.Red)
                                        hoja_trabajo.Cells[rowIndex, Column + 1].Font.Color = Color.White;
                                    hoja_trabajo.Cells[rowIndex, Column + 1] = row.Cells[j].Value;

                                    Column++;
                                }
                            }
                        }
                    }

                    hoja_trabajo.Columns.AutoFit();
                    libros_trabajo.SaveAs(fichero.FileName,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    libros_trabajo.Close(true);
                    aplicacion.Quit();
                    okFile = true;
                    return okFile;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
