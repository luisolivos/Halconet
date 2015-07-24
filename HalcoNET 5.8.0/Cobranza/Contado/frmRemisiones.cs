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

namespace Cobranza.Contado
{
    public partial class frmRemisiones : Form
    {
        private int Rol;
        private string Sucursal;
        private string JefaCobranza;

        public int CodVendedor;//slpcode
        public string NombreUsuario;
        public DataTable JefasCobranza = new DataTable();
        public SqlConnection conection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);
        Clases.Logs log;

        public enum Columnas
        {
            Enviar, Remision, Fecha, Cliente, Nombre, Sucursal, Monto, Meses, Situacion, Vendedor, Comentarios, Enviado, Jefa, Correo1, Correo2
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Enviar].Width = 50;
            dgv.Columns[(int)Columnas.Remision].Width = 100;
            dgv.Columns[(int)Columnas.Fecha].Width = 100;
            dgv.Columns[(int)Columnas.Cliente].Width = 100;
            dgv.Columns[(int)Columnas.Nombre].Width = 250;
            dgv.Columns[(int)Columnas.Sucursal].Width = 100;
            dgv.Columns[(int)Columnas.Monto].Width = 100;
            dgv.Columns[(int)Columnas.Meses].Width = 100;
            dgv.Columns[(int)Columnas.Situacion].Width = 100;
            dgv.Columns[(int)Columnas.Vendedor].Width = 100;
            dgv.Columns[(int)Columnas.Enviado].Width = 50;

            dgv.Columns[(int)Columnas.Remision].ReadOnly = true;
            dgv.Columns[(int)Columnas.Fecha].ReadOnly = true;
            dgv.Columns[(int)Columnas.Cliente].ReadOnly = true;
            dgv.Columns[(int)Columnas.Nombre].ReadOnly = true;
            dgv.Columns[(int)Columnas.Sucursal].ReadOnly = true;
            dgv.Columns[(int)Columnas.Monto].ReadOnly = true;
            dgv.Columns[(int)Columnas.Meses].ReadOnly = true;
            dgv.Columns[(int)Columnas.Situacion].ReadOnly = true;
            dgv.Columns[(int)Columnas.Vendedor].ReadOnly = true;
            dgv.Columns[(int)Columnas.Enviado].ReadOnly = true;

            dgv.Columns[(int)Columnas.Jefa].Visible = false;
            dgv.Columns[(int)Columnas.Correo1].Visible = false;
            dgv.Columns[(int)Columnas.Correo2].Visible = false;

