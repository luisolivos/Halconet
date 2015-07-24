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
    public partial class ctrlGeneral : UserControl
    {
        public DataTable __detalle = new DataTable();
        public enum ColumnasDetalle
        {
            Linea,
            Descripcion,
            Cantidad,
            Sugerido1,
            PrecioU1,
            LineTotal1,
            Sugerido2,
            PrecioU2,
            LineTotal2,
            Sugerido3,
            PrecioU3,
            LineTotal3,
            Comentarios
        }
        Clases.FillControl fill = new Clases.FillControl();

        public Clases.Objetos.Proveedor[]prov = new Clases.Objetos.Proveedor[3];

        public void FormatoDetalle(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasDetalle.Comentarios].Visible = false;

            dgv.Columns[(int)ColumnasDetalle.Linea].Width = 38;
            dgv.Columns[(int)ColumnasDetalle.Descripcion].Width = 185;
            dgv.Columns[(int)ColumnasDetalle.Cantidad].Width = 53;
            dgv.Columns[(int)ColumnasDetalle.Sugerido1].Width = 59;
            dgv.Columns[(int)ColumnasDetalle.PrecioU1].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.LineTotal1].Width = 108;
            dgv.Columns[(int)ColumnasDetalle.Sugerido2].Width = 59;
            dgv.Columns[(int)ColumnasDetalle.PrecioU2].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.LineTotal2].Width = 108;
            dgv.Columns[(int)ColumnasDetalle.Sugerido3].Width = 59;
            dgv.Columns[(int)ColumnasDetalle.PrecioU3].Width = 100;
            dgv.Columns[(int)ColumnasDetalle.LineTotal3].Width = 108;

            dgv.Columns[(int)ColumnasDetalle.Linea].HeaderText = "Linea";
            dgv.Columns[(int)ColumnasDetalle.Descripcion].HeaderText = "Descripción del artículo o servicio";
            dgv.Columns[(int)ColumnasDetalle.Cantidad].HeaderText = "Cantidad";
            dgv.Columns[(int)ColumnasDetalle.Sugerido1].HeaderText = "Sugerido";
            dgv.Columns[(int)ColumnasDetalle.PrecioU1].HeaderText = "Precio Unitario";
            dgv.Columns[(int)ColumnasDetalle.LineTotal1].HeaderText = "Costo Total";
            dgv.Columns[(int)ColumnasDetalle.Sugerido2].HeaderText = "Sugerido";
            dgv.Columns[(int)ColumnasDetalle.PrecioU2].HeaderText = "Precio Unitario";
            dgv.Columns[(int)ColumnasDetalle.LineTotal2].HeaderText = "Costo Total";
            dgv.Columns[(int)ColumnasDetalle.Sugerido3].HeaderText = "Sugerido";
            dgv.Columns[(int)ColumnasDetalle.PrecioU3].HeaderText = "Precio Unitario";
            dgv.Columns[(int)ColumnasDetalle.LineTotal3].HeaderText = "Costo Total";

            dgv.Columns[(int)ColumnasDetalle.Cantidad].DefaultCellStyle.Format = "N2";
            dgv.Columns[(int)ColumnasDetalle.PrecioU1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.LineTotal1].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.PrecioU2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.LineTotal2].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.PrecioU3].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasDetalle.LineTotal3].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumnasDetalle.Linea].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasDetalle.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.PrecioU1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.LineTotal1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.PrecioU2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.LineTotal2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.PrecioU3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasDetalle.LineTotal3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasDetalle.LineTotal1].DefaultCellStyle.BackColor = Color.FromName("Info");
            dgv.Columns[(int)ColumnasDetalle.LineTotal2].DefaultCellStyle.BackColor = Color.FromName("Info");
            dgv.Columns[(int)ColumnasDetalle.LineTotal3].DefaultCellStyle.BackColor = Color.FromName("Info");

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public ctrlGeneral()
        {
            InitializeComponent();

            prov[0] = new Clases.Objetos.Proveedor();
            prov[1] = new Clases.Objetos.Proveedor();
            prov[2] = new Clases.Objetos.Proveedor();

            __detalle.Columns.Add("Linea", typeof(int));
            __detalle.Columns.Add("ItemName", typeof(string));
            __detalle.Columns.Add("Quantity", typeof(decimal));

            __detalle.Columns.Add("Sugerido1", typeof(bool));
            __detalle.Columns.Add("Price1", typeof(decimal));
            __detalle.Columns.Add("LineTotal1", typeof(decimal), "Price1*Quantity");

            __detalle.Columns.Add("Sugerido2", typeof(bool));
            __detalle.Columns.Add("Price2", typeof(decimal));
            __detalle.Columns.Add("LineTotal2", typeof(decimal), "Price2*Quantity");

            __detalle.Columns.Add("Sugerido3", typeof(bool));
            __detalle.Columns.Add("Price3", typeof(decimal));
            __detalle.Columns.Add("LineTotal3", typeof(decimal), "Price3*Quantity");

            __detalle.Columns.Add("Comentarios", typeof(string));
        }

        private void ctrlEspecifico_Load(object sender, EventArgs e)
        {
            dgvDetalle.DataSource = __detalle;
            this.FormatoDetalle(dgvDetalle);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            fill.FillDataSource(cbProveedor1, "-----Selecciona un Proveedor-----", 4);
            fill.FillDataSource(cbProveedor2, "-----Selecciona un Proveedor-----", 4);
            fill.FillDataSource(cbProveedor3, "-----Selecciona un Proveedor-----", 4);
        }

        private void cbProveedor1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRowView row = cbProveedor1.SelectedItem as DataRowView;

                txtRFC1.Text = row["RFC"].ToString();
                prov[0].CardCode = row["Codigo"].ToString();
            }
            catch (Exception)
            { }
        }

        private void cbProveedor2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRowView row = cbProveedor2.SelectedItem as DataRowView;

                txtRFC2.Text = row["RFC"].ToString();
                prov[1].CardCode = row["Codigo"].ToString();
            }
            catch (Exception)
            { }
        }

        private void cbProveedor3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRowView row = cbProveedor3.SelectedItem as DataRowView;

                txtRFC3.Text = row["RFC"].ToString();
                prov[2].CardCode = row["Codigo"].ToString();
            }
            catch (Exception)
            { }
        }

        private void dgvDetalle_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            var grid = (sender as DataGridView);
            e.Row.Cells[(int)ColumnasDetalle.Linea].Value = e.Row.Index + 1;
        }

        private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = new DataGridViewRow();
                row = (sender as DataGridView).CurrentRow;
                row.Cells[(int)ColumnasDetalle.Linea].Value = row.Index + 1;

                DataGridViewRow __row = new DataGridViewRow();
                try
                {
                    __row = (sender as DataGridView).Rows[e.RowIndex];
                    if (string.IsNullOrEmpty(Convert.ToString(__row.Cells[(int)ColumnasDetalle.Descripcion].Value == DBNull.Value ? string.Empty : __row.Cells[(int)ColumnasDetalle.Descripcion].Value)))
                    {
                        __row.ErrorText = "El campo [Descripción] no puede estar vacio.";
                    }
                    else if (Convert.ToDecimal(__row.Cells[(int)ColumnasDetalle.Cantidad].Value == DBNull.Value ? decimal.Zero : __row.Cells[(int)ColumnasDetalle.Cantidad].Value) <= 0)
                    {
                        __row.ErrorText = "El campo [Cantidad] no valido, el mínimo aceptado es 1";
                    }
                    else if (Convert.ToDecimal(__row.Cells[(int)ColumnasDetalle.PrecioU1].Value == DBNull.Value ? decimal.Zero : __row.Cells[(int)ColumnasDetalle.PrecioU1].Value) <= 0)
                    {
                        __row.ErrorText = "El campo [Precio Unitario] Prov 1 no valido.";
                    }
                    else if (Convert.ToDecimal(__row.Cells[(int)ColumnasDetalle.PrecioU2].Value == DBNull.Value ? decimal.Zero : __row.Cells[(int)ColumnasDetalle.PrecioU2].Value) <= 0)
                    {
                        __row.ErrorText = "El campo [Precio Unitario] Prov 2 no valido.";
                    }
                    else if (Convert.ToDecimal(__row.Cells[(int)ColumnasDetalle.PrecioU3].Value == DBNull.Value ? decimal.Zero : __row.Cells[(int)ColumnasDetalle.PrecioU3].Value) <= 0)
                    {
                        __row.ErrorText = "El campo [Precio Unitario] Prov 3 no valido.";
                    }
                    else if (!Convert.ToBoolean(__row.Cells[(int)ColumnasDetalle.Sugerido1].Value == DBNull.Value ? false : __row.Cells[(int)ColumnasDetalle.Sugerido1].Value)
                        & !Convert.ToBoolean(__row.Cells[(int)ColumnasDetalle.Sugerido2].Value == DBNull.Value ? false : __row.Cells[(int)ColumnasDetalle.Sugerido2].Value)
                        & !Convert.ToBoolean(__row.Cells[(int)ColumnasDetalle.Sugerido3].Value == DBNull.Value ? false : __row.Cells[(int)ColumnasDetalle.Sugerido3].Value))
                    {
                        __row.ErrorText = "Debe seleccionar el precio de algún proveedor como sugerencia.";
                    }
                    else __row.ErrorText = string.Empty;
                }
                catch (Exception ex)
                {
                    __row.ErrorText = ex.Message;
                }
            }
        }

        private void MejorPrecio(DataGridViewCell c1, DataGridViewCell c2, DataGridViewCell c3)
        {
            decimal A = Convert.ToDecimal(c1.Value == DBNull.Value ? decimal.Zero : c1.Value);
            decimal B = Convert.ToDecimal(c2.Value == DBNull.Value ? decimal.Zero : c2.Value);
            decimal C = Convert.ToDecimal(c3.Value == DBNull.Value ? decimal.Zero : c3.Value);

            if (A < B && A < C)
            {
                c1.Style.BackColor = Color.FromArgb(196, 215, 155);
                c2.Style.BackColor = Color.White;
                c3.Style.BackColor = Color.White;
            }
            else
            {
                if (B < A && B < C)
                {
                    c2.Style.BackColor = Color.FromArgb(196, 215, 155);
                    c3.Style.BackColor = Color.White;
                    c1.Style.BackColor = Color.White;
                }
                else
                {
                    c3.Style.BackColor = Color.FromArgb(196, 215, 155);
                    c1.Style.BackColor = Color.White;
                    c2.Style.BackColor = Color.White;
                }
            }  
            
        }

        private void dgvDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < (sender as DataGridView).Rows.Count - 1; i++)
            {
                this.MejorPrecio((sender as DataGridView).Rows[i].Cells[(int)ColumnasDetalle.PrecioU1],
                    (sender as DataGridView).Rows[i].Cells[(int)ColumnasDetalle.PrecioU2],
                    (sender as DataGridView).Rows[i].Cells[(int)ColumnasDetalle.PrecioU3]);
            }
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido1].ColumnIndex == e.ColumnIndex)
                if (!Convert.ToBoolean((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido1].Value == DBNull.Value ? false : (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido1].Value))
                {
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido1].Value = true;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido2].Value = false;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido3].Value = false;

                    Requisicion.frmComentarios frm = new frmComentarios(Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Comentarios].Value == DBNull.Value ? string.Empty : (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Comentarios].Value));
                    DialogResult _result = frm.ShowDialog();
                    if (_result == DialogResult.OK) (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Comentarios].Value = frm.Value;
                }
                else
                {
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido1].Value = false;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido2].Value = false;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido3].Value = false;
                }

            if ((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido2].ColumnIndex == e.ColumnIndex)
                if (!Convert.ToBoolean((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido2].Value == DBNull.Value ? false : (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido2].Value))
                {
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido1].Value = false;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido2].Value = true;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido3].Value = false;

                    Requisicion.frmComentarios frm = new frmComentarios(Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Comentarios].Value == DBNull.Value ? string.Empty : (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Comentarios].Value));
                    DialogResult _result = frm.ShowDialog();
                    if (_result == DialogResult.OK) (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Comentarios].Value = frm.Value;
                }
                else
                {
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido1].Value = false;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido2].Value = false;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido3].Value = false;
                }

            if ((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido3].ColumnIndex == e.ColumnIndex)
                if (!Convert.ToBoolean((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido3].Value == DBNull.Value ? false : (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido3].Value))
                {
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido1].Value = false;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido2].Value = false;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido3].Value = true;

                    Requisicion.frmComentarios frm = new frmComentarios(Convert.ToString((sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Comentarios].Value == DBNull.Value ? string.Empty : (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Comentarios].Value));
                    DialogResult _result = frm.ShowDialog();
                    if (_result == DialogResult.OK) (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Comentarios].Value = frm.Value;
                }
                else
                {
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido1].Value = false;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido2].Value = false;
                    (sender as DataGridView).Rows[e.RowIndex].Cells[(int)ColumnasDetalle.Sugerido3].Value = false;
                }
        }

    }
}
