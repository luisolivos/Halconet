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
    public partial class frmPE : Form
    {
        
        string FFM, FLT, IGA;
        string[] Array;
        DataTable __Datos = new DataTable();
        Random r = new Random(DateTime.Now.Millisecond);

        public enum Columnas
        {
            ID, 
            Proveedor,
            Carga,
            ETA, 
            Arriboalmacen,
            MesEntrada,
            Almacen,
           // pdf1,
            FACTURA,
            FFM,
          //  pdf2,
            FFT,
          //  pdf3,
            IGA,
            PAGOS,
            PE,
            PrecioEntrega,
            Otros,
            Comentarios
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.ID].Width = 15;
            dgv.Columns[(int)Columnas.Proveedor].Width = 200;
            dgv.Columns[(int)Columnas.Carga].Width = 70;
            dgv.Columns[(int)Columnas.ETA].Width = 70;
            dgv.Columns[(int)Columnas.FACTURA].Width = 100;
            dgv.Columns[(int)Columnas.Arriboalmacen].Width = 70;
            dgv.Columns[(int)Columnas.MesEntrada].Width = 70;
            dgv.Columns[(int)Columnas.Almacen].Width = 60;
            dgv.Columns[(int)Columnas.FFM].Width = 100;
            dgv.Columns[(int)Columnas.FFT].Width = 100;
            dgv.Columns[(int)Columnas.IGA].Width = 100;
            dgv.Columns[(int)Columnas.PAGOS].Width = 100;
            dgv.Columns[(int)Columnas.Otros].Width = 100;
            dgv.Columns[(int)Columnas.PE].Width = 30;
            dgv.Columns[(int)Columnas.PrecioEntrega].Width = 80;
            dgv.Columns[(int)Columnas.Comentarios].Width = 120;

            dgv.Columns[(int)Columnas.FFM].DefaultCellStyle.BackColor = Color.FromName("Info");
            dgv.Columns[(int)Columnas.FACTURA].DefaultCellStyle.BackColor = Color.FromName("Info");            
            dgv.Columns[(int)Columnas.FFT].DefaultCellStyle.BackColor = Color.FromName("Info");
            dgv.Columns[(int)Columnas.IGA].DefaultCellStyle.BackColor = Color.FromName("Info");
            dgv.Columns[(int)Columnas.Otros].DefaultCellStyle.BackColor = Color.FromName("Info");
            dgv.Columns[(int)Columnas.PAGOS].DefaultCellStyle.BackColor = Color.FromName("Info");

            dgv.Columns[(int)Columnas.FFT].ReadOnly = true;
            dgv.Columns[(int)Columnas.FFM].ReadOnly = true;
            dgv.Columns[(int)Columnas.FACTURA].ReadOnly = true;
            dgv.Columns[(int)Columnas.IGA].ReadOnly = true;
            dgv.Columns[(int)Columnas.Otros].ReadOnly = true;

            dgv.Columns[(int)Columnas.ID].ReadOnly = true;
            dgv.Columns[(int)Columnas.ID].Visible = false;

            if ((int)ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCompras)
                dgv.Columns[(int)Columnas.PE].ReadOnly = true;

        }

        public frmPE()
        {
            InitializeComponent();
        }

        public void CheckPDF(string __valor, DataGridViewCell __cell)
        {
            foreach (string pdf in Array)
            {
                if (System.IO.Path.GetFileNameWithoutExtension(pdf).ToUpper().Equals(__valor.ToUpper()))
                {
                    __cell.Value = true;
                    break;
                }
            }
        }

        private void frmPE_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                btnGuardar.Enabled = ClasesSGUV.Login.Edit == "Y";
                dgvIndicadores.AllowUserToAddRows = ClasesSGUV.Login.Edit == "Y";
                dgvIndicadores.AllowUserToDeleteRows = ClasesSGUV.Login.Edit == "Y";
                dgvIndicadores.ReadOnly = !(ClasesSGUV.Login.Edit == "Y");

                __Datos.Clear();

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_PreciosEntrega", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);

                        // DataTable tbl = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(__Datos);

                        dgvIndicadores.DataSource = __Datos;
                        Array = System.IO.Directory.GetFiles("\\\\192.168.2.98\\Digitalización\\PE\\");


                        //foreach (DataGridViewRow item in dgvIndicadores.Rows)
                        //{
                        //    if (!item.IsNewRow)
                        //    {
                        //        FFM = (item.Cells[(int)Columnas.FFM].Value == DBNull.Value ? string.Empty : item.Cells[(int)Columnas.FFM].Value).ToString();
                        //        this.CheckPDF(FFM, item.Cells[(int)Columnas.pdf1]);

                        //        FLT = (item.Cells[(int)Columnas.FFT].Value == DBNull.Value ? string.Empty : item.Cells[(int)Columnas.FFT].Value).ToString();
                        //        this.CheckPDF(FLT, item.Cells[(int)Columnas.pdf2]);

                        //        IGA = (item.Cells[(int)Columnas.IGA].Value == DBNull.Value ? string.Empty : item.Cells[(int)Columnas.IGA].Value).ToString();
                        //        this.CheckPDF(IGA, item.Cells[(int)Columnas.pdf3]);
                        //    }
                        //}

                        this.Formato(dgvIndicadores);

                        __Datos.AcceptChanges();

                        toolStatus.Text = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            frmPE_Load(sender, e);
        }
        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow row in __Datos.Rows)
                {
                    if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                    {
                        using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            using (SqlCommand command = new SqlCommand("sp_PreciosEntrega", connection))
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@TipoConsulta", 2);
                                command.Parameters.AddWithValue("@Proveedor", row.Field<string>("Proveedor"));
                                command.Parameters.AddWithValue("@Carga", row.Field<string>("Carga"));
                                command.Parameters.AddWithValue("@ETA", row.Field<DateTime>("ETA"));
                                command.Parameters.AddWithValue("@FechaArribo", row.Field<DateTime>("Arribo Almacén"));
                                command.Parameters.AddWithValue("@EntradaSAP", row.Field<string>("Mes Entrada SAP"));
                                command.Parameters.AddWithValue("@Almacen", row.Field<string>("Almacén"));
                                command.Parameters.AddWithValue("@FFM", row.Field<string>("Factura Flete Marítimo"));
                                command.Parameters.AddWithValue("@FFT", row.Field<string>("Factura Flete Terrestre"));
                                command.Parameters.AddWithValue("@IGA", row.Field<string>("Impuestos y Gtos Aduanales"));
                                command.Parameters.AddWithValue("@Pagos", row.Field<string>("PAGOS"));
                                command.Parameters.AddWithValue("@Factura", row.Field<string>("FACTURA"));
                                command.Parameters.AddWithValue("@Otros", row.Field<string>("Otros"));
                                command.Parameters.AddWithValue("@Comentarios", row.Field<string>("Comentarios"));
                                command.Parameters.AddWithValue("@PE", row.Field<bool>("P.E."));
                                command.Parameters.AddWithValue("@PrecioEntrega", row.Field<string>("Num de Precio Entrega"));
                                command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);
                                command.Parameters.AddWithValue("@Code", row.Field<Int64?>("Code") == null ? 0 : row.Field<Int64?>("Code"));

                                connection.Open();

                                command.ExecuteNonQuery();
                            }
                        }

                    }

                   
                }

                toolStatus.Text = "Listo";
                frmPE_Load(sender, e);                
            }
            catch (Exception ex)
            {
                toolStatus.Text = ex.Message;
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvIndicadores_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            try
            {
                DataGridViewRow row = e.Row;

                DialogResult __resul = MessageBox.Show("¿Desea eliminar esta linea?\r\n Proveedor:" + row.Cells[(int)Columnas.Proveedor].Value, "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (__resul == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
                else
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_PreciosEntrega", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", 3);
                            command.Parameters.AddWithValue("@Code", row.Cells[(int)Columnas.ID].Value);
                            command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);
                            command.Parameters.AddWithValue("@Activo", "N");

                            connection.Open();

                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                toolStatus.Text = ex.Message;
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvIndicadores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //try
            //{
            //    DataGridViewRow Row = dgvIndicadores.CurrentRow;
            //    DataGridViewCell cell = dgvIndicadores.CurrentCell;

            //    System.Diagnostics.Process.Start(ClasesSGUV.Propiedades.pathDigitalizacion + "\\PE\\" + cell.Value.ToString());
            //}
            //catch (Exception)
            //{

            //}
        }

        private void dgvIndicadores_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {

                e.Row.Cells[(int)Columnas.PE].Value = false;
            }
            catch (Exception)
            {

            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    DataTable tbl = new DataTable();
                    tbl = (from item in __Datos.AsEnumerable()
                           where item.Field<bool>("P.E.") == false
                           select item).CopyToDataTable();

                    dgvIndicadores.DataSource = tbl;
                }
                else
                {
                    dgvIndicadores.DataSource = __Datos;
                }
            }
            catch (Exception)
            {
                toolStatus.Text = "No se encontraron resultados.";
            }

        }

        public string NameFolder(int index)
        {
            int aleatorio3 = r.Next(0, 100000);
            string folder = dgvIndicadores.Columns[index].HeaderText + "_" + aleatorio3.ToString("000000");

            return folder;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewRow Row = dgvIndicadores.CurrentRow;
                DataGridViewCell cell = dgvIndicadores.CurrentCell;

                if (string.IsNullOrEmpty(dgvIndicadores.Rows[Row.Index].Cells[cell.ColumnIndex].Value.ToString()))
                {

                    int aleatorio3 = r.Next(0, 100000);
                    string __carpeta = this.NameFolder(cell.ColumnIndex);

                    bool __exist = System.IO.Directory.Exists(ClasesSGUV.Propiedades.pathDigitalizacion + "\\PE\\" + __carpeta);

                    while (__exist)
                    {
                        __carpeta = this.NameFolder(cell.ColumnIndex);
                        __exist = System.IO.Directory.Exists(ClasesSGUV.Propiedades.pathDigitalizacion + "\\PE\\" + __carpeta);

                    }

                    dgvIndicadores.Rows[Row.Index].Cells[cell.ColumnIndex].Value = __carpeta;
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Multiselect = true;
                    ofd.FileName = "";
                    string[] filePath;
                    if (DialogResult.OK == ofd.ShowDialog(this))
                    {
                        filePath = ofd.FileNames;
                        if (filePath.Count() > 0)
                        {
                            System.IO.Directory.CreateDirectory(ClasesSGUV.Propiedades.pathDigitalizacion + "\\PE\\" + __carpeta);
                            foreach (string item in filePath)
                            {
                                string aux = string.Empty;
                                aux = System.IO.Path.GetFileName(item);
                                if (System.IO.File.Exists(ClasesSGUV.Propiedades.pathDigitalizacion + "\\PE\\" + __carpeta + "\\" + aux))
                                {
                                    if (MessageBox.Show("Ya se ha cargado un archivo con nombre " + aux + ", ¿Desea reemplazarlo?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        System.IO.File.Copy(item, ClasesSGUV.Propiedades.pathDigitalizacion + "\\PE\\" + __carpeta + "\\" + aux, true);
                                    }

                                }
                                else
                                {
                                    System.IO.File.Copy(item, ClasesSGUV.Propiedades.pathDigitalizacion + "\\PE\\" + __carpeta + "\\" + aux, true);
                                }
                            }

                        }
                    }
                    else
                    {
                        dgvIndicadores.Rows[Row.Index].Cells[cell.ColumnIndex].Value = string.Empty;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void dgvIndicadores_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                dgvIndicadores.Columns[(int)Columnas.FFM].ContextMenuStrip = null;
                dgvIndicadores.Columns[(int)Columnas.FFT].ContextMenuStrip = null;
                dgvIndicadores.Columns[(int)Columnas.IGA].ContextMenuStrip = null;
                dgvIndicadores.Columns[(int)Columnas.Otros].ContextMenuStrip = null;
                dgvIndicadores.Columns[(int)Columnas.PAGOS].ContextMenuStrip = null;

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    dgvIndicadores.CurrentCell = dgvIndicadores.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    if (e.ColumnIndex == (int)Columnas.FFM
                        | e.ColumnIndex == (int)Columnas.FFT
                        | e.ColumnIndex == (int)Columnas.IGA
                        | e.ColumnIndex == (int)Columnas.PAGOS
                         | e.ColumnIndex == (int)Columnas.FACTURA
                        | e.ColumnIndex == (int)Columnas.Otros)
                    {
                        dgvIndicadores.Columns[(int)Columnas.FFM].ContextMenuStrip = contextMenu;
                        dgvIndicadores.Columns[(int)Columnas.FFT].ContextMenuStrip = contextMenu;
                        dgvIndicadores.Columns[(int)Columnas.IGA].ContextMenuStrip = contextMenu;
                        dgvIndicadores.Columns[(int)Columnas.Otros].ContextMenuStrip = contextMenu;
                        dgvIndicadores.Columns[(int)Columnas.PAGOS].ContextMenuStrip = contextMenu;
                        dgvIndicadores.Columns[(int)Columnas.FACTURA].ContextMenuStrip = contextMenu;
                    }
                }
            }
            catch (Exception) { }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                //toolStatus.Text = string.Empty;
                DataGridViewRow Row = dgvIndicadores.CurrentRow;
                DataGridViewCell cell = dgvIndicadores.CurrentCell;
                if (!string.IsNullOrEmpty(cell.Value.ToString()))
                {
                    System.Diagnostics.Process.Start(ClasesSGUV.Propiedades.pathDigitalizacion + "\\PE\\" + cell.Value.ToString());
                }
                else
                {
                   // toolStatus.Text = "No se han adjuntado archivos al precio de entrega seleccionado.";
                }
            }
            catch (Exception)
            {
                
            }
        }

               

    }
}
