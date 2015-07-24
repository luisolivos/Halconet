using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Compras
{
    public partial class frmIdealxLinea : Form
    {
        Clases.Logs log;
        public enum Columnas
        {
            Linea, 
            Stock,
            Ideal,
            Diferencia,
            Ratio
        }

        public frmIdealxLinea()
        {
            InitializeComponent();
        }

        private void IdealxLinea_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            this.MaximizeBox = false;
            CargarLinea(clbLinea, "Todas");
        }

        public void CargarLinea(CheckedListBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 2);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

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

        private void clbSucursal_Click(object sender, EventArgs e)
        {
            if (clbLinea.SelectedIndex == 0)
            {
                if (clbLinea.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbLinea.Items.Count; item++)
                    {
                        clbLinea.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbLinea.Items.Count; item++)
                    {
                        clbLinea.SetItemChecked(item, true);
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _opcion = "";
            string _formato = "";
            string stock = "";
            string ideal = "";
            string diferencia = "";
            if (rbDolares.Checked)
            {
                _opcion = "USD";
                _formato = "C2";

                stock = "[Stock (USD)]";
                ideal = "[Ideal (USD)]";
                diferencia = "[Diferencia (USD)]";
            }
            if (rbPesos.Checked)
            {
                _opcion = "Pesos";
                _formato = "C2";

                stock = "[Stock ($)]";
                ideal = "[Ideal ($)]";
                diferencia = "[Diferencia ($)]";
            }
            if (rbJuegos.Checked)
            {
                _opcion = "Juegos";
                _formato = "N0";

                stock = "[Stock (JUEGOS)]";
                ideal = "[Ideal (JUEGOS)]";
                diferencia = "[Diferencia (JUEGOS)]";
            }
            if (rbPiezas.Checked)
            {
                _opcion = "Piezas";
                _formato = "N0";

                stock = "[Stock (PZ)]";
                ideal = "[Ideal (PZ)]";
                diferencia = "[Diferencia (PZ)]";
            }

            string Lineas = GetCadena(clbLinea);

            SqlCommand command = new SqlCommand("PJ_IdealLinea", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 1);
            command.Parameters.AddWithValue("@Lineas", Lineas);
            command.Parameters.AddWithValue("@Opcion", _opcion);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            gridExceso.DataSource = table;
            Formato(_formato);

            DataTable _t = new DataTable();
            _t.Columns.Add("Stock", typeof(decimal));
            _t.Columns.Add("Ideal", typeof(decimal));
            _t.Columns.Add("Diferencia", typeof(decimal));

            if (table.Columns.Count > 0)
            {
                DataRow row = _t.NewRow();
                row["Stock"] = table.Compute("Sum(" + stock + ")", "");
                row["Ideal"] = table.Compute("Sum(" + ideal + ")", "");
                row["Diferencia"] = table.Compute("Sum(" + diferencia + ")", "");
                _t.Rows.Add(row);
                
            }
            dataGridView1.DataSource = _t;
            Formato(_formato, _opcion);
            //_r["Col5"] = datos.Compute("Sum([>120])", "[>120] <> 0");
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

        public void Formato(string _formato)
        {
            gridExceso.Columns[(int)Columnas.Linea].Width = 100;
            gridExceso.Columns[(int)Columnas.Stock].Width = 100;
            gridExceso.Columns[(int)Columnas.Ideal].Width = 100;
            gridExceso.Columns[(int)Columnas.Diferencia].Width = 100;
            gridExceso.Columns[(int)Columnas.Ratio].Width = 100;

            gridExceso.Columns[(int)Columnas.Stock].DefaultCellStyle.Format = _formato;
            gridExceso.Columns[(int)Columnas.Ideal].DefaultCellStyle.Format = _formato;
            gridExceso.Columns[(int)Columnas.Diferencia].DefaultCellStyle.Format = _formato;
            gridExceso.Columns[(int)Columnas.Ratio].DefaultCellStyle.Format = "N2";

            gridExceso.Columns[(int)Columnas.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)Columnas.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)Columnas.Diferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridExceso.Columns[(int)Columnas.Ratio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void Formato(string _formato, string _opcion)
        {
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;

            dataGridView1.Columns[0].DefaultCellStyle.Format = _formato;
            dataGridView1.Columns[1].DefaultCellStyle.Format = _formato;
            dataGridView1.Columns[2].DefaultCellStyle.Format = _formato;

            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[0].HeaderText = "Stock (" + _opcion + ")";
            dataGridView1.Columns[1].HeaderText = "Ideal (" + _opcion + ")";
            dataGridView1.Columns[2].HeaderText = "Diferencia (" + _opcion + ")";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rbPiezas.Checked = true;

            gridExceso.DataSource = null;
            dataGridView1.DataSource = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ExportarAExcel ex = new ExportarAExcel();
            if (ex.Exportar(gridExceso))
                MessageBox.Show("El archivo se creo con exíto.");
        }

        private void IdealxLinea_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void IdealxLinea_FormClosing(object sender, FormClosingEventArgs e)
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

