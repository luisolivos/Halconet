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

namespace Ventas
{
    public partial class Administración : Form
    {

        #region PARAMETROS

        /// <summary>
        /// Enumerador para los tipos de consulta 
        /// </summary>
        private enum TipoConsulta
        {
            Agregar = 1,
            Modificar = 2,
            Eliminar = 3,
            Verificar = 4,
            ConsultarTodos = 5,
            ConsultarPorUsuario = 6,
            ConsultarSiExisteNombreUusuario = 7
        }

        /// <summary>
        /// Enumerador de las columnas del Grid
        /// </summary>
        private enum ColumnasGrid
        {
            ClaveEntidad = 0,
            NombreUsuario = 1,
            Contrasenha = 2,
            RolNum = 3,
            Rol = 4,
            Vendedor = 5,
            BotonModificar = 6,
            BotonEliminar = 7
        }

        /// <summary>
        /// Roles del sistema
        /// </summary>
        private enum RolSistema
        {
            Vendedor = 1,
            GerenteVentas = 2,
            Cobranza = 3,
            Direccion = 4
        }

        /// <summary>
        /// Indica el resultado de la verificación si ya existe un usuario con el 
        /// mismo nombre en la BD
        /// </summary>
        private enum ResultadoVerficacion
        {
            NoExisteElUsuario = 0,
            SiExisteElUsuario = 1            
        }

        private int ClaveEntidad;
        private string NombreUsuario;
        private string Contrasenha;
        private int Rol;
        private int Vendedor;
        public SqlConnection conectionSGUV = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV);
        public SqlConnection conectionPJ = new SqlConnection(ClasesSGUV.Propiedades.conectionPJ);  

        #endregion


        #region EVENTOS

        /// <summary>
        /// Función que inicializa el Formulario
        /// </summary>
        public Administración()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que ocurre al cargarse la página
        /// Carga los usuarios de la BD en el grid
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void Administración_Load(object sender, EventArgs e)
        {
            CargarGrid();
        }

