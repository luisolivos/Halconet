using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.GestionCobranza
{
    public partial class frmComments : Form
    {
        public frmComments(string _cliente, string _nombre)
        {
            InitializeComponent();
            
            txtCliente.Text = _cliente;
            txtNombre.Text = _nombre;
        }

        public enum Columas
        {
            Code,
            Fecha,
            Comentario
        }

        public void formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columas.Code].Visible = false;
            dgv.Columns[(int)Columas.Fecha].Width = 120;
            dgv.Columns[(int)Columas.Comentario].Width = 250;

            dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        }

        private void frmComments_Load(object sender, EventArgs e)
        {
            try
            {
                button1.Enabled = ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador
                                    || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza;

                txtComentario.Enabled = ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador
                                    || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteCobranza;

                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                using (SqlConnection connection= new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 10);
                        command.Parameters.AddWithValue("@Cliente", txtCliente.Text);

                        command.CommandTimeout = 0;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = command;
                        DataTable tbl = new DataTable();
                        da.Fill(tbl);

                        dataGridView1.DataSource = tbl;

                        this.formato(dataGridView1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtComentario.Text.Trim()))
                {
                    MessageBox.Show("Ingrese un comentario.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 11);
                        command.Parameters.AddWithValue("@Cliente", txtCliente.Text);
                        command.Parameters.AddWithValue("@Code", 0);
                        command.Parameters.AddWithValue("@Coment", txtComentario.Text);

                        connection.Open();

                        command.ExecuteNonQuery();


                    }

                } 
                this.OnLoad(e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if(e.Row.Index > -1)
            try
            {
                if (MessageBox.Show("¿Esta seguro de elimiar este registro?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@TipoConsulta", 11);
                            command.Parameters.AddWithValue("@Code", e.Row.Cells["Code"].Value);

                            connection.Open();

                            command.ExecuteNonQuery();

                            
                        }
                    }

                    //dataGridView1.DataSource = null;
                    //dataGridView1.Columns.Clear();
                    
                    //using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    //{
                    //    using (SqlCommand command = new SqlCommand("PJ_GestionCobranza", connection))
                    //    {
                    //        command.CommandType = CommandType.StoredProcedure;
                    //        command.Parameters.AddWithValue("@TipoConsulta", 10);
                    //        command.Parameters.AddWithValue("@Cliente", txtCliente.Text);

                    //        command.CommandTimeout = 0;
                    //        SqlDataAdapter da = new SqlDataAdapter();
                    //        da.SelectCommand = command;
                    //        DataTable tbl = new DataTable();
                    //        da.Fill(tbl);

                    //        dataGridView1.DataSource = tbl;

                    //    }
                    //}
                }
                else
                {
                    e.Cancel = true;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
