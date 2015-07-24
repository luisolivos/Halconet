using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Cobranza
{
    public partial class frmTemplateNC : Form
    {
        DataTable _Datos = new DataTable();
        public enum Columnas
        {
            Cliente,
            Nombre,
            Sucursal,
            Currency,
            Vendedor,
            Factura,
            FechaConta,
            FechaVto,
            ImporteOriginal,
            NCDESC_Apli,
            NCDESC,
            PagosAplicados,
            Saldo,
            Col2,
            Col1,
            Cta,
            Series,
            Generar,
            Template
        }

        public frmTemplateNC()
        {
            InitializeComponent();
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Cta].Visible = false;
            dgv.Columns[(int)Columnas.Series].Visible = false;
            dgv.Columns[(int)Columnas.Col1].Visible = false;

            dgv.Columns[(int)Columnas.Col2].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)Columnas.Col2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.ImporteOriginal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.ImporteOriginal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NCDESC_Apli].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NCDESC_Apli].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.NCDESC].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.NCDESC].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PagosAplicados].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.PagosAplicados].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Cta].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cliente].ReadOnly = true;
            dgv.Columns[(int)Columnas.Nombre].ReadOnly = true;
            dgv.Columns[(int)Columnas.Currency].ReadOnly = true;
            dgv.Columns[(int)Columnas.Vendedor].ReadOnly = true;
            dgv.Columns[(int)Columnas.Factura].ReadOnly = true;
            dgv.Columns[(int)Columnas.FechaConta].ReadOnly = true;
            dgv.Columns[(int)Columnas.FechaVto].ReadOnly = true;
            dgv.Columns[(int)Columnas.ImporteOriginal].ReadOnly = true;
            dgv.Columns[(int)Columnas.NCDESC_Apli].ReadOnly = true;
            dgv.Columns[(int)Columnas.NCDESC].ReadOnly = true;
            dgv.Columns[(int)Columnas.PagosAplicados].ReadOnly = true;
            dgv.Columns[(int)Columnas.Saldo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Col2].ReadOnly = true;
            dgv.Columns[(int)Columnas.Col1].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cta].ReadOnly = true;
            dgv.Columns[(int)Columnas.Series].ReadOnly = true;
            dgv.Columns[(int)Columnas.Template].ReadOnly = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sd_NC DS = new sd_NC();
                ReportDocument docORIN = new ReportDocument();
                docORIN.Load(@"\\192.168.2.100\HalcoNET\Crystal\ORIN_Template.rpt");
                docORIN.SetDataSource(DS);

                ReportDocument docRIN1 = new ReportDocument();
                docRIN1.Load(@"\\192.168.2.100\HalcoNET\Crystal\RIN1_Template.rpt");
                docRIN1.SetDataSource(DS);
              
                if (folderBrowserDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                string path = folderBrowserDialog.SelectedPath;

                foreach (DataRow row in _Datos.Rows)
                {
                    if (row.Field<bool>("Generar"))
                    {
                        DataRow _rowAdd = DS.Tables[0].NewRow();

                        _rowAdd["Factura"] = row.Field<Int32>("Factura");
                        _rowAdd["Cliente"] = row.Field<string>("Cliente");
                        _rowAdd["MontoNC"] = row.Field<decimal>("NC Precio Especial");
                        _rowAdd["Cuenta"] = row.Field<string>("Cta");
                        _rowAdd["Moneda"] = row.Field<string>("Moneda");
                        _rowAdd["Series"] = row.Field<Int32>("Series");

                        DS.Tables[0].Rows.Add(_rowAdd);
                    }
                }

                docORIN.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.TabSeperatedText, path + "\\ORIN.txt");
                docRIN1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.TabSeperatedText, path + "\\RIN1.txt");

                foreach (DataRow row in _Datos.Rows)
                {
                    if (row.Field<bool>("Generar"))
                    {
                        using (SqlConnection connecion = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand(@"IF NOT EXISTS(select * from tbl_NCGeneradas where DocNum = @Factura) Insert into tbl_NCGeneradas VALUES(@Factura)", connecion))
                            {
                                command.Parameters.AddWithValue("@Factura", row.Field<Int32>("Factura"));
                                connecion.Open();

                                command.ExecuteNonQuery();
                            }
                        }
                        
                    }
                }

                MessageBox.Show("Listo!", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                
            }      
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                comboBox1.Enabled = true;

                _Datos.Clear();

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", connection))
                    {
                        command.Parameters.AddWithValue("@TipoConsulta", 14);
                        command.Parameters.AddWithValue("@Desde", dtpDesde.Value);
                        command.Parameters.AddWithValue("@Hasta", dtpHasta.Value);
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable table = new DataTable();

                        da.Fill(_Datos);

                        dgvDatos.DataSource = _Datos;

                        this.Formato(dgvDatos);

                        decimal total = Convert.ToDecimal(_Datos.Compute("SUM([NC Precio Especial])", string.Empty));

                        textBox1.Text = total.ToString("C2");
                    }
                }
            }
            catch (Exception)
            {

            } 
        }

        private void frmTemplateNC_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)Columnas.NCDESC_Apli].Value) != decimal.Zero)
                    {
                        item.Cells[(int)Columnas.NCDESC_Apli].Style.BackColor = Color.Yellow;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Col2].Value) > (decimal)0.25)
                    {
                        item.Cells[(int)Columnas.Col2].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Col2].Style.ForeColor = Color.White;

                    }
                }
            }
            catch (Exception)
            {
                
            }

        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int index = comboBox1.SelectedIndex;
                DataView dv = new DataView(_Datos);
                this.dgvDatos.CurrentCell = null;

                if (index == 0)
                {
                    foreach (DataGridViewRow item in dgvDatos.Rows)
                    {
                        item.Visible = true;
                    }
                    decimal total = Convert.ToDecimal(_Datos.Compute("SUM([NC Precio Especial])", string.Empty));

                    textBox1.Text = total.ToString("C2");
                }
                else if (index == 1)
                {
                    foreach (DataGridViewRow item in dgvDatos.Rows)
                    {
                        if (Convert.ToDecimal(item.Cells[(int)Columnas.NCDESC_Apli].Value) > decimal.Zero)
                            item.Visible = true;
                        else
                            item.Visible = false;

                        
                    }
                    decimal total = Convert.ToDecimal(_Datos.Compute("SUM([NC Precio Especial])", "[NC Desc (Aplicado)]>0"));

                    textBox1.Text = total.ToString("C2");
                }
                else if (index == 2)
                {
                    foreach (DataGridViewRow item in dgvDatos.Rows)
                    {
                        if (Convert.ToDecimal(item.Cells[(int)Columnas.Col2].Value) > (decimal)0.25)
                            item.Visible = true;
                        else
                            item.Visible = false;

                        //decimal total = Convert.ToDecimal(_Datos.Compute("SUM(NCReal)", "[NC Desc (Aplicado)]>0"));

                        //textBox1.Text = total.ToString("C2");
                            
                    }

                    decimal total = Convert.ToDecimal(_Datos.Compute("SUM([NC Precio Especial])", "[%]>0.25"));

                    textBox1.Text = total.ToString("C2");
                }

            }
            catch (Exception)
            {
               
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
    }
}
