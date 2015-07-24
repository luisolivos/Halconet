using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza
{
    public partial class frmAtradiusJuridico : Form
    {
        private enum Columnas
        {
            Cliente,
            Nombre,
            Sucursal,
            Saldos,
            Año,
            Ene,
            Feb,
            Mar,
            Abrl,
            May,
            Jun,
            Jul,
            Ago,
            Sep,
            Oct,
            Nov,
            Dic,
            Fecha,
            FechaAño,
            Saldo
        }

        public frmAtradiusJuridico()
        {
            InitializeComponent();
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Cliente].Width = 70;
            dgv.Columns[(int)Columnas.Nombre].Width = 200;
            dgv.Columns[(int)Columnas.Sucursal].Width = 100;
            dgv.Columns[(int)Columnas.Saldos].Width = 90;
            dgv.Columns[(int)Columnas.Año].Width = 60;
            dgv.Columns[(int)Columnas.Ene].Width = 75;
            dgv.Columns[(int)Columnas.Feb].Width = 75;
            dgv.Columns[(int)Columnas.Mar].Width = 75;
            dgv.Columns[(int)Columnas.Abrl].Width = 75;
            dgv.Columns[(int)Columnas.May].Width = 75;
            dgv.Columns[(int)Columnas.Jun].Width = 75;
            dgv.Columns[(int)Columnas.Jul].Width = 75;
            dgv.Columns[(int)Columnas.Ago].Width = 75;
            dgv.Columns[(int)Columnas.Sep].Width = 75;
            dgv.Columns[(int)Columnas.Oct].Width = 75;
            dgv.Columns[(int)Columnas.Nov].Width = 75;
            dgv.Columns[(int)Columnas.Dic].Width = 75;
            dgv.Columns[(int)Columnas.Saldo].Width = 75;

            dgv.Columns[(int)Columnas.Saldos].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Ene].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Feb].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mar].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Abrl].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.May].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Jun].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Jul].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Ago].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Sep].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Oct].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Nov].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Dic].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Saldos].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Ene].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Feb].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mar].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Abrl].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.May].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Jun].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Jul].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Ago].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Sep].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Oct].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Nov].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Dic].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Fecha].Visible = false;
            dgv.Columns[(int)Columnas.FechaAño].Visible = false;

        }

        public DataTable Source(int _tipoConsulta, DateTime _fecha)
        {
            DataTable table = new DataTable();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_AtradiusP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", _tipoConsulta);
                    command.Parameters.AddWithValue("@Desde", _fecha);
                    command.Parameters.AddWithValue("@Hasta", _fecha);
                    command.Parameters.AddWithValue("@CardCode", string.Empty);
                    command.Parameters.AddWithValue("@CardName", DateTime.Now);
                    command.Parameters.AddWithValue("@DocEntry", string.Empty);
                    command.Parameters.AddWithValue("@DocNum", string.Empty);

                    SqlParameter ValidaUsuario = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 500);
                    ValidaUsuario.Direction = ParameterDirection.Output;
                    command.Parameters.Add(ValidaUsuario);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.SelectCommand.CommandTimeout = 0;

                    da.Fill(table);
                }
            }

            return table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable tbl = Source(14, DateTime.Now);
            dataGridView1.DataSource = tbl;

            this.Formato(dataGridView1);
        }

        private void frmAtradiusJuridico_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in (sender as DataGridView).Rows)
                {
                    string _header = string.Empty;
                    Color color = Color.White;
                    foreach (DataGridViewColumn item in (sender as DataGridView).Columns)
                    {
                        _header = item.HeaderText;
                        if (row.Cells[(int)Columnas.Fecha].Value.ToString().Equals(_header) & Convert.ToInt32(row.Cells[(int)Columnas.Año].Value) >= Convert.ToInt32(row.Cells[(int)Columnas.FechaAño].Value))
                        {
                           color = Color.FromName("Info");
                        }
                        row.Cells[item.Index].Style.BackColor = color;
                    }

                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
