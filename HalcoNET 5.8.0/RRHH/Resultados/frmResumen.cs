using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RRHH.Resultados
{
    public partial class frmResumen : Form
    {
        private int Mes;
        private int Año;
        private DataGridView Grid = new DataGridView();

        public bool Captura
        {
            get; set;
        }

        public frmResumen()
        {
            InitializeComponent();

            dgvConta.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSis.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }
        #region Ventas
        public void IndicadoresComerciales()
        {
            //otiene el numero de columnas que debe tener el grid
            SqlCommand command = new SqlCommand("sp_Resultados");
            command.Parameters.AddWithValue("@TipoConsulta", 3);
            command.Parameters.AddWithValue("@Mes", Mes);
            command.Parameters.AddWithValue("@Año", Año);

            DataTable tblColums = this.getData(command);

            DataTable tblIndComerciales = new DataTable();
            dgvIndComerciales.DataSource = tblIndComerciales;

            DataTable tblEncabezado = new DataTable();
            dgvComercialesHeader.DataSource = tblEncabezado;

            tblIndComerciales.Columns.Add("Sucursal", typeof(string));
            tblEncabezado.Columns.Add("Sucursal", typeof(string));

            //obiene las filas que contendra el grid(Sucursales)
            SqlCommand commandSuc = new SqlCommand("sp_Resultados");
            commandSuc.Parameters.AddWithValue("@TipoConsulta", 4);
            commandSuc.Parameters.AddWithValue("@Mes", Mes);
            commandSuc.Parameters.AddWithValue("@Año", Año);

            DataTable tblSuc = this.getData(commandSuc);

            foreach (DataRow item in tblSuc.Rows)
            {
                DataRow row = tblIndComerciales.NewRow();
                row["Sucursal"] = item.Field<string>("Sucursal");
                tblIndComerciales.Rows.Add(row);
            }

            DataRow rowPJ = tblIndComerciales.NewRow();
            rowPJ["Sucursal"] = "PJ";
            tblIndComerciales.Rows.Add(rowPJ);

            foreach (DataRow item in tblColums.Rows)
            {
                tblEncabezado.Columns.Add(item.Field<string>("Indicador"));
                dgvComercialesHeader.Columns[item.Field<string>("Indicador")].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvComercialesHeader.Columns[item.Field<string>("Indicador")].Width = 180;
                
                tblIndComerciales.Columns.Add(item.Field<string>("Indicador") + "_obj", typeof(decimal));
                tblIndComerciales.Columns.Add(item.Field<string>("Indicador") + "_real", typeof(decimal));

                dgvIndComerciales.Columns[item.Field<string>("Indicador") + "_obj"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvIndComerciales.Columns[item.Field<string>("Indicador") + "_obj"].Width = 90;
                dgvIndComerciales.Columns[item.Field<string>("Indicador") + "_obj"].HeaderText = "Objetivo";
                dgvIndComerciales.Columns[item.Field<string>("Indicador") + "_obj"].DefaultCellStyle.Format = "C0";

                dgvIndComerciales.Columns[item.Field<string>("Indicador") + "_real"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvIndComerciales.Columns[item.Field<string>("Indicador") + "_real"].Width = 90;
                dgvIndComerciales.Columns[item.Field<string>("Indicador") + "_real"].HeaderText = "Real";
                dgvIndComerciales.Columns[item.Field<string>("Indicador") + "_real"].DefaultCellStyle.Format = "C0";

                SqlCommand com = new SqlCommand("sp_Resultados");
                com.Parameters.AddWithValue("@TipoConsulta", 5);
                com.Parameters.AddWithValue("@Mes", Mes);
                com.Parameters.AddWithValue("@Año", Año);
                com.Parameters.AddWithValue("@Indicador", item.Field<string>("Indicador"));

                DataTable tbl = this.getData(com);

                foreach (DataRow itemRow in tbl.Rows)
                {
                    foreach (DataRow itemSuc in tblIndComerciales.Rows)
                    {
                        if (itemSuc.Field<string>("Sucursal").Equals(itemRow.Field<string>("Sucursal")))
                        {
                            itemSuc.SetField(item.Field<string>("Indicador") + "_obj", itemRow.Field<decimal>("Objetivo"));
                            itemSuc.SetField(item.Field<string>("Indicador") + "_real", itemRow.Field<decimal>("Real"));
                        }
                    }
                }
            }

            dgvIndComerciales.Columns["Sucursal"].HeaderText = string.Empty;
            dgvIndComerciales.Columns["Utilidad_real"].DefaultCellStyle.Format = "P2";
            dgvIndComerciales.Columns["Utilidad_obj"].DefaultCellStyle.Format = "P2";
        }

        public void IndicadoresFuerzaHalconVentas()
        {
            SqlCommand command = new SqlCommand("sp_Resultados");
            command.Parameters.AddWithValue("@TipoConsulta", 6);
            command.Parameters.AddWithValue("@Mes", Mes);
            command.Parameters.AddWithValue("@Año", Año);

            dgvFuerzaVentas.DataSource = this.getData(command);

            dgvFuerzaVentas.Columns[0].Visible = false;
            dgvFuerzaVentas.Columns[1].Width = 150;
            dgvFuerzaVentas.Columns[2].Width = 200;
            dgvFuerzaVentas.Columns[3].Width = 80;


        }
        #endregion

        #region Contabilidad
        public void IndicadoresContabilidad()
        {
            SqlCommand commandConta = new SqlCommand("sp_Resultados");
            commandConta.Parameters.AddWithValue("@TipoConsulta", 2);
            commandConta.Parameters.AddWithValue("@Mes", Mes);
            commandConta.Parameters.AddWithValue("@Año", Año);

            dgvConta.DataSource = this.getData(commandConta);
            dgvConta.Columns[0].Visible = false;
            dgvConta.Columns[2].Width = 400;

            //dgvConta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            dgvConta.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
        }
        #endregion

        #region Sistemas
        public void IndicadoresSistemas()
        {
            SqlCommand commandSis = new SqlCommand("sp_Resultados");
            commandSis.Parameters.AddWithValue("@TipoConsulta", 1);
            commandSis.Parameters.AddWithValue("@Mes", Mes);
            commandSis.Parameters.AddWithValue("@Año", Año);

            dgvSis.DataSource = this.getData(commandSis);
            dgvSis.Columns[0].Visible = false;
            dgvSis.Columns[1].Width = 120;
            dgvSis.Columns[2].Width = 400;

            //dgvSis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            dgvSis.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
        }
        #endregion

        #region Credito y Cobranza
        public void FuerzaHalconCobranza()
        {
            SqlCommand command = new SqlCommand("sp_Resultados");
            command.Parameters.AddWithValue("@TipoConsulta", 7);
            command.Parameters.AddWithValue("@Mes", Mes);
            command.Parameters.AddWithValue("@Año", Año);

            dgvFuerzaHalconCyC.DataSource = this.getData(command);

            dgvFuerzaHalconCyC.Columns[0].Visible = false;
            dgvFuerzaHalconCyC.Columns[1].Width = 150;
            dgvFuerzaHalconCyC.Columns[2].Width = 200;
            dgvFuerzaHalconCyC.Columns[3].Width = 80;
        }

        public void IndicadorObjCob()
        {
            SqlCommand command = new SqlCommand("sp_Resultados");
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Mes", Mes);
            command.Parameters.AddWithValue("@Año", Año);

            dgvIOC.DataSource = this.getData(command);

            dgvIOC.Columns[0].Visible = false;
            dgvIOC.Columns[1].Width = 150;
            dgvIOC.Columns[2].Width = 120;
            dgvIOC.Columns[3].Width = 120;
            dgvIOC.Columns[4].Width = 85;

            dgvIOC.Columns[2].DefaultCellStyle.Format = "C2";
            dgvIOC.Columns[3].DefaultCellStyle.Format = "C2";
            dgvIOC.Columns[4].DefaultCellStyle.Format = "P2";

            dgvIOC.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvIOC.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvIOC.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        public void Indicadorcyc(string _tipo, DataGridView dgv, string _format, string _nombre)
        {
            SqlCommand command = new SqlCommand("sp_Resultados");
            command.Parameters.AddWithValue("@TipoConsulta", 9);
            command.Parameters.AddWithValue("@Mes", Mes);
            command.Parameters.AddWithValue("@Año", Año);
            command.Parameters.AddWithValue("@Tipo", _tipo);

            dgv.DataSource = this.getData(command);

            dgv.Columns[0].Visible = false;
            dgv.Columns[1].Width = 150;
            dgv.Columns[2].HeaderText ="Indicador " + _nombre;
            dgv.Columns[2].Width = 90;
            dgv.Columns[3].Width = 90;

            dgv.Columns[2].DefaultCellStyle.Format = _format;
            dgv.Columns[3].DefaultCellStyle.Format = _format;

            dgv.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void IndicadorNCRD()
        {
            SqlCommand command = new SqlCommand("sp_Resultados");
            command.Parameters.AddWithValue("@TipoConsulta", 10);
            command.Parameters.AddWithValue("@Mes", Mes);
            command.Parameters.AddWithValue("@Año", Año);

            dgvNCRD.DataSource = this.getData(command);

            dgvNCRD.Columns[0].Visible = false;
            dgvNCRD.Columns[1].Width = 200;
            dgvNCRD.Columns[2].Width = 100;

            dgvNCRD.Columns[2].DefaultCellStyle.Format = "N0";

            dgvNCRD.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void IndicadorNew()
        {
            SqlCommand command = new SqlCommand("sp_Resultados");
            command.Parameters.AddWithValue("@TipoConsulta", 11);
            command.Parameters.AddWithValue("@Mes", Mes);
            command.Parameters.AddWithValue("@Año", Año);

            dgvNew.DataSource = this.getData(command);
            dgvNew.Columns[0].Visible = false;
            dgvNew.Columns[1].Width = 150;
            dgvNew.Columns[2].Width = 100;
            dgvNew.Columns[3].Width = 80;
            dgvNew.Columns[4].Width = 80;

            dgvNew.Columns[2].DefaultCellStyle.Format = "C2";
            dgvNew.Columns[3].DefaultCellStyle.Format = "P0";
            dgvNew.Columns[4].DefaultCellStyle.Format = "N0";

            dgvNew.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNew.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvNew.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void IndicadoresCyC()
        {
            this.FuerzaHalconCobranza();
            this.IndicadorObjCob();
            this.Indicadorcyc("ICV", dgvICV, "P0", " cartera vencida");
            this.Indicadorcyc("IDC", dgvIDC, "N0", " días cartera");
            this.IndicadorNew();
            this.IndicadorNCRD();
        }
        #endregion

        #region Metodos
        public DataTable getData(SqlCommand command)
        {
            DataTable tbl = new DataTable();
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (command)
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(tbl);

                    return tbl;
                }
            }
        }

        public void Guardar(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_Resultados", connection))
                        {
                            command.CommandTimeout = 0;
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 12);
                            command.Parameters.AddWithValue("@Code", row.Cells[0].Value);
                            command.Parameters.AddWithValue("@Proyecto", row.Cells[1].Value);
                            command.Parameters.AddWithValue("@Descripcion", row.Cells[2].Value);

                            if (dgv.AccessibleName == "SIS")
                                command.Parameters.AddWithValue("@FechaTermino", row.Cells[3].Value == DBNull.Value ? DateTime.Now : row.Cells[3].Value);

                            command.Parameters.AddWithValue("@Area", dgv.AccessibleName);
                            command.Parameters.AddWithValue("@Mes", Mes);
                            command.Parameters.AddWithValue("@Año", Año);
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
            }
        }

        public void GuardarHalcon(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_Resultados", connection))
                        {
                            command.CommandTimeout = 0;
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 13);
                            command.Parameters.AddWithValue("@Code", row.Cells[0].Value);
                            command.Parameters.AddWithValue("@Area", dgv.AccessibleName);
                            command.Parameters.AddWithValue("@Nombre", row.Cells[2].Value);
                            command.Parameters.AddWithValue("@Rango", row.Cells[3].Value);
                            command.Parameters.AddWithValue("@Sucursal", row.Cells[1].Value);
                            command.Parameters.AddWithValue("@Mes", Mes);
                            command.Parameters.AddWithValue("@Año", Año);

                            
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
            }
        }

        public void GuardarObjetivoCobranza(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_Resultados", connection))
                        {
                            command.CommandTimeout = 0;
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 14);
                            command.Parameters.AddWithValue("@Code", row.Cells[0].Value);
                            command.Parameters.AddWithValue("@Tipo", dgv.AccessibleName);
                            command.Parameters.AddWithValue("@Nombre", row.Cells[1].Value);
                            command.Parameters.AddWithValue("@Objetivo", row.Cells[2].Value);
                            command.Parameters.AddWithValue("@Logrado", row.Cells[3].Value);
                            command.Parameters.AddWithValue("@Mes", Mes);
                            command.Parameters.AddWithValue("@Año", Año);


                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
            }
        }

        public void GuardarINCDR(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_Resultados", connection))
                        {
                            command.CommandTimeout = 0;
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 14);
                            command.Parameters.AddWithValue("@Code", row.Cells[0].Value);
                            command.Parameters.AddWithValue("@Tipo", dgv.AccessibleName);
                            command.Parameters.AddWithValue("@Nombre", row.Cells[1].Value);
                            command.Parameters.AddWithValue("@Objetivo", row.Cells[2].Value);
                            command.Parameters.AddWithValue("@Mes", Mes);
                            command.Parameters.AddWithValue("@Año", Año);


                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
            }
        }

        public void GuardarNEW(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("sp_Resultados", connection))
                        {
                            command.CommandTimeout = 0;
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@TipoConsulta", 14);
                            command.Parameters.AddWithValue("@Code", row.Cells[0].Value);
                            command.Parameters.AddWithValue("@Tipo", dgv.AccessibleName);
                            command.Parameters.AddWithValue("@Nombre", row.Cells[1].Value);
                            command.Parameters.AddWithValue("@Objetivo", row.Cells[2].Value);
                            command.Parameters.AddWithValue("@Porcentaje", row.Cells[3].Value);
                            command.Parameters.AddWithValue("@Mes", Mes);
                            command.Parameters.AddWithValue("@Año", Año);


                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
            }
        }
        #endregion

        #region Eventos
        private void frmResumen_Load(object sender, EventArgs e)
        {
            cbMes.SelectedIndex = DateTime.Now.Month;
            tabPage12.Text = "Objetivo de cartera (" + cbMes.Text + ")";

            cbMes.SelectedIndex = DateTime.Now.Month - 1;
            txtAño.Text = DateTime.Now.Year.ToString();

            

            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            #region Permisos

            dgvConta.AllowUserToAddRows = Captura;
            dgvSis.AllowUserToAddRows = Captura;
            dgvFuerzaVentas.AllowUserToAddRows = Captura;
            dgvFuerzaHalconCyC.AllowUserToAddRows = Captura;
            dgvIOC.AllowUserToAddRows = Captura;
            dgvICV.AllowUserToAddRows = Captura;
            dgvIDC.AllowUserToAddRows = Captura;
            dgvNCRD.AllowUserToAddRows = Captura;
            dgvNew.AllowUserToAddRows = Captura;

            dgvConta.ReadOnly = !Captura;
            dgvSis.ReadOnly = !Captura;
            dgvFuerzaVentas.ReadOnly = !Captura;
            dgvFuerzaHalconCyC.ReadOnly = !Captura;
            dgvIOC.ReadOnly = !Captura;
            dgvICV.ReadOnly = !Captura;
            dgvIDC.ReadOnly = !Captura;
            dgvNCRD.ReadOnly = !Captura;
            dgvNew.ReadOnly = !Captura;

            btnGuardar.Visible = Captura;
            #endregion 
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                Año = Convert.ToInt32(txtAño.Text);
                Mes = cbMes.SelectedIndex + 1;

                this.IndicadoresComerciales();
                this.IndicadoresFuerzaHalconVentas();
                this.IndicadoresContabilidad();
                this.IndicadoresSistemas();

                this.IndicadoresCyC();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvIndComerciales_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                DataGridView dgv = (sender as DataGridView);
                foreach (DataGridViewRow item in dgv.Rows)
                {
                    item.Cells["Sucursal"].Style.BackColor = Color.FromArgb(68, 84, 106);
                    item.Cells["Sucursal"].Style.ForeColor = Color.White;
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                DataGridView dgv = (sender as DataGridView);
                if (e.RowIndex < 0)
                    return;
                
                    if (e.ColumnIndex == 1)
                    {
                        Brush gridColor = new SolidBrush(dgv.GridColor);
                        Brush backColorCell = new SolidBrush(e.CellStyle.BackColor);
                        //
                        Pen gridLinePen = new Pen(gridColor);
                        e.Graphics.FillRectangle(backColorCell, e.CellBounds);
                        //
                        if (e.RowIndex < dgv.Rows.Count && dgv.Rows[e.RowIndex + 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                        {
                            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                        }
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                        //
                        if (String.IsNullOrEmpty(e.Value.ToString()))
                        {
                            if (e.RowIndex > 0 && dgv.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() == e.Value.ToString())
                            {

                            }
                            else
                            {
                                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 5, StringFormat.GenericDefault);
                            }
                        }
                        else
                        {
                            if (e.RowIndex == 0)
                            {
                                e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 5, StringFormat.GenericDefault);
                            }
                            if (e.RowIndex > 0)
                            {
                                if (dgv.Rows[e.RowIndex - 1].Cells[e.ColumnIndex].Value.ToString() != e.Value.ToString())
                                {
                                    e.Graphics.DrawString(e.Value.ToString(), e.CellStyle.Font, Brushes.Black, e.CellBounds.X + 2, e.CellBounds.Y + 5, StringFormat.GenericDefault);
                                }
                                
                            }
                        }
                        e.Handled = true;
                    }
            }
            catch (Exception)
            {
            }
        }

        private void dgv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells[3].Value.ToString().Equals("HALCÓN"))
                    {
                        item.Cells[3].Style.BackColor = Color.Black;
                        item.Cells[3].Style.ForeColor = Color.White;
                    }
                    else if (item.Cells[3].Value.ToString().Equals("PLATA"))
                    {
                        item.Cells[3].Style.BackColor = Color.Gray;
                        item.Cells[3].Style.ForeColor = Color.Black;
                    }
                    else if (item.Cells[3].Value.ToString().Equals("PLATINO"))
                    {
                        item.Cells[3].Style.BackColor = Color.LightGray;
                        item.Cells[3].Style.ForeColor = Color.Black;
                    }
                    else if (item.Cells[3].Value.ToString().Equals("ORO"))
                    {
                        item.Cells[3].Style.BackColor = Color.Gold;
                        item.Cells[3].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void dgv_Click(object sender, EventArgs e)
        {
            Grid = (sender as DataGridView);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Guardar(dgvSis);
            this.Guardar(dgvConta);
            this.GuardarHalcon(dgvFuerzaVentas);
            this.GuardarHalcon(dgvFuerzaHalconCyC);
            this.GuardarObjetivoCobranza(dgvIOC);
            this.GuardarObjetivoCobranza(dgvICV);
            this.GuardarObjetivoCobranza(dgvIDC);
            this.GuardarINCDR(dgvNCRD);
            this.GuardarNEW(dgvNew);
        }
        #endregion 
    }
}
