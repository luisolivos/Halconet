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

namespace Ventas.Garantia
{
    public partial class Garantias_Compras : Form
    {
        public enum Columnas
        {
            Folio,
            Factura,
            Alta,
            Vendedor,
            Cliente,
            NombreCliente,
            Linea,
            Articulo,
            Descripcion,
            Cantidad,
            //Situacion,
            Comentarios,
            Estatus,
            Fotos,
            LineNum,
            DocEntry,
            Dias
        }
        Clases.Logs log;

        #region METODOS

        public Garantias_Compras()
        {
            InitializeComponent();
        }

        public void Formato(DataGridView dgv)
        {
            DataGridViewButtonColumn buttonStatus= new DataGridViewButtonColumn();
            {
                buttonStatus.Name = "btnEstatus";
                buttonStatus.HeaderText = "Estatus";
                buttonStatus.Text = "Estatus";
                buttonStatus.Width = 50;
                buttonStatus.UseColumnTextForButtonValue = true;
            }
            dgv.Columns.Add(buttonStatus);

            DataGridViewButtonColumn buttonPhotos = new DataGridViewButtonColumn();
            {
                buttonPhotos.Name = "btnPhotos";
                buttonPhotos.HeaderText = "Fotos";
                buttonPhotos.Width = 100;
                buttonPhotos.UseColumnTextForButtonValue = true;
            }
            dgv.Columns.Add(buttonPhotos);

            dgv.Columns[(int)Columnas.Alta].HeaderText = "Fecha\r\nde alta";
            dgv.Columns[(int)Columnas.Cantidad].HeaderText = "Cantidad a\r\ngarantía";

            dgv.Columns[(int)Columnas.Folio].Width = 50;
            dgv.Columns[(int)Columnas.Factura].Width = 80;
            dgv.Columns[(int)Columnas.Alta].Width = 80;
            dgv.Columns[(int)Columnas.Vendedor].Width = 100;
            dgv.Columns[(int)Columnas.Cliente].Width = 60;
            dgv.Columns[(int)Columnas.NombreCliente].Width = 110;
            dgv.Columns[(int)Columnas.Linea].Width = 70;
            dgv.Columns[(int)Columnas.Articulo].Width = 90;
            dgv.Columns[(int)Columnas.Descripcion].Width = 250;
            dgv.Columns[(int)Columnas.Cantidad].Width = 70;
            dgv.Columns[(int)Columnas.Dias].Width = 70;
            dgv.Columns[(int)Columnas.Comentarios].Width = 100;
            dgv.Columns[(int)Columnas.Estatus].Width = 70;

            dgv.Columns[(int)Columnas.Folio].ReadOnly = true;
            dgv.Columns[(int)Columnas.Factura].ReadOnly = true;
            dgv.Columns[(int)Columnas.Alta].ReadOnly = true;
            dgv.Columns[(int)Columnas.Vendedor].ReadOnly = true;
            dgv.Columns[(int)Columnas.Articulo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Linea].ReadOnly = true;
            dgv.Columns[(int)Columnas.Descripcion].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cantidad].ReadOnly = true;
            dgv.Columns[(int)Columnas.Dias].ReadOnly = true;
            dgv.Columns[(int)Columnas.Comentarios].ReadOnly = true;
            dgv.Columns[(int)Columnas.Estatus].ReadOnly = true;

