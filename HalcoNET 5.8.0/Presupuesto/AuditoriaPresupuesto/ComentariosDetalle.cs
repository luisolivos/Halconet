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

namespace Presupuesto.AuditoriaPresupuesto
{
    public partial class ComentariosDetalle : Form
    {
        string Proyecto;
        string Cuenta;
        string Nombre;
        string FechaInicio;
        string FechaFin;
        string Sucursal;
        string NR;

        public enum Columnas {
            Cuenta, Proyecto, Comentario
        }

        public void Formato()
        {
            dgvComments.Columns[(int)Columnas.Cuenta].Width = 100;
            dgvComments.Columns[(int)Columnas.Proyecto].Width = 100;
            dgvComments.Columns[(int)Columnas.Comentario].Width = 300;

            dgvComments.Columns[(int)Columnas.Comentario].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }

        public ComentariosDetalle(string proyecto, string cuenta, string nombre, string fechaini, string fechafin, string sucursal, string nr)
        {
            InitializeComponent();

            Proyecto = proyecto;
            Cuenta = cuenta;
            Nombre = nombre;
            FechaInicio = fechaini;
            FechaFin = fechafin;
            Sucursal = sucursal;
            NR = nr;
        }

        private void ComentariosDetalle_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            txtProyecto.Text = Proyecto;
            txtCuenta.Text = Cuenta;
            txtNombre.Text = Nombre;

            dgvComments.Columns.Clear();
            dgvComments.DataSource = null;

            SqlCommand command = new SqlCommand("PJ_PresupuestoCuenta", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 7);
            command.Parameters.AddWithValue("@Sucursal", Sucursal);
            command.Parameters.AddWithValue("@Cuenta", Cuenta);
            command.Parameters.AddWithValue("@FechaInicial", FechaInicio);
            command.Parameters.AddWithValue("@FechaFinal", FechaFin);
            command.Parameters.AddWithValue("@Comentario", Proyecto);
            command.Parameters.AddWithValue("@Rol", 0);
            command.Parameters.AddWithValue("@NR", NR);

            command.CommandTimeout = 0;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);

            dgvComments.DataSource = table;
            this.Formato();
        }
    }
}
