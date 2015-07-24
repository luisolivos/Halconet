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

namespace Compras.Transferencias_Compras
{
    public partial class Transferencias : Form
    {
        DataTable Almacenes = new DataTable();
        string AlmacenOrigen;
        string AlmacenDestino;
        Clases.Logs log;

        public enum Columnas
        {
            Linea,
            Articulo,
            Descripcion,
            Clasificacion,
            Stock_Origen,
            Ideal_Origen,
            Disponible,
            Stock_Destino,
            Ideal_Destino,
            Solicitado,
            Transferible,
            Unidad,
            Embalaje,
            Peso,
            TotalPeso
        }

        public enum ColumnasM
        {
            Linea,
            Articulo,
            Descripcion,
            Clasificacion,
            Stock_Origen,
            Ideal_Origen,
            Disponible,
            Stock_Destino,
            Ideal_Destino,
            Solicitado,SolicitadoM,
            Transferible,
            TransferibleM, 
            Unidad,
            Embalaje,
            Peso,
            TotalPeso
        }

        public enum ColumnasDetalle
        {
            Articulo, Almacen, Ideal, Stock, Solicitado, Diferencia, VecesIdeal
        }

        public Transferencias()
        {
            InitializeComponent();
        }

        public void CargarLinea(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 2);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public void CargarProveedores(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public void CargarLinea(CheckedListBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 2);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public void CargarProveedores(CheckedListBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public void FormatoTransferencia(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Linea].ReadOnly = true;
            dgv.Columns[(int)Columnas.Articulo].ReadOnly = true;
            dgv.Columns[(int)Columnas.Descripcion].ReadOnly = true;
            dgv.Columns[(int)Columnas.Clasificacion].ReadOnly = true;
            dgv.Columns[(int)Columnas.Stock_Origen].ReadOnly = true;
            dgv.Columns[(int)Columnas.Ideal_Origen].ReadOnly = true;
            dgv.Columns[(int)Columnas.Disponible].ReadOnly = true;
            dgv.Columns[(int)Columnas.Stock_Destino].ReadOnly = true;
            dgv.Columns[(int)Columnas.Ideal_Destino].ReadOnly = true;
            dgv.Columns[(int)Columnas.Solicitado].ReadOnly = true;
            dgv.Columns[(int)Columnas.Transferible].ReadOnly = false;
            dgv.Columns[(int)Columnas.Unidad].ReadOnly = true;
            dgv.Columns[(int)Columnas.Embalaje].ReadOnly = false;

            dgv.Columns[(int)Columnas.Peso].Visible = false;
            dgv.Columns[(int)Columnas.Unidad].Visible = false;
            dgv.Columns[(int)Columnas.Peso].ReadOnly = true;

            dgv.Columns[(int)Columnas.Linea].Width = 70;
            dgv.Columns[(int)Columnas.Articulo].Width = 80;
            dgv.Columns[(int)Columnas.Descripcion].Width = 220;
            dgv.Columns[(int)Columnas.Clasificacion].Width = 70;
            dgv.Columns[(int)Columnas.Stock_Origen].Width = 90;
            dgv.Columns[(int)Columnas.Ideal_Origen].Width = 90;
            dgv.Columns[(int)Columnas.Disponible].Width = 90;
            dgv.Columns[(int)Columnas.Stock_Destino].Width = 90;
            dgv.Columns[(int)Columnas.Ideal_Destino].Width = 90;
            dgv.Columns[(int)Columnas.Solicitado].Width = 90;
            dgv.Columns[(int)Columnas.Transferible].Width = 90;
            dgv.Columns[(int)Columnas.Unidad].Width = 90;
            dgv.Columns[(int)Columnas.Embalaje].Width = 90;

            dgv.Columns[(int)Columnas.Stock_Origen].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Ideal_Origen].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Disponible].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Stock_Destino].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Ideal_Destino].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Solicitado].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Transferible].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Unidad].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Embalaje].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)Columnas.Peso].DefaultCellStyle.Format = "N2";
            dgv.Columns[(int)Columnas.TotalPeso].DefaultCellStyle.Format = "N3";

            dgv.Columns[(int)Columnas.Clasificacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Stock_Origen].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Ideal_Origen].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Disponible].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Stock_Destino].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Ideal_Destino].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Solicitado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Transferible].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Unidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Embalaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Peso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.TotalPeso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Stock_Origen].HeaderText = AlmacenOrigen + " STOCK";
            dgv.Columns[(int)Columnas.Ideal_Origen].HeaderText = AlmacenOrigen + " IDEAL";
            dgv.Columns[(int)Columnas.Stock_Destino].HeaderText = AlmacenDestino + " STOCK";
            dgv.Columns[(int)Columnas.Ideal_Destino].HeaderText = AlmacenDestino + " IDEAL";
        }

        public void FormatoTransferenciaM(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasM.Linea].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Articulo].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Descripcion].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Clasificacion].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Stock_Origen].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Ideal_Origen].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Disponible].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Stock_Destino].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Ideal_Destino].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Solicitado].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Transferible].ReadOnly = false;
            dgv.Columns[(int)ColumnasM.SolicitadoM].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.TransferibleM].ReadOnly = false;
            dgv.Columns[(int)ColumnasM.Unidad].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.Embalaje].ReadOnly = false;
            dgv.Columns[(int)ColumnasM.Peso].ReadOnly = true;
            dgv.Columns[(int)ColumnasM.TotalPeso].ReadOnly = false;

            dgv.Columns[(int)ColumnasM.Unidad].Visible = false;
            dgv.Columns[(int)ColumnasM.Peso].Visible = false;

            dgv.Columns[(int)ColumnasM.Linea].Width = 70;
            dgv.Columns[(int)ColumnasM.Articulo].Width = 80;
            dgv.Columns[(int)ColumnasM.Descripcion].Width = 220;
            dgv.Columns[(int)ColumnasM.Clasificacion].Width = 70;
            dgv.Columns[(int)ColumnasM.Stock_Origen].Width = 90;
            dgv.Columns[(int)ColumnasM.Ideal_Origen].Width = 90;
            dgv.Columns[(int)ColumnasM.Disponible].Width = 90;
            dgv.Columns[(int)ColumnasM.Stock_Destino].Width = 90;
            dgv.Columns[(int)ColumnasM.Ideal_Destino].Width = 90;
            dgv.Columns[(int)ColumnasM.Solicitado].Width = 90;
            dgv.Columns[(int)ColumnasM.Transferible].Width = 90;
            dgv.Columns[(int)ColumnasM.SolicitadoM].Width = 90;
            dgv.Columns[(int)ColumnasM.TransferibleM].Width = 90;
            dgv.Columns[(int)ColumnasM.Unidad].Width = 90;
            dgv.Columns[(int)ColumnasM.Embalaje].Width = 90;

            dgv.Columns[(int)ColumnasM.Stock_Origen].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasM.Ideal_Origen].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasM.Disponible].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasM.Stock_Destino].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasM.Ideal_Destino].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasM.Solicitado].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasM.Transferible].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasM.SolicitadoM].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasM.TransferibleM].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumnasM.Unidad].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasM.Embalaje].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasM.Peso].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasM.TotalPeso].DefaultCellStyle.Format = "N3";

            dgv.Columns[(int)ColumnasM.Clasificacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasM.Stock_Origen].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.Ideal_Origen].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.Disponible].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.Stock_Destino].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.Ideal_Destino].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.Solicitado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.Transferible].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.SolicitadoM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.TransferibleM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.Unidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.Embalaje].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.Peso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasM.TotalPeso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasM.Stock_Origen].HeaderText = AlmacenOrigen + " STOCK";
            dgv.Columns[(int)ColumnasM.Ideal_Origen].HeaderText = AlmacenOrigen + " IDEAL";
            dgv.Columns[(int)ColumnasM.Stock_Destino].HeaderText = AlmacenDestino + " STOCK";
            dgv.Columns[(int)ColumnasM.Ideal_Destino].HeaderText = AlmacenDestino + " IDEAL";
        }

        public void CargarVentas(string _articulo, string _almacen, DataGridView _dg)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 4);
            command.Parameters.AddWithValue("@Articulo", _articulo);
            command.Parameters.AddWithValue("@Linea", string.Empty);
            command.Parameters.AddWithValue("@AlmacenDestino", _almacen);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);

            table.TableName = "Detalle";
            _dg.DataSource = table;
        }

        public void CargarDetalle(string _articulo, string _almacen, DataGridView _dg)
        {
            SqlCommand command = new SqlCommand("PJ_TransferenciasP", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 3);
            command.Parameters.AddWithValue("@Articulo", _articulo);
            command.Parameters.AddWithValue("@Linea", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", _almacen);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@Importacion", string.Empty);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);

            table.TableName = "Detalle";
            _dg.DataSource = table;
        }

        public void FormatoDetalle(DataGridView _dg)
        {
            _dg.Columns[(int)ColumnasDetalle.Articulo].Width = 90;
            _dg.Columns[(int)ColumnasDetalle.Almacen].Width = 80;
            _dg.Columns[(int)ColumnasDetalle.Ideal].Width = 90;
            _dg.Columns[(int)ColumnasDetalle.Stock].Width = 90;
            _dg.Columns[(int)ColumnasDetalle.Solicitado].Width = 90;
            _dg.Columns[(int)ColumnasDetalle.Diferencia].Width = 90;
            _dg.Columns[(int)ColumnasDetalle.VecesIdeal].Width = 90;

            _dg.Columns[(int)ColumnasDetalle.Ideal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Solicitado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.Diferencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            _dg.Columns[(int)ColumnasDetalle.VecesIdeal].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            _dg.Columns[(int)ColumnasDetalle.Ideal].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Stock].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Solicitado].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.Diferencia].DefaultCellStyle.Format = "N0";
            _dg.Columns[(int)ColumnasDetalle.VecesIdeal].DefaultCellStyle.Format = "N2";
        }

        private void txtOrigen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string _almacen = txtOrigen.Text.Substring(0,2);

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_TransferenciasP", connection))
                    {
                        command.CommandTimeout = 0;
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@Linea", string.Empty);
                        command.Parameters.AddWithValue("@Proveedor", string.Empty);
                        command.Parameters.AddWithValue("@AlmacenOrigen", _almacen);
                        command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
                        command.Parameters.AddWithValue("@Importacion", string.Empty);

                        DataTable table = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(table);

                        txtDestino.DataSource = null;
                        txtDestino.DataSource = table;
                        txtDestino.DisplayMember = "Nombre";
                        txtDestino.ValueMember = "Codigo";

                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private string Cadena(CheckedListBox clb)
        {
            StringBuilder stb = new StringBuilder();
            foreach (DataRowView item in clb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clb.ToString().Equals(string.Empty))
                    {
                        stb.Append(",");
                    }
                    stb.Append(item["Codigo"].ToString());
                }
            }
            if (clb.CheckedItems.Count == 0)
            {
                stb.Clear();
                stb.Append("Todo");

            }
            if (clb.CheckedItems.Count == clb.Items.Count)
            {
                stb.Clear();
                stb.Append("Todo");
            }

            return stb.ToString();
        }


        private void Transferencias_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                //CargarLinea(cbLinea, "");

                //CargarProveedores(cbProveedor, "");

                CargarLinea(clbLinea, "Todas");

                CargarProveedores(clbProveedor, "Todos");

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Esperar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
        }
        private void Continuar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }

        private void clbSucursal_Click(object sender, EventArgs e)
        {
            if ((sender as CheckedListBox).SelectedIndex == 0)
            {
                if ((sender as CheckedListBox).CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < (sender as CheckedListBox).Items.Count; item++)
                    {
                        (sender as CheckedListBox).SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < (sender as CheckedListBox).Items.Count; item++)
                    {
                        (sender as CheckedListBox).SetItemChecked(item, true);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();
                if (!string.IsNullOrEmpty(txtOrigen.Text))
                {
                    label1.Text = string.Empty;

                    string _almacen = txtOrigen.Text.Substring(0, 2);
                    AlmacenOrigen = txtOrigen.Text;
                    AlmacenDestino = txtDestino.Text;
                    gridTransferencias.DataSource = null;
                    gridTransferencias.Columns.Clear();

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_TransferenciasP", connection))
                        {
                            string Lineas = this.Cadena(clbLinea);
                            string Provs = this.Cadena(clbProveedor);

                             string imp = string.Empty;
                            if (rbTodo.Checked)
                                imp = "T";
                            if (rbNacional.Checked)
                                imp = "N";
                            if (rbImportacion.Checked)
                                imp = "I";

                            command.CommandTimeout = 0;
                            command.CommandType = CommandType.StoredProcedure;
                            if (!checkBox1.Checked) command.Parameters.AddWithValue("@TipoConsulta", 2);
                            else command.Parameters.AddWithValue("@TipoConsulta", 5);
                            command.Parameters.AddWithValue("@Articulo", string.Empty);
                            command.Parameters.AddWithValue("@Linea", Lineas);
                            command.Parameters.AddWithValue("@Proveedor", Provs);
                            command.Parameters.AddWithValue("@AlmacenOrigen", _almacen);
                            command.Parameters.AddWithValue("@AlmacenDestino", txtDestino.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@Importacion", imp);


                            DataTable table = new DataTable();

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;
                            da.Fill(table);

                            gridTransferencias.DataSource = table;

                            if (!checkBox1.Checked) this.FormatoTransferencia(gridTransferencias);
                            else this.FormatoTransferenciaM(gridTransferencias);


                            if (table.Rows.Count > 0)
                            {
                                decimal transferible = decimal.Zero;
                                decimal solicitado = decimal.Zero;

                                if (checkBox1.Checked)
                                {
                                    solicitado = Convert.ToDecimal(table.Compute("SUM([SOLICITADO ($)])", string.Empty));
                                    transferible = Convert.ToDecimal(table.Compute("SUM([TRANSFERIBLE ($)])", string.Empty));

                                    label1.Text = "Solicitado: " + solicitado.ToString("C2") + "\t\t\t Transferible: " + transferible.ToString("C2");
                                }
                            }
                        }
                    }
                }
                else MessageBox.Show("Debe seleccionar un Almacen Origen y un Almacen Destino.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Continuar();
            }
        }

        private void gridTransferencias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = gridTransferencias.CurrentCell.RowIndex;
                int column = gridTransferencias.CurrentCell.ColumnIndex;

                gridVentasT.DataSource = null;
                if (row > -1)
                {
                    string _articulo = gridTransferencias.Rows[row].Cells[(int)Columnas.Articulo].Value.ToString();
                    string _almacen = gridTransferencias.Columns[column].HeaderText.Trim().Substring(0, 2);

                    //CargarVentas(_articulo, _almacen, gridVentasT);

                    CargarDetalle(_articulo, _almacen, gridDetalleT);
                    FormatoDetalle(gridDetalleT);

                    //////////////
                    SqlCommand command = new SqlCommand("sp_HistorialVentas");
                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Almacen", _almacen);
                    command.Parameters.AddWithValue("@Articulo", _articulo);

                    Clases.Ventas vts = new Clases.Ventas();
                    gridVentasT.DataSource = vts.GetVentas(command);


                    foreach (DataGridViewColumn item in gridVentasT.Columns)
                    {
                        if (item.Index > 1)
                        {
                            item.Width = 60;
                            item.DefaultCellStyle.Format = "N0";
                            item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        else
                        {
                            item.Width = 75;
                            item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        }

                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtDestino.SelectedIndex = 0;
            txtOrigen.SelectedIndex = 0;

            cbLinea.SelectedIndex = 0;
            cbProveedor.SelectedIndex = 0;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel ex = new ExportarAExcel();
            if (ex.Exportar(gridTransferencias))
            {
                MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridTransferencias_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void Transferencias_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void Transferencias_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
                log.Fin();
            }
        }
    }
}
