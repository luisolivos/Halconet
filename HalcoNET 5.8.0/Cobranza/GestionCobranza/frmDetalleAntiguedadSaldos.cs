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

namespace Cobranza
{
    public partial class DetalleAntiguedadSaldos : Form
    {
        public string Cliente;
        public string Nombre;
        public string Usuario;
        Clases.Logs log;

        public enum ColumnasGrid
        {
            Factura, FechaFact, FechaVto, ImporteOriginal, PagosAplicados, Saldo, PorVencer, Col1, Col2, Col3, Col4, Col5, Seleccionar
        }

        public enum Total
        {
            Importe, Pago, Saldo, Abono, Col1, Col2, Col3, Col4, Col5
        }

        public void FormatoGrid()
        {
            gridFacturas.Columns[(int)ColumnasGrid.Factura].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.FechaFact].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.FechaVto].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.ImporteOriginal].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.PagosAplicados].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.Saldo].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.PorVencer].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.Col1].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.Col2].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.Col3].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.Col4].ReadOnly = true;
            gridFacturas.Columns[(int)ColumnasGrid.Col5].ReadOnly = true;

            gridFacturas.Columns[(int)ColumnasGrid.Factura].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.FechaFact].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.FechaVto].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.ImporteOriginal].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.PagosAplicados].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.Saldo].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.PorVencer].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.Col1].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.Col2].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.Col3].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.Col4].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.Col5].Width = 100;
            gridFacturas.Columns[(int)ColumnasGrid.Seleccionar].Width = 100;

            gridFacturas.Columns[(int)ColumnasGrid.ImporteOriginal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)ColumnasGrid.PagosAplicados].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)ColumnasGrid.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)ColumnasGrid.PorVencer].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)ColumnasGrid.Col1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)ColumnasGrid.Col2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)ColumnasGrid.Col3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)ColumnasGrid.Col4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)ColumnasGrid.Col5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            gridFacturas.Columns[(int)ColumnasGrid.ImporteOriginal].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.PagosAplicados].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.Saldo].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.PorVencer].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.Col1].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.Col2].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.Col3].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.Col4].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)ColumnasGrid.Col5].DefaultCellStyle.Format = "C2";

        }

        public void FormatoTotal(DataGridView dgv)
        {
            dgv.Columns[(int)Total.Importe].Width = 100;
            dgv.Columns[(int)Total.Pago].Width = 100;
            dgv.Columns[(int)Total.Saldo].Width = 100;
            dgv.Columns[(int)Total.Abono].Width = 100;
            dgv.Columns[(int)Total.Col1].Width = 100;
            dgv.Columns[(int)Total.Col2].Width = 100;
            dgv.Columns[(int)Total.Col3].Width = 100;
            dgv.Columns[(int)Total.Col4].Width = 100;
            dgv.Columns[(int)Total.Col5].Width = 100;

            dgv.Columns[(int)Total.Importe].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Pago].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Saldo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Abono].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Col1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Col2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Col3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Col4].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Total.Col5].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Total.Importe].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Pago].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Abono].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Col1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Col2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Col3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Col4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Total.Col5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.ReadOnly = true;
            }
        }

        public void Totales()
        {
            DataTable _Totales = new DataTable();
            _Totales.Columns.Add("Importeoriginal", typeof(decimal));
            _Totales.Columns.Add("Pagos", typeof(decimal));
            _Totales.Columns.Add("Saldo", typeof(decimal));
            _Totales.Columns.Add("Abono", typeof(decimal));
            _Totales.Columns.Add("0-30", typeof(decimal));
            _Totales.Columns.Add("31-60", typeof(decimal));
            _Totales.Columns.Add("61-90", typeof(decimal));
            _Totales.Columns.Add("91-120", typeof(decimal));
            _Totales.Columns.Add(">120", typeof(decimal));

            dataGridView1.DataSource = _Totales;

            DataRow row = _Totales.NewRow();

            row["Importeoriginal"] = ((DataTable)gridFacturas.DataSource).Compute("SUM([Importe Original])", "");
            row["Saldo"] = ((DataTable)gridFacturas.DataSource).Compute("SUM([Saldo])", "");
            row["Pagos"] = ((DataTable)gridFacturas.DataSource).Compute("SUM([Pagos aplicados])", "");
            row["Abono"] = ((DataTable)gridFacturas.DataSource).Compute("SUM([Abono futuro])", "");
            row["0-30"] = ((DataTable)gridFacturas.DataSource).Compute("SUM([0-30])", "");
            row["31-60"] = ((DataTable)gridFacturas.DataSource).Compute("SUM([31-60])", "");
            row["61-90"] = ((DataTable)gridFacturas.DataSource).Compute("SUM([61-90])", "");
            row["91-120"] = ((DataTable)gridFacturas.DataSource).Compute("SUM([91-120])", "");
            row[">120"] = ((DataTable)gridFacturas.DataSource).Compute("SUM([>120])", "");

            _Totales.Rows.Add(row);
            Porcentajes(_Totales);
            FormatoTotal(dataGridView1);
        }

        public void Porcentajes(DataTable datos)
        {
            DataTable _t = new DataTable("Totales");
            _t.Columns.Add("Saldo", typeof(decimal));
            _t.Columns.Add("Abono", typeof(decimal));
            _t.Columns.Add("0-30", typeof(decimal));
            _t.Columns.Add("31-60", typeof(decimal));
            _t.Columns.Add("61-90", typeof(decimal));
            _t.Columns.Add("91-120", typeof(decimal));
            _t.Columns.Add(">120", typeof(decimal));

            foreach (DataRow r in datos.Rows)
            {
                DataRow _r = _t.NewRow();
                _r["Saldo"] = Convert.ToDecimal(r["Saldo"]) / Convert.ToDecimal(r["Saldo"]);
                _r["Abono"] = Convert.ToDecimal(r["Abono"]) / Convert.ToDecimal(r["Saldo"]);
                _r["0-30"] = Convert.ToDecimal(r["0-30"]) / Convert.ToDecimal(r["Saldo"]);
                _r["31-60"] = Convert.ToDecimal(r["31-60"]) / Convert.ToDecimal(r["Saldo"]);
                _r["61-90"] = Convert.ToDecimal(r["61-90"]) / Convert.ToDecimal(r["Saldo"]);
                _r["91-120"] = Convert.ToDecimal(r["91-120"]) / Convert.ToDecimal(r["Saldo"]);
                _r[">120"] = Convert.ToDecimal(r[">120"]) / Convert.ToDecimal(r["Saldo"]);
                _t.Rows.Add(_r);
            }
            gridPorcentajes.DataSource = _t;
        }


        public DetalleAntiguedadSaldos(string _cliente, string _nombre, string _usuario)
        {
            InitializeComponent();

            Cliente = _cliente;
            Nombre = _nombre;
            Usuario = _usuario;
            log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
        }

        private void DetalleAntiguedadSaldos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            txtCliente.Text = Cliente;
            txtNombre.Text = Nombre;

            using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV)))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 7);
                command.Parameters.AddWithValue("@Vendedores", string.Empty);
                command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@Usuario", string.Empty);
                command.Parameters.AddWithValue("@Cliente", Cliente);
                command.Parameters.AddWithValue("@Factura", string.Empty);
                command.CommandTimeout = 0;

                DataTable _t = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.SelectCommand.CommandTimeout = 0;
                adapter.Fill(_t);

                gridFacturas.DataSource = _t;
                _t.Columns.Add("Seleccionar", typeof(bool));
                FormatoGrid();
                Totales();
            }
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Cobrnaza", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 15);
                    command.Parameters.AddWithValue("@Cliente", Cliente);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtp1.Text = reader["Phone1"].ToString();
                        txtp2.Text = reader["Phone2"].ToString();
                    }
                }
            }
        }

        private void gridFacturas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                this.Close();
            }
        }

        private void gridFacturas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in gridFacturas.Rows)
                {
                    if (Convert.ToDecimal(item.Cells[(int)ColumnasGrid.ImporteOriginal].Value) > Convert.ToDecimal(item.Cells[(int)ColumnasGrid.Saldo].Value))
                    {
                        item.Cells[(int)ColumnasGrid.Factura].Style.BackColor = Color.Yellow;
                        item.Cells[(int)ColumnasGrid.Factura].ToolTipText = "Esta factura tiene un pago aplicado o una nota de crédito";
                        
                    }

                    if (Convert.ToDateTime(item.Cells[(int)ColumnasGrid.FechaVto].Value) <= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) )
                    {
                        item.Cells[(int)ColumnasGrid.FechaVto].Style.BackColor = Color.Red;
                        item.Cells[(int)ColumnasGrid.FechaVto].Style.ForeColor = Color.White;
                        item.Cells[(int)ColumnasGrid.FechaVto].ToolTipText = "Esta factura esta vencida";

                    }
                    else
                    {
                        item.Cells[(int)ColumnasGrid.FechaVto].Style.BackColor = Color.White;
                        item.Cells[(int)ColumnasGrid.FechaVto].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable _td = new DataTable();

            var query = (from item in ((DataTable)gridFacturas.DataSource).AsEnumerable()
                   where item.Field<bool?>("Seleccionar") == true
                   select item);

            if (query.Count() > 0)
            {
                _td = query.CopyToDataTable();
                AntiguedadSaldos.NuevoCompromiso nc = new AntiguedadSaldos.NuevoCompromiso(_td, Cliente, "dd/MM/yyyy", "01", Usuario);
                nc.ShowDialog();
            }
            else
            {
                MessageBox.Show("Debe seleccionar al menos una factura.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DataTable _td = new DataTable();

            //var query = (from item in ((DataTable)gridFacturas.DataSource).AsEnumerable()
            //             where item.Field<bool?>("Seleccionar") == true
            //             select item);

            //if (query.Count() > 0)
           // {
             //   _td = query.CopyToDataTable();
                AntiguedadSaldos.NuevoCompromiso nc = new AntiguedadSaldos.NuevoCompromiso(new DataTable(), Cliente, "hh:mm:ss tt - dd/MM/yyyy", "03", Usuario);
                nc.ShowDialog();
           // }
            //else
            //{
            //    MessageBox.Show("Debe seleccionar al menos una factura.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
           // }
        }

        private void DetalleAntiguedadSaldos_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void DetalleAntiguedadSaldos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Cobranza.GestionCobranza.frmLlamadas form = new GestionCobranza.frmLlamadas(Cliente);
                form.MdiParent = this.MdiParent;

                form.Show();
            }
            catch (Exception)
            {

            }
        }
    }
}
