using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;

namespace Presupuesto
{
    public class ExportarAExcel
    {
        /// <summary>
        /// Método que exporta el DataGrid especificado a una hoja de Excel
        /// </summary>
        /// <param name="grd">DataGrid que se exportará</param>
        /// <param name="Columnas">Nmero de columnas qe no se tomaran en cuenta para la exportación</param>
        /// /// <returns>True si se realizó correctamente, false si no</returns>
        public bool Exportar(DataGridView grd)
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
                        ColumnIndex++;
                        hoja_trabajo.Cells[1, ColumnIndex] = col.HeaderText;
                    }

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
                    }


                    Microsoft.Office.Interop.Excel.Range rango = hoja_trabajo.get_Range("A1", LetraColumna + "1");
                    rango.Font.Bold = true;
                    rango.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rango.Interior.ColorIndex = 15;

                    int rowIndex = 1;
                    foreach (DataGridViewRow row in grd.Rows)
                    {
                        
                        //hoja_trabajo.Range.RowHeight = 100;
                        rowIndex++;
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            string comentarios = "";
                            if (j == 7)
                            {//Comentario.Replace((char)(13), ' ').Trim()
                                string aunx = Convert.ToString(row.Cells[j].Value).Replace((char)(13), '|').Replace((char)(10), ' ').Trim().Replace("   ", "|") + "|";
                                //Convert.ToString(row.Cells[j].Value).Replace((char)(13), ' ').Replace((char)(10), ' ').Trim().Replace("   ", "|") + "|" == "PUEBLA:|APIZACO:|CORDOBA:|GDL:|EDOMEX:|TEPEACA:|MTY:|"
                                if (Convert.ToString(row.Cells[j].Value).Replace((char)(13), '|').Replace((char)(10), ' ').Trim().Replace("   ", "|") + "|" == "PUEBLA:|APIZACO:|CORDOBA:|GDL:|EDOMEX:|TEPEACA:|MTY:|GLOBAL:||")
                                {
                                    hoja_trabajo.Cells[rowIndex, j + 1] = "-";
                                }
                                else
                                {
                                    char[] delimitadores = { '|' };
                                    string[] words = (Convert.ToString(row.Cells[j].Value).Replace((char)(13), '|').Replace((char)(10), ' ').Trim().Replace("   ", "|") + "|").Split(delimitadores);
                                    //foreach (string item in words)
                                   // {
                                    try
                                    {
                                        if (!words[0].Trim().Equals("PUEBLA:"))
                                            comentarios += words[0];
                                        if (!words[1].Trim().Equals("APIZACO:"))
                                            comentarios += words[1];
                                        if (!words[2].Trim().Equals("CORDOBA:"))
                                            comentarios += words[2];
                                        if (!words[3].Trim().Equals("GDL:"))
                                            comentarios += words[3];
                                        if (!words[4].Trim().Equals("EDOMEX:"))
                                            comentarios += words[4];
                                        if (!words[5].Trim().Equals("TEPEACA:"))
                                            comentarios += words[5];
                                        if (!words[6].Trim().Equals("MTY:"))
                                            comentarios += words[6];
                                        if (!words[7].Trim().Equals("GLOBAL:"))
                                            comentarios += words[7];
                                    }catch(Exception){}


                                    hoja_trabajo.Cells[rowIndex, j + 1] = comentarios; // Convert.ToString(row.Cells[j].Value).Replace((char)(13), ' ').Replace((char)(10), ' ').Trim();
                                }
                            }
                            else
                                hoja_trabajo.Cells[rowIndex, j + 1] = row.Cells[j].Value;
                        }
                    }
                    rango.RowHeight = 20;
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
        
        /// <summary>
        /// Método que exporta 2 DataGrids a una misma hoja de Excel
        /// </summary>
        /// <param name="grd1">DataGrid 1</param>
        /// <param name="grd2">DataGrid 2</param>
        /// <returns>True si se realizó correctamente, false si no</returns>
        public bool Exportar2Grids(DataGridView grd1, DataGridView grd2)
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
                    hoja_trabajo = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

                    //DataGrid 1
                    int ColumnIndex = 0;
                    foreach (DataGridViewColumn col in grd1.Columns)
                    {
                        ColumnIndex++;
                        hoja_trabajo.Cells[1, ColumnIndex] = col.HeaderText;
                    }

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
                    }

                    Microsoft.Office.Interop.Excel.Range rango = hoja_trabajo.get_Range("A1", LetraColumna + "1");
                    rango.Font.Bold = true;
                    rango.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rango.Interior.ColorIndex = 15;

                    int rowIndex = 1;
                    foreach (DataGridViewRow row in grd1.Rows)
                    {
                        rowIndex++;
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            hoja_trabajo.Cells[rowIndex, j + 1] = row.Cells[j].Value;
                        }
                    }

                    //DataGrid 2
                    int ColumnIndex2 = 0;
                    int rowIndex2 = rowIndex + 2;
                    foreach (DataGridViewColumn col in grd2.Columns)
                    {
                        ColumnIndex2++;
                        hoja_trabajo.Cells[rowIndex2, ColumnIndex2] = col.HeaderText;
                    }

                    string LetraColumna2 = string.Empty;
                    switch (ColumnIndex2)
                    {
                        case 1:
                            LetraColumna2 = "A";
                            break;
                        case 2:
                            LetraColumna2 = "B";
                            break;
                        case 3:
                            LetraColumna2 = "C";
                            break;
                        case 4:
                            LetraColumna2 = "D";
                            break;
                        case 5:
                            LetraColumna2 = "E";
                            break;
                        case 6:
                            LetraColumna2 = "F";
                            break;
                        case 7:
                            LetraColumna2 = "G";
                            break;
                        case 8:
                            LetraColumna2 = "H";
                            break;
                        case 9:
                            LetraColumna2 = "I";
                            break;
                        case 10:
                            LetraColumna2 = "J";
                            break;
                        case 11:
                            LetraColumna2 = "K";
                            break;
                        case 12:
                            LetraColumna2 = "L";
                            break;
                        case 13:
                            LetraColumna2 = "M";
                            break;
                        case 14:
                            LetraColumna2 = "N";
                            break;
                        case 15:
                            LetraColumna2 = "O";
                            break;
                        case 16:
                            LetraColumna2 = "P";
                            break;
                    }

                    Microsoft.Office.Interop.Excel.Range rango2 = hoja_trabajo.get_Range("A" + rowIndex2.ToString(), LetraColumna2 + rowIndex2.ToString());
                    rango2.Font.Bold = true;
                    rango2.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rango2.Interior.ColorIndex = 15;
                    
                    foreach (DataGridViewRow row in grd2.Rows)
                    {
                        rowIndex2++;
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            hoja_trabajo.Cells[rowIndex2, j + 1] = row.Cells[j].Value;
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


        /// <summary>
        /// Método que exporta el DataGrid especificado a una hoja de Excel
        /// Exporta las columnas correspondientes unicamente al numero de días de corte
        /// </summary>
        /// <param name="grd">DataGrid que se exportará</param>
        /// <param name="Columnas">Nmero de columnas qe no se tomaran en cuenta para la exportación</param>
        /// /// <returns>True si se realizó correctamente, false si no</returns>
        public bool ExportarFlujo(DataGridView grd)
        {
            Microsoft.Office.Interop.Excel.Application aplicacion = null; ;
            Microsoft.Office.Interop.Excel.Workbook libros_trabajo = null; ;
            Microsoft.Office.Interop.Excel.Worksheet hoja_trabajo = null; ;
            try
            {
               
                bool okFile = false;
                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xls)|*.xls";
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    

                    aplicacion = new Microsoft.Office.Interop.Excel.Application();
                    libros_trabajo = aplicacion.Workbooks.Add();
                    hoja_trabajo =
                        (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);

                    int ColumnIndex = 0;
                    for (int j = 1; j < grd.Columns.Count; j++)
                    {
                        if (grd.Columns[j].Visible != false)
                        {
                            ColumnIndex++;
                            hoja_trabajo.Cells[1, ColumnIndex] = grd.Columns[j].HeaderText;
                        }
                    }

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
                    }


                    Microsoft.Office.Interop.Excel.Range rango = hoja_trabajo.get_Range("A1", LetraColumna + "1");
                    rango.Font.Bold = true;
                    rango.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    rango.Interior.ColorIndex = 15;

                    //rango1.Interior.ColorIndex = 15;

                    // grd.Columns.Remove("TipoColumna");
                    int rowIndex = 1;
                    int columnIndex = 0;
                    foreach (DataGridViewRow row in grd.Rows)
                    {
                        if (row.Cells[0].Value.ToString() != "3")
                        {
                            columnIndex = 0;
                            //hoja_trabajo.Range.RowHeight = 100;
                            rowIndex++;

                            for (int j = 0; j < row.Cells.Count; j++)
                            {
                                if (grd.Columns[j].Visible != false)
                                {
                                    columnIndex++;
                                    if (row.Cells[j].Value.GetType() == typeof(decimal))
                                        hoja_trabajo.Cells[rowIndex, columnIndex] = decimal.Round(Decimal.Parse(row.Cells[j].Value.ToString()), 2);
                                    else
                                        hoja_trabajo.Cells[rowIndex, columnIndex] = row.Cells[j].Value;
                                }
                            }

                        }
                    }
                    rango.RowHeight = 20;
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
            finally
            {
                
            }
        }


        public bool ExportarFormato(DataGridView grd)
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
                                Color backcolor = row.DefaultCellStyle.BackColor;
                                Color forecolor = row.DefaultCellStyle.ForeColor;

                                if (row.DefaultCellStyle.BackColor.Name == "0")
                                {
                                    backcolor = Color.White;
                                }
                                else
                                    backcolor = row.DefaultCellStyle.BackColor;

                                if (grd.Columns[j].Visible)
                                {
                                    if (grd.Columns[Column].DefaultCellStyle.Format == "C" || grd.Columns[Column].DefaultCellStyle.Format == "C2" || grd.Columns[Column].DefaultCellStyle.Format == "C0")
                                        hoja_trabajo.Cells[rowIndex, Column + 1].NumberFormat = "$#,##0.00";
                                    else if (grd.Columns[Column].DefaultCellStyle.Format == "P0" || grd.Columns[Column].DefaultCellStyle.Format == "P2")
                                        hoja_trabajo.Cells[rowIndex, j + 1].NumberFormat = "0.00%";
                                    else
                                    {
                                        hoja_trabajo.Cells[rowIndex, Column + 1].NumberFormat = "@";
                                    }


                                    hoja_trabajo.Cells[rowIndex, Column + 1].Interior.Color = backcolor;//ColorTranslator.ToOle(row.Cells[rowIndex - 1].Style.BackColor);
                                   
                                   // if (row.DefaultCellStyle.BackColor == Color.Red)
                                     //   hoja_trabajo.Cells[rowIndex, Column + 1].Font.Color = Color.White;

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
