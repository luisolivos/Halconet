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

namespace Cobranza.AntiguedadSaldos
{
    public partial class SeguimientoCompromisos : Form
    {
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);
        public string Sucursal;
        public int Rol;
        string JefaCobranza;
        private string Usuario;
        Clases.Logs log;
        public enum Columnas
        {
            Code, Tipo, Cliente, Nombre, Jefa, Creacion, FechaCompromiso, Debe, Comprometido, PagosCompromiso, PagosHoy, Pagado, Efectividad, Estatus
        }
        public enum ColumnasDetalle
        {
            Factura, FechaVto, TotalFactura, Saldo, PagosPrevios, PagosVto, PagosComp
        }

        public SeguimientoCompromisos(string _sucursal, int _rol, string _jefa, string _usuario)
        {
            JefaCobranza = _jefa;
            Rol = _rol;
            Sucursal = _sucursal;
            Usuario = _usuario;
            InitializeComponent();
            log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
        }

        public void FormatoDetalle()
        {
            //Cliente, , , , , , , , , , 
            gridDetalle.Columns[(int)ColumnasDetalle.Factura].Width = 90;
            gridDetalle.Columns[(int)ColumnasDetalle.FechaVto].Width = 90;
            gridDetalle.Columns[(int)ColumnasDetalle.TotalFactura].Width = 190;
            gridDetalle.Columns[(int)ColumnasDetalle.Saldo].Width = 100;
            gridDetalle.Columns[(int)ColumnasDetalle.PagosPrevios].Width = 100;
            gridDetalle.Columns[(int)ColumnasDetalle.PagosVto].Width = 100;
            gridDetalle.Columns[(int)ColumnasDetalle.PagosComp].Width = 100;

            gridDetalle.Columns[(int)ColumnasDetalle.TotalFactura].DefaultCellStyle.Format = "C2";
            gridDetalle.Columns[(int)ColumnasDetalle.Saldo].DefaultCellStyle.Format = "C2";
            gridDetalle.Columns[(int)ColumnasDetalle.PagosPrevios].DefaultCellStyle.Format = "C2";
            gridDetalle.Columns[(int)ColumnasDetalle.PagosVto].DefaultCellStyle.Format = "C2";
            gridDetalle.Columns[(int)ColumnasDetalle.PagosComp].DefaultCellStyle.Format = "C2";

            gridDetalle.Columns[(int)ColumnasDetalle.TotalFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridDetalle.Columns[(int)ColumnasDetalle.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridDetalle.Columns[(int)ColumnasDetalle.PagosPrevios].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridDetalle.Columns[(int)ColumnasDetalle.PagosVto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridDetalle.Columns[(int)ColumnasDetalle.PagosComp].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void Formato()
        {
            //Cliente, , , , , , , , , , 
            gridFacturas.Columns[(int)Columnas.Code].Visible = false;
            gridFacturas.Columns[(int)Columnas.Cliente].Width = 90;
            gridFacturas.Columns[(int)Columnas.Nombre].Width = 190;
            gridFacturas.Columns[(int)Columnas.Jefa].Width = 100;
            gridFacturas.Columns[(int)Columnas.Creacion].Width = 100;
            gridFacturas.Columns[(int)Columnas.FechaCompromiso].Width = 100;
            gridFacturas.Columns[(int)Columnas.Debe].Width = 100;
            gridFacturas.Columns[(int)Columnas.Comprometido].Width = 100;
            gridFacturas.Columns[(int)Columnas.PagosCompromiso].Width = 100;
            gridFacturas.Columns[(int)Columnas.PagosHoy].Width = 100;
            gridFacturas.Columns[(int)Columnas.Pagado].Width = 100;
            gridFacturas.Columns[(int)Columnas.Efectividad].Width = 100;
            gridFacturas.Columns[(int)Columnas.Estatus].Width = 100;

            gridFacturas.Columns[(int)Columnas.Debe].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)Columnas.Comprometido].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)Columnas.PagosCompromiso].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)Columnas.PagosHoy].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)Columnas.Pagado].DefaultCellStyle.Format = "C2";
            gridFacturas.Columns[(int)Columnas.Efectividad].DefaultCellStyle.Format = "P2";

            gridFacturas.Columns[(int)Columnas.Debe].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)Columnas.Comprometido].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)Columnas.PagosCompromiso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)Columnas.PagosHoy].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)Columnas.Pagado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridFacturas.Columns[(int)Columnas.Efectividad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void SeguimientoCompromisos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            CargarJefesCobranza();

            DateTime fechatemp = DateTime.Today;
            DateTime fecha1 = new DateTime(fechatemp.Year, fechatemp.Month, 1);

            dtInicial.Value = fecha1;
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

                //DataTable _t = new DataTable();
                //var query = from item in table.AsEnumerable()
                //            where item.Field<string>("Codigo").ToUpper() == getMemo(Sucursal)
                //            select item;

                //if (query.Count() > 0)
                //{
                //    _t = query.CopyToDataTable();
                //    clbCobranza.DataSource = _t;
                //    clbCobranza.DisplayMember = "Nombre";
                //    clbCobranza.ValueMember = "Codigo";
                //   // JefasCobranza = _t.Copy();
                //}

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
            }

            return _memo;
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

        /// <sumary> 
        /// Metodos para cambiar la apariencia del cursor
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

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Esperar();
                using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 4);
                        command.Parameters.AddWithValue("@Fecha", dtInicial.Value);
                        command.Parameters.AddWithValue("@FechaFinal", dtFinal.Value);
                        command.Parameters.AddWithValue("@Monto", 0);
                        command.Parameters.AddWithValue("@Comentario", string.Empty);
                        command.Parameters.AddWithValue("@Factura", 0);
                        command.Parameters.AddWithValue("@Otro", Cadena(clbCobranza));
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
                        Formato();
                    }
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            ExportarAExcel exp = new ExportarAExcel();
            if (exp.ExportarCobranza(gridFacturas))
                MessageBox.Show("Archivo creado con exíto.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnPresentar_Click(sender, e);
            }
        }

        private void gridFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    string _code = Convert.ToString(gridFacturas.Rows[gridFacturas.CurrentRow.Index].Cells[(int)Columnas.Code].Value);

                    try
                    {
                        this.Esperar();
                        using (SqlConnection con = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                        {
                            con.Open();
                            using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", con))
                            {
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.AddWithValue("@TipoConsulta", 5);
                                command.Parameters.AddWithValue("@Fecha", dtInicial.Value);
                                command.Parameters.AddWithValue("@FechaFinal", dtFinal.Value);
                                command.Parameters.AddWithValue("@Monto", 0);
                                command.Parameters.AddWithValue("@Comentario", string.Empty);
                                command.Parameters.AddWithValue("@Factura", 0);
                                command.Parameters.AddWithValue("@Otro", string.Empty);
                                command.Parameters.AddWithValue("@NumCompromiso", _code);
                                command.Parameters.AddWithValue("@Comprometido", 0);
                                command.Parameters.AddWithValue("@Tipo", "");

                                SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                                parameter.Direction = ParameterDirection.Output;
                                command.Parameters.Add(parameter);

                                DataTable table = new DataTable();
                                SqlDataAdapter adapter = new SqlDataAdapter();
                                adapter.SelectCommand = command;
                                adapter.Fill(table);

                                gridDetalle.DataSource = table;

                                FormatoDetalle();
                            }
                        }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridFacturas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in gridFacturas.Rows)
                {
                    if (Convert.ToString(item.Cells[(int)Columnas.Estatus].Value) == "No cumplido")
                    {
                        item.Cells[(int)Columnas.Estatus].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Estatus].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Estatus].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Estatus].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void SeguimientoCompromisos_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void SeguimientoCompromisos_FormClosing(object sender, FormClosingEventArgs e)
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

