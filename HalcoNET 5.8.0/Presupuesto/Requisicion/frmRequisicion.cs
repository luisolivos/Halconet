using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Presupuesto.Requisicion
{
    public partial class frmRequisicion : Form
    {
        public string __Type;
        public string __Proveedor;
        public int __Folio;

        ctrlGeneral ctrG = new ctrlGeneral(); 
        ctrlEspecifico ctrE = new ctrlEspecifico();

        public frmRequisicion()
        {
            InitializeComponent();
        }

        public void Info_Usurio()
        {
        }

        public bool ValidarGrid(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count - 1; i++)
            {
                if (!string.IsNullOrEmpty(dgv.Rows[i].ErrorText))
                    return false;
            }
            return true;
        }

        public void Save(ctrlEspecifico ctrEsp)
        {
            ctrEsp = panel1.Controls[0] as ctrlEspecifico;
            DataGridView dgv = ctrEsp.Controls["dgvDetalle"] as DataGridView;
            __Proveedor = ctrEsp.__Proveedor;

            if (!string.IsNullOrEmpty(__Proveedor))
            {
                if (!cbCuenta.SelectedValue.ToString().Equals("0"))
                {
                    Clases.Requisicion.Estatus = "0";
                    Clases.Requisicion.ID = 0;
                    Clases.Requisicion.Fecha = DateTime.Now;
                    Clases.Requisicion.Cuenta = string.Empty;
                    Clases.Requisicion.Tipo = string.Empty;
                    Clases.Requisicion.Modificacion = DateTime.Now;

                    Clases.Requisicion.Fecha = dateTimePicker1.Value;
                    Clases.Requisicion.Total = dgv.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["LineTotal"].Value)) * (decimal)1.16;
                    Clases.Requisicion.Iva = dgv.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["LineTotal"].Value)) * (decimal)0.16;
                    Clases.Requisicion.Cuenta = cbCuenta.SelectedValue.ToString();
                    Clases.Requisicion.Tipo = txtTipo.Text == "ESPECIFICA" ? "E" : "G";
                    Clases.Requisicion.Modificacion = DateTime.Now;

                    if (Clases.Requisicion.Total != 0 && dgv.Rows.Count > 1)
                    {
                        Clases.Requisicion.ExecuteReader();

                        txtFolio.Text = Clases.Requisicion.ID.ToString();

                        for (int i = 0; i < dgv.Rows.Count - 1; i++)
                        {
                            Clases.Objetos.DetalleRequisicion dreq = new Clases.Objetos.DetalleRequisicion();
                            dreq.ID = Clases.Requisicion.ID;
                            dreq.NumProv = 1;
                            dreq.Proveedor = __Proveedor;
                            dreq.Linea = Convert.ToInt32(dgv.Rows[i].Cells["Linea"].Value);
                            dreq.Articulo = Convert.ToString(dgv.Rows[i].Cells["ItemCode"].Value);
                            dreq.Descripcion = Convert.ToString(dgv.Rows[i].Cells["ItemName"].Value);
                            dreq.Cantidad = Convert.ToDecimal(dgv.Rows[i].Cells["Quantity"].Value);
                            dreq.Precio = Convert.ToDecimal(dgv.Rows[i].Cells["Price"].Value);
                            dreq.LineTotal = Convert.ToDecimal(dgv.Rows[i].Cells["LineTotal"].Value);

                            dreq.ExecuteNonQuery(dreq);
                        }

                        lblEstatus.Text = "Listo";
                    }
                    else
                    {
                        lblEstatus.Text = "El total de la requisición no puede ser cero, debe ingresar al menos un artículo o servicio";
                    }
                }
                else lblEstatus.Text = "Error: Selecciona una cuenta.";
            }
            else lblEstatus.Text = "Error: Selecciona un Proveedor.";
        }

        public void Save(ctrlGeneral ctrGrl)
        {
            ctrGrl = panel1.Controls[0] as ctrlGeneral;
            DataGridView dgv = ctrGrl.Controls["panel1"].Controls["dgvDetalle"] as DataGridView;

            if (!string.IsNullOrEmpty(ctrGrl.prov[0].CardCode == "0" ? string.Empty : ctrGrl.prov[0].CardCode))
            {
                if (!string.IsNullOrEmpty(ctrGrl.prov[1].CardCode == "0" ? string.Empty : ctrGrl.prov[1].CardCode))
                {
                    if (!string.IsNullOrEmpty(ctrGrl.prov[2].CardCode == "0" ? string.Empty : ctrGrl.prov[2].CardCode))
                    {

                        Clases.Requisicion.Estatus = "0";
                        Clases.Requisicion.ID = 0;
                        Clases.Requisicion.Fecha = DateTime.Now;
                        Clases.Requisicion.Cuenta = string.Empty;
                        Clases.Requisicion.Tipo = string.Empty;
                        Clases.Requisicion.Modificacion = DateTime.Now;

                        Clases.Requisicion.Fecha = dateTimePicker1.Value;
                        Clases.Requisicion.Cuenta = cbCuenta.SelectedValue.ToString();
                        Clases.Requisicion.Tipo = txtTipo.Text == "ESPECIFICA" ? "E" : "G";
                        Clases.Requisicion.Modificacion = DateTime.Now;

                        if (ValidarGrid(dgv))
                        {
                            if (dgv.Rows.Count > 1)
                            {

                                Clases.Requisicion.ExecuteReader();

                                txtFolio.Text = Clases.Requisicion.ID.ToString();

                                for (int j = 1; j <= 3; j++)
                                {
                                    for (int i = 0; i < dgv.Rows.Count - 1; i++)
                                    {
                                        Clases.Objetos.DetalleRequisicion dreq = new Clases.Objetos.DetalleRequisicion();
                                        dreq.ID = Clases.Requisicion.ID;
                                        dreq.NumProv = j;
                                        dreq.Proveedor = ctrGrl.prov[j - 1].CardCode;
                                        dreq.Linea = Convert.ToInt32(dgv.Rows[i].Cells["Linea"].Value);
                                        dreq.Sugerencia = Convert.ToBoolean(dgv.Rows[i].Cells["Sugerido" + j].Value == DBNull.Value ? false : dgv.Rows[i].Cells["Sugerido" + j].Value);
                                        dreq.Articulo = null;
                                        dreq.Descripcion = Convert.ToString(dgv.Rows[i].Cells["ItemName"].Value);
                                        dreq.Cantidad = Convert.ToDecimal(dgv.Rows[i].Cells["Quantity"].Value);
                                        dreq.Precio = Convert.ToDecimal(dgv.Rows[i].Cells["Price" + j].Value);
                                        dreq.LineTotal = Convert.ToDecimal(dgv.Rows[i].Cells["LineTotal" + j].Value);

                                        dreq.ExecuteNonQuery(dreq);
                                    }
                                }
                                lblEstatus.Text = "Listo";
                            }
                            else lblEstatus.Text = "Error: Debe ingresar al menos un artículo o servicio";
                        }
                        else lblEstatus.Text = "Algunas lineas contien errores, corrijalos para poder continuar.";
                    }
                    else lblEstatus.Text = "Seleccione un proveedor [Proveedor 3]";
                }
                else lblEstatus.Text = "Seleccione un proveedor [Proveedor 2]";
            }
            else lblEstatus.Text = "Seleccione un proveedor [Proveedor 1]";
        }

        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            DataRowView row = cbCuenta.SelectedItem as DataRowView;

            txtCuenta.Text = row["Codigo"].ToString();
            txtTipo.Text = row["Tipo"].ToString();

            Clases.Requisicion.Cuenta = row["Codigo"].ToString();
            __Type = row["Tipo"].ToString();

            if (row["Tipo"].ToString().Equals("GENERAL"))
            {
                ctrG = new ctrlGeneral();
                ctrG.Dock = DockStyle.Fill;
                panel1.Controls.Add(ctrG);
            }
            if (row["Tipo"].ToString().Equals("ESPECIFICA"))
            {
                ctrE = new ctrlEspecifico();
                ctrE.Dock = DockStyle.Fill;
                panel1.Controls.Add(ctrE);
            }
        }

        private void frmRequisicion_Load(object sender, EventArgs e)
        {
            this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

            Clases.FillControl fill = new Clases.FillControl();
            fill.FillDataSource(cbCuenta, "-----Selecciona una Cuenta-----", 3);
            fill.FillDataSource(cbSucursal, "-----Selecciona una Sucursal-----", 5);
            fill.FillDataSource(cbDepto, "-----Selecciona un Departamento-----", 6);

            try
            {
                System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand("PRES_Catalogos", new System.Data.SqlClient.SqlConnection(ClasesSGUV.Propiedades.conectionPJ_Pres));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 7);
                command.Parameters.AddWithValue("@Usuario", ClasesSGUV.Login.Id_Usuario);

                DataTable tblUser = Clases.Datos.Sql.Fill(command);
                txtUsuario.Text = tblUser.Rows[0].Field<string>("Nombre");
                cbSucursal.SelectedValue = tblUser.Rows[0].Field<string>("Sucursal");
                cbDepto.SelectedValue = tblUser.Rows[0].Field<string>("Depto");
            }
            catch (Exception)
            {
            }
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                lblEstatus.Text = string.Empty;

                if (panel1.Controls.Count > 0)
                {
                    if (panel1.Controls[0] is ctrlGeneral)
                    {                        
                        this.Save(new ctrlGeneral());
                    }
                    else if (panel1.Controls[0] is ctrlEspecifico)
                    {
                        this.Save(new ctrlEspecifico());
                    }
                }
                else
                {
                    MessageBox.Show("3");
                }
            }
            catch (Exception ex)
            {
                lblEstatus.Text = ex.Message;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.frmRequisicion_Load(sender, e);
        }
    }
}
