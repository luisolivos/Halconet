using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Presupuesto
{
    public partial class Presupuesto : Form
    {
        #region PARAMETROS
        
        Clases.Comun_Presupuesto objPresup = new Clases.Comun_Presupuesto();
        Clases.Datos_Presupuesto objDatosPresup = new Clases.Datos_Presupuesto();
        public enum Columnas
        {
            NR, 
            Proyecto,
            Enero,
            Febreo,
            Marzo,
            Abril,
            Mayo,
            Junio,
            Julio,
            Agosto,
            Septiembre,
            Octubre,
            Noviembre,
            Diciembre,
            Save
        }
        #endregion 

        public Presupuesto()
        {
            InitializeComponent();
        }

        #region METODOS
            
        private void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)Columnas.NR].Width = 90;
            dgv.Columns[(int)Columnas.Proyecto].Width = 90;
            dgv.Columns[(int)Columnas.Enero].Width = 90;
            dgv.Columns[(int)Columnas.Febreo].Width = 90;
            dgv.Columns[(int)Columnas.Marzo].Width = 90;
            dgv.Columns[(int)Columnas.Abril].Width = 90;
            dgv.Columns[(int)Columnas.Mayo].Width = 90;
            dgv.Columns[(int)Columnas.Junio].Width = 90;
            dgv.Columns[(int)Columnas.Julio].Width = 90;
            dgv.Columns[(int)Columnas.Agosto].Width = 90;
            dgv.Columns[(int)Columnas.Septiembre].Width = 90;
            dgv.Columns[(int)Columnas.Octubre].Width = 90;
            dgv.Columns[(int)Columnas.Noviembre].Width = 90;
            dgv.Columns[(int)Columnas.Diciembre].Width = 90;
            dgv.Columns[(int)Columnas.Save].Visible = false;

            dgv.Columns[(int)Columnas.Enero].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Febreo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Marzo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Abril].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Mayo].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Junio].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Julio].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Agosto].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Septiembre].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Octubre].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Noviembre].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)Columnas.Diciembre].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)Columnas.Enero].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Febreo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Marzo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Abril].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Mayo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Junio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Julio].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Agosto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Septiembre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Octubre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Noviembre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)Columnas.Diciembre].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        } 
        
        #endregion

        #region EVENTOS

        private void dgvPresupuesto_Resize(object sender, EventArgs e)
        {
            Point p = new Point((Convert.ToInt32((sender as Control).Width / 2) - (lblTitulo.Width / 2)), lblTitulo.Location.Y);

            lblTitulo.Location = p;
        }

        private void Presupuesto_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                Clases.FillControl fill = new Clases.FillControl();
                fill.FillDataSource(cbCuenta, string.Empty, 1);
                fill.FillDataSource(cbNR, "TODAS", 2);
                cbCuenta.SelectedIndex = 0;

                txtAño.Text = DateTime.Now.Year.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbCuenta.SelectedValue.ToString() != "0")
                {
                    objPresup.TipoConsulta = 1;
                    objPresup.Cuenta = cbCuenta.SelectedValue.ToString();
                    objPresup.NR = cbNR.SelectedValue.ToString();
                    objPresup.Año = Convert.ToInt32(txtAño.Text);

                    dgvPresupuesto.DataSource = objDatosPresup.GetData(objPresup);

                    this.Formato(dgvPresupuesto);
                }
                else
                {
                    MessageBox.Show("Debe seleccionar una cuenta!!!", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int Column = 2;
                DataTable table = new DataTable();
                string _mensaje = string.Empty;

                objPresup.TipoConsulta = 2;
                foreach (DataGridViewRow row in dgvPresupuesto.Rows)
                {
                    if ((row.Cells[(int)Columnas.Proyecto].Value == null ? string.Empty : row.Cells[(int)Columnas.Proyecto].Value.ToString().Trim()) != string.Empty)
                        if (row.Cells[(int)Columnas.Save].Value.Equals("Y"))
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                if (cell.ColumnIndex >= Column && cell.ColumnIndex != (int)Columnas.Save)
                                {

                                    objPresup.Mes = cell.ColumnIndex - 1;
                                    objPresup.Original = Convert.ToDecimal(row.Cells[cell.ColumnIndex].Value);
                                    objPresup.Presupuesto = Convert.ToDecimal(row.Cells[cell.ColumnIndex].Value);
                                    objPresup.Proyecto = Convert.ToString(row.Cells[(int)Columnas.Proyecto].Value);
                                    objPresup.NR = Convert.ToString(row.Cells[(int)Columnas.NR].Value);

                                    table = objDatosPresup.GetData(objPresup);

                                    _mensaje = (from it in table.AsEnumerable()
                                                select it.Field<string>("Mensaje")).FirstOrDefault();

                                    if (!_mensaje.Trim().Equals(string.Empty))
                                    {
                                        row.Cells[(int)Columnas.Proyecto].Style.BackColor = Color.Red;
                                        row.Cells[(int)Columnas.Proyecto].Style.ForeColor = Color.White;
                                        row.Cells[(int)Columnas.NR].Style.BackColor = Color.Red;
                                        row.Cells[(int)Columnas.NR].Style.ForeColor = Color.White;
                                        MessageBox.Show("Error: " + _mensaje, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        break;
                                    }
                                    else
                                    {
                                        row.Cells[(int)Columnas.Proyecto].Style.BackColor = Color.White;
                                        row.Cells[(int)Columnas.Proyecto].Style.ForeColor = Color.Black;
                                        row.Cells[(int)Columnas.NR].Style.BackColor = Color.White;
                                        row.Cells[(int)Columnas.NR].Style.ForeColor = Color.Black;
                                    }

                                }
                            }
                }
                if(_mensaje.Trim().Equals(string.Empty))
                    MessageBox.Show("Registro exitoso.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvPresupuesto_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[(int)Columnas.NR].Value = cbNR.SelectedValue.ToString() == "0" ? string.Empty : cbNR.SelectedValue.ToString();

                e.Row.Cells[(int)Columnas.Enero].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Febreo].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Marzo].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Abril].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Mayo].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Junio].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Julio].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Agosto].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Septiembre].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Octubre].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Noviembre].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Diciembre].Value = decimal.Zero;
                e.Row.Cells[(int)Columnas.Save].Value = "Y";
            }
            catch (Exception)
            {
            }
        }

        private void dgvPresupuesto_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != (int)Columnas.Save)
            {
                DataGridViewRow row = (sender as DataGridView).Rows[e.RowIndex];

                row.Cells[(int)Columnas.Save].Value = "Y";
            }
        }

        #endregion
    }
}
