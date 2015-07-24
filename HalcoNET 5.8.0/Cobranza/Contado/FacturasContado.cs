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
    public partial class FacturasContado : Form
    {
        private int Rol;
        private string Sucursal;
        private string JefaCobranza;
        private Clases.Logs log;

        public enum Columnas
        {
            Fecha,
            Factura,
            Cliente,
            Nombre,
            Jefa,
            Monto,
            Saldo,
            Dias,
            Pagos
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Fecha].Width = 100;
            dgv.Columns[(int)Columnas.Factura].Width = 100;
            dgv.Columns[(int)Columnas.Cliente].Width = 100;
            dgv.Columns[(int)Columnas.Nombre].Width = 300;
            dgv.Columns[(int)Columnas.Jefa].Width = 100;
            dgv.Columns[(int)Columnas.Monto].Width = 100;
            dgv.Columns[(int)Columnas.Dias].Width = 100;
            dgv.Columns[(int)Columnas.Pagos].Width = 100;
            dgv.Columns[(int)Columnas.Saldo].Width = 100;

            dgv.Columns[(int)Columnas.Monto].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Dias].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)Columnas.Pagos].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Monto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Dias].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Pagos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public FacturasContado(int _rol, string _sucursal, string _jefa)
        {
            InitializeComponent();

            Rol = _rol;
            Sucursal = _sucursal;
            JefaCobranza = _jefa;
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

        private void FacturasContado_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            this.CargarJefesCobranza();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {

                        command.Connection = connection;

                        command.CommandText = "PJ_ClientesContado";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Cliente", string.Empty);
                        command.Parameters.AddWithValue("@JefasCobranza", Cadena(clbCobranza));
                        command.Parameters.AddWithValue("@Sucursales", string.Empty);
                        command.Parameters.AddWithValue("@FechaInicio", DateTime.Now);
                        command.Parameters.AddWithValue("@FechaFin", DateTime.Now);

                        DataTable table = new DataTable();
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(table);

                        gridFacturas.DataSource = table;

                        this.Formato(gridFacturas);
                    }

                }
            }
            catch (Exception)
            {
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

        private void button2_Click(object sender, EventArgs e)
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

        private void FacturasContado_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
            }
        }

        private void FacturasContado_FormClosing(object sender, FormClosingEventArgs e)
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
