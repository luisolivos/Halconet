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

namespace Cobranza.Indicadores
{
    public partial class AlertasPR : Form
    {
        string Sucursal;
        string JefaCobranza;

        public enum Columnas
        {
            Factura, Fecha, Cliente, Nombre, PRecioSap, NC, Meses
        }

        public enum ColumnasDetalle
        {
            Factura, Articulo, Nombre, Cantidad, PrecioSap, PrecioReal, PrecioRealActualizado, Diferencia, NC
        }

        public void Formato1(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Factura].Width = 100;
            dgv.Columns[(int)Columnas.Fecha].Width = 100;
            dgv.Columns[(int)Columnas.Cliente].Width = 100;
            dgv.Columns[(int)Columnas.Nombre].Width = 250;
            dgv.Columns[(int)Columnas.PRecioSap].Width = 100;
            dgv.Columns[(int)Columnas.NC].Width = 100;
            dgv.Columns[(int)Columnas.Meses].Width = 100;

            dgv.Columns[(int)Columnas.PRecioSap].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.PRecioSap].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Meses].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)Columnas.Meses].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void Formato2(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasDetalle.Factura].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.Articulo].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.Nombre].Width = 200;
            dgv.Columns[(int)ColumnasDetalle.Cantidad].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.PrecioSap].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.PrecioReal].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.PrecioRealActualizado].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.NC].Visible = false;

            dgv.Columns[(int)ColumnasDetalle.Cantidad].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasDetalle.PrecioSap].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.PrecioReal].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.PrecioRealActualizado].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.Diferencia].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumnasDetalle.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasDetalle.PrecioSap].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.PrecioRealActualizado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.PrecioReal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.Diferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            /*
             , , , , */
        }

        public AlertasPR(string _sucursal, string _jefaCobranza)
        {
            InitializeComponent();
            Sucursal = _sucursal;
            JefaCobranza = _jefaCobranza;
        }

        public DataTable ProcedureSelect(int _tipo, string _cliente, int _docentry, decimal _precio, DateTime _fecha, 
                int _vendedor, int _factura, string _articulo, int _linenum, string _coments, string _name)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_NCPedientes", conn))
                    {
                        conn.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", _tipo);
                        command.Parameters.AddWithValue("@Cliente", _cliente);
                        command.Parameters.AddWithValue("@Docentry", _docentry);
                        command.Parameters.AddWithValue("@PrecioCliente", _precio);
                        command.Parameters.AddWithValue("@FechaEnvio", _fecha);
                        command.Parameters.AddWithValue("@Vendedor", _vendedor);
                        command.Parameters.AddWithValue("@Factura", _factura);

                        command.Parameters.AddWithValue("@Articulo", _articulo);
                        command.Parameters.AddWithValue("@Linenum", _linenum);
                        command.Parameters.AddWithValue("@Comentario", _coments);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(table);
                        table.TableName = _name;
                        return table;
                    }
                }
            }
            catch (Exception)
            {
                return new DataTable(_name);
            }
        }

        private void NCPorAclarar_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                DataSet data = new DataSet();
                BindingSource masterBindingSource = new BindingSource();
                BindingSource detailsBindingSource = new BindingSource();

                data.Tables.Add(this.ProcedureSelect(10, string.Empty, 0, decimal.Zero, DateTime.Now, 0, 0, string.Empty, 0, JefaCobranza, "Facturas"));

                data.Tables.Add(this.ProcedureSelect(11, string.Empty, 0, decimal.Zero, DateTime.Now, 0, 0, string.Empty, 0, JefaCobranza, "Detalle"));

                //gridFacturas.DataSource = data.Tables["Facturas"];
                //gridDetalles.DataSource = data.Tables["Detalle"];

                DataRelation relation = new DataRelation("FacturaDetalle", data.Tables["Facturas"].Columns["Factura"], data.Tables["Detalle"].Columns["Factura"]);

                data.Relations.Add(relation);

                masterBindingSource.DataSource = data;
                masterBindingSource.DataMember = "Facturas";
                detailsBindingSource.DataSource = masterBindingSource;
                detailsBindingSource.DataMember = "FacturaDetalle";
                gridFacturas.DataSource = masterBindingSource;
                gridDetalles.DataSource = detailsBindingSource;

                this.Formato1(gridFacturas);
                this.Formato2(gridDetalles);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridFacturas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                //foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                //{
                //    if (Convert.ToInt32(item.Cells[(int)Columnas.Dias].Value) > 15)
                //    {
                //        item.Cells[(int)Columnas.Factura].Style.BackColor = Color.Red;
                //        item.Cells[(int)Columnas.Factura].Style.ForeColor = Color.Black;
                //    }
                //}
            }
            catch (Exception)
            {
                
            }
        }

        private void gridDetalles_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToInt32(item.Cells[(int)ColumnasDetalle.Diferencia].Value) != 0)
                    {
                        item.Cells[(int)ColumnasDetalle.Diferencia].Style.BackColor = Color.Red;
                        item.Cells[(int)ColumnasDetalle.Diferencia].Style.ForeColor = Color.White;
                    }
                }

            }
            catch (Exception)
            {

            }
        }
  
    }
}

