using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.Pagos
{
    public partial class frmConfirmacion : Form
    {
        DataTable Datos;

        public enum Columnas
        {
            Banco,
            CuentaContable,
            FechaMovimiento,
            FechaCarga,
            Abono,
            Referencia,
            CuentaNI
        }

        public void Formato()
        {
            dataGridView1.Columns[(int)Columnas.Abono].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)Columnas.Abono].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public frmConfirmacion(DataTable t)
        {
            Datos = t;
            InitializeComponent();
        }

        private void FormConfirmacion_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
            dataGridView1.DataSource = Datos;
            this.Formato();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               // int SinRef = (from item in (dataGridView1.DataSource as DataTable).AsEnumerable()
                       //       where item.Field<string>("CuentaNI") == string.Empty
                       //       select item).Count();

               //if (SinRef == 0)
                //{
                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                //}
//                else
//                {
////                    MessageBox.Show(@"Algunos clientes no tienen asignada una referencia e SAP
////                                      \r\n.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    this.DialogResult = DialogResult.OK;
//                    this.Close();
//                    //this.DialogResult = DialogResult.Cancel;
//                }

            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (Convert.ToString(item.Cells[(int)Columnas.CuentaNI].Value) == string.Empty)
                    {
                        item.Cells[(int)Columnas.Referencia].Style.BackColor = Color.Red;
                        item.Cells[(int)Columnas.Referencia].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        item.Cells[(int)Columnas.Referencia].Style.BackColor = Color.White;
                        item.Cells[(int)Columnas.Referencia].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
