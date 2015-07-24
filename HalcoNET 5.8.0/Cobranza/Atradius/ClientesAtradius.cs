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

namespace Cobranza
{
    public partial class ClientesAtradius : Form
    {
        Clases.Logs log;

        public enum Columnas
        {
            Cliente,
            Nombre,
            Sucursal,
            Vendedor,
            CondicionCredito,
            Moneda,
            CreditoSAP,
            CreditoAtradius,
            Resultado,
            Saldo,
            Uso
        }

        public ClientesAtradius()
        {
            InitializeComponent();
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Cliente].Width = 90;
            dgv.Columns[(int)Columnas.Nombre].Width = 250;
            dgv.Columns[(int)Columnas.Vendedor].Width = 150;
            dgv.Columns[(int)Columnas.Sucursal].Width = 90;
            dgv.Columns[(int)Columnas.CondicionCredito].Width = 90;
            dgv.Columns[(int)Columnas.Moneda].Width = 70;
            dgv.Columns[(int)Columnas.CreditoSAP].Width = 90;
            dgv.Columns[(int)Columnas.CreditoAtradius].Width = 90;
            dgv.Columns[(int)Columnas.Resultado].Width = 90;
            dgv.Columns[(int)Columnas.Saldo].Width = 90;
            dgv.Columns[(int)Columnas.Uso].Width = 90;

            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Uso].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)Columnas.CreditoSAP].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.CreditoAtradius].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Uso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.CreditoSAP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.CreditoAtradius].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Moneda].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Atradius", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;
                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@Desde", DateTime.Now);
                        command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                        command.Parameters.AddWithValue("@CardCode", txtCliente.Text);
                        command.Parameters.AddWithValue("@CardName", txtNombre.Text);

                        DataTable table = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(table);


                        gridFacturas.DataSource = table;

                        this.Formato(gridFacturas);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClientesAtradius_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Atradius", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;
                        command.Parameters.AddWithValue("@TipoConsulta", 5);
                        command.Parameters.AddWithValue("@Desde", DateTime.Now);
                        command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                        command.Parameters.AddWithValue("@CardCode", string.Empty);
                        command.Parameters.AddWithValue("@CardName", string.Empty);

                        DataTable table = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(table);

                        txtCliente.AutoCompleteCustomSource = Autocomplete(table, "CardCode");
                        txtCliente.AutoCompleteMode = AutoCompleteMode.Suggest;
                        txtCliente.AutoCompleteSource = AutoCompleteSource.CustomSource;

                        txtNombre.AutoCompleteCustomSource = Autocomplete(table, "CardName");
                        txtNombre.AutoCompleteMode = AutoCompleteMode.Suggest;
                        txtNombre.AutoCompleteSource = AutoCompleteSource.CustomSource;

                        log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConsultar_Click(sender, e);
            }
        }

        private void ClientesAtradius_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void ClientesAtradius_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel ex = new ExportarAExcel();
            if (ex.Exportar(gridFacturas, false))
                MessageBox.Show("El archivo se creo correctamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void gridFacturas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Uso].Value) > (decimal)1) item.Cells[(int)Columnas.Uso].Style.ForeColor = Color.Red;
                    else item.Cells[(int)Columnas.Uso].Style.ForeColor = Color.Black;
                 
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
