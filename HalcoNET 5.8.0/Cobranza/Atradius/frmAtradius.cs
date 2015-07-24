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

namespace Cobranza.Atradius
{
    public partial class frmAtradius : Form
    {
        public frmAtradius()
        {
            InitializeComponent();
        }

        Clases.Logs log;

        public enum Columnas
        {
            Cliente,
            Nombre,
            Sucursal,
            Credito,
            CreditoAtradius,
            Cond_Pago,
            Atradius,
            Juridico,
            Venta, 
            Reporta
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.Cliente].Width = 80;
            dgv.Columns[(int)Columnas.Nombre].Width = 240;
            dgv.Columns[(int)Columnas.Sucursal].Width = 90;
            dgv.Columns[(int)Columnas.Credito].Width = 90;
            dgv.Columns[(int)Columnas.CreditoAtradius].Width = 90;
            dgv.Columns[(int)Columnas.Cond_Pago].Width = 70;
            dgv.Columns[(int)Columnas.Atradius].Width = 70;
            dgv.Columns[(int)Columnas.Juridico].Width = 70;
            dgv.Columns[(int)Columnas.Venta].Width = 90;
            dgv.Columns[(int)Columnas.Reporta].Width = 90;

            dgv.Columns[(int)Columnas.Credito].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.CreditoAtradius].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Venta].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Reporta].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Credito].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.CreditoAtradius].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Reporta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)Columnas.Atradius].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.CreditoAtradius].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)Columnas.Juridico].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void Respaldar(int mes, int año)
        {
            progressBar.Maximum = gridFacturas.Rows.Count;
            progressBar.Value = 0;
            progressBar.Step = 1;

            foreach (DataGridViewRow row in gridFacturas.Rows)
            {
                string qry = @"IF NOT EXISTS (SELECT * FROM PJ_RAtradius Where U_Cliente = @Cliente AND U_Mes = @Mes AND U_Año = @Año)
                               begin Insert into PJ_RAtradius Values(@Cliente, @Nombre, @Sucursal, @LimiteCred, @LimAtradius, @CondPago, @Atradius, @Juridico, @VentaNeta, @Reporta, @Mes, @Año) end
                               ELSE begin Update PJ_RAtradius Set U_Cliente = @Cliente Where U_Cliente = @Cliente end";

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand(qry))
                    {
                        command.Connection = connection;

                        command.Parameters.AddWithValue("@Cliente", row.Cells[(int)Columnas.Cliente].Value.ToString());
                        command.Parameters.AddWithValue("@Nombre", row.Cells[(int)Columnas.Nombre].Value.ToString());
                        command.Parameters.AddWithValue("@Sucursal", row.Cells[(int)Columnas.Sucursal].Value.ToString());
                        command.Parameters.AddWithValue("@LimiteCred", Convert.ToDecimal(row.Cells[(int)Columnas.Credito].Value));
                        command.Parameters.AddWithValue("@LimAtradius", Convert.ToDecimal(row.Cells[(int)Columnas.CreditoAtradius].Value));
                        command.Parameters.AddWithValue("@CondPago", row.Cells[(int)Columnas.Cond_Pago].Value.ToString());
                        command.Parameters.AddWithValue("@Atradius", row.Cells[(int)Columnas.Atradius].Value.ToString());
                        command.Parameters.AddWithValue("@Juridico", row.Cells[(int)Columnas.Juridico].Value.ToString());
                        command.Parameters.AddWithValue("@VentaNeta", Convert.ToDecimal(row.Cells[(int)Columnas.Venta].Value));
                        command.Parameters.AddWithValue("@Reporta", Convert.ToDecimal(row.Cells[(int)Columnas.Reporta].Value));
                        command.Parameters.AddWithValue("@Mes", mes);
                        command.Parameters.AddWithValue("@Año", año);

                        connection.Open();
                        command.ExecuteNonQuery();

                        

                        progressBar.Value++;

                        toolPercent.Text = "Procesando: " + (progressBar.Value / progressBar.Maximum).ToString("P2");

                    }
                }
            }
            progressBar.Value = 0;
            toolPercent.Text = "Listo!";

        }

        public void Eliminar(int mes, int año)
        {
            progressBar.Maximum = gridFacturas.Rows.Count;
            progressBar.Value = 0;
            progressBar.Step = 1;

            //foreach (DataGridViewRow row in gridFacturas.Rows)
            //{
                string qry = @"Delete From PJ_RAtradius Where U_Mes = @Mes AND U_Año = @Año";

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand(qry))
                    {
                        command.Connection = connection;

                        //command.Parameters.AddWithValue("@Cliente", row.Cells[(int)Columnas.Cliente].Value.ToString());
                        //command.Parameters.AddWithValue("@Nombre", row.Cells[(int)Columnas.Nombre].Value.ToString());
                        //command.Parameters.AddWithValue("@Sucursal", row.Cells[(int)Columnas.Sucursal].Value.ToString());
                        //command.Parameters.AddWithValue("@LimiteCred", Convert.ToDecimal(row.Cells[(int)Columnas.Credito].Value));
                        //command.Parameters.AddWithValue("@LimAtradius", Convert.ToDecimal(row.Cells[(int)Columnas.CreditoAtradius].Value));
                        //command.Parameters.AddWithValue("@CondPago", row.Cells[(int)Columnas.Cond_Pago].Value.ToString());
                        //command.Parameters.AddWithValue("@Atradius", row.Cells[(int)Columnas.Atradius].Value.ToString());
                        //command.Parameters.AddWithValue("@Juridico", row.Cells[(int)Columnas.Juridico].Value.ToString());
                        //command.Parameters.AddWithValue("@VentaNeta", Convert.ToDecimal(row.Cells[(int)Columnas.Venta].Value));
                        //command.Parameters.AddWithValue("@Reporta", Convert.ToDecimal(row.Cells[(int)Columnas.Reporta].Value));
                        command.Parameters.AddWithValue("@Mes", mes);
                        command.Parameters.AddWithValue("@Año", año);

                        connection.Open();
                        command.ExecuteNonQuery();



                        //progressBar.Value++;

                        //toolPercent.Text = "Procesando: " + (progressBar.Value / progressBar.Maximum).ToString("P2");

                    }
                }
            //}
            //progressBar.Value = 0;
            //toolPercent.Text = "Listo!";

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_Atradius", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Desde", dtDesde.Value);
                    command.Parameters.AddWithValue("@Hasta", dtHasta.Value);
                    command.Parameters.AddWithValue("@CardCode", string.Empty);
                    command.Parameters.AddWithValue("@CardName", string.Empty);

                    DataTable table = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(table);

                    gridFacturas.DataSource = table;

                    decimal reporta = Convert.ToDecimal(table.Compute("SUM(Reporta)", string.Empty));

                    label5.Text = reporta.ToString("C2");

                    this.Formato(gridFacturas);
                }
            }
        }

        private void Atradius_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            dtDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            
                ExportarAExcel ex = new ExportarAExcel();
                if (ex.Exportar(gridFacturas, false))
                    MessageBox.Show("El archivo se creo correctamente.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }

        private void cbFilter_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbFilter.Checked)
                {
                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand("PJ_Atradius", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 0;

                            command.Parameters.AddWithValue("@TipoConsulta", 2);
                            command.Parameters.AddWithValue("@Desde", dtDesde.Value);
                            command.Parameters.AddWithValue("@Hasta", dtHasta.Value);
                            command.Parameters.AddWithValue("@CardCode", string.Empty);
                            command.Parameters.AddWithValue("@CardName", string.Empty);

                            DataTable table = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = command;
                            da.Fill(table);

                            gridFacturas.DataSource = table;

                            decimal reporta = Convert.ToDecimal(table.Compute("SUM(Reporta)", string.Empty));

                            label5.Text = reporta.ToString("C2");

                            this.Formato(gridFacturas);
                        }
                    }
                }
                else
                {
                    btnConsultar_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Atradius_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void Atradius_Shown(object sender, EventArgs e)
        {

            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DatosMes frmMes = new DatosMes();
                int existe = 0;

                if (DialogResult.Yes == frmMes.ShowDialog())
                {
                    
                                        

                    string qrySelect = @"Select COUNT(U_Cliente) FROM PJ_RAtradius Where U_Mes = @Mes AND U_Año = @Año";

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand(qrySelect))
                        {
                            command.Connection = connection;

                            command.Parameters.AddWithValue("@Mes", frmMes.Mes);
                            command.Parameters.AddWithValue("@Año", frmMes.Año);

                            connection.Open();

                            existe = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }

                    System.Threading.Thread.Sleep(1000);

                    if (existe == 0)
                    {
                        this.Respaldar(frmMes.Mes, frmMes.Año);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Ya existe un respando para el mes seleccionado, ¿Desea reemplazarlo?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                            this.Respaldar(frmMes.Mes, frmMes.Año);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DatosMes frmMes = new DatosMes();
                int existe = 0;

                if (DialogResult.Yes == frmMes.ShowDialog())
                {
                    string qrySelect = @"Select COUNT(U_Cliente) FROM PJ_RAtradius Where U_Mes = @Mes AND U_Año = @Año";

                    using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                    {
                        using (SqlCommand command = new SqlCommand(qrySelect))
                        {
                            command.Connection = connection;

                            command.Parameters.AddWithValue("@Mes", frmMes.Mes);
                            command.Parameters.AddWithValue("@Año", frmMes.Año);

                            connection.Open();

                            existe = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }

                    System.Threading.Thread.Sleep(1000);

                    if (existe == 0)
                    {
                        MessageBox.Show("No existe un respaldo para este mes.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("La información será eliminada permentemente, ¿Desea continuar?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                            this.Eliminar(frmMes.Mes, frmMes.Año);
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
