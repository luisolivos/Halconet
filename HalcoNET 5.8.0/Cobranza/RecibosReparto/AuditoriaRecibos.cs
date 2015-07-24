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

namespace Cobranza.RecibosReparto
{
    public partial class AuditoriaRecibos : Form
    {
        public enum Columnas
        {
            Folio, Cliente, Nombre, JefaCobranza, Factura, FechaFactura, FechaRecibo, Dif1, FechaConfirmacion, Dif2
        }
        public string Sucursal;
        public string JefaCobranza;
        public int Rol;
        private string Usuario;
        Clases.Logs log;

        public AuditoriaRecibos(string _sucursal, string _jefa, int _rol, string _usuario)
        {
            InitializeComponent();

            Sucursal = _sucursal;
            JefaCobranza = _jefa;
            Rol = _rol;
            Usuario = _usuario;
            log = new Clases.Logs(Usuario, this.AccessibleDescription, 0);
        }

        public void Cargar()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
            {
                using (SqlCommand command = new SqlCommand("PJ_ReciboReparto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 10);
                    command.Parameters.AddWithValue("@JefaCobranza", string.Empty);
                    command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                    command.Parameters.AddWithValue("@Vendedor", 0);
                    command.Parameters.AddWithValue("@Revision", this.Cadena(clbCobranza));
                    command.Parameters.AddWithValue("@Cobro", string.Empty);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);

                    gridFacturas.DataSource = table;
                    this.Formato();
                }
            }
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
            }
        }

        public void Formato()
        {
            gridFacturas.Columns[(int)Columnas.Cliente].Visible = false;

            gridFacturas.Columns[(int)Columnas.Cliente].Width = 80;
            gridFacturas.Columns[(int)Columnas.Cliente].Width = 100;
            gridFacturas.Columns[(int)Columnas.Nombre].Width = 250;
            gridFacturas.Columns[(int)Columnas.JefaCobranza].Width = 100;
            gridFacturas.Columns[(int)Columnas.Factura].Width = 100;
            gridFacturas.Columns[(int)Columnas.FechaFactura].Width = 90;
            gridFacturas.Columns[(int)Columnas.FechaRecibo].Width = 90;
            gridFacturas.Columns[(int)Columnas.Dif1].Width = 90;
            gridFacturas.Columns[(int)Columnas.FechaConfirmacion].Width = 90;
            gridFacturas.Columns[(int)Columnas.Dif2].Width = 90;


            gridFacturas.Columns[(int)Columnas.FechaConfirmacion].DefaultCellStyle.NullValue = "--/--/----";
            gridFacturas.Columns[(int)Columnas.Dif2].DefaultCellStyle.NullValue = "--";
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

        private void AuditoriaRecibos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            this.CargarJefesCobranza();
        }

        private void gridFacturas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in gridFacturas.Rows)
                {
                    if (Convert.ToInt32(item.Cells[(int)Columnas.Dif1].Value) > 7)
                    {
                        item.Cells[(int)Columnas.Dif1].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Dif1].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Dif1].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Dif1].Style.ForeColor = Color.Black;
                    }

                    if (item.Cells[(int)Columnas.FechaConfirmacion].Value == System.DBNull.Value && Convert.ToDateTime(item.Cells[(int)Columnas.FechaRecibo].Value).AddDays(3) < DateTime.Now)
                    {
                        item.Cells[(int)Columnas.Dif2].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Dif2].Style.ForeColor = Color.White;
                    }
                    else if(item.Cells[(int)Columnas.FechaConfirmacion].Value != System.DBNull.Value)
                        if (Convert.ToInt32(item.Cells[(int)Columnas.Dif2].Value) > 3)
                        {
                            item.Cells[(int)Columnas.Dif2].Style.BackColor = Color.Red;
                            item.Cells[(int)Columnas.Dif2].Style.ForeColor = Color.White;
                        }
                        else
                        {
                            item.Cells[(int)Columnas.Dif2].Style.BackColor = Color.White;
                            item.Cells[(int)Columnas.Dif2].Style.ForeColor = Color.Black;
                        }
                }                   
            }
            catch (Exception)
            {
            }
        }

        private void btnPresentar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AuditoriaRecibos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void AuditoriaRecibos_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }
    }
}