            dgv.Columns[(int)Columnas.Monto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Meses].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[(int)Columnas.Monto].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Meses].DefaultCellStyle.Format = "N0";
        }

        public frmRemisiones(int _rol, string _sucursal, string _jefa)
        {
            InitializeComponent();

            Rol = _rol;
            Sucursal = _sucursal;
            JefaCobranza = _jefa;
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
                case "GDL": _memo = "18"; break;
                case "GUADALAJARA": _memo = "18"; break;
            }

            return _memo;
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

            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                DataTable _t = new DataTable();
                var query = from item in table.AsEnumerable()
                            where item.Field<string>("Codigo").ToUpper() == getMemo(Sucursal)
                            select item;

                if (query.Count() > 0)
                {

                    _t = query.CopyToDataTable();
                    clbCobranza.DataSource = _t;
                    clbCobranza.DisplayMember = "Nombre";
                    clbCobranza.ValueMember = "Codigo";
                    JefasCobranza = _t.Copy();
                    DataRow row = _t.NewRow();
                    row["Nombre"] = "TODAS";
                    row["Codigo"] = "0";
                    _t.Rows.InsertAt(row, 0);
                }

            }
            else
            {
                clbCobranza.DataSource = table;
                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);
                clbCobranza.DisplayMember = "Nombre";
                clbCobranza.ValueMember = "Codigo";
                JefasCobranza = table.Copy();
            }
        }

        private void CargarSucursales()
        {

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

            if (Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.JefasCobranza)
            {
                DataTable _t = new DataTable();
                string suc = "";
                if (Sucursal.ToUpper() == "MTY")
                    suc = "MONTERREY";


                else if (Sucursal.ToUpper() == "GDL")
                    suc = "GUADALAJARA";
                else
                    suc = Sucursal;

                var query = from item in table.AsEnumerable()
                            where item.Field<string>("Codigo").ToUpper() == suc
                            select item;

                if (query.Count() > 0)
                {
                    _t = query.CopyToDataTable();
                    clbSucursal.DataSource = _t;
                    clbSucursal.DisplayMember = "Nombre";
                    clbSucursal.ValueMember = "Codigo";
                }

            }
            else
            //if (Rol == (int)Constantes.RolesSistemaSGUV.Administrador)
            {
                clbSucursal.DataSource = table;
                clbSucursal.DisplayMember = "Nombre";
                clbSucursal.ValueMember = "Codigo";
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

        public void RegistraEnviado(string _rem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_ClientesContado", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Usuario);
                        command.Parameters.AddWithValue("@DocNum", _rem);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Remisiones_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            this.CargarSucursales();
            this.CargarJefesCobranza();
        }

        DataTable JefasxSucursal = new DataTable();
        private void clbSucursal_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (e.NewValue == CheckState.Checked)
                {
                    DataRowView c = clbSucursal.Items[e.Index] as DataRowView;
                    string _memo = getMemo(Convert.ToString(c["Codigo"]).ToUpper());
                    JefasxSucursal.Merge((from item in JefasCobranza.AsEnumerable() where item["Codigo"].ToString() == _memo select item).CopyToDataTable());
                }
                else if (e.NewValue == CheckState.Unchecked)
                {
                    DataRowView c = clbSucursal.Items[e.Index] as DataRowView;
                    string _memo = getMemo(Convert.ToString(c["Codigo"]).ToUpper());
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
                gridFacturas.DataSource = null;
                gridFacturas.Columns.Clear();
                
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        btnExportar.Enabled = false;
                        command.Connection = connection;

                        command.CommandText = "PJ_ClientesContado";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 2);
                        command.Parameters.AddWithValue("@Cliente", string.Empty);
                        command.Parameters.AddWithValue("@JefasCobranza", Cadena(clbCobranza));
                        command.Parameters.AddWithValue("@Sucursales", Cadena(clbSucursal));
                        command.Parameters.AddWithValue("@FechaInicio", DateTime.Now);
                        command.Parameters.AddWithValue("@FechaFin", DateTime.Now);

                        command.Parameters.AddWithValue("@Todo", checkBox1.Checked ? "Y" : "N");

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(table);

                        gridFacturas.DataSource = table;

                        if (table.Rows.Count > 0)
                        {
                            btnExportar.Enabled = true;
                        }

                        this.Formato(gridFacturas);
                    }

                }
            }
            catch (Exception)
            {

            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarAExcel exp = new ExportarAExcel();
                if (exp.Exportar(gridFacturas, false))
                {
                    MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {


            }
        }

        private void Remisiones_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
            }
        }

        private void Remisiones_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
            }
        }

        private void gridFacturas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    foreach (DataGridViewCell cell in item.Cells)
                    {
                        cell.Style.ForeColor = Color.Black;
                        cell.Style.BackColor = Color.White;
                    }
                }


                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToDateTime(item.Cells[(int)Columnas.Fecha].Value).Month != DateTime.Now.Month)
                    {
                        item.Cells[(int)Columnas.Fecha].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Fecha].Style.ForeColor = Color.White;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        DataGridView DGV = new DataGridView();
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var qry = (from item in (gridFacturas.DataSource as DataTable).AsEnumerable()
                           where item.Field<bool>("Enviar") == true
                           select item);

                if (qry.Count() <= 0)
                {
                    MessageBox.Show("Debe seleccionar una remisión", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    var vendors = (from v in qry.AsEnumerable()
                                   select
                                   new
                                   {
                                       Vendedor = v.Field<string>("Vendedor"),
                                       Jefa = v.Field<string>("Jefa de cobranza"),
                                       CorreoVendor = v.Field<string>("CorreoVendor"),
                                       CorreoJefa = v.Field<string>("CorreoJefa")
                                   }).Distinct();

                    foreach (var vendedor in vendors.AsEnumerable())
                    {
                        var remisiones = (from item in (gridFacturas.DataSource as DataTable).AsEnumerable()
                                          where item.Field<bool>("Enviar") == true && item.Field<string>("Vendedor").Equals(vendedor.Vendedor)
                                          select item);
                        //==========================================================
                        try
                        {
                            String Mensaje = "Se adjuntan remisiones pendientes por cerrarse.";


                            string tabla = @"
                                    <style type='text/css'>
                                    table, th, td {
                                        border: 1px solid black;
                                        border-spacing: 50px 50px 50px 50px;
                                        border-collapse: collapse;
                                    }
                                    </style>

                                   <br><br>
                                   <table cellpadding='6'>
                                   <tr>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>No<br>Remisión</strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Fecha de<br>Contabilizaci&oacute;n</strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Cliente</strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Nombre del<br>Cliente</strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Total<br>documento</strong></font></td>
                                        <td bgcolor='#2A2627'><font size=2 face='Calibri' color='#FFFFFF'><strong>Días<br>transcurridos</strong></font></td>
                                        <td bgcolor='#FF0000'><font size=2 face='Calibri' color='#FFFFFF'><strong>Commentarios</strong></font></td>
                                   </tr>";


                            DataTable _resul = remisiones.CopyToDataTable();
                            string _color = string.Empty;
                            int filas = 0;

                            foreach (DataRow row in _resul.Rows)
                            {
                                tabla += @"<tr>
                                        <td><font size=2 face='Calibri'>" + row.Field<int>("Remisión") + "</font></td>" +
                                                "<td><font size=2 face='Calibri'>" + row.Field<DateTime>("Fecha de contabilización").ToShortDateString() + "</font></td>" +
                                                "<td><font size=2 face='Calibri'>" + row.Field<string>("Cliente") + "</font></td>" +
                                                "<td><font size=2 face='Calibri'><p align='right'>" + row.Field<string>("Nombre del cliente") + "</p></font></td>" +
                                                "<td><font size=2 face='Calibri'><p align='right'>" + row.Field<decimal>("Total documento").ToString("C2") + "</p></font></td>" +
                                                "<td><font size=2 face='Calibri'><p align='right'>" + row.Field<int>("Días transcurridos").ToString("C2") + "</p></font></td>" +
                                                "<td><font size=2 face='Calibri'><p align='right'>" + row.Field<string>("Comentarios") + "</p></font></td>" +
                                          "</tr>";
                                filas++;
                            }

                            tabla += @"
                                </table>
                                ";

                            ClasesSGUV.ConvertToPDF pfd = new ClasesSGUV.ConvertToPDF();

                            //DGV = gridFacturas;
                           


                            //int x = DGV.Rows.Count;

                            //for (int i = x-1; i > 0; i--)
                            //{
                            //    bool enviar = Convert.ToBoolean(DGV.Rows[i].Cells[0].Value);
                            //    if (!enviar)
                            //        DGV.Rows.RemoveAt(i);
                            //}
                            dataGridView1.DataSource = null;
                            dataGridView1.DataSource = remisiones.CopyToDataTable();
                            this.Formato(dataGridView1);
                            dataGridView1.Columns.RemoveAt(14);
                            dataGridView1.Columns.RemoveAt(13);
                            dataGridView1.Columns.RemoveAt(12);
                            dataGridView1.Columns.RemoveAt(11);
                            dataGridView1.Columns.RemoveAt(9);
                            dataGridView1.Columns.RemoveAt(8);
                            dataGridView1.Columns.RemoveAt(0);

                            string _Path = pfd.CreatePDF(dataGridView1);
                            

                            Cobranza.SendMail mail = new SendMail();

                            if (!string.IsNullOrEmpty(vendedor.CorreoVendor))
                            {
                                if (!string.IsNullOrEmpty(vendedor.CorreoJefa))
                                {
                                    //mail.EnviarEstadoCuenta("jose.olivos@pj.com.mx", Mensaje + tabla, "Remisiones abiertas", string.Empty, vendedor.Jefa, "jose.olivos@pj.com.mx", _Path);

                                    //mail.EnviarEstadoCuenta(vendedor.CorreoVendor, Mensaje + tabla, "Remisiones abiertas", vendedor.CorreoJefa, vendedor.Jefa, vendedor.CorreoJefa, _Path);

                                    foreach (DataRow row in _resul.Rows)
                                    {
                                        this.RegistraEnviado(row.Field<int>("Remisión").ToString());
                                    }
                                }
                                else
                                    MessageBox.Show("La Jefa de Cobranza: " + vendedor.Jefa + " no tiene asignada una cuenta de correo", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            else
                                MessageBox.Show("El vendedor: " + vendedor.Vendedor + " no tiene asignada una cuenta de correo", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        }
                        catch (Exception ex1)
                        {
                            MessageBox.Show("Error: " + ex1.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        //==========================================================

                    }
                    MessageBox.Show("Listo", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnPresentar_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
