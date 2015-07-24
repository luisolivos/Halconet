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

namespace Ventas.Ventas
{
    public partial class Garantias : Form
    {
        #region PARAMETROS

        int DocEntry;
        int SlpCode;
        Clases.Logs log;
        public enum Columnas
        {
            LineNum, Articulo, Descripcion, Cantidad, Precio, Garantia, Condiciones
        }

        #endregion PARAMETROS

        #region METODOS
        public Garantias()
        {
            InitializeComponent();
        }

        public void Formato(DataGridView dgv)
        {
            DataGridViewButtonColumn buttonComent = new DataGridViewButtonColumn();
            {
                buttonComent.Name = "btnPhotos";
                buttonComent.HeaderText = "Cargar";
                buttonComent.Width = 100;
                buttonComent.UseColumnTextForButtonValue = true;
            }
            dgv.Columns.Add(buttonComent);

            DataGridViewTextBoxColumn column = (DataGridViewTextBoxColumn)dgv.Columns[(int)Columnas.Condiciones];
            column.MaxInputLength = 500;

            dgv.Columns[(int)Columnas.LineNum].Visible = false;

            dgv.Columns[(int)Columnas.Articulo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Descripcion].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cantidad].ReadOnly = true;
            dgv.Columns[(int)Columnas.Precio].ReadOnly = true;

            dgv.Columns[(int)Columnas.Articulo].Width = 90;
            dgv.Columns[(int)Columnas.Descripcion].Width = 250;
            dgv.Columns[(int)Columnas.Cantidad].Width = 80;
            dgv.Columns[(int)Columnas.Precio].Width = 90;
            dgv.Columns[(int)Columnas.Garantia].Width = 80;
            //dgv.Columns[(int)Columnas.Situacion].Width = 120;
            dgv.Columns[(int)Columnas.Condiciones].Width = 200;

            dgv.Columns[(int)Columnas.Cantidad].HeaderText = "Cantidad\r\nFacturada";
            dgv.Columns[(int)Columnas.Precio].HeaderText = "Precio\r\nUnitario";
            dgv.Columns[(int)Columnas.Garantia].HeaderText = "Cantidad\r\nGarantía";

