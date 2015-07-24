using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compras.Desarrollo
{
    public partial class frmCostoVtaSucursal : Form
    {
        public frmCostoVtaSucursal()
        {
            InitializeComponent();
        }

        public enum Columnas1
        {
            Linea,
            Apizaco,
            Tepeaca,
            Cordoba,
            Monterrey,
            Saltillo,
            EdoMex,
            Guadalajara,
            Puebla
        }

        public enum Columnas2
        {
            Año,
            Mes,
            Linea,
            Apizaco,
            Tepeaca,
            Cordoba,
            Monterrey,
            Saltillo,
            EdoMex,
            Guadalajara,
            Puebla
        }

        private void CargarLinea()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 4);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODAS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            clbLinea.DataSource = table;
            clbLinea.DisplayMember = "Nombre";
            clbLinea.ValueMember = "Codigo";
        }

        public void Formato1(DataGridView dgv, string formato)
        {
            dgv.Columns[(int)Columnas1.Apizaco].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas1.Tepeaca].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas1.Cordoba].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas1.Monterrey].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas1.Saltillo].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas1.EdoMex].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas1.Guadalajara].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas1.Puebla].DefaultCellStyle.Format = formato;

            dgv.Columns[(int)Columnas1.Apizaco].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Tepeaca].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Cordoba].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Monterrey].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Saltillo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.EdoMex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Guadalajara].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas1.Puebla].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        public void Formato2(DataGridView dgv, string formato)
        {
            dgv.Columns[(int)Columnas2.Año].Width = 50;
            dgv.Columns[(int)Columnas2.Mes].Width = 60;
            
            dgv.Columns[(int)Columnas2.Linea].Visible = false;

            dgv.Columns[(int)Columnas2.Apizaco].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas2.Tepeaca].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas2.Cordoba].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas2.Monterrey].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas2.Saltillo].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas2.EdoMex].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas2.Guadalajara].DefaultCellStyle.Format = formato;
            dgv.Columns[(int)Columnas2.Puebla].DefaultCellStyle.Format = formato;

            dgv.Columns[(int)Columnas2.Apizaco].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas2.Tepeaca].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas2.Cordoba].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas2.Monterrey].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas2.Saltillo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas2.EdoMex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas2.Guadalajara].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas2.Puebla].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        public string GetCadena(CheckedListBox clb)
        {
            StringBuilder stb = new StringBuilder();
            foreach (DataRowView item in clb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clb.ToString().Equals(string.Empty))
                    {
                        stb.Append(",");
                    }
                    stb.Append(item["Codigo"].ToString());
                }
            }
            if (clb.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clb.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clb.ToString().Equals(string.Empty))
                        {
                            stb.Append(",");
                        }
                        stb.Append(item["Codigo"].ToString());
                    }
                }
            }

            return stb.ToString().Trim(',');
        }

        private void frmCostoVtaSucursal_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                this.CargarLinea();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNet", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Compras", connection))
                    {
                        string Lineas = this.GetCadena(clbLinea);
                        string Reporte = rbPesos.Checked ? "MXP" : (rbUSD.Checked ? "USD" : (rbPiezas.Checked ? "PZ" : string.Empty));
                        string Formato = rbPesos.Checked ? "C2" : (rbUSD.Checked ? "C2" : (rbPiezas.Checked ? "N0" : string.Empty));

                        command.Parameters.AddWithValue("@TipoConsulta", 22);
                        command.Parameters.AddWithValue("@FechaInical", dtpDesde.Value);
                        command.Parameters.AddWithValue("@FechaFinal", dtpHasta.Value);
                        command.Parameters.AddWithValue("@TC", txtTC.Text);
                        command.Parameters.AddWithValue("@TipoReporte", Reporte);
                        command.Parameters.AddWithValue("@Lineas", Lineas);


                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(table);

                        var query1 = from item in table.AsEnumerable()
                                     group item by new
                                     {
                                         Linea = item.Field<string>("Linea")
                                     } into grouped
                                     select new
                                     {
                                         Linea = grouped.Key.Linea,
                                         Apizaco = grouped.Sum(ix => ix.Field<decimal>("Apizaco")),
                                         Tepeaca = grouped.Sum(ix => ix.Field<decimal>("Tepeaca")),
                                         Cordoba = grouped.Sum(ix => ix.Field<decimal>("Cordoba")),
                                         Monterrey = grouped.Sum(ix => ix.Field<decimal>("Monterrey")),
                                         Saltillo = grouped.Sum(ix => ix.Field<decimal>("Saltillo")),
                                         EdoMex = grouped.Sum(ix => ix.Field<decimal>("EdoMex")),
                                         Guadalajara = grouped.Sum(ix => ix.Field<decimal>("Guadalajara")),
                                         Puebla = grouped.Sum(ix => ix.Field<decimal>("Puebla"))
                                     };

                        DataTable table1 = Cobranza.ListConverter.ToDataTable(query1.ToList());

                        var query2 = from item in table.AsEnumerable()
                                     group item by new
                                     {
                                         Año = item.Field<Int32>("Año"),
                                         MesNum = item.Field<Int32>("Mes"),
                                         Mes = item.Field<string>("MesNombre"),
                                         Linea = item.Field<string>("Linea")
                                     } into grouped
                                     select new
                                     {
                                         Año = grouped.Key.Año,
                                         Mes = grouped.Key.Mes,

                                         Linea = grouped.Key.Linea,
                                         Apizaco = grouped.Sum(ix => ix.Field<decimal>("Apizaco")),
                                         Tepeaca = grouped.Sum(ix => ix.Field<decimal>("Tepeaca")),
                                         Cordoba = grouped.Sum(ix => ix.Field<decimal>("Cordoba")),
                                         Monterrey = grouped.Sum(ix => ix.Field<decimal>("Monterrey")),
                                         Saltillo = grouped.Sum(ix => ix.Field<decimal>("Saltillo")),
                                         EdoMex = grouped.Sum(ix => ix.Field<decimal>("EdoMex")),
                                         Guadalajara = grouped.Sum(ix => ix.Field<decimal>("Guadalajara")),
                                         Puebla = grouped.Sum(ix => ix.Field<decimal>("Puebla"))
                                     };

                        DataTable table2 = Cobranza.ListConverter.ToDataTable(query2.ToList());

                        DataSet data = new DataSet();
                        BindingSource masterBindingSource = new BindingSource();
                        BindingSource detailsBindingSource = new BindingSource();

                        table1.TableName = "T1";
                        table2.TableName = "T2";

                        data.Tables.Add(table1);
                        data.Tables.Add(table2);

                        DataRelation relation = new DataRelation("FacturaDetalle", data.Tables["T1"].Columns["Linea"], data.Tables["T2"].Columns["Linea"]);
                        data.Relations.Add(relation);

                        masterBindingSource.DataSource = data;
                        masterBindingSource.DataMember = "T1";
                        detailsBindingSource.DataSource = masterBindingSource;
                        detailsBindingSource.DataMember = "FacturaDetalle";

                        dgvEncabezado.DataSource = masterBindingSource;
                        dgvDetalle.DataSource = detailsBindingSource;

                        this.Formato1(dgvEncabezado, Formato);
                        this.Formato2(dgvDetalle, Formato);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNet", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void setColorEncabezado(DataGridViewRow row)
        {
            decimal[] array = new decimal[8];

            array[0] = Convert.ToDecimal(row.Cells[1].Value);
            array[1] = Convert.ToDecimal(row.Cells[2].Value);
            array[2] = Convert.ToDecimal(row.Cells[3].Value);
            array[3] = Convert.ToDecimal(row.Cells[4].Value);
            array[4] = Convert.ToDecimal(row.Cells[5].Value);
            array[5] = Convert.ToDecimal(row.Cells[6].Value);
            array[6] = Convert.ToDecimal(row.Cells[7].Value);
            array[7] = Convert.ToDecimal(row.Cells[8].Value);

            int x = 0;
            foreach (DataGridViewCell item in row.Cells)
            {
                if (x > 0)
                {
                    if (Convert.ToDecimal(item.Value) == array.Max())
                    {
                        item.Style.BackColor = Color.Green;
                        item.Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Value) == array.Min())
                    {
                        item.Style.BackColor = Color.Red;
                        item.Style.ForeColor = Color.White;
                    }
                }
                x++;
            }
        }

        public void setColorDetalle(DataGridViewRow row)
        {
            decimal[] array = new decimal[8];

            array[0] = Convert.ToDecimal(row.Cells[3].Value);
            array[1] = Convert.ToDecimal(row.Cells[4].Value);
            array[2] = Convert.ToDecimal(row.Cells[5].Value);
            array[3] = Convert.ToDecimal(row.Cells[6].Value);
            array[4] = Convert.ToDecimal(row.Cells[7].Value);
            array[5] = Convert.ToDecimal(row.Cells[8].Value);
            array[6] = Convert.ToDecimal(row.Cells[9].Value);
            array[7] = Convert.ToDecimal(row.Cells[10].Value);

            int x = 0;
            foreach (DataGridViewCell item in row.Cells)
            {
                if (x > 2)
                {
                    if (Convert.ToDecimal(item.Value) == array.Max())
                    {
                        item.Style.BackColor = Color.Green;
                        item.Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Value) == array.Min())
                    {
                        item.Style.BackColor = Color.Red;
                        item.Style.ForeColor = Color.White;
                    }
                }
                x++;
            }
        }

        private void dgvEncabezado_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    this.setColorEncabezado(item);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void dgvDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    this.setColorDetalle(item);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
