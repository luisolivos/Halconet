using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Cobranza
{
    public partial class ScocreCardGlobal : Form
    {
        #region PARAMETROS

        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);
        public string Sucursales;
        public string Cobranza;
        public string Vendedores;
        public string Sucursal;
        public string Cliente;
        public string Factura;
        public string NombreSucursal;
        public int CodVendedor;
        public DateTime FechaInicial;
        public DateTime FechaFinal;
        public int Rol;

        public string NombreUsuario;
        public DataTable JefasCobranza = new DataTable();

        decimal _diasmes;
        decimal _diastrans;
        Clases.Logs log;

        public enum ColumnasGrid
        {
            JefaCobranza,
            Sucursal,
            Cobranza,
            NoIdentificado,
            Objetivo,
            CobranzavsObjetivoM,
            CobranzavsObjetivoP,
            CobranzaRequeridaDia,
            PronosticoCobranzaM,
            PronosticoCobranzaP,
            Boton
        }

        #endregion
        public ScocreCardGlobal(int rolUsuario, int codigoVendedor, string nombreUsuario, string sucursal)
        {
            InitializeComponent();
            Rol = rolUsuario;
            CodVendedor = codigoVendedor;
            NombreUsuario = nombreUsuario;
            Sucursal = sucursal;
            
        }

        private void ScocreCardGlobal_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                DateTime fechatemp = DateTime.Today;
                DateTime fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);
                DateTime fecha2 = new DateTime(fechatemp.Year, fechatemp.Month + 1, 1).AddDays(-1);

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

                dateTimePicker1.Value = fecha1;
                dateTimePicker2.Value = fecha2;
                if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                {
                    //clbCobranza.Visible = true;
                    //label2.Visible = true;
                }
                else if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
                {
                    clbCobranza.Visible = false;
                    label2.Visible = false;
                    lblSucursal.Visible = false;
                    clbSucursal.Visible = false;
                }

                CargarJefesCobranza();
                CargarSucursales();

                gridScore.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado. \r\n" + ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region METODOS

        private void CargarSucursales()
        {
            //if (Rol == (int)Constantes.RolesSistemaSGUV.Administrador || Rol == (int)Constantes.RolesSistemaSGUV.GerenteCobranza)
            //{
            SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@FechaInicial", DateTime.Now);
            command.Parameters.AddWithValue("@FechaFinal", DateTime.Now);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.CommandTimeout = 0;

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

        private void CargarJefesCobranza()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", conection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.JefesCobranza);
            command.Parameters.AddWithValue("@Sucursal", Sucursal);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = "TODAS";
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);
            JefasCobranza = table.Copy();

            clbCobranza.DataSource = table;
            clbCobranza.DisplayMember = "Nombre";
            clbCobranza.ValueMember = "Codigo";
        }
        
        public string getMemo(int GroupCode)
        {
            string _memo = "";
            switch (GroupCode)
            {
                case 107: _memo = "01"; break;
                case 105: _memo = "02"; break;
                case 106: _memo = "02"; break;
                case 100: _memo = "03"; break;
                case 102: _memo = "05"; break;
                case 108: _memo = "06"; break;
                case 103: _memo = "16"; break;
                case 104: _memo = "18"; break;
            }

            return _memo;
        }

        public void FormatoGrid()
        {
            DataGridViewButtonColumn boton = new DataGridViewButtonColumn();
            {
                boton.Name = "Detalle";
                boton.HeaderText = "Detalle";
            
                boton.Width = 130;
                boton.UseColumnTextForButtonValue = true;
            }
            gridScore.Columns.Add(boton);

            gridScore.Columns[(int)ColumnasGrid.JefaCobranza].Width = 180;
            gridScore.Columns[(int)ColumnasGrid.Sucursal].Width = 130;
            gridScore.Columns[(int)ColumnasGrid.Cobranza].Width = 100;
            gridScore.Columns[(int)ColumnasGrid.NoIdentificado].Width = 100;
            gridScore.Columns[(int)ColumnasGrid.Objetivo].Width = 100;
            gridScore.Columns[(int)ColumnasGrid.CobranzavsObjetivoM].Width = 100;
            gridScore.Columns[(int)ColumnasGrid.CobranzavsObjetivoP].Width = 100;
            gridScore.Columns[(int)ColumnasGrid.CobranzaRequeridaDia].Width = 100;
            gridScore.Columns[(int)ColumnasGrid.PronosticoCobranzaM].Width = 100;
            gridScore.Columns[(int)ColumnasGrid.PronosticoCobranzaP].Width = 100;

            gridScore.Columns[(int)ColumnasGrid.JefaCobranza].ReadOnly = true;
            gridScore.Columns[(int)ColumnasGrid.Sucursal].ReadOnly = true;
            gridScore.Columns[(int)ColumnasGrid.Cobranza].ReadOnly = true;
            gridScore.Columns[(int)ColumnasGrid.NoIdentificado].ReadOnly = true;
            gridScore.Columns[(int)ColumnasGrid.Objetivo].ReadOnly = true;
            gridScore.Columns[(int)ColumnasGrid.CobranzavsObjetivoM].ReadOnly = true;
            gridScore.Columns[(int)ColumnasGrid.CobranzavsObjetivoP].ReadOnly = true;
            gridScore.Columns[(int)ColumnasGrid.CobranzaRequeridaDia].ReadOnly = true;
            gridScore.Columns[(int)ColumnasGrid.PronosticoCobranzaM].ReadOnly = true;
            gridScore.Columns[(int)ColumnasGrid.PronosticoCobranzaP].ReadOnly = true;

            gridScore.Columns[(int)ColumnasGrid.Cobranza].DefaultCellStyle.Format = "C0";
            gridScore.Columns[(int)ColumnasGrid.NoIdentificado].DefaultCellStyle.Format = "C0";
            gridScore.Columns[(int)ColumnasGrid.Objetivo].DefaultCellStyle.Format = "C0";
            gridScore.Columns[(int)ColumnasGrid.CobranzavsObjetivoM].DefaultCellStyle.Format = "C0";
            gridScore.Columns[(int)ColumnasGrid.CobranzavsObjetivoP].DefaultCellStyle.Format = "P0";
            gridScore.Columns[(int)ColumnasGrid.CobranzaRequeridaDia].DefaultCellStyle.Format = "C0";
            gridScore.Columns[(int)ColumnasGrid.PronosticoCobranzaM].DefaultCellStyle.Format = "C0";
            gridScore.Columns[(int)ColumnasGrid.PronosticoCobranzaP].DefaultCellStyle.Format = "P0";

            gridScore.Columns[(int)ColumnasGrid.Cobranza].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridScore.Columns[(int)ColumnasGrid.NoIdentificado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridScore.Columns[(int)ColumnasGrid.Objetivo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridScore.Columns[(int)ColumnasGrid.CobranzavsObjetivoM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridScore.Columns[(int)ColumnasGrid.CobranzavsObjetivoP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridScore.Columns[(int)ColumnasGrid.CobranzaRequeridaDia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridScore.Columns[(int)ColumnasGrid.PronosticoCobranzaM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridScore.Columns[(int)ColumnasGrid.PronosticoCobranzaP].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            gridScore.Columns[(int)ColumnasGrid.JefaCobranza].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridScore.Columns[(int)ColumnasGrid.Sucursal].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridScore.Columns[(int)ColumnasGrid.Cobranza].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridScore.Columns[(int)ColumnasGrid.NoIdentificado].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridScore.Columns[(int)ColumnasGrid.Objetivo].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridScore.Columns[(int)ColumnasGrid.CobranzavsObjetivoM].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridScore.Columns[(int)ColumnasGrid.CobranzavsObjetivoP].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridScore.Columns[(int)ColumnasGrid.CobranzaRequeridaDia].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridScore.Columns[(int)ColumnasGrid.PronosticoCobranzaM].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridScore.Columns[(int)ColumnasGrid.PronosticoCobranzaP].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        public void FormatoGridTotales()
        {
            dataGridView1.Columns[0].Width = 130;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].Width = 100;

            dataGridView1.Columns[1].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns[2].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns[3].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns[4].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns[5].DefaultCellStyle.Format = "P0";
            dataGridView1.Columns[6].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns[7].DefaultCellStyle.Format = "C0";
            dataGridView1.Columns[8].DefaultCellStyle.Format = "P0";

            dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public decimal DiasMes()
        {
            decimal dias = 0;
            SqlConnection con =  new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 3);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@FechaInicial", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.CommandTimeout = 0;

            try
            {
                con.Open();
                dias = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            finally
            {
                con.Close();
            }
            return dias;
        }

        public decimal NoIdentificados(string _sucursal)
        {
            decimal _noId = 0;
            SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 2);
            command.Parameters.AddWithValue("@Sucursales", Sucursales);
            command.Parameters.AddWithValue("@JefasCobranza", Cobranza);
            command.Parameters.AddWithValue("@FechaInicial", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
            command.Parameters.AddWithValue("@Sucursal", _sucursal);
            command.CommandTimeout = 0;

            try
            {
                con.Open();
                //SqlDataReader r =  command.ExecuteReader();
                _noId = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            finally
            {
                con.Close();
            }
            return _noId;
        }

        public decimal DiasTranscurridos()
        {
            decimal dias = 0;
            SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
            SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 4);
            command.Parameters.AddWithValue("@Sucursales", string.Empty);
            command.Parameters.AddWithValue("@JefasCobranza", string.Empty);
            command.Parameters.AddWithValue("@FechaInicial", dateTimePicker1.Value);
            command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.CommandTimeout = 0;

            try
            {
                con.Open();
                dias = Convert.ToDecimal(command.ExecuteScalar());
            }
            catch (Exception)
            {
            }
            finally
            {
                con.Close();
            }
            return dias;
        }

        public DataTable Totales(DataTable _t)
        {
            DataTable Total = new DataTable();
            Total.Columns.Add("Sucursal", typeof(string));
            Total.Columns.Add("Cobranza", typeof(decimal));
            Total.Columns.Add("NoID", typeof(decimal));
            Total.Columns.Add("Objetivo", typeof(decimal));
            Total.Columns.Add("CobranzaObjetivoM", typeof(decimal));
            Total.Columns.Add("CobranzaObjetivoP", typeof(decimal));
            Total.Columns.Add("CobranzaReqDia", typeof(decimal));
            Total.Columns.Add("PronosticoM", typeof(decimal));
            Total.Columns.Add("PronosticoP", typeof(decimal));

            DataRow r = Total.NewRow();
            decimal _cobranza = Convert.ToDecimal((from acum in _t.AsEnumerable()
                                                   where acum.Field<string>("Jefa de cobranza") != ""
                                                   select acum.Field<decimal>("Cobranza")).Sum());
            decimal _objetivo = Convert.ToDecimal((from acum in _t.AsEnumerable()
                                                   where acum.Field<string>("Jefa de cobranza") != ""
                                                   select acum.Field<decimal>("Objetivo de Cobranza")).Sum());
            decimal _noIdentificado = Convert.ToDecimal((from acum in _t.AsEnumerable()
                                                   where acum.Field<string>("Jefa de cobranza") == ""
                                                   select acum.Field<decimal>("No Identificado")).Sum());
            DataRow r1 = _t.NewRow();
            r1["Sucursal"] = "Total";
            r["Sucursal"] = "Total";
            r1["Cobranza"] = _cobranza;
            r["Cobranza"] = _cobranza;
            r1["No Identificado"] = _noIdentificado;
            r["NoID"] = _noIdentificado;
            r1["Objetivo de Cobranza"] = _objetivo;
            r["Objetivo"] = _objetivo;
            r1["Cobranza vs Objetivo ($)"] = _cobranza - _objetivo;
            r["CobranzaObjetivoM"] = _cobranza - _objetivo;
            if (_objetivo > 0)
            {
                r1["Cobranza vs Objetivo (%)"] = _cobranza / _objetivo;
                r["CobranzaObjetivoP"] = _cobranza / _objetivo;
                r1["Pronostico de cobranza (%)"] = ((_cobranza / _diastrans) * _diasmes) / _objetivo;
                r["PronosticoP"] = ((_cobranza / _diastrans) * _diasmes) / _objetivo;
            }
            else
            {
                r1["Cobranza vs Objetivo (%)"] = 0;
                r["CobranzaObjetivoP"] = 0;
                r1["Pronostico de cobranza (%)"] = 0; 
                r["PronosticoP"] = 0;
            }
            if (_diasmes - _diastrans > 0)
            {
                r1["Cobranza requerida por día"] = (_objetivo - _cobranza) / (_diasmes - _diastrans);
                r["CobranzaReqDia"] = (_objetivo - _cobranza) / (_diasmes - _diastrans);
            }
            else
            {
                r1["Cobranza requerida por día"] = (_objetivo - _cobranza);
                r["CobranzaReqDia"] = (_objetivo - _cobranza);
            }

            if (_diastrans > 0)
            {
                r1["Pronostico de cobranza ($)"] = (_cobranza / _diastrans) * _diasmes;
                r["PronosticoM"] = (_cobranza / _diastrans) * _diasmes;
            }
            _t.Rows.Add(r1);

            Total.Rows.Add(r);
            return Total;
        }

        #endregion

        #region EVENTOS
        private void clbSucursal_Click(object sender, EventArgs e)
        {
            if (clbSucursal.SelectedIndex == 0)
            {
                if (clbSucursal.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbSucursal.Items.Count; item++)
                    {
                        clbSucursal.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbSucursal.Items.Count; item++)
                    {
                        clbSucursal.SetItemChecked(item, true);
                    }
                }
            }

        }

        DataTable JefasxSucursal = new DataTable();
        private void clbSucursal_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Checked)
                {
                    DataRowView c = clbSucursal.Items[e.Index] as DataRowView;
                    string _memo = getMemo(Convert.ToInt32(c["Codigo"]));
                    JefasxSucursal.Merge((from item in JefasCobranza.AsEnumerable() where item["Codigo"].ToString() == _memo select item).CopyToDataTable());
                }
                else if (e.NewValue == CheckState.Unchecked)
                {
                    DataRowView c = clbSucursal.Items[e.Index] as DataRowView;
                    string _memo = getMemo(Convert.ToInt32(c["Codigo"]));
                    JefasxSucursal = ((from item in JefasxSucursal.AsEnumerable() where item["Codigo"].ToString() != _memo select item).CopyToDataTable());
                }
                DataView vista = new DataView(JefasxSucursal);
                DataTable a = vista.ToTable(true, new string[] { "Codigo", "Nombre" });
                clbCobranza.DataSource = a;
                clbCobranza.DisplayMember = "Nombre";
                clbCobranza.ValueMember = "Codigo";
            }
            catch (Exception)
            {
                JefasxSucursal.Clear();
                clbCobranza.DataSource = JefasCobranza;
                clbCobranza.DisplayMember = "Nombre";
                clbCobranza.ValueMember = "Codigo";
            }
        }

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            gridScore.DataSource = null;

            StringBuilder stbSucursales = new StringBuilder();
            foreach (DataRowView item in clbSucursal.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clbSucursal.ToString().Equals(string.Empty))
                    {
                        stbSucursales.Append(",");
                    }
                    stbSucursales.Append(item["Codigo"].ToString());
                }
            }
            if (clbSucursal.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clbSucursal.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbSucursal.ToString().Equals(string.Empty))
                        {
                            stbSucursales.Append(",");
                        }
                        stbSucursales.Append(item["Codigo"].ToString());
                    }
                }
            }

            StringBuilder stbCobranza = new StringBuilder();
            foreach (DataRowView item in clbCobranza.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clbCobranza.ToString().Equals(string.Empty))
                    {
                        stbCobranza.Append(",");
                    }
                    stbCobranza.Append(item["Nombre"].ToString());
                }
            }
            if (clbCobranza.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clbCobranza.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clbCobranza.ToString().Equals(string.Empty))
                        {
                            stbCobranza.Append(",");
                        }
                        stbCobranza.Append(item["Nombre"].ToString());
                    }
                }
            }

            Sucursales = stbSucursales.ToString();
            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza || Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas)
                Cobranza = stbCobranza.ToString();
            else if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                SqlCommand co = new SqlCommand("PJ_Cobrnaza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                co.CommandType = CommandType.StoredProcedure;
                co.Parameters.AddWithValue("@TipoConsulta", 5);
                co.Parameters.AddWithValue("@Vendedores", string.Empty);
                co.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                co.Parameters.AddWithValue("@Cliente", Sucursal);
                co.Parameters.AddWithValue("@Sucursal", string.Empty);
                co.Parameters.AddWithValue("@Usuario", NombreUsuario);
                co.Parameters.AddWithValue("@Factura", string.Empty);
                co.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter();
                ad.SelectCommand = co;
                ad.Fill(table);
                string jefa = "";
                foreach (DataRow r in table.Rows)
                {
                    jefa += r.Field<string>("Nombre") + ",";
                }
                Cobranza = "," + jefa;
            }
            _diasmes = 0;
            _diastrans = 0;
            btnExportar.Enabled = false;
            Vendedores = string.Empty;
            Cliente = string.Empty;
            Factura = dateTimePicker1.Value.ToShortDateString();
            gridScore.Columns.Clear();
            FechaInicial = dateTimePicker1.Value;

            if (dateTimePicker2.Value > DateTime.Now)
                FechaFinal = DateTime.Now;
            else
                FechaFinal = dateTimePicker2.Value;

            DataTable _t = new DataTable();
            using (SqlConnection conn = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Sucursales", Sucursales);
                    command.Parameters.AddWithValue("@JefasCobranza", Cobranza);
                    command.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                    command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.CommandTimeout = 0;


                    //Datos.Clear();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(_t);
                    command.CommandTimeout = 0;
                }
            }
            
            
            if (_t.Rows.Count > 0)
            {
                var query = (from item in _t.AsEnumerable()
                             select item.Field<string>("Sucursal")).Distinct();
                _diasmes = Convert.ToDecimal(DiasMes());
                _diastrans = Convert.ToDecimal(DiasTranscurridos());

                label1.Text = _diasmes.ToString();
                label3.Text = _diastrans.ToString();
                //int id = 1016;
                foreach (var item in query.ToList())
                {
                    decimal _cobranza = Convert.ToDecimal((from acum in _t.AsEnumerable()
                                         where acum.Field<string>("Sucursal") == item
                                         select acum.Field<decimal>("Cobranza")).Sum());
                    decimal _objetivo = Convert.ToDecimal((from acum in _t.AsEnumerable()
                                                where acum.Field<string>("Sucursal") == item
                                                 select acum.Field<decimal>("Objetivo de Cobranza")).Sum());
                    decimal _noIdentificado = NoIdentificados(item);
                    DataRow r = _t.NewRow();
                    r["Jefa de cobranza"] = "";
                    r["Sucursal"] = item + " Total";
                    r["Cobranza"] = _cobranza;
                    r["No Identificado"] = _noIdentificado;
                    r["Objetivo de cobranza"] = _objetivo;
                    r["Cobranza vs Objetivo ($)"] = _cobranza - _objetivo;
                    if (_objetivo > 0)
                    {
                        r["Pronostico de cobranza (%)"] = ((_cobranza / _diastrans) * (_diasmes )) / _objetivo;
                        r["Cobranza vs Objetivo (%)"] = _cobranza / _objetivo;
                    }
                    else
                    {
                        r["Pronostico de cobranza (%)"] = 0;
                        r["Cobranza vs Objetivo (%)"] = 0;
                    }
                    if (_diasmes - _diastrans > 0)
                    {
                        r["Cobranza requerida por día"] = (_objetivo - _cobranza) / (_diasmes - _diastrans);

                    }
                    else
                    {
                        r["Cobranza requerida por día"] = 0;
                    }

                    if (_diastrans > 0)
                        r["Pronostico de cobranza ($)"] = (_cobranza / _diastrans) * (_diasmes);

                    

                    _t.Rows.Add(r);

                    _t = (from tv in _t.AsEnumerable()
                          orderby tv.Field<string>("Sucursal")
                          select tv).CopyToDataTable();
                    btnExportar.Enabled = true;
                   // dataGridView1.DataSource = Totales(_t);
                   // FormatoGridTotales();
                }
                Totales(_t);
                gridScore.DataSource = _t;
                FormatoGrid();
            }
        }
        #endregion 

        private void gridScore_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)ColumnasGrid.Boton)
                    {
                        if (Convert.ToString(gridScore.Rows[e.RowIndex].Cells[(int)ColumnasGrid.JefaCobranza].Value) == ""
                            && Convert.ToString(gridScore.Rows[e.RowIndex].Cells[(int)ColumnasGrid.Sucursal].Value) != "Total")
                        {
                            string _sucursal = Convert.ToString(gridScore.Rows[gridScore.CurrentRow.Index].Cells[(int)ColumnasGrid.Sucursal].Value);
                            string []words = _sucursal.Split(' ');
                            _sucursal = words[0];

                            ScorecardSemanal scs = new ScorecardSemanal(_sucursal, FechaInicial, FechaFinal);
                            scs.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridScore_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in gridScore.Rows)
                {
                    if (Convert.ToString(row.Cells[(int)ColumnasGrid.JefaCobranza].Value) == "")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                    }
                    if (Convert.ToString(row.Cells[(int)ColumnasGrid.Sucursal].Value) == "Total")
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(226,239,218);
                    }
                }
            }
            catch (Exception) { }
        }

        private void gridScore_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.gridScore.Columns[e.ColumnIndex].Name == "Detalle" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = this.gridScore.Rows[e.RowIndex].Cells["Detalle"] as DataGridViewButtonCell;
                Icon icoAtomico;

                if (this.gridScore.Rows[e.RowIndex].Cells[(int)ColumnasGrid.JefaCobranza].Value.ToString() != ""
                    || this.gridScore.Rows[e.RowIndex].Cells[(int)ColumnasGrid.Sucursal].Value.ToString() == "Total")
                {
                    icoAtomico = Properties.Resources.boton_nodetalle;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 5, e.CellBounds.Top + 5);
                }
                else
                {
                    icoAtomico = Properties.Resources.boton_detalle;
                    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 5, e.CellBounds.Top + 5);
                }

                this.gridScore.Rows[e.RowIndex].Height = icoAtomico.Height + 10;
                this.gridScore.Columns[e.ColumnIndex].Width = icoAtomico.Width + 10;

                e.Handled = true;
            }
          
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel exp = new ExportarAExcel();
            if (exp.Exportar(gridScore, true))
                MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ScocreCardGlobal_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void ScocreCardGlobal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
              
            }
        }


        
    }
}
