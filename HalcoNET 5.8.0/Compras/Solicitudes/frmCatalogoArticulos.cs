using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compras.Solicitudes
{
    public partial class frmCatalogoArticulos : Form
    {
        int Linea;
        private string _item;
        private DataTable Articulos;
        public string Item
        {
            get { return _item; }
            set { _item = value; }
        }
        private int _itmsGrpCod;

        public int ItmsGrpCod
        {
            get { return _itmsGrpCod; }
            set { _itmsGrpCod = value; }
        }

        public frmCatalogoArticulos(int _linea, string _nombre, DataTable _articulos)
        {
            try
            {
                InitializeComponent();
                Linea = _linea;
                this.Text += " " + _nombre;

                if (Linea > 0)
                {
                    Articulos = (from item in _articulos.AsEnumerable()
                                 where item.Field<Int16>("ItmsGrpCod") == Linea
                                 select item).CopyToDataTable();
                }
                else
                    Articulos = _articulos;
                 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CatalogoArticulos_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                gridArticulos.DataSource = Articulos;
                gridArticulos.Columns[0].Width = 122;
                gridArticulos.Columns[1].Width = 274;
                gridArticulos.Columns[2].Visible = false;
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _item = gridArticulos.CurrentRow.Cells[0].Value.ToString(); 

            if (!string.IsNullOrEmpty(_item))
            {
                _itmsGrpCod = Convert.ToInt32(gridArticulos.CurrentRow.Cells[2].Value); 
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable _t = (from item in Articulos.AsEnumerable()
                                where item.Field<string>("Codigo").ToLower().Contains(textBox1.Text.ToLower())
                                       // && item.Field<string>("Nombre").ToLower().Contains(textBox2.Text.ToLower())
                                select item).CopyToDataTable();

                gridArticulos.DataSource = _t;
            }
            catch (Exception )
            {
              //  MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable _t = (from item in Articulos.AsEnumerable()
                                where item.Field<string>("Nombre").ToLower().Contains(textBox2.Text.ToLower())
                                select item).CopyToDataTable();

                gridArticulos.DataSource = _t;
            }
            catch (Exception )
            {
              //  MessageBox.Show(ex.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable _t = (from item in Articulos.AsEnumerable()
                                where item.Field<string>("ItmsGrpNam").ToLower().Contains(textBox3.Text.ToLower())
                                select item).CopyToDataTable();

                gridArticulos.DataSource = _t;
            }
            catch (Exception )
            {
              //  MessageBox.Show(ex.Message);
            }
        }
    }
}
