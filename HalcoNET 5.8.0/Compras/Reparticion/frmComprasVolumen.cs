using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compras.Desarrollo
{
    public partial class frmLineasCompromiso : Form
    {
        public frmLineasCompromiso()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Linea,
            Articulo,
            Nombre,
            Clasificacion,
            Price,
            VIAnterior,
            VI,
            Comprar,
            Mult,
            ComprarMult,
            Stock,
            Ideal
        }


        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Price].DefaultCellStyle.Format = "N2";
            dgv.Columns[(int)Columnas.VIAnterior].DefaultCellStyle.Format = "N2";
            dgv.Columns[(int)Columnas.VI].DefaultCellStyle.Format = "N2";
            dgv.Columns[(int)Columnas.Comprar].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Mult].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.ComprarMult].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Stock].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Ideal].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)Columnas.Price].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Price].Visible = rbDinero.Checked;

            dgv.Columns[(int)Columnas.VIAnterior].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.VI].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Comprar].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mult].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.ComprarMult].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }


        public void CargarLinea(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("sp_RepartoM1", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 1);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public void CargarProveedores(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 16);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public string GetCadena(CheckedListBox cb)
        {
            StringBuilder stb = new StringBuilder();
            foreach (DataRowView item in cb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!cb.ToString().Equals(string.Empty))
                    {
                        stb.Append(",");
                    }
                    stb.Append(item["Codigo"].ToString());
                }
            }
            if (cb.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in cb.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!cb.ToString().Equals(string.Empty))
                        {
                            stb.Append(",");
                        }
                        stb.Append(item["Codigo"].ToString());
                    }
                }
            }
            return stb.ToString();
        }

        private void LineasCompromiso_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                this.CargarLinea(cbLinea, "---Seleccione una línea---");
                this.CargarProveedores(cbProveedor, "---Seleccione un proveedor---");

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_RepartoM1", connection))
                    {
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        DataTable tbl = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        da.Fill(tbl);

                        DataRow row = tbl.NewRow();
                        row["Nombre"] = "TODOS";
                        row["Codigo"] = "0";
                        tbl.Rows.InsertAt(row, 0);

                        lbAlmacenes.DataSource = tbl;
                        lbAlmacenes.ValueMember = "Codigo";
                        lbAlmacenes.DisplayMember = "Nombre";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Halconet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_RepartoM1", connection))
                    {
                        string _almacenes = this.GetCadena(lbAlmacenes);
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 2);
                        command.Parameters.AddWithValue("@Linea", cbLinea.SelectedValue);
                        command.Parameters.AddWithValue("@Proveedor", cbProveedor.SelectedValue);
                        command.Parameters.AddWithValue("@Monto", textBox1.Text == string.Empty ? "1" : textBox1.Text);
                        command.Parameters.AddWithValue("@Moneda", rbMXP.Checked == true ? "MXP" : "USD");
                        command.Parameters.AddWithValue("@TC", textBox2.Text);
                        command.Parameters.AddWithValue("@Tipo", rbDinero.Checked ? "DIN" : "PZ");
                        command.Parameters.AddWithValue("@Almacen", _almacenes);

                        //transcaction = connection.BeginTransaction("TransactionReparto"); 
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        DataTable tbl = new DataTable();
                        da.Fill(tbl);

                        dgvDatos.DataSource = tbl;
                        
                        decimal c1 = decimal.Zero;
                        decimal c2 = decimal.Zero;
                        foreach (DataGridViewRow item in dgvDatos.Rows)
                        {
                            c1 += Convert.ToDecimal(item.Cells[(int)Columnas.Comprar].Value) * Convert.ToDecimal(item.Cells[(int)Columnas.Price].Value);
                            c2 += Convert.ToDecimal(item.Cells[(int)Columnas.ComprarMult].Value) * Convert.ToDecimal(item.Cells[(int)Columnas.Mult].Value) * Convert.ToDecimal(item.Cells[(int)Columnas.Price].Value);
                        }

                        txt1.Text = c1.ToString("N2");
                        txt2.Text = c2.ToString("N2");

                        this.Formato(dgvDatos);
                    }
                }
            }
            catch (Exception ex)
            {
               

                MessageBox.Show("Error inesperado: " + ex.Message, "Halconet", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lbl.Text = Convert.ToDecimal(textBox1.Text).ToString("N2");
            }
            catch
                (Exception)
            {
                lbl.Text = Convert.ToDecimal("0").ToString("N0");
            }
                 
        }

        private void lbAlmacenes_Click(object sender, EventArgs e)
        {
            if (lbAlmacenes.SelectedIndex == 0)
            {
                if (lbAlmacenes.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < lbAlmacenes.Items.Count; item++)
                    {
                        lbAlmacenes.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < lbAlmacenes.Items.Count; item++)
                    {
                        lbAlmacenes.SetItemChecked(item, true);
                    }
                }
            }
        }
    }
}
