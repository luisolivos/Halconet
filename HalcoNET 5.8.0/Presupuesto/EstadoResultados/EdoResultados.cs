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

namespace Presupuesto
{
    public partial class EdoResultados : Form
    {
        bool format = false;
        private int RolUsuario;
        private string SucursalUsuario;
        Logs log;
        
        public EdoResultados(int _RolUsuario, string _SucursalUsuario)
        {
            InitializeComponent();

            RolUsuario = _RolUsuario;
            SucursalUsuario = _SucursalUsuario;
        }

        public enum Columnas
        {
            Formato,
            U_Tipo,
            Sumar,
            Total,
            Apizaco,
            Cordoba,
            EdoMex,
            GDL,
            MTY,
            Puebla,
            Tepeaca,
            Saltillo
        }
        private void Presentar()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
            {
                using (SqlCommand command = new SqlCommand("PJ_Edo_Resultados", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                    command.Parameters.AddWithValue("@Sucursal", cbSucursal.Text);
                    command.Parameters.AddWithValue("@Mes", cmbMes.SelectedIndex + 1);
                    command.Parameters.AddWithValue("@Year", txtAño1.Text);

                    DataTable table = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter();
                    ad.SelectCommand = command;
                    ad.Fill(table);

                    dgvResultados.DataSource = table;
                }
            }
        }

