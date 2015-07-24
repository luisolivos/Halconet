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
    public partial class SeguimientoCompras : Form
    {
        public string Articulo;
        public DateTime FechaInicial;
        public DateTime FechaFinal;
        public string Almacen;
        public string Tipo;
        Clases.Logs log;
        public enum Columnas
        {
            Fecha, 
            Articulo, 
            Nombre, 
            Tipo, 
            Precio, 
            Cantidad,
            Stock,
            Total, 
            Almacen, 
            Responsable, 
            Comentarios, 
            Dias
        }


        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Fecha].Width = 90;
            dgv.Columns[(int)Columnas.Articulo].Width = 90;
            dgv.Columns[(int)Columnas.Nombre].Width = 200;
            dgv.Columns[(int)Columnas.Precio].Width = 100;
            dgv.Columns[(int)Columnas.Cantidad].Width = 100;
            dgv.Columns[(int)Columnas.Total].Width = 100;
            dgv.Columns[(int)Columnas.Almacen].Width = 90;
            dgv.Columns[(int)Columnas.Responsable].Width = 130;
            dgv.Columns[(int)Columnas.Comentarios].Width = 200;
            dgv.Columns[(int)Columnas.Dias].Width = 90;
            dgv.Columns[(int)Columnas.Tipo].Width = 100;
            dgv.Columns[(int)Columnas.Stock].Width = 90;

            dgv.Columns[(int)Columnas.Precio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Almacen].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Dias].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Responsable].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgv.Columns[(int)Columnas.Precio].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Dias].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Stock].DefaultCellStyle.Format = "N0";
        }

        public SeguimientoCompras()
        {
            InitializeComponent();

            DataTable _t = new DataTable();
            _t.Columns.Add("Codigo");
            _t.Columns.Add("Nombre");

            _t.Rows.Add(new Object[]{"0", "Todos"});
            _t.Rows.Add(new Object[] { "01", "Puebla" });
            _t.Rows.Add(new Object[] { "02", "Monterrey" });
            _t.Rows.Add(new Object[] { "03", "Apizaco" });
            _t.Rows.Add(new Object[] { "05", "Cordoba" });
            _t.Rows.Add(new Object[] { "06", "Tepeaca" });
            _t.Rows.Add(new Object[] { "16", "México" });
            _t.Rows.Add(new Object[] { "18", "Guadalajara" });
            _t.Rows.Add(new Object[] { "23", "Saltillo" });

            clbAlmacen.DataSource = _t;
            clbAlmacen.DisplayMember = "Nombre";
            clbAlmacen.ValueMember = "Codigo";
        }

        private string Cadena(CheckedListBox ch)
        {
            StringBuilder stbSucursales = new StringBuilder();
            foreach (DataRowView item in clbAlmacen.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clbAlmacen.ToString().Equals(string.Empty))
                    {
                        stbSucursales.Append(",");
                    }
                    stbSucursales.Append(item["Codigo"].ToString());
                }
            }
            if (clbAlmacen.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clbAlmacen.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbAlmacen.ToString().Equals(string.Empty))
                        {
                            stbSucursales.Append(",");
                        }
                        stbSucursales.Append(item["Codigo"].ToString());
                    }
                }
            }

            return stbSucursales.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FechaInicial = dtFechaInicial.Value;
            FechaFinal = dtFechaFinal.Value;
            Articulo = txtArticulo.Text;
            Almacen = Cadena(clbAlmacen);
            Tipo = "";
            if (cbEspecial.Checked)
                Tipo = ",Compra especial";
            if (cbPPC.Checked)
                Tipo += ",PPC";
            if (!cbEspecial.Checked && !cbPPC.Checked)
                Tipo = ",Compra especial,PPC";

            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SeguimientoCompras", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 4);
                    command.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                    command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    command.Parameters.AddWithValue("@Articulo", Articulo);
                    command.Parameters.AddWithValue("@Almacen", Almacen);
                    command.Parameters.AddWithValue("@Tipo", Tipo);
                    command.Parameters.AddWithValue("@Vendedor", 0);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    gridExceso.DataSource = table;

                    Formato(gridExceso);
                }
            }
        }

        private void clbSucursal_Click(object sender, EventArgs e)
        {
            if (clbAlmacen.SelectedIndex == 0)
            {
                if (clbAlmacen.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbAlmacen.Items.Count; item++)
                    {
                        clbAlmacen.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbAlmacen.Items.Count; item++)
                    {
                        clbAlmacen.SetItemChecked(item, true);
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExportarAExcel exp = new ExportarAExcel();
            if(exp.Exportar(gridExceso))
                MessageBox.Show("El Archivo se creo con exito", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridExceso_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridExceso.Rows)
                {
                    if (Convert.ToString(row.Cells[(int)Columnas.Tipo].Value).Equals("PPC") && Convert.ToDecimal(row.Cells[(int)Columnas.Dias].Value) >= 7)
                    {
                        row.Cells[(int)Columnas.Dias].Style.BackColor = Color.Red;
                        row.Cells[(int)Columnas.Dias].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        row.Cells[(int)Columnas.Dias].Style.BackColor = Color.White;
                        row.Cells[(int)Columnas.Dias].Style.ForeColor = Color.Black;
                    }
                    if (Convert.ToString(row.Cells[(int)Columnas.Tipo].Value).Equals("Compra especial") && Convert.ToDecimal(row.Cells[(int)Columnas.Dias].Value) >= 10)
                    {
                        row.Cells[(int)Columnas.Dias].Style.BackColor = Color.Red;
                        row.Cells[(int)Columnas.Dias].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        row.Cells[(int)Columnas.Dias].Style.BackColor = Color.White;
                        row.Cells[(int)Columnas.Dias].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception)
            {
             
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button1_Click(sender, e);
        }
        DataTable Articul = new DataTable();
        private void SeguimientoCompras_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

            this.ListaArticulos();

            txtArticulo.AutoCompleteCustomSource = Autocomplete(Articul, "ItemCode");
            txtArticulo.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        public static AutoCompleteStringCollection Autocomplete(DataTable _t, string _column)
        {
            DataTable dt = _t;

            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

            foreach (DataRow row in dt.Rows)
            {
                coleccion.Add(Convert.ToString(row[_column]));
            }

            return coleccion;
        }
        public void ListaArticulos()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_ReparticionStock";

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@CantiadOK", decimal.Zero);
                    command.Parameters.AddWithValue("@Incremento", decimal.Zero);

                    command.CommandTimeout = 0;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(Articul);

                }
            }
        }

        private void SeguimientoCompras_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
       
            }
        }

        private void SeguimientoCompras_FormClosing(object sender, FormClosingEventArgs e)
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
