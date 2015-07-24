using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Presupuesto.Requisicion
{
    public partial class ctrlEspecifico : UserControl
    {
        DataTable __detalle = new DataTable();
        Clases.FillControl fill = new Clases.FillControl();
        public string __Proveedor;
        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("PRES_Catalogos", new System.Data.SqlClient.SqlConnection(ClasesSGUV.Propiedades.conectionPJ_Pres));
        DataTable tblItems = new DataTable();

                
        public enum ColumnasGrid
        {
            Linea,
            ItemCode,
            ItemName,
            Quantity,
            Price,
            LineTotal
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasGrid.Linea].ReadOnly = true;
            dgv.Columns[(int)ColumnasGrid.ItemCode].ReadOnly = true;

            dgv.Columns[(int)ColumnasGrid.Linea].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasGrid.Quantity].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasGrid.Price].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasGrid.LineTotal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasGrid.Quantity].DefaultCellStyle.Format = "N2";
            dgv.Columns[(int)ColumnasGrid.Price].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasGrid.LineTotal].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumnasGrid.Linea].HeaderText = "Línea";
            dgv.Columns[(int)ColumnasGrid.ItemCode].HeaderText = "Artículo";
            dgv.Columns[(int)ColumnasGrid.ItemName].HeaderText = "Descripción del\r\nartículo o servicio";
            dgv.Columns[(int)ColumnasGrid.Quantity].HeaderText = "Cantidad";
            dgv.Columns[(int)ColumnasGrid.Price].HeaderText = "Precio Unitario";
            dgv.Columns[(int)ColumnasGrid.LineTotal].HeaderText = "Costo Total";

            dgv.Columns[(int)ColumnasGrid.Linea].Width = 40;
            dgv.Columns[(int)ColumnasGrid.ItemCode].Width = 70;
            dgv.Columns[(int)ColumnasGrid.ItemName].Width = 280;
            dgv.Columns[(int)ColumnasGrid.Quantity].Width = 70;
            dgv.Columns[(int)ColumnasGrid.Price].Width = 70;
            dgv.Columns[(int)ColumnasGrid.LineTotal].Width = 80;

            dgv.Columns[(int)ColumnasGrid.LineTotal].DefaultCellStyle.BackColor = Color.FromName("Info");

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public ctrlEspecifico()
        {
            InitializeComponent();

            __detalle.Columns.Add("Linea", typeof(int));
            __detalle.Columns.Add("ItemCode", typeof(string));
            __detalle.Columns.Add("ItemName", typeof(string));
            __detalle.Columns.Add("Quantity", typeof(decimal));
            __detalle.Columns.Add("Price", typeof(decimal));
            __detalle.Columns.Add("LineTotal", typeof(decimal), "Price*Quantity");
        }

        private void ctrlEspecifico_Load(object sender, EventArgs e)
        {
            fill.FillDataSource(cbProveedor, "-----Selecciona un Proveedor-----", 4);
            dgvDetalle.DataSource = __detalle;
            this.Formato(dgvDetalle);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Condition", Clases.Requisicion.Cuenta);
            tblItems = Clases.Datos.Sql.Fill(command);

        }

        private void dgvDetalle_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            var grid = (sender as DataGridView);
            e.Row.Cells[(int)ColumnasGrid.Linea].Value = e.Row.Index + 1;
        }

        private void cbrProveedor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRowView row = cbProveedor.SelectedItem as DataRowView;

                txtRFC.Text = row["RFC"].ToString();
                __Proveedor = row["Codigo"].ToString();
            }
            catch (Exception)
            { }
        }

        private void dgvDetalle_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((sender as DataGridView).CurrentCell.ColumnIndex == (int)ColumnasGrid.ItemName)
            {
                var source = new AutoCompleteStringCollection();


                string[] stringArray = Array.ConvertAll<DataRow, String>(tblItems.Select(), delegate(DataRow row) { return (String)row["Descripcion"]; });

                source.AddRange(stringArray);

                TextBox prodCode = e.Control as TextBox;
                if (prodCode != null)
                {
                    prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    prodCode.AutoCompleteCustomSource = source;
                    prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
            }
        }

        private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((sender as DataGridView).CurrentCell.ColumnIndex == (int)ColumnasGrid.ItemName)
                {
                    string item = (sender as DataGridView).CurrentCell.Value.ToString();

                    var qry = (from itemvar in tblItems.AsEnumerable()
                               where itemvar.Field<string>("Descripcion").ToLower().Equals(item.ToLower())
                               select itemvar).CopyToDataTable();

                    if (qry.Rows.Count > 0)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row = (sender as DataGridView).CurrentRow;

                        row.Cells[(int)ColumnasGrid.ItemCode].Value = qry.Rows[0].Field<string>("Articulo");
                        row.Cells[(int)ColumnasGrid.Price].Value = qry.Rows[0].Field<decimal>("Precio");
                        row.Cells[(int)ColumnasGrid.Linea].Value = row.Index + 1;


                    }
                }
            }
            catch (Exception) { }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void dgvDetalle_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            //validaciones por linea !! :) si el articulo existe o si los camp
        }

        private void dgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtSubtotal.Text = (sender as DataGridView).Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["LineTotal"].Value)).ToString("C2");
                txtIVA.Text = ((sender as DataGridView).Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["LineTotal"].Value)) * (decimal)0.16).ToString("C2");
                txtTotal.Text = ((sender as DataGridView).Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["LineTotal"].Value)) * (decimal)1.16).ToString("C2");
            }
            catch (Exception) { }
        }
    }
}
