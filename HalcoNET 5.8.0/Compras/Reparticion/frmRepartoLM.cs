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
    public partial class frmRepartoLM : Form
    {
        public enum Columas
        {
            Linea,
            Articulo,
            Descripcion,
            IdealZcentro,
            StockZCentro,
            ZCentro,
            IdealZNorte,
            StockZNorte,
            ZNorte,
            IdealGDL,
            StocGDL,
            GDL
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columas.Linea].Width = 75;

            dgv.Columns[(int)Columas.IdealZcentro].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.StockZCentro].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.ZCentro].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.IdealZNorte].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.StockZNorte].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.ZNorte].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.IdealGDL].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.StocGDL].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columas.GDL].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)Columas.IdealZcentro].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.StockZCentro].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.ZCentro].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.IdealZNorte].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.StockZNorte].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.ZNorte].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.IdealGDL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.StocGDL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columas.GDL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public frmRepartoLM()
        {
            InitializeComponent();
        }

        public void CargarLinea(CheckedListBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("sp_Importacion", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 2);

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

            _cb.SelectedValue = 0;
        }

        public void CargarCompradores(CheckedListBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("sp_Importacion", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
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

            _cb.SelectedValue = 0;
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

        private void frmTraspasosImpo_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            this.CargarLinea(clbLineas, "TODOS");
            this.CargarCompradores(clbCompradores, "TODOS");
            cbZonas.SelectedIndex = 0;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtArticulo.Text.Trim() != string.Empty || cbLinea.SelectedValue.ToString() != "0" || cbProveedor.SelectedValue.ToString() != "0")
                //{
                string _compradores = string.Empty;
                _compradores = "'" + this.GetCadena(clbCompradores).Trim(',').Replace(",", "','") + "'";
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_Importacion", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 8);
                        command.Parameters.AddWithValue("@Zona", cbZonas.Text);
                        command.Parameters.AddWithValue("@Articulo", txtArticulo.Text);
                        command.Parameters.AddWithValue("@Linea", this.GetCadena(clbLineas).Trim(','));
                        command.Parameters.AddWithValue("@Compradores", _compradores);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        DataTable table = new DataTable();
                        da.Fill(table);

                        dgvDatos.DataSource = table;

                        this.Formato(dgvDatos);
                    }
                }
                //}
                //else
                //{
                //    MessageBox.Show("Seleccione una Linea, Proveedor o ingrese un Artículo", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)Columas.ZCentro].Value) < 0)
                    {
                        item.Cells[(int)Columas.ZCentro].Style.BackColor = Color.Red;
                        item.Cells[(int)Columas.ZCentro].Style.ForeColor = Color.White;
                    }
                    if (Convert.ToDecimal(item.Cells[(int)Columas.ZCentro].Value) > 0)
                    {
                        item.Cells[(int)Columas.ZCentro].Style.BackColor = Color.Green;
                        item.Cells[(int)Columas.ZCentro].Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columas.ZNorte].Value) < 0)
                    {
                        item.Cells[(int)Columas.ZNorte].Style.BackColor = Color.Red;
                        item.Cells[(int)Columas.ZNorte].Style.ForeColor = Color.White;
                    }
                    if (Convert.ToDecimal(item.Cells[(int)Columas.ZNorte].Value) > 0)
                    {
                        item.Cells[(int)Columas.ZNorte].Style.BackColor = Color.Green;
                        item.Cells[(int)Columas.ZNorte].Style.ForeColor = Color.Black;
                    }

                    if (Convert.ToDecimal(item.Cells[(int)Columas.GDL].Value) < 0)
                    {
                        item.Cells[(int)Columas.GDL].Style.BackColor = Color.Red;
                        item.Cells[(int)Columas.GDL].Style.ForeColor = Color.White;
                    }
                    if (Convert.ToDecimal(item.Cells[(int)Columas.GDL].Value) > 0)
                    {
                        item.Cells[(int)Columas.GDL].Style.BackColor = Color.Green;
                        item.Cells[(int)Columas.GDL].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel exp = new ExportarAExcel();
            exp.ExportarSinFormato(dgvDatos);
        }
    }
}
