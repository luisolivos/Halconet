using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

using System.IO;

namespace Ventas
{
    public class ExportarAExcel
    {
        /// <summary>
        /// Método que exporta el DataGrid especificado a una hoja de Excel
        /// </summary>
        /// <param name="grd">DataGrid que se exportará</param>
        public bool Exportar(DataGridView grd, bool eliminarUltimo)
        {
            try
            {//DataGridView1.Columns.RemoveAt(1)
                if(eliminarUltimo)
                    grd.Columns.RemoveAt(grd.ColumnCount - 1);
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
                    
                    ColumnIndex = grd.ColumnCount;

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
                        rowIndex++;
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            if (grd.Columns[j].DefaultCellStyle.Format == "c")
                                hoja_trabajo.Cells[rowIndex, j + 1].NumberFormat = "$#,##0.00";
                            if (grd.Columns[j].DefaultCellStyle.Format == "p" || grd.Columns[j].DefaultCellStyle.Format == "p2")
                                hoja_trabajo.Cells[rowIndex, j + 1].NumberFormat = "#,0.00%";
                            hoja_trabajo.Cells[rowIndex, j + 1] = row.Cells[j].Value;
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
        /// </summary>
        /// <param name="grd">DataGrid que se exportará</param>
        public bool ExportarCobranza(DataGridView grd)
        {
            bool flag = false;
            try
            {//DataGridView1.Columns.RemoveAt(1)
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

                    ColumnIndex = grd.ColumnCount;

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
                        rowIndex++;
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            hoja_trabajo.Cells[rowIndex, j + 1] = row.Cells[j].Value;
                        }
                    }
                    hoja_trabajo.Columns.AutoFit();
                    libros_trabajo.SaveAs(fichero.FileName,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    libros_trabajo.Close(true);
                    aplicacion.Quit();
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                //libros_trabajo.Close(true);
                //aplicacion.Quit();

                MessageBox.Show("Ocurrió un error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                flag =  false;
                
            }
            return flag;
        }

        public bool ExportarSinFormato(DataGridView grd)
        {
            StreamWriter file = null; ;
            try
            {
                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Archivo de valores separados por comas de Microsoft Excel (.csv)|*.csv";
                // "Excel (*.xls)|*.xls";
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    //    MessageBox.Show("El archivo se creo con exíto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    file = new StreamWriter(fichero.FileName);

                    foreach (DataGridViewColumn item in grd.Columns)
                    {
                        file.Write(item.HeaderText + ",");
                    }
                    file.Write("\n");
                    foreach (DataGridViewRow row in grd.Rows)
                    {
                        for (int i = 0; i < grd.Columns.Count; i++)
                        {
                            try
                            {
                                file.Write(row.Cells[i].Value.ToString().Replace(',', ' ').Replace('á','a').Replace('Á', 'A').Replace('é','e').Replace('É','E').Replace('Í','I').Replace('í', 'i').Replace('Ó','O').Replace('ó','o').Replace('Ú','U').Replace('ú','u') + ",");
                            }
                            catch (Exception)
                            {
                            }
                        }
                        file.Write("\n");
                    }

                }
                return true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                file.Close();
            }
        }
    }
}
