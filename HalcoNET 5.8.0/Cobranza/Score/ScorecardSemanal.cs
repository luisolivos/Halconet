using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Cobranza
{
    public partial class ScorecardSemanal : Form
    {
        public string Sucursal;
        public DateTime FechaInicial;
        public DateTime FechaFinal;
        public string NombreSucursal;
        Clases.Logs log;

        public ScorecardSemanal(string _sucursal, DateTime _fecha, DateTime _fechaFinal)
        {
            InitializeComponent();
            label1.Text = "Sucursal: " + _sucursal;
            Sucursal = getMemo(_sucursal);
            FechaInicial = _fecha;
            FechaFinal = _fechaFinal;
            NombreSucursal = _sucursal;
        }

        public string getMemo(string _sucursal)
        {
            switch (_sucursal)
            {
                case "Apizaco":
                    return "03";
                case "Cordoba":
                    return "05";
                case "EdoMex":
                    return "16";
                case "Guadalajara":
                    return "18";
                case "Monterrey":
                    return "02";
                case "Puebla":
                    return "01";
                case "Tepeaca":
                    return "06";
                default: return "00";
            }
        }

        private DataTable CargarJefesCobranza()
        {
            SqlCommand command = new SqlCommand("PJ_ConsultasVariasSGUV", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)Constantes.ConsultasVariasPJ.JefesCobranza);
            command.Parameters.AddWithValue("@Sucursal", string.Empty);
            command.Parameters.AddWithValue("@SlpCode", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return (from item in table.AsEnumerable() where item.Field<string>("Codigo") == Sucursal select item).CopyToDataTable();
        }

        private void ScorecardSemanal_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                DataTable JefasCobranza = CargarJefesCobranza();

                foreach (DataRow item in JefasCobranza.Rows)
                {
                    String _jefa = item.Field<String>("Nombre");
                    SqlCommand command = new SqlCommand("PJ_ScoreCardCobranza", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 5);
                    command.Parameters.AddWithValue("@Sucursales", string.Empty);
                    command.Parameters.AddWithValue("@JefasCobranza", _jefa);
                    command.Parameters.AddWithValue("@FechaInicial", FechaInicial);
                    command.Parameters.AddWithValue("@FechaFinal", FechaFinal);
                    command.Parameters.AddWithValue("@Sucursal", NombreSucursal);
                    command.CommandTimeout = 0;

                    DataTable table = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(table);

                    this.Controls.Add(new DataGridView()
                    {
                        DataSource = table,
                        BackgroundColor = gridScore.BackgroundColor,
                        BorderStyle = gridScore.BorderStyle,
                        CellBorderStyle = gridScore.CellBorderStyle,
                        ReadOnly = true,
                        AllowUserToAddRows = false,
                        Width = 550,
                        Name = _jefa,
                        EnableHeadersVisualStyles = false,
                        ColumnHeadersHeight = 35,
                        ColumnHeadersDefaultCellStyle = gridScore.ColumnHeadersDefaultCellStyle
                    });

                    foreach (Control dg in this.Controls)
                    {
                        if (dg is DataGridView)
                        {
                            if (_jefa == dg.Name)
                            {
                                DataGridView dgv = dg as DataGridView;
                                dgv.Columns[0].HeaderText = _jefa;

                                dgv.Columns[0].Width = 100;
                                dgv.Columns[1].Width = 100;
                                dgv.Columns[2].Width = 100;
                                dgv.Columns[3].Width = 100;
                                dgv.Columns[4].Width = 100;

                                dgv.Columns[1].DefaultCellStyle.Format = "C0";
                                dgv.Columns[2].DefaultCellStyle.Format = "C0";
                                dgv.Columns[3].DefaultCellStyle.Format = "C0";
                                dgv.Columns[4].DefaultCellStyle.Format = "P0";

                                dgv.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgv.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgv.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgv.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            }
                        }
                    }
                }

                DataGridView anterior = null;
                int cont = 0;
                foreach (Control item in this.Controls)
                {
                    if (item is DataGridView && item.Visible == true)
                    {
                        if (anterior == null && item.Visible == true)
                        {
                            item.Location = new Point(9, 35);
                        }
                        else// if (cont < 2)
                        {
                            item.Location = new Point(9, 35 + ((10 + anterior.Height) * cont));
                        }

                        anterior = item as DataGridView;
                        cont++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ScorecardSemanal_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void ScorecardSemanal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