        private void Formato(DataGridView dgv)
        {
            format = true;
            dgv.Columns[(int)Columnas.Formato].Visible = false;
            dgv.Columns[(int)Columnas.U_Tipo].HeaderText = "\r\n \r\n";
            dgv.Columns[(int)Columnas.Sumar].HeaderText = "";
            dgv.Columns[(int)Columnas.U_Tipo].Width = 180;
            dgv.Columns[(int)Columnas.Sumar].Width = 100;
            dgv.Columns[(int)Columnas.Total].Width = 100;
            dgv.Columns[(int)Columnas.Apizaco].Width = 100;
            dgv.Columns[(int)Columnas.Cordoba].Width = 100;
            dgv.Columns[(int)Columnas.EdoMex].Width = 100;
            dgv.Columns[(int)Columnas.GDL].Width = 100;
            dgv.Columns[(int)Columnas.MTY].Width = 100;
            dgv.Columns[(int)Columnas.Puebla].Width = 100;
            dgv.Columns[(int)Columnas.Tepeaca].Width = 100;
            dgv.Columns[(int)Columnas.Saltillo].Width = 100;

            dgv.Columns[(int)Columnas.Formato].ReadOnly = true;
            dgv.Columns[(int)Columnas.U_Tipo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Sumar].ReadOnly = false;
            dgv.Columns[(int)Columnas.U_Tipo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Total].ReadOnly = true;
            dgv.Columns[(int)Columnas.Apizaco].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cordoba].ReadOnly = true;
            dgv.Columns[(int)Columnas.EdoMex].ReadOnly = true;
            dgv.Columns[(int)Columnas.GDL].ReadOnly = true;
            dgv.Columns[(int)Columnas.MTY].ReadOnly = true;
            dgv.Columns[(int)Columnas.Puebla].ReadOnly = true;
            dgv.Columns[(int)Columnas.Tepeaca].ReadOnly = true;
            dgv.Columns[(int)Columnas.Saltillo].ReadOnly = true;

            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Apizaco].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Cordoba].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.EdoMex].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.GDL].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.MTY].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Puebla].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Tepeaca].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Saltillo].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Total].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Apizaco].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Cordoba].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.EdoMex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.GDL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.MTY].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Puebla].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Tepeaca].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Saltillo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
                item.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, FontStyle.Regular);
            }

            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.5F, FontStyle.Regular);
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        /// <summary>
        /// Método que carga las sucursales en el cbSucursal
        /// </summary>
        private void CargarSucursales()
        {
            if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteGastos)
            {
                cbSucursal.Items.Clear();
                cbSucursal.Items.Add("TODOS");
                cbSucursal.Items.Add("APIZACO");
                cbSucursal.Items.Add("CORDOBA");
                cbSucursal.Items.Add("EDOMEX");
                cbSucursal.Items.Add("GDL");
                cbSucursal.Items.Add("MTY");
                cbSucursal.Items.Add("PUEBLA");
                cbSucursal.Items.Add("TEPEACA");
                cbSucursal.SelectedIndex = 0;
                cbSucursal.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else if (RolUsuario == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal)
            {
                cbSucursal.Items.Clear();
                cbSucursal.Items.Add(SucursalUsuario);
                cbSucursal.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Método que carga los meses en el cblMes
        /// </summary>
        private void CargarMeses()
        {
            DataTable Meses = new DataTable();
            Meses.Columns.Add("Index", typeof(int));
            Meses.Columns.Add("Mes", typeof(string));
            string[] array = new string[12] { "ENERO", "FEBRERO", "MARZO", "ABRIL", "MAYO", "JUNIO", "JULIO", "AGOSTO", "SEPTIEMBRE", "OCTUBRE", "NOVIEMBRE", "DICIEMBRE" };

            for (int i = 0; i < 12; i++)
            {
                DataRow row = Meses.NewRow();
                row["Index"] = i;
                row["Mes"] = array[i];

                Meses.Rows.Add(row);
            }
            DataTable meses1 = Meses.Copy();
            cmbMes.DataSource = Meses;
            cmbMes.DisplayMember = "Mes";
            cmbMes.ValueMember = "Index";

            cmbMes.DataSource = meses1;
            cmbMes.DisplayMember = "Mes";
            cmbMes.ValueMember = "Index";
        }

        private void EdoResultados_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            log = new Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            this.CargarMeses();
            this.CargarSucursales();

            cmbMes.SelectedIndex = DateTime.Now.Month - 1;
            txtAño1.Text = DateTime.Now.Year.ToString();
        }

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Presentar();
                if(!format)
                    this.Formato(dgvResultados);


                //var qry = from item in (dgvResultados.DataSource as DataTable).AsEnumerable()
                //          where item.Field<string>("Nombre de la cuenta") != ""
                //          select item;

                //dataGridView1.DataSource = qry.CopyToDataTable();


                //Formato(dataGridView1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void dgvResultados_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells[(int)Columnas.Formato].Value.ToString() == "01")
                    {
                        item.DefaultCellStyle.Font = new Font("Calibri", 10, FontStyle.Bold, GraphicsUnit.Point);
                        item.DefaultCellStyle.BackColor = Color.FromArgb(180, 198, 231);
                    }
                    if (item.Cells[(int)Columnas.Formato].Value.ToString() == "02")
                    {
                        item.DefaultCellStyle.Font = new Font("Calibri", 10, FontStyle.Bold, GraphicsUnit.Point);
                        item.DefaultCellStyle.BackColor = Color.FromArgb(198, 224, 180);
                    }
                    if (item.Cells[(int)Columnas.Formato].Value.ToString() == "03")
                    {
                        item.DefaultCellStyle.Font = new Font("Calibri", 10, FontStyle.Bold, GraphicsUnit.Point);
                        item.DefaultCellStyle.BackColor = Color.FromArgb(217, 225, 242);
                    }
                    if (item.Cells[(int)Columnas.Formato].Value.ToString() == "04")
                    {
                        item.DefaultCellStyle.Font = new Font("Calibri", 10, FontStyle.Bold, GraphicsUnit.Point);
                        item.DefaultCellStyle.BackColor = Color.FromArgb(226, 239, 218);
                    }
                    if (item.Cells[(int)Columnas.Formato].Value.ToString() == "05")
                    {
                        item.DefaultCellStyle.Font = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point);
                        item.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
                    }
                    if (item.Cells[(int)Columnas.Formato].Value.ToString() == "06")
                    {
                        item.DefaultCellStyle.Font = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point);
                        item.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
                        item.DefaultCellStyle.ForeColor = Color.FromArgb(255, 255, 255);
                        item.Height = 6;
                    }
                    if (item.Cells[(int)Columnas.Formato].Value.ToString() == "07")
                    {
                        item.DefaultCellStyle.Font = new Font("Calibri", 10, FontStyle.Regular, GraphicsUnit.Point);
                        item.DefaultCellStyle.BackColor = Color.FromArgb(0, 0, 0);
                        item.DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
                        item.Height = 3;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            
 
            ExportarAExcel expo = new ExportarAExcel();

            if (expo.ExportarFormato(dgvResultados))
            {
                MessageBox.Show("El archivo se con exíto.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EdoResultados_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void EdoResultados_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            EstadoResultados.CargarDatos from = new EstadoResultados.CargarDatos();
            from.MdiParent = this.MdiParent;
            from.Show();
        }

        private void dgvResultados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvResultados_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvResultados_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == (int)Columnas.Sumar)
                {
                    decimal ingresos = decimal.Zero;
                    decimal costoVentas = decimal.Zero;
                    decimal gananciaBruta = decimal.Zero;
                    decimal gastoDirecto = decimal.Zero;
                    decimal gastoDispersado = decimal.Zero;
                    decimal gastoComisiones = decimal.Zero;
                    decimal amortizacionesDepreciaciones = decimal.Zero;
                    decimal gastos = decimal.Zero;
                    decimal gananciasComerciales = decimal.Zero;
                    decimal financieros = decimal.Zero;
                    decimal gananciasFianciacion = decimal.Zero;
                    decimal periodoGanancias = decimal.Zero;

                    foreach (DataGridViewColumn col in dgvResultados.Columns)
                    {
                        if (col.Index > 2)
                        {
                            if (Convert.ToBoolean(dgvResultados.Rows[0].Cells[(int)Columnas.Sumar].Value)) ingresos = Convert.ToDecimal(dgvResultados.Rows[0].Cells[col.Index].Value);
                            if (Convert.ToBoolean(dgvResultados.Rows[1].Cells[(int)Columnas.Sumar].Value)) costoVentas = Convert.ToDecimal(dgvResultados.Rows[1].Cells[col.Index].Value);
                            if (Convert.ToBoolean(dgvResultados.Rows[2].Cells[(int)Columnas.Sumar].Value)) gananciaBruta = ingresos - costoVentas;
                            if (Convert.ToBoolean(dgvResultados.Rows[3].Cells[(int)Columnas.Sumar].Value)) gastoDirecto = Convert.ToDecimal(dgvResultados.Rows[3].Cells[col.Index].Value);
                            if (Convert.ToBoolean(dgvResultados.Rows[4].Cells[(int)Columnas.Sumar].Value)) gastoDispersado = Convert.ToDecimal(dgvResultados.Rows[4].Cells[col.Index].Value);
                            if (Convert.ToBoolean(dgvResultados.Rows[5].Cells[(int)Columnas.Sumar].Value)) gastoComisiones = Convert.ToDecimal(dgvResultados.Rows[5].Cells[col.Index].Value);
                            if (Convert.ToBoolean(dgvResultados.Rows[6].Cells[(int)Columnas.Sumar].Value)) amortizacionesDepreciaciones = Convert.ToDecimal(dgvResultados.Rows[6].Cells[col.Index].Value);
                            if (Convert.ToBoolean(dgvResultados.Rows[7].Cells[(int)Columnas.Sumar].Value)) gastos = gastoDirecto + gastoDispersado + gastoComisiones + amortizacionesDepreciaciones;
                            if (Convert.ToBoolean(dgvResultados.Rows[8].Cells[(int)Columnas.Sumar].Value)) gananciasComerciales = gananciaBruta - gastos;
                            if (Convert.ToBoolean(dgvResultados.Rows[9].Cells[(int)Columnas.Sumar].Value)) financieros = Convert.ToDecimal(dgvResultados.Rows[9].Cells[col.Index].Value);
                            if (Convert.ToBoolean(dgvResultados.Rows[10].Cells[(int)Columnas.Sumar].Value)) gananciasFianciacion = gananciasComerciales - financieros;
                            if (Convert.ToBoolean(dgvResultados.Rows[11].Cells[(int)Columnas.Sumar].Value)) periodoGanancias = gananciasComerciales - financieros;


                            dgvResultados.Rows[2].Cells[col.Index].Value = gananciaBruta;
                            dgvResultados.Rows[7].Cells[col.Index].Value = gastos;
                            dgvResultados.Rows[8].Cells[col.Index].Value = gananciasComerciales;
                            dgvResultados.Rows[10].Cells[col.Index].Value = gananciasFianciacion;
                            dgvResultados.Rows[11].Cells[col.Index].Value = periodoGanancias;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
