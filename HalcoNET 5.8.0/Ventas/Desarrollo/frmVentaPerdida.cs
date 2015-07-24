using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas.Desarrollo
{
    public partial class frmVentaPerdida : Form
    {
        DataTable tbl = new DataTable();
        DataSet data1 = new DataSet();

        public frmVentaPerdida()
        {
            InitializeComponent();
        }

        private enum Columnas
        {
            Code,
            ItemCode,
            ItemName,
            ItmsGrpCod,
            Linea,
            Almacen,
            Fecha,
            Solicitado,
            Stock,
            Diferencia,
            Comprador,
            Usuario,
            PickerFecha
        }

        public DataTable Fill()
        {
            DataTable tbl = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_SolicitudProducto", connection))
                {
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 6);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(tbl);
                    tbl.TableName = "Articulos";

                    data1.Tables.Add(tbl);
                }
            }


            return tbl;
        }

        public void Formato()
        {
            dgvDatos.Columns[(int)Columnas.Code].Visible = false;
            dgvDatos.Columns[(int)Columnas.Usuario].Visible = false;
            dgvDatos.Columns[(int)Columnas.Comprador].Visible = false;
            dgvDatos.Columns[(int)Columnas.ItmsGrpCod].Visible = false;
            dgvDatos.Columns[(int)Columnas.Fecha].Visible = false;
            dgvDatos.Columns[(int)Columnas.PickerFecha].Visible = true;

            dgvDatos.Columns[(int)Columnas.Stock].ReadOnly = true;
            dgvDatos.Columns[(int)Columnas.Diferencia].ReadOnly = true;

            dgvDatos.Columns[(int)Columnas.Fecha].DefaultCellStyle.Format = "dd/MM/yyyy H:mm";
            dgvDatos.Columns[(int)Columnas.Stock].DefaultCellStyle.Format = "N0";
            dgvDatos.Columns[(int)Columnas.Solicitado].DefaultCellStyle.Format = "N0";
            dgvDatos.Columns[(int)Columnas.Stock].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDatos.Columns[(int)Columnas.Solicitado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            dgvDatos.Columns[(int)Columnas.Code].DisplayIndex = 1;
            dgvDatos.Columns[(int)Columnas.ItemCode].DisplayIndex = 2;
            dgvDatos.Columns[(int)Columnas.ItemName].DisplayIndex = 3;
            dgvDatos.Columns[(int)Columnas.Linea].DisplayIndex = 4;
            dgvDatos.Columns[(int)Columnas.Almacen].DisplayIndex = 5;
            dgvDatos.Columns[(int)Columnas.PickerFecha].DisplayIndex = 6;
            dgvDatos.Columns[(int)Columnas.Solicitado].DisplayIndex = 7;
            dgvDatos.Columns[(int)Columnas.Stock].DisplayIndex = 8;
            dgvDatos.Columns[(int)Columnas.Diferencia].DisplayIndex = 9;
        }

        private void Combo(int filaEdit, int columnaEdit, int ColumnaCombo, DataGridView dgv, DataTable _DataSource, string ValueMember, string DisplayMember)
        {
            if (filaEdit == -1) return;
            if (columnaEdit == ColumnaCombo)
            {
                object objValCell = dgv.Rows[filaEdit].Cells[columnaEdit].Value;
                if (objValCell == null)
                    return;
                string valCell = objValCell.ToString();
                DataGridViewComboBoxCell celcombo = new DataGridViewComboBoxCell();
                object objTipoInci = dgv.Rows[filaEdit].Cells[ColumnaCombo].Value;

                if (objTipoInci == null) return;

                string tipoInci = objTipoInci.ToString();
                celcombo.DataSource = _DataSource;
                celcombo.ValueMember = ValueMember;
                celcombo.DisplayMember = DisplayMember;
                //celcombo.
                // el campo es NULL(BD, no se a justificado) 
                if (valCell == string.Empty)
                {
                    dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                }
                else
                {
                    celcombo.Value = valCell.Trim();
                    dgv.Rows[filaEdit].Cells[columnaEdit] = celcombo;
                }
            }
        }

        private DataTable GetData(int _type)
        {
            DataTable _tbl = new DataTable();
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_VentaPerdida", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", _type);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    da.Fill(_tbl);
                }
            }

            return _tbl;
        }

        private void frmVentaPerdida_Load(object sender, EventArgs e)
        {
            try
            {
                this.Fill();
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                tbl.Clear();
                tbl.Columns.Clear();
                dgvDatos.DataSource = null;
                dgvDatos.Columns.Clear();
                dgvDatos.CellBeginEdit -= new DataGridViewCellCancelEventHandler(dgvDatos_CellBeginEdit);

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_VentaPerdida", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Rol", ClasesSGUV.Login.Rol);
                        command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;

                        da.Fill(tbl);

                        dgvDatos.DataSource = tbl;


                        Clases.CalendarColumn dol = new Clases.CalendarColumn();
                        dol.HeaderText = "Fecha";
                        dol.Name = "pFecha";
                        dgvDatos.Columns.Add(dol);

                        this.Formato();

                        

                        foreach (DataGridViewRow item in dgvDatos.Rows)
                        {
                            if (!item.IsNewRow)
                                item.Cells[(int)Columnas.PickerFecha].Value = item.Cells[(int)Columnas.Fecha].Value;
                        }

                        dgvDatos.CellBeginEdit += new DataGridViewCellCancelEventHandler(dgvDatos_CellBeginEdit);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void dgvDatos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    item.ReadOnly = false;


                    if (Convert.ToDecimal(item.Cells[(int)Columnas.Code].Value) != decimal.Zero)
                        item.ReadOnly = true;

                }
            }
            catch (Exception)
            {

            }
        }

        private void dgvDatos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        }

        private void dgvDatos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

            DataGridView dgv = (sender as DataGridView);
            if (dgv == null) return;

            int filaEdit = e.RowIndex;
            int columnaEdit = e.ColumnIndex;
            this.Combo(filaEdit, columnaEdit, dgvDatos.Columns["Almacen"].Index, dgv, this.GetData(3), "Codigo", "Nombre");
        }

        private void dgvDatos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty((sender as DataGridView).Rows[e.RowIndex].Cells["Artículo"].Value.ToString()))
                {
                    MessageBox.Show("Ingrese un código de artículo", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("sp_VentaPerdida", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;

                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@Articulo", (sender as DataGridView).Rows[e.RowIndex].Cells["Artículo"].Value);

                        DataTable tbl = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.Fill(tbl);

                        (sender as DataGridView).Rows[e.RowIndex].Cells["ItmsGrpCode"].Value = tbl.Rows[0].Field<Int16>(0);
                        (sender as DataGridView).Rows[e.RowIndex].Cells["Línea"].Value = tbl.Rows[0].Field<string>(1);
                        (sender as DataGridView).Rows[e.RowIndex].Cells["Nombre"].Value = tbl.Rows[0].Field<string>(2);
                        (sender as DataGridView).Rows[e.RowIndex].Cells["Correo"].Value = tbl.Rows[0].Field<string>(3);
                        (sender as DataGridView).Rows[e.RowIndex].Cells["Solicitante"].Value = ClasesSGUV.Login.Usuario;


                    }
                }


                //if (e.ColumnIndex == )
                //{
                (sender as DataGridView).Rows[e.RowIndex].Cells["Fecha"].Value = (sender as DataGridView).Rows[e.RowIndex].Cells["pFecha"].Value;
                //}

                if (e.ColumnIndex == dgvDatos.Columns["pFecha"].Index ||
                   // e.ColumnIndex == (int)Columnas.ItemCode ||
                    e.ColumnIndex == dgvDatos.Columns["Almacen"].Index
                    )
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_VentaPerdida", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;

                            //if ((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Almacen].Value == DBNull.Value
                            //    && (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Fecha].Value != DBNull.Value)
                            //{
                            //    MessageBox.Show("Selecciona un almacén", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    return;
                            //}

                            //if ((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Fecha].Value == DBNull.Value
                            //    && (sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.Almacen].Value!= DBNull.Value)
                            //{
                            //    MessageBox.Show("Ingresa una fecha", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    return;
                            //}

                            //if (Convert.ToInt16((sender as DataGridView).Rows[e.RowIndex].Cells[(int)Columnas.ItmsGrpCod].Value) == 0)
                            //{
                            //    MessageBox.Show("Ingresa un codigo de artículo válido", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    return;
                            //}

                            command.Parameters.AddWithValue("@TipoConsulta", 2);
                            command.Parameters.AddWithValue("@Almacen", (sender as DataGridView).Rows[e.RowIndex].Cells["Almacen"].Value);
                            command.Parameters.AddWithValue("@Articulo", (sender as DataGridView).Rows[e.RowIndex].Cells["Artículo"].Value);
                            command.Parameters.AddWithValue("@Fecha", (sender as DataGridView).Rows[e.RowIndex].Cells["pFecha"].Value);

                            connection.Open();

                            (sender as DataGridView).Rows[e.RowIndex].Cells["Stock actual"].Value = command.ExecuteScalar();
                        }

                    }

                }

                (sender as DataGridView).Rows[e.RowIndex].Cells["Diferencia"].Value = Convert.ToDecimal((sender as DataGridView).Rows[e.RowIndex].Cells["Cantidad Solicitada"].Value == DBNull.Value ? decimal.Zero : (sender as DataGridView).Rows[e.RowIndex].Cells["Cantidad Solicitada"].Value) -
                    Convert.ToDecimal((sender as DataGridView).Rows[e.RowIndex].Cells["Stock actual"].Value == DBNull.Value ? decimal.Zero : (sender as DataGridView).Rows[e.RowIndex].Cells["Stock actual"].Value);

            }
            catch (Exception) { }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int Cambios = 0;
            string Mensaje = string.Empty;
            string Mail = string.Empty;

            Mensaje = @"<style type='text/css'>
                                    table, th, td {
                                        border: 1px solid black;
                                        border-spacing: 50px 50px 50px 50px;
                                        border-collapse: collapse;
                                    }
                                    </style>

                        <br><br>
                        <table cellpadding='6'>
                        <tr>
                            <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Articulo</strong></font></td>
                            <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Nombre</strong></font></td>
                            <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Linea<strong></font></td>
                            <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Almacen</strong></font></td>
                            <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Fecha</strong></font></td>
                            <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Cantidad<br>Solicitada</strong></font></td>
                            <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Stock<br>actual</strong></font></td>
                            <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Diferencia</strong></font></td>
                            <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Usuario</strong></font></td>
                        </tr>";
            foreach (DataRow item in tbl.Rows)
            {
                if (item.RowState == DataRowState.Added)
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_VentaPerdida", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;

                            command.Parameters.AddWithValue("@TipoConsulta", 5);
                            command.Parameters.AddWithValue("@Articulo", item.Field<string>("Artículo"));
                            command.Parameters.AddWithValue("@Almacen", item.Field<string>("Almacen"));
                            command.Parameters.AddWithValue("@ItmsGrpCod", item.Field<Int16>("ItmsGrpCode"));
                            command.Parameters.AddWithValue("@Fecha", item.Field<DateTime>("Fecha"));
                            command.Parameters.AddWithValue("@Solicitado", item.Field<decimal>("Cantidad Solicitada"));
                            command.Parameters.AddWithValue("@Stock", item.Field<decimal>("Stock actual"));
                            command.Parameters.AddWithValue("@UserId", ClasesSGUV.Login.Id_Usuario);

                            connection.Open();

                            command.ExecuteNonQuery();
                            Cambios++;

                            Mail += item.Field<string>("Correo") + ";";

                            Mensaje += @"<tr>
                                            <td><font size=2 face='Calibri' color='#000000'><strong>" + item.Field<string>("Artículo") + @"</strong></font></td>
                                            <td><font size=2 face='Calibri' color='#000000'><strong>" + item.Field<string>("Nombre") + @"</strong></font></td>
                                            <td><font size=2 face='Calibri' color='#000000'><strong>" + item.Field<string>("Línea") + @"<strong></font></td>
                                            <td><font size=2 face='Calibri' color='#000000'><strong>" + item.Field<string>("Almacen") + @"</strong></font></td>
                                            <td><font size=2 face='Calibri' color='#000000'><strong>" + item.Field<DateTime>("Fecha") + @"</strong></font></td>
                                            <td><font size=2 face='Calibri' color='#000000'><strong>" + item.Field<decimal>("Cantidad solicitada").ToString("N0") + @"</strong></font></td>
                                            <td><font size=2 face='Calibri' color='#000000'><strong>" + item.Field<decimal>("Stock actual").ToString("N0") + @"</strong></font></td>
                                            <td><font size=2 face='Calibri' color='#000000'><strong>" + item.Field<decimal>("Diferencia").ToString("N0") + @"</strong></font></td>
                                            <td><font size=2 face='Calibri' color='#000000'><strong>" + item.Field<string>("Solicitante") + @"</strong></font></td>
                                        </tr>";
                        }
                    }
                }
            }
            tbl.AcceptChanges();
            MessageBox.Show("Registro exitoso" , "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (Cambios > 0)
            {
                Cobranza.SendMail mail = new Cobranza.SendMail();
                mail.EnviarVentaPerdida(Mail, Mensaje+"</table><br><br>", "Venta pérdida", "jose.olivos@pj.com.mx");
            }
            this.OnLoad(e);
        }

        private void dgvDatos_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {

                if ((sender as DataGridView).CurrentCell.ColumnIndex == dgvDatos.Columns["Artículo"].Index)
                {
                    var source = new AutoCompleteStringCollection();


                    string[] stringArray = Array.ConvertAll<DataRow, String>(data1.Tables["Articulos"].Select(), delegate(DataRow row) { return (String)row["Codigo"]; });

                    source.AddRange(stringArray);

                    TextBox prodCode = e.Control as TextBox;
                    if (prodCode != null)
                    {
                        prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode.AutoCompleteCustomSource = source;
                        prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
                else
                {
                    TextBox prodCode = e.Control as TextBox;
                    if (prodCode != null)
                    {
                        prodCode.AutoCompleteMode = AutoCompleteMode.None;
                        prodCode.AutoCompleteSource = AutoCompleteSource.None;
                    }
                }
            }
            catch (Exception) { }
        }

    }
}
