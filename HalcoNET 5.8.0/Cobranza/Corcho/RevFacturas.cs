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

namespace Cobranza.Corcho
{
    public partial class RevFacturas : Form
    {
        public RevFacturas()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            Sucursal,
            FechaRev,
            FechaCreacion,
            Dias
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Sucursal].Width = 100;
            dgv.Columns[(int)Columnas.FechaCreacion].Width = 100;
            dgv.Columns[(int)Columnas.FechaRev].Width = 100;
            dgv.Columns[(int)Columnas.Dias].Width = 80;
        }

        private void CargarSucursales()
        {
            DataTable tblSucursales = new DataTable();

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

            //if (Rol == (int)Constantes.RolesSistemaSGUV.Administrador)
            //{
                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                tblSucursales = table.Copy();
            //}
            //else
            //{
            //    if (Sucursal.Trim().ToUpper() == "MTY")
            //        Sucursal = "Monterrey";
            //    if (Sucursal.Trim().ToUpper() == "GDL")
            //        Sucursal = "Guadalajara";

            //    tblSucursales = (from item in table.AsEnumerable()
            //                     where item.Field<string>("Codigo").Trim().ToLower() == Sucursal.Trim().ToLower()
            //                     select item).CopyToDataTable();
            //}



            clbSucursal.DataSource = tblSucursales;
            clbSucursal.DisplayMember = "Nombre";
            clbSucursal.ValueMember = "Codigo";
        }

        public string Cadena(CheckedListBox clb)
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
                foreach (DataRowView item in clb.Items)
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
            }

            return stb.ToString();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "sp_Corcho";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;

                        command.Parameters.AddWithValue("@TipoConsulta", 8);
                        command.Parameters.AddWithValue("@Fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                        command.Parameters.AddWithValue("@DocEntry", cbMes.SelectedIndex);
                        command.Parameters.AddWithValue("@DocNum", 0);
                        command.Parameters.AddWithValue("@Responsable", string.Empty);
                        command.Parameters.AddWithValue("@chkb1", false);
                        command.Parameters.AddWithValue("@chkb2", false);
                        command.Parameters.AddWithValue("@chkb3", false);
                        command.Parameters.AddWithValue("@chkb4", false);
                        command.Parameters.AddWithValue("@chkb5", false);
                        command.Parameters.AddWithValue("@chkb6", false);
                        command.Parameters.AddWithValue("@chkb7", false);
                        command.Parameters.AddWithValue("@chkb8", false);
                        command.Parameters.AddWithValue("@chkb9", false);
                        command.Parameters.AddWithValue("@chkb10", false);
                        command.Parameters.AddWithValue("@Sucursales", this.Cadena(clbSucursal));
                        command.Parameters.AddWithValue("@Completar", string.Empty);
                        command.Parameters.AddWithValue("@Tipo", string.Empty);
                        command.Parameters.AddWithValue("@Estatus", string.Empty);
                        command.Parameters.AddWithValue("@Descuentos", string.Empty);
                        command.Parameters.AddWithValue("@TC_TD", string.Empty);
                        command.Parameters.AddWithValue("@Recibido", decimal.Zero);

                        DataTable table = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        da.SelectCommand.CommandTimeout = 0;
                        da.Fill(table);

                        dgvFacts.DataSource = table;
                        this.Formato(dgvFacts);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RevFacturas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                this.CargarSucursales();

                cbMes.SelectedIndex = DateTime.Now.Month - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dgvFacts_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (Convert.ToInt32(item.Cells[(int)Columnas.Dias].Value) == 1)
                    {

                        item.Cells[(int)Columnas.Dias].Style.BackColor = Color.Green;
                        item.Cells[(int)Columnas.Dias].Style.ForeColor = Color.Black;


                    }
                    else
                    {
                        item.Cells[(int)Columnas.Dias].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Dias].Style.ForeColor = Color.White;
                    }

                }
            }
            catch (Exception)
            {
            }
        }

    }
}
