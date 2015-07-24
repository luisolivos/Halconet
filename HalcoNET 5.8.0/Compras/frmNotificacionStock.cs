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

namespace Compras
{
    public partial class frmNotificacionStock : Form
    {
        string Usuario;
        int Vendedor;
        private Clases.Logs log;

        public frmNotificacionStock(string _usuario, int vendedor)
        {
            InitializeComponent();
            Usuario = _usuario;
            Vendedor = vendedor;
            log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
        }

        public void CargarCompradores(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_NotificacionStock", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 1);
            command.Parameters.AddWithValue("@Comprador", string.Empty);
            command.Parameters.AddWithValue("@Linea", string.Empty);
            command.Parameters.AddWithValue("@Almacen", string.Empty);

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
            _cb.ValueMember = "Nombre";
        }

        public enum Columnas
        {
            Linea, Articulo, Descripcion, Comprador, Clasificacion, FechaStockOut, FechaEntrada, Almacen, Cantidad, Dias             
        }

        public void CargarLineas(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_NotificacionStock", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 2);
            command.Parameters.AddWithValue("@Comprador", string.Empty);
            command.Parameters.AddWithValue("@Linea", string.Empty);
            command.Parameters.AddWithValue("@Almacen", string.Empty);

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

        public void FormatoGrid()
        {
            dgvStock.Columns[(int)Columnas.Linea].Width = 90;
            dgvStock.Columns[(int)Columnas.Articulo].Width = 100;
            dgvStock.Columns[(int)Columnas.Descripcion].Width = 150;
            dgvStock.Columns[(int)Columnas.Comprador].Width = 90;
            dgvStock.Columns[(int)Columnas.Clasificacion].Width = 90;
            dgvStock.Columns[(int)Columnas.FechaStockOut].Width = 90;
            dgvStock.Columns[(int)Columnas.FechaEntrada].Width = 90;
            dgvStock.Columns[(int)Columnas.Almacen].Width = 90;
            dgvStock.Columns[(int)Columnas.Cantidad].Width = 90;
            dgvStock.Columns[(int)Columnas.Dias].Width = 90;

            dgvStock.Columns[(int)Columnas.Almacen].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStock.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvStock.Columns[(int)Columnas.Dias].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvStock.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Format = "N0";
        }

        /// <sumary> 
        /// Metodos para cambiar la apariencia del cursor
        private void Esperar()
        {
            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
        }
        private void Continuar()
        {
            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }

        private string GetMemo()
        {
            string qry = "select Memo from OSLP Where SlpCode  = @Vendedor";
            try
            {
                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
                {
                    using (SqlCommand command = new SqlCommand(qry, conn))
                    {
                        conn.Open();

                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Vendedor", Vendedor);

                        string memo =Convert.ToString(command.ExecuteScalar());
                        switch(memo)
                        {
                            case "01": return memo + " - PUE";
                            case "02": return memo + " - MTY";
                            case "03": return memo + " - API";
                            case "05": return memo + " - COR";
                            case "06": return memo + " - TEP";
                            case "16": return memo + " - MEX";
                            case "18": return memo + " - GDL";
                            default: return "";
                        }                       
                    }
                    
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cbLinea.SelectedIndex = 0;
            cbComprador.SelectedIndex = 0;
        }

        private void NotificacionStock_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                this.CargarCompradores(cbComprador, "--");
                this.CargarLineas(cbLinea, "--");



                if (Vendedor != 0)
                {
                    cbAlmacen.Text = GetMemo();
                    cbAlmacen.Enabled = false;
                }

                button1_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();
                dgvStock.DataSource = null;
                using (SqlConnection CONNECTION = new SqlConnection())
                {
                    CONNECTION.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand COMMAND = new SqlCommand("PJ_NotificacionStock", CONNECTION))
                    {
                        COMMAND.CommandType = CommandType.StoredProcedure;
                        COMMAND.Parameters.AddWithValue("@TipoConsulta", 3);
                        COMMAND.Parameters.AddWithValue("@Comprador", cbComprador.SelectedValue);
                        COMMAND.Parameters.AddWithValue("@Linea", cbLinea.SelectedValue);
                        COMMAND.Parameters.AddWithValue("@Almacen", cbAlmacen.SelectedItem.ToString().Substring(0, 2));

                        COMMAND.CommandTimeout = 0;

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = COMMAND;
                        adapter.Fill(table);

                        dgvStock.DataSource = table;

                        this.FormatoGrid();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Continuar();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {


                ExportarAExcel ex = new ExportarAExcel();
                if (ex.ExportarSinFormato(dgvStock))
                    MessageBox.Show("El archivo se creo correctamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
            }
        }

        private void NotificacionStock_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void NotificacionStock_FormClosing(object sender, FormClosingEventArgs e)
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
