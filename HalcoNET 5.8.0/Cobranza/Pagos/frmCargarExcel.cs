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
using System.Data.OleDb;

namespace Cobranza.Pagos
{
    public partial class frmCargarExcel : Form
    {
        DataTable TblDatos = new DataTable();
        Clases.Logs log;
        DataTable Datos = new DataTable();


        public frmCargarExcel()
        {
            InitializeComponent();
        }

        public void CargarCtasBancos()
        {
            using (SqlConnection connection =new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Pagos";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                    command.Parameters.AddWithValue("@Banco", string.Empty);
                    command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                    command.Parameters.AddWithValue("@Abono", decimal.Zero);
                    command.Parameters.AddWithValue("@Referencia", string.Empty);
                    command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", decimal.Zero);
                    command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                    command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                    command.Parameters.AddWithValue("@Code", 0);

                    SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);
                    Datos = table.Copy();

                    cbBanco.DataSource = table;
                    cbBanco.ValueMember = "Codigo";
                    cbBanco.DisplayMember = "Nombre";
                }                
            }
        }

        public DataTable CargarReferencias()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "PJ_Pagos";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 16);
                    command.Parameters.AddWithValue("@Sucursal", string.Empty);
                    command.Parameters.AddWithValue("@FechaDesde", DateTime.Now);
                    command.Parameters.AddWithValue("@FechaHasta", DateTime.Now);

                    command.Parameters.AddWithValue("@Banco", string.Empty);
                    command.Parameters.AddWithValue("@CuentaContable", string.Empty);
                    command.Parameters.AddWithValue("@Abono", decimal.Zero);
                    command.Parameters.AddWithValue("@Referencia", string.Empty);
                    command.Parameters.AddWithValue("@CuentaNI", string.Empty);

                    command.Parameters.AddWithValue("@Cliente", string.Empty);
                    command.Parameters.AddWithValue("@Cantidad", decimal.Zero);
                    command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                    command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                    command.Parameters.AddWithValue("@Code", 0);

                    SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);

                    return table;
                }
            }
        }

        public void GuardarMovimientos()
        {
            string mensaje = "";
            int line = 1;

            foreach (DataRow item in TblDatos.Rows)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;

                    using (SqlCommand command = new SqlCommand())
                    {
                        connection.Open();
                        command.Connection = connection;
                        command.CommandText = "PJ_Pagos";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@TipoConsulta", 2);
                        command.Parameters.AddWithValue("@Sucursal", string.Empty);
                        command.Parameters.AddWithValue("@FechaDesde", item.Field<DateTime>("FechaMovimiento"));
                        command.Parameters.AddWithValue("@FechaHasta", item.Field<DateTime>("FechaCarga"));

                        command.Parameters.AddWithValue("@Banco", item.Field<string>("Banco"));
                        command.Parameters.AddWithValue("@CuentaContable", item.Field<string>("CuentaContable"));
                        command.Parameters.AddWithValue("@Abono", item.Field<decimal?>("Abono") == null ? decimal.Zero : item.Field<decimal?>("Abono"));
                        command.Parameters.AddWithValue("@Referencia", item.Field<string>("Referencia") == null ? string.Empty : item.Field<string>("Referencia"));
                        command.Parameters.AddWithValue("@CuentaNI", item.Field<string>("CuentaNI") == null ? string.Empty : item.Field<string>("CuentaNI"));

                        command.Parameters.AddWithValue("@Cliente", string.Empty);
                        command.Parameters.AddWithValue("@Cantidad", decimal.Zero);

                        command.Parameters.AddWithValue("@NCPP", decimal.Zero);
                        command.Parameters.AddWithValue("@NCPE", decimal.Zero);

                        command.Parameters.AddWithValue("@Code", 0);
                        command.Parameters.AddWithValue("@U_Tipo_Pago", item.Field<string>("U_TIPO_PAGO"));

                        SqlParameter parameter = new SqlParameter("@Message", SqlDbType.VarChar, 100);
                        parameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(parameter);

                        command.ExecuteNonQuery();
                        
                        mensaje = Convert.ToString(command.Parameters["@Message"].Value);
                        if (!mensaje.Equals("Registro exitoso!"))
                        {
                            break;
                        }
                        line++;
                        toolProgress.PerformStep();
                        decimal porcentaje = ((decimal)toolProgress.Value) / (decimal)toolProgress.Maximum;
                        statuslbl.Text = "Fase 1: Cargar template de bancos - " + porcentaje.ToString("P2");
                    }
                }
            }
            if (mensaje.Equals("Registro exitoso!"))
            {
                pictureBox1.Visible = false;
                toolProgress.Value = 0;
               // button1.Enabled = true;
                statuslbl.Text = "Listo.";
                MessageBox.Show(mensaje, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show(mensaje + "\r\n Linea: " + line, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CargarMovimientos_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
            this.MaximizeBox = false;
            pictureBox1.Visible = false;
            toolProgress.Step = 1;
            try
            {
                this.CargarCtasBancos();
            }
            catch (Exception)
            {
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
           

            if (!string.IsNullOrEmpty(cbBanco.SelectedValue.ToString().Trim()))
            {
                TblDatos.Clear();
                try
                {
                    OpenFileDialog form = new OpenFileDialog();
                    //form.InitialDirectory = @"c:\";
                    form.Filter = "Excel (*.xls)|*.xls";

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        
                        string connectionString = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", form.FileName);
                        string query = String.Format("select * from [{0}$]", "Hoja1");

                        OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, connectionString);
                        DataSet dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);

                        DataTable myTable = dataSet.Tables[0];

                        if (myTable.Columns.Count == 5)
                        {
                            /// validar referencias
                           
                            DataTable TblReferencias = new DataTable();
                            TblReferencias = this.CargarReferencias();




                            var data = from item in myTable.AsEnumerable()
                                       select new
                                       {
                                           Banco = txtNombrBanco.Text,
                                           CuentaContable = Convert.ToString(cbBanco.SelectedValue),
                                           FechaMovimiento = item.Field<DateTime>("FECHAMOVIMIENTO"),
                                           FechaCarga = DateTime.Now.Date,
                                           Abono = Convert.ToDecimal(item.Field<string>("ABONO")),
                                           Referencia = item.Field<string>("REFERENCIA"),
                                           CuentaNI = item.Field<string>("Cuenta Contable"),
                                           U_TIPO_PAGO = item.Field<string>("Metodo de Pago") 
                                       };

                            TblDatos = ListConverter.ToDataTable(data.ToList());
                            foreach (DataRow item in TblDatos.AsEnumerable())
                            {
                                string __aux = string.Empty;
                                if (item.Field<string>("Referencia").Length > 25)
                                    __aux = item.Field<string>("Referencia").ToUpper().Substring(0, 25);

                                foreach (DataRow reference in TblReferencias.AsEnumerable())
                                {
                                    if (item.Field<string>("Referencia").Split(' ')[0].Trim().Equals(reference.Field<string>("Referencia").Trim()))
                                    {
                                        item.SetField<string>("CuentaNI", reference.Field<string>("CuentaContableNI"));
                                        break;
                                    }
                                    else if (__aux.Length == 25)
                                    {
                                        string[] _array = item.Field<string>("Referencia").Split(' ');
                                        if (_array.Length > 1)
                                            if (("CV_" + _array[_array.Length - 1].Trim()).Equals(reference.Field<string>("Referencia").Trim()))
                                            {
                                                item.SetField<string>("CuentaNI", reference.Field<string>("CuentaContableNI"));
                                                break;
                                            }
                                            else
                                            {
                                                if (item.Field<string>("Referencia").Trim().Equals(reference.Field<string>("Referencia").Trim()))
                                                {
                                                    item.SetField<string>("CuentaNI", reference.Field<string>("CuentaContableNI"));
                                                    break;
                                                }
                                            }
                                    }
                                    else
                                    {
                                        item.SetField<string>("CuentaNI", string.Empty);
                                    }
                                }
                            }
                           
                            //var Left = (from mov in data.AsEnumerable()
                            //            join referencia in TblReferencias.AsEnumerable()
                            //            on mov.Referencia.ToString().Split(' ')[0].Trim() equals referencia.Field<string>("Referencia").Trim() into JoinReferencias
                            //            from referencia in JoinReferencias.DefaultIfEmpty()
                            //            select new
                            //            {
                            //                Banco = mov.Banco,
                            //                CuentaContable = mov.CuentaContable,
                            //                FechaMovimiento = mov.FechaMovimiento,
                            //                FechaCarga = mov.FechaCarga,
                            //                Abono = mov.Abono,
                            //                Referencia = mov.Referencia,
                            //                CuentaNI = referencia != null ? referencia.Field<string>("CuentaContableNI") : string.Empty
                            //                //---Cliente = referencia != null ? referencia.Field<string>("CardCode") : string.Empty
                            //            });

                           // TblDatos = ListConverter.ToDataTable(Left.ToList());
                            toolProgress.Maximum = TblDatos.Rows.Count;
                            DialogResult dialogResult = new frmConfirmacion(TblDatos).ShowDialog();

                            if (dialogResult == DialogResult.OK)
                            {
                                //button1.Enabled = false;
                                pictureBox1.Visible = true;
                                /*C A R G A R T E M P L A T E     D E    B A N C O S*/
                                System.Threading.Thread proceso1 = new System.Threading.Thread(GuardarMovimientos);
                                proceso1.Start();
                                proceso1.Join();
                                statuslbl.Text = "Preparando fase 2...";
                                System.Threading.Thread.Sleep(1000);
                                /*G E N E R A R    T E  M P L A T E -------- N O    I D E N T I F I C A D O S*/
                                //System.Threading.Thread proceso2 = new System.Threading.Thread(GenerarTemplate);
                                //proceso2.Start();
                                //proceso2.Join();
                                statuslbl.Text = "Fase 2: Generacion del template..";
                                frmTempleteNI forms = new frmTempleteNI();
                                forms.TempletePagos_Load(sender, e);
                                forms.cbBanco.Text = this.cbBanco.Text;
                                forms.dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                forms.dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                                forms.MdiParent = this.MdiParent;
                                forms.Show();

                                statuslbl.Text = "Listo";
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                //do something else
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error: El número de columnas en el archivo de excel no coincide con las solicitadas.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   // button1.Enabled = true;
                    toolProgress.Value = 0;
                    pictureBox1.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Error: Debe seleccionar un banco", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbBanco_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string banco = (from item in
                                Datos.AsEnumerable()
                            where item.Field<string>("Codigo").Equals(cbBanco.SelectedValue)
                            select item.Field<string>("Banco")).FirstOrDefault();
            txtNombrBanco.Text = banco;

            if (!string.IsNullOrEmpty(banco))
            {
               // button1.Enabled = true;
            }
        }

        private void CargarMovimientos_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
            }
               
        }

        private void CargarMovimientos_Shown(object sender, EventArgs e)
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
