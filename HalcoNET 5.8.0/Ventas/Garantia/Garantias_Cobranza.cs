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
using System.IO;

namespace Ventas.Garantia
{
    public partial class Garantias_Cobranza : Form
    {
        decimal DocTotal;
        decimal Monto_Garantia = 0;
        Clases.Logs log;

        BinaryWriter bw;
        FileStream fs;
        byte[] bytes;

        public enum Columnas
        {
            Folio,
            Alta, 
            Articulo,
            Descripcion,
            Cantidad,
            PrecioU,
            Estatus,
            Detalle,
            Dias,
            File
        }

        public Garantias_Cobranza()
        {
            InitializeComponent();
        }

        public void Formato(DataGridView dgv)
        {
            DataGridViewButtonColumn buttonDictamen = new DataGridViewButtonColumn();
            {
                buttonDictamen.Name = "btnDictamen";
                buttonDictamen.HeaderText = "Dictamen";
                buttonDictamen.Text = "";
                buttonDictamen.Width = 50;
                buttonDictamen.UseColumnTextForButtonValue = true;
            }
            dgv.Columns.Add(buttonDictamen);

            dgv.Columns[(int)Columnas.File].Visible = false;

            dgv.Columns[(int)Columnas.Folio].Width = 50;
            dgv.Columns[(int)Columnas.Alta].Width = 80;
            dgv.Columns[(int)Columnas.Articulo].Width = 90;
            dgv.Columns[(int)Columnas.Descripcion].Width = 250;
            dgv.Columns[(int)Columnas.Cantidad].Width = 80;
            dgv.Columns[(int)Columnas.PrecioU].Width = 80;
            dgv.Columns[(int)Columnas.Estatus].Width = 80;
            dgv.Columns[(int)Columnas.Dias].Width = 80;

            dgv.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.PrecioU].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Cantidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.PrecioU].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public string DecodificarArchivo(string sBase64, string _fileName, string _extension)
        {
            string sFileTemporal = _fileName + _extension;

            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fs = new FileStream(dialog.SelectedPath + "\\" + sFileTemporal, FileMode.Create);

                    bw = new BinaryWriter(fs); bytes = Convert.FromBase64String(sBase64);

                    bw.Write(bytes);

                    MessageBox.Show("Se creo correctamente el archivo en:\r\n" + dialog.SelectedPath + "\\" + sFileTemporal, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

                return sFileTemporal;
            }
            catch
            {
                MessageBox.Show("Ocurrió un error al leer la imagen.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return sFileTemporal = string.Empty;
            }
            finally
            {
                fs.Close();
                // bytes = null;
                bw = null;
                sBase64 = null;
            }
        }


        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            try
            {
                DocTotal = decimal.Zero;
                Monto_Garantia = decimal.Zero;
    
                dtDocDate.Value = DateTime.Now;
                txtVendedor.Enabled = true;
                txtVendedor.Text = string.Empty;
                txtVendedor.Enabled = false;
                txtCardCode.Text = string.Empty;
                txtCardName.Text = string.Empty;
                DocTotal = decimal.Zero;
                txtDocTotal.Text = DocTotal.ToString("C2");

                if (!String.IsNullOrEmpty(txtFactura.Text))
                {
                    dgvItems.DataSource = null;
                    dgvItems.Columns.Clear();

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
                            command.Parameters.AddWithValue("@User", string.Empty);
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
                                txtCardCode.Text = reader.GetString(3);
                                txtCardName.Text = reader.GetString(4);
                                DocTotal = reader.GetDecimal(6);
                                txtDocTotal.Text = DocTotal.ToString("C2");

                            }
                        }
                    }

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_Garantias", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;

                            command.Parameters.AddWithValue("@TipoConsulta", 8);
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

                            if (table.Rows.Count > 0)
                            {
                                Monto_Garantia = (from item in table.AsEnumerable()
                                                  select item.Field<decimal>("Cantidad") * item.Field<decimal>("Precio unitario")).Sum();

                                txtEnGarantia.Text = Monto_Garantia.ToString("C2");

                                txtTotal.Text = (DocTotal - Monto_Garantia).ToString("C2");

                            }
                            else if (table.Rows.Count <= 0 && txtCardCode.Text != string.Empty)
                            {
                                txtDocTotal.Text = DocTotal.ToString("C2");
                                txtEnGarantia.Text = Monto_Garantia.ToString("C2");
                                txtTotal.Text = (DocTotal - Monto_Garantia).ToString("C2");


                                MessageBox.Show("La factura " + txtFactura.Text + " no tiene registro de garantía.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) kryptonButton1_Click(sender, e);
        }

        private void Garantias_Cobranza_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Garantias_Cobranza_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void Garantias_Cobranza_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void dgvItems_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex].Name == "btnDictamen" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = (sender as DataGridView).Rows[e.RowIndex].Cells["btnDictamen"] as DataGridViewButtonCell;
                Icon icoAtomico;


                if (this.dgvItems.Rows[e.RowIndex].Cells["File"].Value.ToString().Trim() == string.Empty)
                {
                    icoAtomico = Properties.Resources.not;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 13, e.CellBounds.Top + 5);
                }
                else
                {
                    icoAtomico = Properties.Resources.File;
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
                if ((sender as DataGridView).Rows[e.RowIndex].Cells["btnDictamen"].ColumnIndex == e.ColumnIndex)
                {
                    DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_Garantias", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;

                            command.Parameters.AddWithValue("@TipoConsulta", 10);
                            command.Parameters.AddWithValue("@ItemCode", string.Empty);
                            command.Parameters.AddWithValue("@CantidadGarantia", 0);
                            command.Parameters.AddWithValue("@Dscripcion", string.Empty);
                            command.Parameters.AddWithValue("@Cndicion", string.Empty);
                            command.Parameters.AddWithValue("@DoceEntry", row.Cells[(int)Columnas.Folio].Value);
                            command.Parameters.AddWithValue("@User", string.Empty);
                            command.Parameters.AddWithValue("@SlpCode", 0);
                            command.Parameters.AddWithValue("@LineNum", 0);
                            command.Parameters.AddWithValue("@Photo", new System.IO.MemoryStream().GetBuffer());
                            command.Parameters.AddWithValue("@Desde", DateTime.Now);
                            command.Parameters.AddWithValue("@Hasta", DateTime.Now);
                            command.Parameters.AddWithValue("@Cliente", "Code");

                            command.Parameters.AddWithValue("@Sucursales", string.Empty);
                            command.Parameters.AddWithValue("@Vendedores", string.Empty);

                            connection.Open();

                            SqlDataReader reader = command.ExecuteReader();
                            string _Filename = null;
                            string _File = null;
                            string _Extension = null;

                            if (reader.Read())
                            {
                                _Filename = reader[0].ToString();
                                _File = reader[2].ToString();
                                _Extension = reader[1].ToString();
                                
                            }

                            if (_File != null)
                            {
                                this.DecodificarArchivo(_File, _Filename, _Extension);
                            }
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
