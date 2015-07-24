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
using System.Threading;
//using System.ThrowHelper;

namespace Ventas.KPIS
{
    public partial class KPS : Form
    {
        Clases.Logs log;

        DataTable tblIndicadores = new DataTable();
        public enum ColumnasIndicadores
        {
            Num,
            Descripcion,
            Couta,
            Venta,
            AcumuladoCouta,
            Pronostico,
            Formato
        }
        public enum ColumnasProgramas
        {
            Ds, 
            Mayoreo,
            Transporte,
            Armadores,
            Couta,
            Acumulado,
            AcumuladoCouta,
            Tendencia
        }
        public enum ColumnasSubdistribuidores
        {
            Cliente,
            Nombre,
            Linea,
            Couta,
            Acumulado,
            AcumuladoCouta,
            Tendencia
        }
        public enum ColumnasConsignas
        {
            Cliente,
            Nombre,
            Couta,
            Acumulado,
            AcumuladoCouta,
            Tendencia
        }

        public KPS()
        {
            InitializeComponent();
            progressBar.Maximum = 100;
            
        }

        public void getIndicadores()
        {
            tblIndicadores.Clear();
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_KPIS", connection))
                {
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue);
                    command.Parameters.AddWithValue("@Fecha", dtFecha.Value);

                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = command;


                    da.Fill(tblIndicadores);

                    dgvIndicadores.DataSource = tblIndicadores;
                    dgvIndicadores.Refresh();

                    progressBar.Value += 25;

                    formatoIndicadores(dgvIndicadores);
                }
            }
        }
        public void formatoIndicadores(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasIndicadores.Num].Visible = false;
            dgv.Columns[(int)ColumnasIndicadores.Formato].Visible = false;
            dgv.Columns[(int)ColumnasIndicadores.AcumuladoCouta].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumnasIndicadores.Pronostico].DefaultCellStyle.Format = "P2";
            
            dgv.Columns[(int)ColumnasIndicadores.Couta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasIndicadores.AcumuladoCouta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasIndicadores.Pronostico].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; 
            dgv.Columns[(int)ColumnasIndicadores.Venta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void getProgramaHalcon()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_KPIS", connection))
                {
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue);
                    command.Parameters.AddWithValue("@Fecha", dtFecha.Value);

                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = command;

                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    dgvProgramaHalcon.DataSource = tbl;
                    formatoProgramaHalcon(dgvProgramaHalcon);

                    progressBar.Value += 25;
                }
            }
        }
        public void formatoProgramaHalcon(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasProgramas.Ds].DefaultCellStyle.BackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.Columns[(int)ColumnasProgramas.Ds].DefaultCellStyle.ForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
            dgv.Columns[(int)ColumnasProgramas.Mayoreo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasProgramas.Transporte].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasProgramas.Armadores].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgv.Columns[(int)ColumnasProgramas.Mayoreo].Width = 75;
            dgv.Columns[(int)ColumnasProgramas.Transporte].Width = 75;
            dgv.Columns[(int)ColumnasProgramas.Armadores].Width = 75;
            dgv.Columns[(int)ColumnasProgramas.Acumulado].Width = 90;
            dgv.Columns[(int)ColumnasProgramas.Couta].Width = 90;
            dgv.Columns[(int)ColumnasProgramas.AcumuladoCouta].Width = 90;
            dgv.Columns[(int)ColumnasProgramas.Tendencia].Width = 90;

            dgv.Columns[(int)ColumnasProgramas.Couta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasProgramas.Acumulado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasProgramas.AcumuladoCouta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasProgramas.Tendencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasProgramas.Tendencia].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumnasProgramas.AcumuladoCouta].DefaultCellStyle.Format = "P2";

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void getSubdistribuidores()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_KPIS", connection))
                {
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 3);
                    command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue);
                    command.Parameters.AddWithValue("@Fecha", dtFecha.Value);

                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = command;

                    DataTable tbl = new DataTable();
                    da.Fill(tbl);
                    
                    dgvSubdstribuciones.DataSource = tbl;
                    formatoSubdistribuidores(dgvSubdstribuciones);

                    progressBar.Value += 25;
                }
            }
        }
        public void formatoSubdistribuidores(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasSubdistribuidores.Cliente].DefaultCellStyle.BackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.Columns[(int)ColumnasSubdistribuidores.Cliente].DefaultCellStyle.ForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;


            dgv.Columns[(int)ColumnasSubdistribuidores.Cliente].Width = 75;
            dgv.Columns[(int)ColumnasSubdistribuidores.Nombre].Width = 220;
            dgv.Columns[(int)ColumnasSubdistribuidores.Linea].Width = 75;
            dgv.Columns[(int)ColumnasSubdistribuidores.Couta].Width = 90;
            dgv.Columns[(int)ColumnasSubdistribuidores.Acumulado].Width = 90;
            dgv.Columns[(int)ColumnasSubdistribuidores.AcumuladoCouta].Width = 90;
            dgv.Columns[(int)ColumnasSubdistribuidores.Tendencia].Width = 90;

            dgv.Columns[(int)ColumnasSubdistribuidores.Couta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasSubdistribuidores.Acumulado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasSubdistribuidores.AcumuladoCouta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasSubdistribuidores.Tendencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasSubdistribuidores.Couta].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)ColumnasSubdistribuidores.Acumulado].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)ColumnasSubdistribuidores.Tendencia].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumnasSubdistribuidores.AcumuladoCouta].DefaultCellStyle.Format = "P2";

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void getConsignaciones()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_KPIS", connection))
                {
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 4);
                    command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue);
                    command.Parameters.AddWithValue("@Fecha", dtFecha.Value);

                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = command;

                    DataTable tbl = new DataTable();
                    da.Fill(tbl);

                    dgvConsignas.DataSource = tbl;
                    formatoConsignas(dgvConsignas);

                    progressBar.Value += 25;
                }
            }
        }
        public void formatoConsignas(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasConsignas.Cliente].DefaultCellStyle.BackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
            dgv.Columns[(int)ColumnasConsignas.Cliente].DefaultCellStyle.ForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;


            dgv.Columns[(int)ColumnasConsignas.Cliente].Width = 75;
            dgv.Columns[(int)ColumnasConsignas.Nombre].Width = 220;
            dgv.Columns[(int)ColumnasConsignas.Couta].Width = 90;
            dgv.Columns[(int)ColumnasConsignas.Acumulado].Width = 90;
            dgv.Columns[(int)ColumnasConsignas.AcumuladoCouta].Width = 90;
            dgv.Columns[(int)ColumnasConsignas.Tendencia].Width = 90;

            dgv.Columns[(int)ColumnasConsignas.Couta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasConsignas.Acumulado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasConsignas.AcumuladoCouta].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasConsignas.Tendencia].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasConsignas.Couta].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)ColumnasConsignas.Acumulado].DefaultCellStyle.Format = "C0";
            dgv.Columns[(int)ColumnasConsignas.Tendencia].DefaultCellStyle.Format = "P2";
            dgv.Columns[(int)ColumnasConsignas.AcumuladoCouta].DefaultCellStyle.Format = "P2";

            foreach (DataGridViewColumn item in dgv.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void CargarSucursales()
        {
            if (ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteFinanzas || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Administrador || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentas || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Zulma)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.Sucursales);
                command.Parameters.AddWithValue("@Sucursal", string.Empty);
                command.Parameters.AddWithValue("@SlpCode", 0);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "Selecciona una sucursal";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                cbSucursal.DataSource = table;
                cbSucursal.DisplayMember = "Nombre";
                cbSucursal.ValueMember = "Codigo";
            }
            else if (ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.GerenteVentasSucursal || ClasesSGUV.Login.Rol == (int)ClasesSGUV.Propiedades.RolesHalcoNET.Ventas)
            {
                SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 12);
                command.Parameters.AddWithValue("@Sucursal", ClasesSGUV.Login.Sucursal.Trim());
                command.Parameters.AddWithValue("@SlpCode", 0);
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                DataRow row = table.NewRow();
                row["Nombre"] = "TODAS";
                row["Codigo"] = "0";
                table.Rows.InsertAt(row, 0);

                cbSucursal.DataSource = table;
                cbSucursal.DisplayMember = "Selecciona una sucursal";
                cbSucursal.ValueMember = "Codigo";
            }
        }

        DataTable ToolTip = new DataTable();
        public void getClientes(string clasificacion, string canal)
        {
            ToolTip.Clear();
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("sp_KPIS", connection))
                {
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                    command.Parameters.AddWithValue("@Sucursal", cbSucursal.SelectedValue);
                    command.Parameters.AddWithValue("@Fecha", dtFecha.Value);

                    command.Parameters.AddWithValue("@Clasificacion", clasificacion);
                    command.Parameters.AddWithValue("@Canal", canal);

                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = command;

                    da.Fill(ToolTip);

                    
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            dgvIndicadores.DataSource = null;
            tblIndicadores.Clear();

        }

        Thread indicadores;
        Thread programasHalcon;
        Thread subdistribuciones;
        Thread consignaciones;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar.Value = 0;

                //if (indicadores.ThreadState == ThreadState.Unstarted)
                //    indicadores.Start();
                //else
                //    if (indicadores.ThreadState == ThreadState.Stopped)
                //    {
                //        indicadores = new Thread(getIndicadores);
                //        indicadores.Start();
                //    }
                //    else
                //        MessageBox.Show("Estado del proceso: " + indicadores.ThreadState);
                if (Convert.ToInt32(cbSucursal.SelectedValue) != 0)
                {
                    getIndicadores();
                    getProgramaHalcon();
                    getSubdistribuidores();
                    getConsignaciones();
                   // getClientes();
                }
            }
            
            catch (ArgumentOutOfRangeException ex1)
            {
                MessageBox.Show( ex1.Message, "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ThreadInterruptedException ex1)
            {
                MessageBox.Show(ex1.Message, "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ThreadStartException ex1)
            {
                MessageBox.Show(ex1.Message, "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ThreadStateException ex1)
            {
                MessageBox.Show(ex1.Message, "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ThreadAbortException ex1)
            {
                MessageBox.Show(ex1.Message, "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dgvIndicadores.DataSource = null;
            
            dgvIndicadores.DataSource = tblIndicadores;
        }

        private void KPS_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);

                CheckForIllegalCrossThreadCalls = false;
                indicadores = new Thread(getIndicadores);
                CargarSucursales();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Halconet", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dgvIndicadores_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        public void setFormatCell(DataGridViewCell cell)
        {
            if (cell.Value != DBNull.Value)
            {
                if (Convert.ToDecimal(cell.Value) >= 1)
                {
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.BackColor = Color.FromArgb(0, 176, 80);
                }
                else if (Convert.ToDecimal(cell.Value) < (decimal)0.85)
                {
                    cell.Style.ForeColor = Color.White;
                    cell.Style.BackColor = Color.FromArgb(255, 0, 0);
                }
                else if (Convert.ToDecimal(cell.Value) < (decimal)1)
                {
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.BackColor = Color.FromArgb(255, 255, 0);
                }
            }
        }

        private void dgvIndicadores_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // if(indicadores.ThreadState != ThreadState.Running)
            foreach (DataGridViewRow item in (sender as DataGridView).Rows)
            {
                item.Cells[(int)ColumnasIndicadores.Couta].Style.Format = item.Cells[(int)ColumnasIndicadores.Formato].Value.ToString();
                item.Cells[(int)ColumnasIndicadores.Venta].Style.Format = item.Cells[(int)ColumnasIndicadores.Formato].Value.ToString();
                item.Cells[(int)ColumnasIndicadores.Descripcion].Style.BackColor = (sender as DataGridView).ColumnHeadersDefaultCellStyle.BackColor;
                item.Cells[(int)ColumnasIndicadores.Descripcion].Style.ForeColor = (sender as DataGridView).ColumnHeadersDefaultCellStyle.ForeColor;

                if (Convert.ToInt32(item.Cells[(int)ColumnasIndicadores.Num].Value) != 5 &&
                    Convert.ToInt32(item.Cells[(int)ColumnasIndicadores.Num].Value) != 6 &&
                    Convert.ToInt32(item.Cells[(int)ColumnasIndicadores.Num].Value) != 7 &&
                    Convert.ToInt32(item.Cells[(int)ColumnasIndicadores.Num].Value) != 8)
                {
                    this.setFormatCell(item.Cells[(int)ColumnasIndicadores.AcumuladoCouta]);
                    this.setFormatCell(item.Cells[(int)ColumnasIndicadores.Pronostico]);
                }
                else
                {
                    if ((Convert.ToDecimal(item.Cells[(int)ColumnasIndicadores.AcumuladoCouta].Value==DBNull.Value ? (decimal)0 : Convert.ToDecimal(item.Cells[(int)ColumnasIndicadores.AcumuladoCouta].Value)) >= 0))
                        item.Cells[(int)ColumnasIndicadores.AcumuladoCouta].Style.ForeColor = Color.Black;
                    else
                        item.Cells[(int)ColumnasIndicadores.AcumuladoCouta].Style.ForeColor = Color.Red;

                    if ((Convert.ToDecimal(item.Cells[(int)ColumnasIndicadores.Pronostico].Value == DBNull.Value ? (decimal)0: Convert.ToDecimal(item.Cells[(int)ColumnasIndicadores.Pronostico].Value)) >= 0))
                        item.Cells[(int)ColumnasIndicadores.Pronostico].Style.ForeColor = Color.Black;
                    else
                        item.Cells[(int)ColumnasIndicadores.Pronostico].Style.ForeColor = Color.Red;
                }
            }
        }

        private void dgvProgramaHalcon_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells[(int)ColumnasProgramas.Ds].Value.ToString().Equals("Plata") || item.Cells[(int)ColumnasProgramas.Ds].Value.ToString().Equals("Platino")
                        || item.Cells[(int)ColumnasProgramas.Ds].Value.ToString().Equals("Oro") || item.Cells[(int)ColumnasProgramas.Ds].Value.ToString().Equals("Total"))
                    {
                        item.Cells[(int)ColumnasProgramas.Couta].Style.Format = "C0";
                        item.Cells[(int)ColumnasProgramas.Acumulado].Style.Format = "C0";

                        this.setFormatCell(item.Cells[(int)ColumnasProgramas.AcumuladoCouta]);
                        this.setFormatCell(item.Cells[(int)ColumnasProgramas.Tendencia]);

                        foreach (DataGridViewColumn column in (sender as DataGridView).Columns)
                        {
                            if (column.HeaderText.Equals("Mayoreo") || column.HeaderText.Equals("Transporte") || column.HeaderText.Equals("Armadores"))
                            {
                                getClientes(item.Cells[(int)ColumnasProgramas.Ds].Value.ToString(), column.HeaderText);
                                string tool = string.Empty;
                                foreach (DataRow row in ToolTip.Rows)
                                {
                                    tool += row.Field<string>("Cliente") + " - " + row.Field<string>("Nombre") + " - " + row.Field<string>("Vendedor") + "\r\n";
                                }


                                item.Cells[column.Index].ToolTipText = tool;
                            }
                        }
                    }
                    else
                    {
                        item.Cells[(int)ColumnasProgramas.Couta].Style.Format = "P2";
                        item.Cells[(int)ColumnasProgramas.Acumulado].Style.Format = "C0";
                    }


                }
            }
            catch (Exception)
            {
                
            }
        }

        private void dgvSubdstribuciones_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    this.setFormatCell(item.Cells[(int)ColumnasSubdistribuidores.AcumuladoCouta]);
                    this.setFormatCell(item.Cells[(int)ColumnasSubdistribuidores.Tendencia]);
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void dgvConsignas_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    this.setFormatCell(item.Cells[(int)ColumnasConsignas.AcumuladoCouta]);
                    this.setFormatCell(item.Cells[(int)ColumnasConsignas.Tendencia]);
                }
            }
            catch (Exception)
            {

            }
        }

        private void KPS_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void KPS_FormClosing(object sender, FormClosingEventArgs e)
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
