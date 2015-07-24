using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ventas.AnalisisClientes.Controles
{
    public partial class Pregunta1 : UserControl
    {
        string Cliente;
        string Nombre;
        Font _Fuente;

        DataTable _Respuestas = new DataTable();
        DataRow _Row;


        public Pregunta1(string _cliente, string _nombre)
        {
            InitializeComponent();

            Cliente = _cliente;
            Nombre = _nombre;
            _Fuente = rbA.Font;

            _Respuestas.Columns.Add("U_Pregunta", typeof(Int32));
            _Respuestas.Columns.Add("U_Clasificacion", typeof(string));
            _Respuestas.Columns.Add("U_Respuesta", typeof(string));
            _Respuestas.Columns.Add("U_Especificacion", typeof(string));
            _Respuestas.Columns.Add("U_Cliente", typeof(string));
            _Respuestas.Columns.Add("U_Linea", typeof(string));

            _Row = _Respuestas.NewRow();
            _Respuestas.Rows.Add(_Row);
        }

        private void Guardar(int _pregunta, string _clasificacion)
        {
            string letra = string.Empty;

            if(rbA.Checked)
                letra = "A";
            if(rbB.Checked)
                letra = "B";

            _Row["U_Pregunta"] = _pregunta;
            _Row["U_Clasificacion"] = _clasificacion;
            _Row["U_Respuesta"] = letra;
            _Row["U_Especificacion"] = txtEspecificar.Text;
            _Row["U_Cliente"] = Clases.Contantes.Cliente;
            _Row["U_Linea"] = Clases.Contantes.Linea;

        }

        private void Pregunta1_Load(object sender, EventArgs e)
        {
            this.Focus();
            lblCliente.Text = "Cliente: " + Nombre;

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            _Respuestas.Rows.Remove(_Row);

            this.Visible = false;

            this.Parent.Controls.Remove(this);
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (rbA.Checked || rbB.Checked)
            {
                string clasificacion = string.Empty;
                if (rbA.Checked == true)
                    clasificacion = "S";
                else if (rbB.Checked == true)
                {
                    clasificacion = "N";
                }

                if (!rbO.Checked && txtEspecificar.Text.Trim() == "")
                {
                    //GUardar
                    this.Guardar(1, clasificacion);
                    Controles.Pregunta2 p2 = new Pregunta2(Cliente, Nombre, 2, clasificacion, _Respuestas);
                    p2.Dock = DockStyle.Fill;

                    this.Parent.Controls.Add(p2);
                    p2.BringToFront();
                }
                else if (rbO.Checked && txtEspecificar.Text.Trim() != "")
                {
                    //Guardar
                    this.Guardar(1, clasificacion);
                    Controles.Pregunta2 p2 = new Pregunta2(Cliente, Nombre, 2, clasificacion, _Respuestas);
                    p2.Dock = DockStyle.Fill;

                    this.Parent.Controls.Add(p2);
                    p2.BringToFront();
                }
                else
                    MessageBox.Show("Debe el campo especificar no puede estar vacio.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void rb_Click(object sender, EventArgs e)
        {
            foreach (Control item in gpPregunta.Controls)
            {
                if (item is RadioButton)
                {
                    item.Font = _Fuente;
                }
            }

            (sender as Control).Font = new Font(_Fuente.FontFamily, _Fuente.Size, FontStyle.Bold);
        }

        private void form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                toolStripStatusLabel1_Click(sender, e);
            }
        }
    }
}
