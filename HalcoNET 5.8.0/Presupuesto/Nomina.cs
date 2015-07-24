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

namespace Presupuesto
{
    public partial class Nomina : Form
    {
        public Nomina()
        {
            InitializeComponent();
        }

        public enum Columnas
        {
            cuenta,
            descripcion,
            tipogasto,
            sucursal,
            codigopuesto,
            empleado,
            depto,
            proyecto,
            nr,
            enero,
            febrero,
            marzo,
            abril,
            mayo,
            junio,
            julio,
            agosto,
            septiembre,
            octubre,
            nov,
            dic

        }

        public void formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.enero].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.febrero].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.marzo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.abril].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.mayo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.junio].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.julio].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.agosto].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.septiembre].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.octubre].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.nov].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.dic].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.enero].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.febrero].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.marzo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.abril].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.mayo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.junio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.julio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.agosto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.septiembre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.octubre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.nov].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.dic].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }


        public DataTable getdata(int tipo)
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionRH))
            {
                using (SqlCommand command = new SqlCommand("sp_Presupuesto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", tipo);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;

                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }

        private void Nomina_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                dgvOutsourcing.DataSource = getdata(1);
                dgvCostoSocial.DataSource = getdata(2);
                dgvVigilancia.DataSource = getdata(1);

                formato(dgvOutsourcing);
                formato(dgvCostoSocial);
                formato(dgvVigilancia);
            }
            catch (Exception)
            {
                
            }

        }
    }
}
