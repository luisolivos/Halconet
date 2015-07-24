using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas.AnalisisClientes.Controles
{
    public partial class PrincipalGerencia : UserControl
    {
        DataTable _Table = new DataTable();
        int Vendedor;
        string nombreVendedor;

        public enum Columnas
        {
            Letra,
            Descripcion,
            Clientes,
            Boton
        }

        public PrincipalGerencia()
        {
            InitializeComponent();
        }

        private void CargarVendedores()
        {
            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ))
            {
                using (SqlCommand command = new SqlCommand(@"Select SlpCode Codigo, SlpName Nombre, 
                                                             Case When Memo = '01' Then 'Puebla'
                                                              When Memo = '02' Then 'Monterrey'
                                                              When Memo = '03' Then 'Apizaco'
                                                              When Memo = '05' Then 'Cordoba'
                                                              When Memo = '06' Then 'Tepeaca'
                                                              When Memo = '16' Then 'Estado de México'
                                                              When Memo = '18' Then 'Guadalajara' End Sucursal from OSLP Where Active = 'Y'
                                                              AND SlpCode not in (41, 53)
	                                                          AND SlpCode in (select SlpCode from OCRD where CardType = 'C')
	                                                          ORDER BY Nombre", connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = command;

                    DataTable table = new DataTable();
                    da.Fill(table);

                    DataRow row = table.NewRow();
                    row["Nombre"] = string.Empty;
                    row["Codigo"] = "0";
                    table.Rows.InsertAt(row, 0);

                    clbVendedor.DataSource = table;
                    clbVendedor.DisplayMember = "Nombre";
                    clbVendedor.ValueMember = "Codigo";

                }
            }
        }

        public void Formato(DataGridView dgv)
        {
            DataGridViewButtonColumn buttonComent = new DataGridViewButtonColumn();
            {
                buttonComent.Name = "btnSeleccionar";
                buttonComent.HeaderText = "Seleccionar";
                buttonComent.Width = 100;
                buttonComent.UseColumnTextForButtonValue = true;
                buttonComent.FlatStyle = FlatStyle.Popup;
                //buttonComent.DisplayIndex = (int)ColumnasGrid.BtnComentarios;
            }
            dgv.Columns.Add(buttonComent);

            dgv.Columns[(int)Columnas.Letra].Width = 80;
            dgv.Columns[(int)Columnas.Descripcion].Width = 300;
            dgv.Columns[(int)Columnas.Clientes].Width = 80;
        }

        private void PrincipalGerencia_Load(object sender, EventArgs e)
        {
            try
            {
                this.CargarVendedores();

                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "PJ_AnalisisVentas";

                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 1);
                        command.Parameters.AddWithValue("@Pregunta", 0);
                        command.Parameters.AddWithValue("@Clasificacion", string.Empty);
                        command.Parameters.AddWithValue("@Letra", string.Empty);
                        command.Parameters.AddWithValue("@Especificacion", string.Empty);
                        command.Parameters.AddWithValue("@Linea", 0);
                        command.Parameters.AddWithValue("@Cliente", string.Empty);

                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                        command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                        command.Parameters.AddWithValue("@Nombre", string.Empty);

                        command.CommandTimeout = 0;

                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(_Table);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable DataSource(int _vendedor, string _clasificacion)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_AnalisisVentas";

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 8);
                    command.Parameters.AddWithValue("@Pregunta", _vendedor);
                    command.Parameters.AddWithValue("@Clasificacion", _clasificacion);
                    command.Parameters.AddWithValue("@Letra", string.Empty);
                    command.Parameters.AddWithValue("@Especificacion", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);

                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                    command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                    command.Parameters.AddWithValue("@Nombre", string.Empty);

                    command.CommandTimeout = 0;

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(table);

                    return table;      
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dgvNo.DataSource = null;
                dgvNo.Columns.Clear();

                dgvSi.DataSource = null;
                dgvSi.Columns.Clear();

                Vendedor = Convert.ToInt32(clbVendedor.SelectedValue);
                nombreVendedor = clbVendedor.Text;

                dgvNo.DataSource = this.DataSource(Vendedor, "N");
                dgvSi.DataSource = this.DataSource(Vendedor, "S");

                this.Formato(dgvSi);
                this.Formato(dgvNo);
            }
            catch (Exception)
            {
            }
        }

        private void dgvNo_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex].Name == "btnSeleccionar" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBoton = (sender as DataGridView).Rows[e.RowIndex].Cells["btnSeleccionar"] as DataGridViewButtonCell;
                Icon icoAtomico;



                icoAtomico = Properties.Resources.miniarrow_right_blue;
                e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left, e.CellBounds.Top);



                (sender as DataGridView).Rows[e.RowIndex].Height = icoAtomico.Height + 2;
                (sender as DataGridView).Columns[e.ColumnIndex].Width = icoAtomico.Width + 2;

                e.Handled = true;
            }
        }

        private void dgvNo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && (sender as DataGridView).Columns[e.ColumnIndex].Name == "btnSeleccionar" && e.RowIndex >= 0)
            {
                if ((sender as DataGridView).Name == "dgvNo")
                {
                    string letra = (sender as DataGridView).Rows[e.RowIndex].Cells["Letra"].Value.ToString();
                    string respuesta = (sender as DataGridView).Rows[e.RowIndex].Cells["Descripción"].Value.ToString();
                    
                    AnalisisClientes.Controles.GerenciaClientes ctlGerCli = new GerenciaClientes(Vendedor, "N", letra, respuesta, nombreVendedor);

                    ctlGerCli.Dock = DockStyle.Fill;

                    this.Parent.Controls.Add(ctlGerCli);
                    ctlGerCli.BringToFront();

                }

                if ((sender as DataGridView).Name == "dgvSi")
                {
                    List<string> Letras = new List<string>();
                    Letras.Add("A");
                    Letras.Add("E");

                    string letra = (sender as DataGridView).Rows[e.RowIndex].Cells["Letra"].Value.ToString().Trim();
                    string respuesta = (sender as DataGridView).Rows[e.RowIndex].Cells["Descripción"].Value.ToString();

                    if (!Letras.Contains(letra))
                    {
                        AnalisisClientes.Controles.GerenciaClientes ctlGerCli = new GerenciaClientes(Vendedor, "S", letra, respuesta, nombreVendedor);

                        ctlGerCli.Dock = DockStyle.Fill;

                        this.Parent.Controls.Add(ctlGerCli);
                        ctlGerCli.BringToFront();
                    }

                    else if (letra == "A")
                    {
                        AnalisisClientes.Controles.Gerencia.SI_RespA ctlGerCli = new AnalisisClientes.Controles.Gerencia.SI_RespA(Vendedor, nombreVendedor);

                        ctlGerCli.Dock = DockStyle.Fill;

                        this.Parent.Controls.Add(ctlGerCli);
                        ctlGerCli.BringToFront();
                    }
                    else if (letra == "E")
                    {
                        AnalisisClientes.Controles.Gerencia.SI_RespE ctlGerCli = new AnalisisClientes.Controles.Gerencia.SI_RespE(Vendedor, nombreVendedor, respuesta);

                        ctlGerCli.Dock = DockStyle.Fill;

                        this.Parent.Controls.Add(ctlGerCli);
                        ctlGerCli.BringToFront();
                    }
                }
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            this.Visible = false;

            this.Parent.Controls.Remove(this);
        }
    }
}
