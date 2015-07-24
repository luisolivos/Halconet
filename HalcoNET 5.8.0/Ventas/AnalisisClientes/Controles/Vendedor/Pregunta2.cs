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
    public partial class Pregunta2 : UserControl
    {
        string Cliente;
        string Nombre;
        Font _Fuente;
        DataTable _Respuestas;
        DataRow _Row;
        /**/
        int Pregunta;
        string Clasificacion;
        /**/


        public Pregunta2(string _cliente, string _nombre, int _numero, string _clasificacion, DataTable _table)
        {
            InitializeComponent();

            Cliente = _cliente;
            Nombre = _nombre;
            Pregunta = _numero;
            Clasificacion = _clasificacion;
            _Fuente = rbA.Font;
            _Respuestas = _table;
            _Row = _Respuestas.NewRow();
            _Respuestas.Rows.Add(_Row);
        }

        private void Guardar(int _pregunta, string _clasificacion)
        {
            string letra = string.Empty;

            if (rbA.Checked)
                letra = "A";
            if (rbB.Checked)
                letra = "B";
            if (rbC.Checked)
                letra = "C";
            if (rbD.Checked)
                letra = "D";
            if (rbE.Checked)
                letra = "E";
            if (rbF.Checked)
                letra = "F";
            if (rbO.Checked)
            {
                letra = "O";
                _Row["U_Especificacion"] = txtEspecificar.Text;
            }
            else
            {
                _Row["U_Especificacion"] = string.Empty;
            }

            _Row["U_Pregunta"] = _pregunta;
            _Row["U_Clasificacion"] = _clasificacion;
            _Row["U_Respuesta"] = letra;
            
            _Row["U_Cliente"] = Clases.Contantes.Cliente;
            _Row["U_Linea"] = Clases.Contantes.Linea;

        }

        private void Finalizar()
        {
            foreach (DataRow item in _Respuestas.Rows)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = "PJ_AnalisisVentas";

                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TipoConsulta", 3);
                        command.Parameters.AddWithValue("@Pregunta", item.Field<Int32>("U_Pregunta"));
                        command.Parameters.AddWithValue("@Clasificacion", item.Field<string>("U_clasificacion"));
                        command.Parameters.AddWithValue("@Letra", item.Field<string>("U_Respuesta"));
                        command.Parameters.AddWithValue("@Especificacion", item.Field<string>("U_Especificacion"));
                        command.Parameters.AddWithValue("@Linea", item.Field<string>("U_Linea"));
                        command.Parameters.AddWithValue("@Cliente", item.Field<string>("U_Cliente"));

                        command.Parameters.AddWithValue("@Articulo", string.Empty);
                        command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                        command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                        command.Parameters.AddWithValue("@Nombre", string.Empty);

                        command.CommandTimeout = 0;

                        connection.Open();
                        command.ExecuteNonQuery();

                    }
                }
            }

        }

        private void Pregunta1_Load(object sender, EventArgs e)
        {
            lblCliente.Text = Nombre;

            this.Focus();

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand("PJ_AnalisisVentas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TipoConsulta", 2);
                    command.Parameters.AddWithValue("@Pregunta", Pregunta);
                    command.Parameters.AddWithValue("@Clasificacion", Clasificacion);
                    command.Parameters.AddWithValue("@Letra", string.Empty);
                    command.Parameters.AddWithValue("@Especificacion", string.Empty);
                    command.Parameters.AddWithValue("@Linea", 0);
                    command.Parameters.AddWithValue("@Cliente", string.Empty);

                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@PrecioPJ", decimal.Zero);
                    command.Parameters.AddWithValue("@PrecioComp", decimal.Zero);
                    command.Parameters.AddWithValue("@Nombre", string.Empty);


                    DataTable tblPregunta = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = command;
                    da.Fill(tblPregunta);

                    gpPregunta.Text = (from item in tblPregunta.AsEnumerable()
                                       select item.Field<string>("U_Pregunta")).FirstOrDefault();

                    rbA.Text = (from item in tblPregunta.AsEnumerable()
                                where item.Field<string>("U_Letra").Trim().Equals("A")
                                select item.Field<string>("U_Letra") +") " + item.Field<string>("U_Respuesta")).FirstOrDefault();
                    rbA.Visible = rbA.Text != string.Empty;

                    rbB.Text = (from item in tblPregunta.AsEnumerable()
                                where item.Field<string>("U_Letra").Trim().Equals("B")
                                select item.Field<string>("U_Letra") + ") " + item.Field<string>("U_Respuesta")).FirstOrDefault();
                    rbB.Visible = rbB.Text != string.Empty;

                    rbC.Text = (from item in tblPregunta.AsEnumerable()
                                where item.Field<string>("U_Letra").Trim().Equals("C")
                                select item.Field<string>("U_Letra") + ") " + item.Field<string>("U_Respuesta")).FirstOrDefault();
                    rbC.Visible = rbC.Text != string.Empty;

                    rbD.Text = (from item in tblPregunta.AsEnumerable()
                                where item.Field<string>("U_Letra").Trim().Equals("D")
                                select item.Field<string>("U_Letra") + ") " + item.Field<string>("U_Respuesta")).FirstOrDefault();
                    rbD.Visible = rbD.Text != string.Empty;

                    rbE.Text = (from item in tblPregunta.AsEnumerable()
                                where item.Field<string>("U_Letra").Trim().Equals("E")
                                select item.Field<string>("U_Letra") + ") " + item.Field<string>("U_Respuesta")).FirstOrDefault();
                    rbE.Visible = rbE.Text != string.Empty;

                    rbF.Text = (from item in tblPregunta.AsEnumerable()
                                where item.Field<string>("U_Letra").Trim().Equals("F")
                                select item.Field<string>("U_Letra") + ") " + item.Field<string>("U_Respuesta")).FirstOrDefault();
                    rbF.Visible = rbF.Text != string.Empty;

                }
            }

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            _Respuestas.Rows.Remove(_Row);

            this.Parent.Controls.Remove(this);
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

            if (rbO.Checked)
            {
                txtEspecificar.ReadOnly = false;
            }
            else
            {
                txtEspecificar.ReadOnly = true;
            }

            if (Pregunta == 2 && Clasificacion == "S")
            {
                if (rbA.Checked || rbE.Checked)
                {
                    btnGuardar.Visible = true;
                    btnFinalizar.Visible = false;
                }
                else
                {
                    btnGuardar.Visible = false;
                    btnFinalizar.Visible = true;
                }
            }

            if (Pregunta == 2 && Clasificacion == "N")
            {
                if (rbD.Checked)
                {
                    btnGuardar.Visible = true;
                    btnFinalizar.Visible = false;
                }
                else
                {
                    btnGuardar.Visible = false;
                    btnFinalizar.Visible = true;
                }
            }

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (Clasificacion == "S")
            {
                if (rbA.Checked)
                {
                    this.Guardar(2, Clasificacion);
                    Pregunta3AD pregunta3 = new Pregunta3AD(_Respuestas);
                    pregunta3.Dock = DockStyle.Fill;

                    this.Parent.Controls.Add(pregunta3);
                    pregunta3.BringToFront();

                }
                else if (rbE.Checked)
                {
                    this.Guardar(2, Clasificacion);
                    Pregunta3E pregunta3 = new Pregunta3E(_Respuestas);
                    pregunta3.Dock = DockStyle.Fill;

                    this.Parent.Controls.Add(pregunta3);
                    pregunta3.BringToFront();
                }


            }
            if (Clasificacion == "N")
            {
                if (rbD.Checked)
                {
                    this.Guardar(2, Clasificacion);
                    Pregunta3AD pregunta3 = new Pregunta3AD(_Respuestas);
                    pregunta3.Dock = DockStyle.Fill;

                    this.Parent.Controls.Add(pregunta3);
                    pregunta3.BringToFront();
                }
            }
            //if (!rbO.Checked && txtEspecificar.Text.Trim() == "")
            //{
            //    if (Pregunta == 2 && Clasificacion == "S" && rbA.Checked)
            //    {
            //        this.Guardar(2, "S");

            //        Pregunta3AD pregunta3 = new Pregunta3AD();
            //        pregunta3.Dock = DockStyle.Fill;

            //        this.Parent.Controls.Add(pregunta3);
            //        pregunta3.BringToFront();
            //    }
            //    else if (Pregunta == 2 && Clasificacion == "N" && rbD.Checked)
            //    {
            //        this.Guardar(2, "N");
            //        Pregunta3AD pregunta3 = new Pregunta3AD();
            //        pregunta3.Dock = DockStyle.Fill;

            //        this.Parent.Controls.Add(pregunta3);
            //        pregunta3.BringToFront();
            //    }
            //    else if (Pregunta == 2 && Clasificacion == "S" && rbE.Checked)
            //    {
            //        this.Guardar(2, "S");
            //        Pregunta3E pregunta3 = new Pregunta3E();
            //        pregunta3.Dock = DockStyle.Fill;

            //        this.Parent.Controls.Add(pregunta3);
            //        pregunta3.BringToFront();
            //    }
            //}
            //else if (rbO.Checked && txtEspecificar.Text.Trim() != "")
            //{
            //    if (Pregunta == 2 && Clasificacion == "S" && rbA.Checked)
            //    {
            //        this.Guardar(2, "S");
            //        Pregunta3AD pregunta3 = new Pregunta3AD();
            //        pregunta3.Dock = DockStyle.Fill;

            //        this.Parent.Controls.Add(pregunta3);
            //        pregunta3.BringToFront();
            //    }
            //    else if (Pregunta == 2 && Clasificacion == "N" && rbD.Checked)
            //    {
            //        this.Guardar(2, "N");
            //        Pregunta3AD pregunta3 = new Pregunta3AD();
            //        pregunta3.Dock = DockStyle.Fill;

            //        this.Parent.Controls.Add(pregunta3);
            //        pregunta3.BringToFront();
            //    }
            //    else if (Pregunta == 2 && Clasificacion == "S" && rbE.Checked)
            //    {
            //        this.Guardar(2, "S");
            //        Pregunta3E pregunta3 = new Pregunta3E();
            //        pregunta3.Dock = DockStyle.Fill;

            //        this.Parent.Controls.Add(pregunta3);
            //        pregunta3.BringToFront();
            //    }
            //}
            //else
            //    MessageBox.Show("Debe el campo especificar no puede estar vacio.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            if (Clasificacion == "S")
            {
                if (rbB.Checked || rbC.Checked || rbD.Checked || rbF.Checked || rbO.Checked)
                {
                    if (rbO.Checked && txtEspecificar.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Debe el campo especificar no puede estar vacio.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.Guardar(2, Clasificacion);
                        this.Finalizar();

                        MessageBox.Show("Registro exitoso.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblHome.Visible = true;
                        lblBack.Visible = false;
                        btnFinalizar.Enabled = false;
                    }

                }
            }
            if (Clasificacion == "N")
            {
                if (rbA.Checked || rbB.Checked || rbC.Checked || rbD.Checked || rbF.Checked || rbO.Checked)
                {
                    if (rbO.Checked && txtEspecificar.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Debe el campo especificar no puede estar vacio.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        this.Guardar(2, Clasificacion);
                        this.Finalizar();

                        MessageBox.Show("Registro exitoso.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblHome.Visible = true;
                        lblBack.Visible = false;
                        btnFinalizar.Enabled = false;
                    }

                }
            }
        }

        private void lblHome_Click(object sender, EventArgs e)
        {//this.Parent.Controls.Add(pregunta3);
            foreach (Control item in this.Parent.Controls)
            {
                if (!(item is Controles.Ranking))
                {
                    try
                    {
                        item.Visible = false;
                    }
                    catch (Exception)
                    {
                    }
                }
                
            }
        }

        private void form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnFinalizar.Enabled)
            if (e.KeyCode == Keys.Escape)
            {
                toolStripStatusLabel1_Click(sender, e);
            }
        }
    }
}