            dgv.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Dias].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[(int)Columnas.Fotos].Visible = false;
            dgv.Columns[(int)Columnas.LineNum].Visible = false;
            dgv.Columns[(int)Columnas.DocEntry].Visible = false;
        }

        public void Sucursales()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Sucursales);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODAS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            clbSucursal.DataSource = table;
            clbSucursal.DisplayMember = "Nombre";
            clbSucursal.ValueMember = "Codigo";
        }

        public void Vendedores()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Vendedores);
            command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal);
            command.Parameters.AddWithValue("@SlpCode", ClasesSGUV.Login.Vendedor1);
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODOS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            clbVendedor.DataSource = table;
            clbVendedor.DisplayMember = "Nombre";
            clbVendedor.ValueMember = "Codigo";
        }

        #endregion 

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                dgvItems.DataSource = null;
                dgvItems.Columns.Clear();

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_Garantias", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 6);
                        command.Parameters.AddWithValue("@ItemCode", txtFolio.Text);
                        command.Parameters.AddWithValue("@CantidadGarantia", 0);
                        command.Parameters.AddWithValue("@Dscripcion", string.Empty);
                        command.Parameters.AddWithValue("@Cndicion", string.Empty);
                        command.Parameters.AddWithValue("@DoceEntry", txtFactura.Text);
                        command.Parameters.AddWithValue("@User", string.Empty);
                        command.Parameters.AddWithValue("@SlpCode", 0);
                        command.Parameters.AddWithValue("@LineNum", 0);
                        command.Parameters.AddWithValue("@Photo", new System.IO.MemoryStream().GetBuffer());
                        command.Parameters.AddWithValue("@Desde", dtInicio.Value);
                        command.Parameters.AddWithValue("@Hasta", dtFin.Value);
                        command.Parameters.AddWithValue("@Cliente", txtCardCode.Text);

                        command.Parameters.AddWithValue("@Sucursales", clbSucursal.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Vendedores", clbVendedor.SelectedValue.ToString());

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable table = new DataTable();
                        da.Fill(table);

                        dgvItems.DataSource = table;

                        this.Formato(dgvItems);
                      
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (string.IsNullOrEmpty(txtCardCode.Text))
                {
                    groupBox2.Visible = false;
                }
                else
                {
                    txtCliente.Clear();
                    txtPhone2.Clear();
                    txtPhone1.Clear();
                    txtMail.Clear();

                    groupBox2.Visible = true;

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_Garantias", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;

                            command.Parameters.AddWithValue("@TipoConsulta", 9);
                            command.Parameters.AddWithValue("@ItemCode", string.Empty);
                            command.Parameters.AddWithValue("@CantidadGarantia", 0);
                            command.Parameters.AddWithValue("@Dscripcion", string.Empty);
                            command.Parameters.AddWithValue("@Cndicion", string.Empty);
                            command.Parameters.AddWithValue("@DoceEntry", txtFactura.Text);
                            command.Parameters.AddWithValue("@User", string.Empty);
                            command.Parameters.AddWithValue("@SlpCode", 0);
                            command.Parameters.AddWithValue("@LineNum", 0);
                            command.Parameters.AddWithValue("@Photo", new System.IO.MemoryStream().GetBuffer());
                            command.Parameters.AddWithValue("@Desde", dtInicio.Value);
                            command.Parameters.AddWithValue("@Hasta", dtFin.Value);
                            command.Parameters.AddWithValue("@Cliente", txtCardCode.Text);

                            command.Parameters.AddWithValue("@Sucursales", clbSucursal.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@Vendedores", clbVendedor.SelectedValue.ToString());

                            connection.Open();

                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.Read())
                            {
                                txtCliente.Text = reader.GetString(3);
                                txtPhone1.Text = reader.GetString(1);
                                txtPhone2.Text = reader.GetString(2);
                                txtMail.Text = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar información de contacto: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if ((sender as DataGridView).Rows[e.RowIndex].Cells["btnEstatus"].ColumnIndex == e.ColumnIndex)
                {
                    DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
                    Garantia.Estatus form = new Estatus(row.Cells[(int)Columnas.Estatus].Value.ToString(), row.Cells[(int)Columnas.Articulo].Value.ToString(),
                        Convert.ToInt32(row.Cells[(int)Columnas.DocEntry].Value), Convert.ToInt32(row.Cells[(int)Columnas.LineNum].Value));
                    form.ShowDialog();

                    kryptonButton1_Click(sender, e);
                }
                if ((sender as DataGridView).Rows[e.RowIndex].Cells["btnPhotos"].ColumnIndex == e.ColumnIndex)
                {
                    DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];
                    Garantia.FotosCompras form = new Garantia.FotosCompras(row.Cells[(int)Columnas.Articulo].Value.ToString(),
                        Convert.ToInt32(row.Cells[(int)Columnas.LineNum].Value), Convert.ToInt32(row.Cells[(int)Columnas.DocEntry].Value));
                    form.ShowDialog();

                    kryptonButton1_Click(sender, e);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void Garantias_Compras_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                dtInicio.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                this.Sucursales();
                this.Vendedores();

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.End) kryptonButton1_Click(sender, e);            
        }

        private void Garantias_Compras_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void Garantias_Compras_FormClosing(object sender, FormClosingEventArgs e)
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
            ExportarAExcel expt = new ExportarAExcel();
            if (expt.Exportar(dgvItems, false))
                MessageBox.Show("El archivo se creó con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