        /// <summary>
        /// Evento que ocurre al hacer click sobre el gridUsuarios
        /// Controla los botones modificar y eliminar del grid
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void gridUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)ColumnasGrid.BotonModificar)
                    {
                        ClaveEntidad = Convert.ToInt32(gridUsuarios.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.ClaveEntidad].Value.ToString());
                        NombreUsuario = gridUsuarios.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.NombreUsuario].Value.ToString();
                        Contrasenha = gridUsuarios.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.Contrasenha].Value.ToString();
                        Rol = Convert.ToInt32(gridUsuarios.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.RolNum].Value.ToString());
                        Vendedor = Convert.ToInt32(gridUsuarios.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.Vendedor].Value.ToString());

                        ModificarUsuario form = new ModificarUsuario(ClaveEntidad, NombreUsuario, Contrasenha, Rol, Vendedor);
                        DialogResult dialogresult = form.ShowDialog();
                        if (dialogresult == DialogResult.OK)
                        {
                            CargarGrid();
                        }
                        form.Dispose();
                    }
                    if (((System.Windows.Forms.DataGridView)(sender)).CurrentCell.ColumnIndex == (int)ColumnasGrid.BotonEliminar)
                    {
                        DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar este Usuario?", "HalcoNET", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        switch (result)
                        {
                            case DialogResult.Yes:
                                ClaveEntidad = Convert.ToInt32(gridUsuarios.Rows[((System.Windows.Forms.DataGridView)(sender)).CurrentCell.RowIndex].Cells[(int)ColumnasGrid.ClaveEntidad].Value.ToString());
                                EliminarUsuario(ClaveEntidad);
                                break;
                            case DialogResult.No:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento que muestra las imagenes de Editar y Eliminar en la BD
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void gridUsuarios_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && this.gridUsuarios.Columns[e.ColumnIndex].Name == "Editar" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBotonModificar = this.gridUsuarios.Rows[e.RowIndex].Cells["Editar"] as DataGridViewButtonCell;
                Icon icoModificar = new Icon(Environment.CurrentDirectory + @"\Editar.ico");
                e.Graphics.DrawIcon(icoModificar, e.CellBounds.Left + 18, e.CellBounds.Top + 5);

                this.gridUsuarios.Rows[e.RowIndex].Height = icoModificar.Height + 10;

                e.Handled = true;
            }

            if (e.ColumnIndex >= 0 && this.gridUsuarios.Columns[e.ColumnIndex].Name == "Eliminar" && e.RowIndex >= 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                DataGridViewButtonCell celBotonEliminar = this.gridUsuarios.Rows[e.RowIndex].Cells["Eliminar"] as DataGridViewButtonCell;
                Icon icoEliminar = new Icon(Environment.CurrentDirectory + @"\Eliminar.ico");
                e.Graphics.DrawIcon(icoEliminar, e.CellBounds.Left + 18, e.CellBounds.Top + 5);

                this.gridUsuarios.Rows[e.RowIndex].Height = icoEliminar.Height + 10;

                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento que ocurre al hacer click en el btnNuevo
        /// Abre un formulario para agregar un nuevo usuario
        /// </summary>
        /// <param name="sender">Objeto que produce el evento</param>
        /// <param name="e">Parámetros del evento</param>
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            AgregarUsuario form = new AgregarUsuario();
            DialogResult dialogresult = form.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                CargarGrid();
            }
            form.Dispose();
        }

        #endregion


        #region FUNCIONES

        /// <summary>
        /// Función que carga el gridUsuarios
        /// </summary>
        private void CargarGrid()
        {
            gridUsuarios.Columns.Clear();
            gridUsuarios.DataSource = null;

            SqlCommand command = new SqlCommand("SGUV_Usuarios", conectionSGUV);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.ConsultarTodos);
            command.Parameters.AddWithValue("@ClaveEntidad", 0);
            command.Parameters.AddWithValue("@Usuario", string.Empty);
            command.Parameters.AddWithValue("@Contrasenha", string.Empty);
            command.Parameters.AddWithValue("@Rol", 0);
            command.Parameters.AddWithValue("@Vendedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            gridUsuarios.DataSource = table;
            DarFormatoADataGrid();
        }

        /// <summary>
        /// Función que establece el formato de celdas y columnas para el DataGridView
        /// </summary>
        private void DarFormatoADataGrid()
        {
            DataGridViewButtonColumn botonModificar = new DataGridViewButtonColumn();
            {
                botonModificar.Name = "Editar";
                botonModificar.HeaderText = "Editar";
                botonModificar.ToolTipText = "Editar";
                botonModificar.Width = 70;
                botonModificar.UseColumnTextForButtonValue = true;
            }
            gridUsuarios.Columns.Add(botonModificar);

            DataGridViewButtonColumn botonEliminar = new DataGridViewButtonColumn();
            {
                botonEliminar.Name = "Eliminar";
                botonEliminar.HeaderText = "Eliminar";
                botonEliminar.ToolTipText = "Eliminar";
                botonEliminar.Width = 70;                
                botonEliminar.UseColumnTextForButtonValue = true;
            }
            gridUsuarios.Columns.Add(botonEliminar);

            gridUsuarios.Columns[(int)ColumnasGrid.ClaveEntidad].HeaderText = "Clave de Usuario";
            gridUsuarios.Columns[(int)ColumnasGrid.ClaveEntidad].Width = 75;
            gridUsuarios.Columns[(int)ColumnasGrid.ClaveEntidad].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridUsuarios.Columns[(int)ColumnasGrid.NombreUsuario].HeaderText = "Nombre de Usuario";
            gridUsuarios.Columns[(int)ColumnasGrid.NombreUsuario].Width = 125;
            gridUsuarios.Columns[(int)ColumnasGrid.NombreUsuario].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridUsuarios.Columns[(int)ColumnasGrid.Contrasenha].HeaderText = "Contraseña";
            gridUsuarios.Columns[(int)ColumnasGrid.Contrasenha].Width = 125;
            gridUsuarios.Columns[(int)ColumnasGrid.Contrasenha].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridUsuarios.Columns[(int)ColumnasGrid.RolNum].Visible = false;

            gridUsuarios.Columns[(int)ColumnasGrid.Vendedor].Visible = false;

            gridUsuarios.Columns[(int)ColumnasGrid.Rol].HeaderText = "Rol";
            gridUsuarios.Columns[(int)ColumnasGrid.Rol].Width = 130;
            gridUsuarios.Columns[(int)ColumnasGrid.Rol].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridUsuarios.AllowUserToAddRows = false;
            gridUsuarios.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridUsuarios.Columns[(int)ColumnasGrid.ClaveEntidad].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridUsuarios.Columns[(int)ColumnasGrid.NombreUsuario].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridUsuarios.Columns[(int)ColumnasGrid.Contrasenha].SortMode = DataGridViewColumnSortMode.NotSortable;
            gridUsuarios.Columns[(int)ColumnasGrid.Rol].SortMode = DataGridViewColumnSortMode.NotSortable;
        }        

        /// <summary>
        /// Función que realiza la operación de eliminar un usuario en la BD
        /// </summary>
        /// <param name="ClaveUsuario">Clave del usuario a eliminar</param>
        public void EliminarUsuario(int ClaveUsuario)
        {
            try
            {
                SqlCommand command = new SqlCommand("SGUV_Usuarios", conectionSGUV);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", (int)TipoConsulta.Eliminar);
                command.Parameters.AddWithValue("@ClaveEntidad", ClaveUsuario);
                command.Parameters.AddWithValue("@Usuario", string.Empty);
                command.Parameters.AddWithValue("@Contrasenha", string.Empty);
                command.Parameters.AddWithValue("@Rol", 0);
                command.Parameters.AddWithValue("@Vendedor", 0);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                gridUsuarios.Columns.Clear();
                gridUsuarios.DataSource = null;

                gridUsuarios.DataSource = table;
                DarFormatoADataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

    }
}