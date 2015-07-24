using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Cobranza.GestionCobranza
{
    public partial class CoberturaCartera : Form
    {
        private string Sucursal;
        private int Rol;
        private string Usuario;
        private string JefaCobranza;
        Clases.Logs log;

        private enum Columnas
        {
            Cliente, Nombre, JefaCobranza, Compromisos, Cumplidos, Parciales, NoCumplidos, EnProceso,Desempeño
        }

        public CoberturaCartera(string _sucursal, string _usuario, int _rol, string _jefa)
        {
            InitializeComponent();
            Sucursal = this.GetSucursalName(_sucursal);
            Rol = _rol;
            Usuario = _usuario;
            JefaCobranza = _jefa;
            log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
        }

        private void CargarJefesCobranza()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
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
            //JefasCobranza = table.Copy();


            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                DataTable cob = new DataTable();
                cob.Columns.Add("Codigo");
                cob.Columns.Add("Nombre");

                DataRow row1 = cob.NewRow();
                row1["Nombre"] = JefaCobranza;
                row1["Codigo"] = JefaCobranza;
                cob.Rows.InsertAt(row1, 0);

                clbCobranza.DataSource = cob;
                clbCobranza.DisplayMember = "Nombre";
                clbCobranza.ValueMember = "Codigo";

            }
            else
            {
                clbCobranza.DataSource = table;
                clbCobranza.DisplayMember = "Nombre";
                clbCobranza.ValueMember = "Codigo";
                //JefasCobranza = table.Copy();
            }
        }

        public string getMemo(string Sucursal)
        {
            string _memo = "";
            switch (Sucursal)
            {
                case "PUEBLA": _memo = "01"; break;
                case "MONTERREY": _memo = "02"; break;
                case "MTY": _memo = "02"; break;
                case "APIZACO": _memo = "03"; break;
                case "CORDOBA": _memo = "05"; break;
                case "TEPEACA": _memo = "06"; break;
                case "EDOMEX": _memo = "16"; break;
                case "GUADALAJARA": _memo = "18"; break;
                case "SALTILLO": _memo = "23"; break;
            }

            return _memo;
        }

        private string GetSucursalName(string _suc)
        {
            switch (_suc)
            {
                case "PUEBLA": return "PUEBLA";
                case "CORDOBA": return "CORDOBA";
                case "EDOMEX": return "EDOMEX";
                case "GDL": return "GUADALAJARA";
                case "MTY": return "MONTERREY";
                case "MTYT": return "MONTERREY";
                case "MTYM": return "MONTERREY";
                case "TEPEACA": return "TEPEACA";
                case "APIZACO": return "APIZACO";
                case "SALTILLO": return "SALTILLO";
                default: return "";

            }
        }

        private string Cadena(CheckedListBox clb)
        {
            StringBuilder stbCobranza = new StringBuilder();
            foreach (DataRowView item in clb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!clb.ToString().Equals(string.Empty))
                    {
                        stbCobranza.Append(",");
                    }
                    stbCobranza.Append(item["Nombre"].ToString());
                }
            }
            if (clb.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in clb.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!clb.ToString().Equals(string.Empty))
                        {
                            stbCobranza.Append(",");
                        }
                        stbCobranza.Append(item["Nombre"].ToString());
                    }
                }
            }
            return stbCobranza.ToString();
        }

        private void CoberturaCartera_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            this.CargarJefesCobranza();
            comboBox1.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void Formato()
        {
            gridFacturas.Columns[(int)Columnas.Cliente].Width = 90;
            gridFacturas.Columns[(int)Columnas.Nombre].Width = 200;
            gridFacturas.Columns[(int)Columnas.JefaCobranza].Width = 120;
            gridFacturas.Columns[(int)Columnas.Compromisos].Width = 100;
            gridFacturas.Columns[(int)Columnas.Cumplidos].Width = 100;
            gridFacturas.Columns[(int)Columnas.Parciales].Width = 100;
            gridFacturas.Columns[(int)Columnas.NoCumplidos].Width = 100;
            gridFacturas.Columns[(int)Columnas.EnProceso].Width = 100;
            gridFacturas.Columns[(int)Columnas.Desempeño].Width = 100;

            gridFacturas.Columns[(int)Columnas.Compromisos].DefaultCellStyle.Format = "N0";
            gridFacturas.Columns[(int)Columnas.Cumplidos].DefaultCellStyle.Format = "N0";
            gridFacturas.Columns[(int)Columnas.Parciales].DefaultCellStyle.Format = "N0";
            gridFacturas.Columns[(int)Columnas.NoCumplidos].DefaultCellStyle.Format = "N0";
            gridFacturas.Columns[(int)Columnas.EnProceso].DefaultCellStyle.Format = "N0";
            gridFacturas.Columns[(int)Columnas.Desempeño].DefaultCellStyle.Format = "P2";

            gridFacturas.Columns[(int)Columnas.Compromisos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridFacturas.Columns[(int)Columnas.Cumplidos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridFacturas.Columns[(int)Columnas.Parciales].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridFacturas.Columns[(int)Columnas.NoCumplidos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridFacturas.Columns[(int)Columnas.EnProceso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridFacturas.Columns[(int)Columnas.Desempeño].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void Totales(DataTable _t)
        {
            DataTable _totales = new DataTable();
            _totales.Columns.Add("# Compromisos generados", typeof(Int32));
            _totales.Columns.Add("Cumplidos", typeof(Int32));
            _totales.Columns.Add("Parciales", typeof(Int32));
            _totales.Columns.Add("No cumplidos", typeof(Int32));
            _totales.Columns.Add("En proceso", typeof(Int32));
            _totales.Columns.Add("% de desempeño", typeof(decimal));
            _totales.Columns.Add("Llamadas", typeof(decimal));

            int Compromisos = 0;
            int Cumplidos = 0;
            int Parciales = 0;
            int NoCumplidos = 0;
            int Proceso = 0;
            int Llamadas = 0;

            Compromisos = Convert.ToInt32(_t.Compute("SUM([# Compromisos generados])", ""));
            Cumplidos = Convert.ToInt32(_t.Compute("SUM(Cumplidos)", ""));
            Parciales = Convert.ToInt32(_t.Compute("SUM(Parciales)", ""));
            NoCumplidos = Convert.ToInt32(_t.Compute("SUM([No cumplidos])", ""));
            Proceso = Convert.ToInt32(_t.Compute("SUM([En proceso])", ""));
            Llamadas = Convert.ToInt32(_t.Compute("SUM([Llamadas])", ""));

            DataRow row = _totales.NewRow();
            row["# Compromisos generados"] = Compromisos;
            row["Cumplidos"] = Cumplidos;
            row["Parciales"] = Parciales;
            row["No cumplidos"] = NoCumplidos;
            row["En proceso"] = Proceso;
            row["Llamadas"] = Llamadas;

            if (Compromisos - Proceso > 0)
                row["% de desempeño"] = (decimal)(Cumplidos + (Parciales * 0.5)) / (decimal)(Compromisos - Proceso);
            else row["% de desempeño"] = 0;
            _totales.Rows.Add(row);

            dataGridView1.DataSource = _totales;

            dataGridView1.Columns[5].DefaultCellStyle.Format = "P2";

            TxT(_t);
        }

        private void TxT(DataTable _t)
        {
            int clientes = (from item in _t.AsEnumerable()
                            where item.Field<Int32>("# Compromisos generados") > 0
                            select item).Count();

            int llamadas = (from item in _t.AsEnumerable()
                            where item.Field<Int32>("Llamadas") > 1
                            select item).Count();

            int cartera = (from item in _t.AsEnumerable()
                           // where item.Field<decimal>("# Compromisos generados") > 0
                            select item).Count();

            txtclientes.Text = clientes.ToString();
            txtcartera.Text = cartera.ToString();
            txtLlamadas.Text = llamadas.ToString();


            if (cartera > 0)
            {
                txtCobertura.Text = (((decimal)clientes / (decimal)cartera)).ToString("P2");
                txtCobertura2.Text = (((decimal)llamadas / (decimal)cartera)).ToString("P2");
            }

        }

        private void clbCobranza_Click(object sender, EventArgs e)
        {
            if (clbCobranza.SelectedIndex == 0)
            {
                if (clbCobranza.CheckedIndices.Contains(0))
                {
                    for (int item = 1; item < clbCobranza.Items.Count; item++)
                    {
                        clbCobranza.SetItemChecked(item, false);
                    }
                }
                else
                {
                    for (int item = 1; item < clbCobranza.Items.Count; item++)
                    {
                        clbCobranza.SetItemChecked(item, true);
                    }
                }
            }
        }

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                btnExportar.Enabled = false;
                string Jefas = this.Cadena(clbCobranza);
                DateTime FechaInicial = new DateTime(DateTime.Now.Year, comboBox1.SelectedIndex + 1, 1);
                DateTime FechaFinal = new DateTime();
                if (DateTime.Now.Month < comboBox1.SelectedIndex + 1)
                {
                    FechaFinal = new DateTime(DateTime.Now.Year, comboBox1.SelectedIndex + 1, 1).AddMonths(1).AddDays(-1);
                }
                else
                    FechaFinal = DateTime.Now;

                using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 6);
                        command.Parameters.AddWithValue("@Fecha", FechaInicial);
                        command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                        command.Parameters.AddWithValue("@Monto", 0);
                        command.Parameters.AddWithValue("@Comentario", string.Empty);
                        command.Parameters.AddWithValue("@Factura", 0);
                        command.Parameters.AddWithValue("@Otro", Jefas);
                        command.Parameters.AddWithValue("@NumCompromiso", textBox1.Text);
                        command.Parameters.AddWithValue("@Comprometido", 0);
                        command.Parameters.AddWithValue("@Tipo", "");

                        SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                        parameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parameter);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(table);

                        gridFacturas.DataSource = table;

                        if (table.Rows.Count > 0)
                        {
                            this.Totales(table);
                            btnExportar.Enabled = true;
                            this.Formato();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarAExcel exp = new ExportarAExcel();
            if (exp.Exportar(gridFacturas, false))
            {
                MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnPresentar_Click(sender, e);
            }
        }

        private void CoberturaCartera_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void CoberturaCartera_FormClosing(object sender, FormClosingEventArgs e)
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
