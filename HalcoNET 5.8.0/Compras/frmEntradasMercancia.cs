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

namespace Compras
{
    public partial class frmEntradasMercancia : Form
    {

        public enum ColumnasEncabezado
        {
            EntradaMercancia,
            Docdate,
            FechaCreacion,
            Proveedor,
            NombreProveedor,
            Moneda,
            Total,
            Saldo,
            Dias
        }

        public enum ColumnasDetalle
        {
            DocNum,
            Articulo,
            Descripcion,
            Openqty,
            Cantidad
        }

        Clases.Logs log;
        public frmEntradasMercancia()
        {
            InitializeComponent();
        }

        public DataTable Encabezado()
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 6);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable("Encabezado");
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);

            return table;
        }

        public DataTable Detalle()
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 7);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable("Detalle");
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);

            return table;
        }

        public void FormatoGridEncabezado()
        {
            dataGridView1.Columns[(int)ColumnasEncabezado.Dias].Visible = false;

            dataGridView1.Columns[(int)ColumnasEncabezado.EntradaMercancia].Width = 90;
            dataGridView1.Columns[(int)ColumnasEncabezado.Docdate].Width = 100;
            dataGridView1.Columns[(int)ColumnasEncabezado.FechaCreacion].Width = 100;
            dataGridView1.Columns[(int)ColumnasEncabezado.Proveedor].Width = 100;
            dataGridView1.Columns[(int)ColumnasEncabezado.NombreProveedor].Width = 250;
            dataGridView1.Columns[(int)ColumnasEncabezado.Moneda].Width = 60;
            dataGridView1.Columns[(int)ColumnasEncabezado.Total].Width = 100;
            dataGridView1.Columns[(int)ColumnasEncabezado.Saldo].Width = 100;

            dataGridView1.Columns[(int)ColumnasEncabezado.Total].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasEncabezado.Saldo].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)ColumnasEncabezado.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)ColumnasEncabezado.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void FormatoGridDetalle()
        {
            dataGridView2.Columns[(int)ColumnasDetalle.DocNum].Visible = false;

            dataGridView2.Columns[(int)ColumnasDetalle.Articulo].Width = 100;
            dataGridView2.Columns[(int)ColumnasDetalle.Descripcion].Width = 250;
            dataGridView2.Columns[(int)ColumnasDetalle.Openqty].Width = 100;
            dataGridView2.Columns[(int)ColumnasDetalle.Cantidad].Width = 100;

            dataGridView2.Columns[(int)ColumnasDetalle.Openqty].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns[(int)ColumnasDetalle.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView2.Columns[(int)ColumnasDetalle.Openqty].DefaultCellStyle.Format = "N0";
            dataGridView2.Columns[(int)ColumnasDetalle.Cantidad].DefaultCellStyle.Format = "N0";
        }

        private void EntradasMercancia_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                DataSet data = new DataSet();
                BindingSource masterBindingSource = new BindingSource();
                BindingSource detailsBindingSource = new BindingSource();

                data.Tables.Add(Encabezado());
                data.Tables.Add(Detalle());

                DataRelation relation = new DataRelation("RelacionDetalle", data.Tables["Encabezado"].Columns["Núm EM"], data.Tables["Detalle"].Columns["DocNum"]);

                data.Relations.Add(relation);
                masterBindingSource.DataSource = data;
                masterBindingSource.DataMember = "Encabezado";
                detailsBindingSource.DataSource = masterBindingSource;
                detailsBindingSource.DataMember = "RelacionDetalle";
                dataGridView1.DataSource = masterBindingSource;
                dataGridView2.DataSource = detailsBindingSource;

                FormatoGridEncabezado();
                FormatoGridDetalle();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                EntradasMercancia_Load(sender, e);
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                int Uno = 0;
                int Dos = 0;
                int MayorTres = 0;
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (Convert.ToInt32(item.Cells[(int)ColumnasEncabezado.Dias].Value) <= 1)
                    {
                        item.Cells[(int)ColumnasEncabezado.EntradaMercancia].Style.BackColor = Color.Green;
                        item.Cells[(int)ColumnasEncabezado.EntradaMercancia].Style.ForeColor = Color.Black;
                        Uno++;
                    }
                    else if(Convert.ToInt32(item.Cells[(int)ColumnasEncabezado.Dias].Value) == 2)
                    {
                        item.Cells[(int)ColumnasEncabezado.EntradaMercancia].Style.BackColor = Color.Yellow;
                        item.Cells[(int)ColumnasEncabezado.EntradaMercancia].Style.ForeColor = Color.Black;
                        Dos++;
                    }
                    else if (Convert.ToInt32(item.Cells[(int)ColumnasEncabezado.Dias].Value) >= 3)
                    {
                        item.Cells[(int)ColumnasEncabezado.EntradaMercancia].Style.BackColor = Color.Red;
                        item.Cells[(int)ColumnasEncabezado.EntradaMercancia].Style.ForeColor = Color.White;
                        MayorTres++;
                    }
                }
                lblUno.Text = Uno.ToString();
                lblDos.Text = Dos.ToString();
                lblTres.Text = MayorTres.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void EntradasMercancia_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void EntradasMercancia_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
            }
        }
    }
}