            dgv.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Precio].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Garantia].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Precio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Garantia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns["Imagenes"].Visible = false;
        }

        public bool ValidarGid(DataGridView dgv)
        {
            foreach (DataGridViewRow item in dgv.Rows)
            {
                item.ErrorText = string.Empty;
                if (Convert.ToInt32(item.Cells[(int)Columnas.Garantia].Value) > Convert.ToInt32(item.Cells[(int)Columnas.Cantidad].Value))
                {
                    item.Cells[(int)Columnas.Garantia].Style.BackColor = Color.Red;
                    item.Cells[(int)Columnas.Garantia].Style.ForeColor = Color.White;

                    item.ErrorText = "La cantidad a garantia no puede ser mayor a la cantidad facturada.";

                    toolStatus.Text = "La cantidad a garantia no puede ser mayor a la cantidad facturada. Línea: " + (item.Index + 1);

                    DataGridViewCell cell = item.Cells[(int)Columnas.Garantia];
                    dgv.CurrentCell = cell;
                    dgv.BeginEdit(true);

                    return false;
                }
                else if ((item.Cells["Imagenes"].Value == null ? string.Empty : item.Cells["Imagenes"].Value.ToString()) == string.Empty && Convert.ToInt32(item.Cells[(int)Columnas.Garantia].Value) > 0)
                {
                    item.ErrorText = "Debe cargar las fotografías.";
                  
                    toolStatus.Text = "Debe cargar las fotografías. Línea:" + (item.Index + 1);

                    return false;
                }
                //else if ((item.Cells[(int)Columnas.Situacion].Value == null ? string.Empty : item.Cells[(int)Columnas.Situacion].Value.ToString()) == string.Empty && Convert.ToInt32(item.Cells[(int)Columnas.Garantia].Value) > 0)
                //{
                //    item.Cells[(int)Columnas.Situacion].Style.BackColor = Color.Red;
                //    item.Cells[(int)Columnas.Situacion].Style.ForeColor = Color.White;

                //    item.ErrorText = "El campo [Situación] no puede estar vacio.";

                //    toolStatus.Text = "El campo [Situación] no puede estar vacio. Línea:" + (item.Index + 1);

                //    DataGridViewCell cell = item.Cells[(int)Columnas.Situacion];
                //    dgv.CurrentCell = cell;
                //    dgv.BeginEdit(true);

                //    return false;
                //}
                else if ((item.Cells[(int)Columnas.Condiciones].Value == null ? string.Empty : item.Cells[(int)Columnas.Condiciones].Value.ToString()) == string.Empty && Convert.ToInt32(item.Cells[(int)Columnas.Garantia].Value) > 0)
                {
                    item.Cells[(int)Columnas.Condiciones].Style.BackColor = Color.Red;
                    item.Cells[(int)Columnas.Condiciones].Style.ForeColor = Color.White;


                    item.ErrorText = "El campo [Comentarios] no puede estar vacio.";

                    toolStatus.Text = "El campo [Comentarios] no puede estar vacio. Línea:" + (item.Index + 1);

                    DataGridViewCell cell = item.Cells[(int)Columnas.Condiciones];
                    dgv.CurrentCell = cell;
                    dgv.BeginEdit(true);

                    return false;
                }
                else
                {
                    item.Cells[(int)Columnas.Garantia].Style.BackColor = Color.White;
                    item.Cells[(int)Columnas.Garantia].Style.ForeColor = Color.Black;

                    //item.Cells[(int)Columnas.Situacion].Style.BackColor = Color.White;
                    //item.Cells[(int)Columnas.Situacion].Style.ForeColor = Color.Black;

                    item.Cells[(int)Columnas.Condiciones].Style.BackColor = Color.White;
                    item.Cells[(int)Columnas.Condiciones].Style.ForeColor = Color.Black;

                    toolStatus.Text = string.Empty;
                }
            }
            return true;
        }

        public void GuardarFotos(string _itemCode, int _lineNum, int _docEntry, string _fotos)
        {
            string[] _photos = _fotos.Split('\t');
            foreach (string item in _photos)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    using (SqlConnection connection = new SqlConnection())
                    {
                        connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.CommandText = "PJ_Garantias";
                            command.CommandTimeout = 0;
                            command.Connection = connection;
                            command.CommandType = CommandType.StoredProcedure;

                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            Image.FromFile(item).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                            command.Parameters.AddWithValue("@TipoConsulta", 4);
                            command.Parameters.AddWithValue("@ItemCode", _itemCode);
                            command.Parameters.AddWithValue("@CantidadGarantia", 0);
                            command.Parameters.AddWithValue("@Dscripcion", string.Empty);
                            command.Parameters.AddWithValue("@Cndicion", string.Empty);
                            command.Parameters.AddWithValue("@DoceEntry", DocEntry);
                            command.Parameters.AddWithValue("@User", string.Empty);
                            command.Parameters.AddWithValue("@SlpCode", SlpCode);
                            command.Parameters.AddWithValue("@LineNum", _lineNum);
                            command.Parameters.AddWithValue("@Photo", ms.GetBuffer());
                            command.Parameters.AddWithValue("@Desde", DateTime.Now);
                            command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                            command.Parameters.AddWithValue("@Cliente", string.Empty);

                            command.Parameters.AddWithValue("@Sucursales", string.Empty);
                            command.Parameters.AddWithValue("@Vendedores", string.Empty);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        
        private void Garantias_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            DataGridView dgv = dgvItems;

            dgv.BorderStyle = BorderStyle.FixedSingle;

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
        }

        private void dgvRepartir_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.dgvItems.Columns[e.ColumnIndex].Name == "btnComentarios" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);


                DataGridViewButtonCell celBoton = this.dgvItems.Rows[e.RowIndex].Cells["btnComentarios"] as DataGridViewButtonCell;
                Icon icoAtomico;

                if (this.dgvItems.Rows[e.RowIndex].Cells["Imagenes"].Value.ToString().Trim() != string.Empty)
                {
                    icoAtomico = Properties.Resources.photo;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 12, e.CellBounds.Top + 4);
                }
                else
                {
                    icoAtomico = Properties.Resources.photo_o;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 12, e.CellBounds.Top + 4);
                }

                this.dgvItems.Rows[e.RowIndex].Height = icoAtomico.Height + 10;
                this.dgvItems.Columns[e.ColumnIndex].Width = icoAtomico.Width + 25;

                e.Handled = true;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtFactura.Text))
                {
                    dgvItems.DataSource = null;
                    dgvItems.Columns.Clear();

                    dtDocDate.Value = DateTime.Now;
                    txtVendedor.Enabled = true;
                    txtVendedor.Text = string.Empty;
                    txtVendedor.Enabled = false;
                    txtCardCode.Text = string.Empty;
                    txtCardName.Text = string.Empty;

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_Garantias", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;

                            command.Parameters.AddWithValue("@TipoConsulta", 1);
                            command.Parameters.AddWithValue("@ItemCode", string.Empty);
                            command.Parameters.AddWithValue("@CantidadGarantia", 0);
                            command.Parameters.AddWithValue("@Dscripcion", string.Empty);
                            command.Parameters.AddWithValue("@Cndicion", string.Empty);
                            command.Parameters.AddWithValue("@DoceEntry", txtFactura.Text);
                            command.Parameters.AddWithValue("@User", ClasesSGUV.Login.NombreUsuario);
                            command.Parameters.AddWithValue("@SlpCode", 0);
                            command.Parameters.AddWithValue("@LineNum", 0);
                            command.Parameters.AddWithValue("@Photo", new System.IO.MemoryStream().GetBuffer());
                            command.Parameters.AddWithValue("@Desde", DateTime.Now);
                            command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                            command.Parameters.AddWithValue("@Cliente", string.Empty);

                            command.Parameters.AddWithValue("@Sucursales", string.Empty);
                            command.Parameters.AddWithValue("@Vendedores", string.Empty);

                            connection.Open();

                            SqlDataReader reader = command.ExecuteReader();
                            if (reader.Read())
                            {
                                dtDocDate.Value = reader.GetDateTime(0);
                                txtVendedor.Enabled = true;
                                txtVendedor.Text = reader.GetString(1);
                                txtVendedor.Enabled = false;
                                SlpCode = reader.GetInt32(2);
                                txtCardCode.Text = reader.GetString(3);
                                txtCardName.Text = reader.GetString(4);
                                DocEntry = reader.GetInt32(5);
                            }
                        }
                    }

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_Garantias", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;

                            command.Parameters.AddWithValue("@TipoConsulta", 2);
                            command.Parameters.AddWithValue("@ItemCode", string.Empty);
                            command.Parameters.AddWithValue("@CantidadGarantia", 0);
                            command.Parameters.AddWithValue("@Dscripcion", string.Empty);
                            command.Parameters.AddWithValue("@Cndicion", string.Empty);
                            command.Parameters.AddWithValue("@DoceEntry", txtFactura.Text);
                            command.Parameters.AddWithValue("@User", string.Empty);
                            command.Parameters.AddWithValue("@SlpCode", 0);
                            command.Parameters.AddWithValue("@LineNum", 0);
                            command.Parameters.AddWithValue("@Photo", new System.IO.MemoryStream().GetBuffer());
                            command.Parameters.AddWithValue("@Desde", DateTime.Now);
                            command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                            command.Parameters.AddWithValue("@Cliente", string.Empty);

                            command.Parameters.AddWithValue("@Sucursales", string.Empty);
                            command.Parameters.AddWithValue("@Vendedores", string.Empty);

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;
                            DataTable table = new DataTable();
                            da.Fill(table);

                            dgvItems.DataSource = table;

                            this.Formato(dgvItems);
                        }
                    }
                }
                else MessageBox.Show("Ingresa el número de factura!", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItems_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView grd1 = (DataGridView)sender;
                grd1.Rows[e.RowIndex].ErrorText = string.Empty;
                grd1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;

                DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
                if (e.ColumnIndex == (int)Columnas.Garantia)
                {
                    if (Convert.ToInt32(row.Cells[(int)Columnas.Garantia].Value) > Convert.ToInt32(row.Cells[(int)Columnas.Cantidad].Value))
                    {
                        DataGridViewCell cell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];

                        toolStatus.Text = "La cantidad a garantia no puede ser mayor a la cantidad facturada. Línea: " + (e.RowIndex + 1);
                    }
                    else
                    {
                        toolStatus.Text = string.Empty;
                        row.Cells[e.ColumnIndex].Style.BackColor = Color.White;
                        row.Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :" + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            try
            {
                int total = 0, guardado = 0;
                bool valido = this.ValidarGid(dgvItems);
                bool save = false;
                if (valido)
                {
                    foreach (DataGridViewRow item in dgvItems.Rows)
                    {
                        if (Convert.ToInt32(item.Cells[(int)Columnas.Garantia].Value) > 0 && !string.IsNullOrEmpty(item.Cells["Imagenes"].Value.ToString()))
                        {
                            total++;
                            using (SqlConnection connection  = new SqlConnection())
                            {
                                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                                using (SqlCommand command = new SqlCommand())
                                {
                                    command.CommandText = "PJ_Garantias";
                                    command.CommandTimeout = 0;
                                    command.Connection = connection;
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                                    command.Parameters.AddWithValue("@ItemCode", item.Cells[(int)Columnas.Articulo].Value.ToString());
                                    command.Parameters.AddWithValue("@CantidadGarantia", Convert.ToInt32(item.Cells[(int)Columnas.Garantia].Value));
                                    command.Parameters.AddWithValue("@Dscripcion", item.Cells[(int)Columnas.Condiciones].Value.ToString());
                                    command.Parameters.AddWithValue("@Cndicion", string.Empty);
                                    command.Parameters.AddWithValue("@DoceEntry", DocEntry);
                                    command.Parameters.AddWithValue("@User", ClasesSGUV.Login.NombreUsuario);
                                    command.Parameters.AddWithValue("@SlpCode", SlpCode);
                                    command.Parameters.AddWithValue("@LineNum", Convert.ToInt32(item.Cells[(int)Columnas.LineNum].Value));
                                    command.Parameters.AddWithValue("@Photo", new System.IO.MemoryStream().GetBuffer());
                                    command.Parameters.AddWithValue("@Desde", DateTime.Now);
                                    command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                                    command.Parameters.AddWithValue("@Cliente", string.Empty);

                                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                                    command.Parameters.AddWithValue("@Vendedores", string.Empty);

                                    connection.Open();
                                    int pp = command.ExecuteNonQuery();

                                    if (pp == 0)
                                    {
                                        MessageBox.Show("Ya existe una garantía para el artículo: " + item.Cells[(int)Columnas.Articulo].Value.ToString(), "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                    else
                                    {
                                        this.GuardarFotos(item.Cells[(int)Columnas.Articulo].Value.ToString(), Convert.ToInt32(item.Cells[(int)Columnas.LineNum].Value), DocEntry, item.Cells["Imagenes"].Value.ToString());
                                        guardado++;
                                    }
                                }
                            }
                            save = true;                     
                        }
                    }
                    if (save && guardado == total) {MessageBox.Show("Registro exitoso!", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information); toolStatus.Text = "Listo";}
                    else if (save && guardado != total && guardado != 0) MessageBox.Show("Se guardaron [" + guardado + "] de [" + total + "] registros.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvItems_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex].Name == "btnPhotos" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = (sender as DataGridView).Rows[e.RowIndex].Cells["btnPhotos"] as DataGridViewButtonCell;
                Icon icoAtomico;

                if (this.dgvItems.Rows[e.RowIndex].Cells["Imagenes"].Value.ToString().Trim() != string.Empty)
                {
                    icoAtomico = Properties.Resources.photo_o;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 13, e.CellBounds.Top + 5);
                }
                else
                {
                    icoAtomico = Properties.Resources.photo;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 13, e.CellBounds.Top + 5);
                }

                (sender as DataGridView).Rows[e.RowIndex].Height = icoAtomico.Height + 10;
                (sender as DataGridView).Columns[e.ColumnIndex].Width = icoAtomico.Width + 30;

                e.Handled = true;
            }
        }

        private void dgvItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((sender as DataGridView).Rows[e.RowIndex].Cells["btnPhotos"].ColumnIndex == e.ColumnIndex)
                {
                    DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
                    Garantia.FotosGarantias form = new Garantia.FotosGarantias(row.Cells["Imagenes"].Value.ToString(), row.Cells[(int)Columnas.Articulo].Value.ToString(), Convert.ToInt32(row.Cells[(int)Columnas.LineNum].Value), DocEntry);
                    form.ShowDialog();

                    (sender as DataGridView).Rows[e.RowIndex].Cells["Imagenes"].Value = form.Images;
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void dgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           // if ((e.Exception) is ConstraintException)
           // {
                DataGridView grd1 = (DataGridView)sender;
                grd1.Rows[e.RowIndex].ErrorText = e.Exception.Message;
                grd1.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = e.Exception.Message;
                MessageBox.Show("Error: " + e.Exception.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);

                e.ThrowException = false;
            //}
        }

        private void txtFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                kryptonButton1_Click(sender, e);
            }
        }

        private void Garantias_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void Garantias_FormClosing(object sender, FormClosingEventArgs e)
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
            
        }
        #endregion 

        
    }
}
