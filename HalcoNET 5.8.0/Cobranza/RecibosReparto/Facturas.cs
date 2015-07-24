using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cobranza.RecibosReparto
{
    public partial class Facturas : Form
    {
        DataTable Datos = new DataTable();
        private string _factura;

        public string Factura
        {
            get { return _factura; }
            set { _factura = value; }
        }

        private enum Columnas
        {
            Enviar,
            Clave,
            Cliente,
            FechaFactura,
            FechaVto,
            Actividad,
            Factura,
            Facturado,
            Saldo
        }


        public Facturas(DataTable table)
        {
            Datos = table;
            InitializeComponent();
        }

        private void FormatoGrid()
        {
            dataGridView1.Columns[(int)Columnas.Clave].Visible = true;
            dataGridView1.Columns[(int)Columnas.Actividad].Visible = false;
            dataGridView1.Columns[(int)Columnas.Cliente].Visible = false;
            dataGridView1.Columns[(int)Columnas.Factura].Visible = true;
            dataGridView1.Columns[(int)Columnas.FechaFactura].Visible = true;
            dataGridView1.Columns[(int)Columnas.FechaVto].Visible = true;
            dataGridView1.Columns[(int)Columnas.Facturado].Visible = true;
            dataGridView1.Columns[(int)Columnas.Saldo].Visible = true;
            dataGridView1.Columns[(int)Columnas.Enviar].Visible = false;

            dataGridView1.Columns[(int)Columnas.Clave].Width = 70;
            dataGridView1.Columns[(int)Columnas.Actividad].Width = 70;
            dataGridView1.Columns[(int)Columnas.Cliente].Width = 250;
            dataGridView1.Columns[(int)Columnas.Factura].Width = 100;
            dataGridView1.Columns[(int)Columnas.FechaFactura].Width = 100;
            dataGridView1.Columns[(int)Columnas.FechaVto].Width = 100;
            dataGridView1.Columns[(int)Columnas.Facturado].Width = 100;
            dataGridView1.Columns[(int)Columnas.Saldo].Width = 100;
            dataGridView1.Columns[(int)Columnas.Enviar].Width = 60;

            dataGridView1.Columns[(int)Columnas.Clave].ReadOnly = true;
            dataGridView1.Columns[(int)Columnas.Cliente].ReadOnly = true;
            dataGridView1.Columns[(int)Columnas.Factura].ReadOnly = true;
            dataGridView1.Columns[(int)Columnas.Factura].ReadOnly = true;
            dataGridView1.Columns[(int)Columnas.FechaVto].ReadOnly = true;
            dataGridView1.Columns[(int)Columnas.Facturado].ReadOnly = true;
            dataGridView1.Columns[(int)Columnas.Saldo].ReadOnly = true;

            dataGridView1.Columns[(int)Columnas.Clave].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[(int)Columnas.Cliente].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.Columns[(int)Columnas.Factura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)Columnas.FechaFactura].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)Columnas.FechaVto].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)Columnas.Facturado].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[(int)Columnas.Saldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[(int)Columnas.Facturado].DefaultCellStyle.Format = "C2";
            dataGridView1.Columns[(int)Columnas.Saldo].DefaultCellStyle.Format = "C2";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void Facturas_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;
                //dataGridView1.DataSource = Datos;
            }
            catch (Exception)
            {
                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Factura = string.Empty;
                var query = from item in Datos.AsEnumerable()
                            where item.Field<string>("Factura").Equals(textBox1.Text)
                            select item;

                dataGridView1.DataSource = query.CopyToDataTable();
                Factura = textBox1.Text;
                this.FormatoGrid();
            }
            catch (Exception)
            {
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button2_Click(sender, e);
            }
        }
    }
}
