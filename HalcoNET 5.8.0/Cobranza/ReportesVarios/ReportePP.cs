using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Cobranza
{
    public partial class ReportePP : Form
    {
        DataTable table = new DataTable();
        public ReportePP()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Cliente,
            Nombre,
            NoFactura,
            FechaFac,
            FechaCto,
            FechaPago,
            DiasTrans,
            CondicionPago,
            Evaluacion,
            TotalFactura,
            DEP,
            PPP,
            DEAplicado,
            PPAplicado,
            NoNCAplicada
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.DiasTrans].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[(int)Columnas.TotalFactura].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.DEAplicado].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.PPAplicado].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.TotalFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.DEAplicado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PPAplicado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.DEP].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)Columnas.PPP].DefaultCellStyle.Format = "P2";

            dgv.Columns[(int)Columnas.DEP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PPP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.NoNCAplicada].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "PJ_Comisiones";
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 6);
                        command.Parameters.AddWithValue("@Desde", dtFecha1.Value.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Hasta", dtFecha2.Value.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@Estatus", txtCliente.Text);

                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            da.SelectCommand = command;

                            table.Rows.Clear();
                           

                            da.Fill(table);

                            gridFacturas.DataSource = table;

                            decimal totalPP = Convert.ToDecimal(table.Compute("Sum([PP Aplicado])", string.Empty) == DBNull.Value ? decimal.Zero : table.Compute("Sum([PP Aplicado])", string.Empty));
                            decimal totalNoValido = Convert.ToDecimal(table.Compute("Sum([PP Aplicado])", "[Evaluación] = 'PP no valido'") == DBNull.Value ? decimal.Zero : table.Compute("Sum([PP Aplicado])", "[Evaluación] = 'PP no valido'"));
                            decimal sinPP = Convert.ToDecimal(table.Compute("Sum([Total factura])", "[Evaluación] = 'SIN PP'") == DBNull.Value ? decimal.Zero : table.Compute("Sum([Total factura])", "[Evaluación] = 'SIN PP'")) * (decimal)0.05;

                            txtNCPP.Text = totalPP.ToString("C2");
                            txtNCNoValido.Text = totalNoValido.ToString("C2");
                            txtNCNoAplicado.Text = sinPP.ToString("C2");
                            
                            this.Formato(gridFacturas);
                            
                            checkBox1.Visible = true;
                            checkBox2.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReportePP_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            dtFecha1.Value = new DateTime(DateTime.Now.AddYears(0).Year, DateTime.Now.AddMonths(0).Month, 1);
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            try
            {  
                DataView dv = new DataView(table);
               // DataTable t = (gridFacturas.DataSource as DataTable);
                if (checkBox1.Checked && !checkBox2.Checked)
                {
                  
                    if (checkBox1.Checked)
                    {
                        dv.RowFilter = "[% PP] > 0.05";
                        gridFacturas.DataSource = dv.ToTable();
                    }


                    decimal totalPP = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([PP Aplicado])", string.Empty) == DBNull.Value ? decimal.Zero : table.Compute("Sum([PP Aplicado])", string.Empty));
                    decimal totalNoValido = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([PP Aplicado])", "[Evaluación] = 'PP no valido'") == DBNull.Value ? decimal.Zero : table.Compute("Sum([PP Aplicado])", "[Evaluación] = 'PP no valido'"));
                    decimal sinPP = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([Total factura])", "[Evaluación] = 'SIN PP'") == DBNull.Value ? decimal.Zero : table.Compute("Sum([Total factura])", "[Evaluación] = 'SIN PP'")) * (decimal)0.05;

                    txtNCPP.Text = totalPP.ToString("C2");
                    txtNCNoValido.Text = totalNoValido.ToString("C2");
                    txtNCNoAplicado.Text = sinPP.ToString("C2");
                }

                if (!checkBox1.Checked && checkBox2.Checked)
                {

                    if (checkBox2.Checked)
                    {
                        dv.RowFilter = "[Evaluación] = 'PP no valido'";
                        gridFacturas.DataSource = dv.ToTable();
                    }


                    decimal totalPP = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([PP Aplicado])", string.Empty) == DBNull.Value ? decimal.Zero : table.Compute("Sum([PP Aplicado])", string.Empty));
                    decimal totalNoValido = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([PP Aplicado])", "[Evaluación] = 'PP no valido'") == DBNull.Value ? decimal.Zero : table.Compute("Sum([PP Aplicado])", "[Evaluación] = 'PP no valido'"));
                    decimal sinPP = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([Total factura])", "[Evaluación] = 'SIN PP'") == DBNull.Value ? decimal.Zero : table.Compute("Sum([Total factura])", "[Evaluación] = 'SIN PP'")) * (decimal)0.05;

                    txtNCPP.Text = totalPP.ToString("C2");
                    txtNCNoValido.Text = totalNoValido.ToString("C2");
                    txtNCNoAplicado.Text = sinPP.ToString("C2");
                }

                if (checkBox1.Checked && checkBox2.Checked)
                {

                    if (checkBox1.Checked &&  checkBox2.Checked)
                    {
                        dv.RowFilter = "[Evaluación] = 'PP no valido' AND [% PP] > 0.05";
                        gridFacturas.DataSource = dv.ToTable();
                    }

                    decimal totalPP = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([PP Aplicado])", string.Empty) == DBNull.Value ? decimal.Zero : table.Compute("Sum([PP Aplicado])", string.Empty));
                    decimal totalNoValido = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([PP Aplicado])", "[Evaluación] = 'PP no valido'") == DBNull.Value ? decimal.Zero : table.Compute("Sum([PP Aplicado])", "[Evaluación] = 'PP no valido'"));
                    decimal sinPP = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([Total factura])", "[Evaluación] = 'SIN PP'") == DBNull.Value ? decimal.Zero : table.Compute("Sum([Total factura])", "[Evaluación] = 'SIN PP'")) * (decimal)0.05;

                    txtNCPP.Text = totalPP.ToString("C2");
                    txtNCNoValido.Text = totalNoValido.ToString("C2");
                    txtNCNoAplicado.Text = sinPP.ToString("C2");
                }

                if (!checkBox1.Checked && !checkBox2.Checked)
                {
                    gridFacturas.DataSource = dv.Table;

                    decimal totalPP = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([PP Aplicado])", string.Empty) == DBNull.Value ? decimal.Zero : table.Compute("Sum([PP Aplicado])", string.Empty));
                    decimal totalNoValido = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([PP Aplicado])", "[Evaluación] = 'PP no valido'") == DBNull.Value ? decimal.Zero : table.Compute("Sum([PP Aplicado])", "[Evaluación] = 'PP no valido'"));
                    decimal sinPP = Convert.ToDecimal((gridFacturas.DataSource as DataTable).Compute("Sum([Total factura])", "[Evaluación] = 'SIN PP'") == DBNull.Value ? decimal.Zero : table.Compute("Sum([Total factura])", "[Evaluación] = 'SIN PP'")) * (decimal)0.05;

                    txtNCPP.Text = totalPP.ToString("C2");
                    txtNCNoValido.Text = totalNoValido.ToString("C2");
                    txtNCNoAplicado.Text = sinPP.ToString("C2");
                }

            }
            catch (Exception)
            {
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog fichero = new SaveFileDialog();
                string[] encabezado;
                fichero.Filter = "Libro de Excel(.xls)|*.xls";

                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    encabezado = new string[gridFacturas.Rows.Count + 2];
                    int x = 0;
                    foreach (DataGridViewColumn item in gridFacturas.Columns)
                    {
                        //x++;
                        //encabezado[0] += "["+x.ToString() + "]\t";
                        encabezado[0] += item.HeaderText + "\t";
                    }
                    x = 1;
                    foreach (DataGridViewRow row in gridFacturas.Rows)
                    {
                        for (int i = 0; i < gridFacturas.Columns.Count; i++)
                        {
                            encabezado[x] += row.Cells[i].Value.ToString().Replace('\t', ' ') + "\t";
                        }
                        x++;
                    }

                    //System.IO.File.WriteAllLines(System.IO.Path.GetDirectoryName(fichero.FileName) + @"\" + System.IO.Path.GetFileNameWithoutExtension(fichero.FileName) + ".xls", encabezado, Encoding.GetEncoding("iso-8859-15"));
                    System.IO.File.WriteAllLines(fichero.FileName, encabezado, Encoding.GetEncoding("iso-8859-15"));

                    MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }

        }
    }
}
