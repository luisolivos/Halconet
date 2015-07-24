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

namespace Compras
{
    public partial class frmReubicaciones : Form
    {
        public string Almacen;
        public String AlmacenPadre;
        public string Lineas;
        public string Proveedores;
        public string BidingComplete;
        public string isChild;
        public string isMoney;

        Clases.Logs log;

        public enum ColumasPadres
        {
            Linea,
            Articulo,
            Descripcion,
            Clasificacion,
            Stock_O,
            Ideal_O,
            Exceso_O,
            Respaldo,
            Reubicar_D1,
            Reubicar_D2,
            Sobrante,
            Color,
            Peso
        }

        public enum ColumnasHijos
        {
            Linea,
            Articulo,
            Descripcion,
            Clasificacion,
            HIJO_STOCK,
            HIJO_IDEAL,
            PADRE_STOCK,
            PADRE_IDEAL,
            REUBICAR_PADRE,
            Color,
            Peso,
            Directo
        }

        public enum ColumasPadresM
        {
            Linea,
            Articulo,
            Descripcion,
            Clasificacion,
            Stock_O,
            Ideal_O,
            Exceso_O,Respaldo,
            Exceso_OM,
            
            Reubicar_D1,
            Reubicar_D1_M,
            Reubicar_D2,
            Reubicar_D2_M,
            Sobrante,
            Sobrante_M,
            Color,
            Peso
        }

        public enum ColumnasHijosM
        {
            Linea,
            Articulo,
            Descripcion,
            Clasificacion,
            HIJO_STOCK,
            HIJO_IDEAL,
            PADRE_STOCK,
            PADRE_IDEAL,
            REUBICAR_PADRE,
            REUBICAR_PADRE_M,
            Color,
            Peso,
            Directo
        }

        public frmReubicaciones()
        {
            InitializeComponent();
        }

        DataTable Articul = new DataTable();
        private void Reubicaciones_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = ClasesSGUV.Propiedades.IconHalcoNET;

                log = new Clases.Logs(ClasesSGUV.Login.NombreUsuario, this.AccessibleDescription, 0);
                cbAlmacen.SelectedIndex = 0;
                CargarLinea(cbLinea, "Todas");
                CargarProveedores(cbProveedor, "Todos");

                this.ListaArticulos();

                txtArticulo.AutoCompleteCustomSource = Autocomplete(Articul, "ItemCode");
                txtArticulo.AutoCompleteMode = AutoCompleteMode.Suggest;
                txtArticulo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region METODOS

        public string GetName(string _alm, bool _padre)
        {
            if (!_padre)
                switch (_alm)
                {
                    case "03": return "API";
                    case "05": return "COR";
                    case "06": return "TEP";
                    case "16": return "MEX";
                    case "01": return "PUE";
                    case "02": return "MTY";
                    case "18": return "GDL";
                    case "23": return "SAL";
                    default: return string.Empty;
                }
            else
                switch (_alm)
                {
                    case "03": return "PUE";
                    case "05": return "PUE";
                    case "06": return "PUE";
                    case "16": return "PUE";
                    case "01": return "PUE";
                    case "02": return "MTY";
                    case "18": return "MTY";
                    case "23": return "MTY";
                    default: return string.Empty;
                }
        }

        public void Formato(DataGridView dgv)
        {
            dgv.Columns[(int)ColumasPadres.Linea].Width = 70;
            dgv.Columns[(int)ColumasPadres.Articulo].Width = 90;
            dgv.Columns[(int)ColumasPadres.Descripcion].Width = 250;
            dgv.Columns[(int)ColumasPadres.Clasificacion].Width = 70;
            dgv.Columns[(int)ColumasPadres.Stock_O].Width = 90;
            dgv.Columns[(int)ColumasPadres.Ideal_O].Width = 90;
            dgv.Columns[(int)ColumasPadres.Exceso_O].Width = 90;
            dgv.Columns[(int)ColumasPadres.Reubicar_D1].Width = 90;
            dgv.Columns[(int)ColumasPadres.Reubicar_D2].Width = 90;
            dgv.Columns[(int)ColumasPadres.Sobrante].Width = 90;
            dgv.Columns[(int)ColumasPadres.Peso].Width = 90;
            dgv.Columns[(int)ColumasPadres.Color].Visible = false;

            dgv.Columns[(int)ColumasPadres.Stock_O].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadres.Ideal_O].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadres.Exceso_O].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadres.Respaldo].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadres.Reubicar_D1].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadres.Reubicar_D2].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadres.Sobrante].DefaultCellStyle.Format = "N0";

            dgv.Columns[(int)ColumasPadres.Peso].DefaultCellStyle.Format = "N3";

            dgv.Columns[(int)ColumasPadres.Clasificacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumasPadres.Stock_O].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadres.Ideal_O].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadres.Exceso_O].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadres.Respaldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadres.Reubicar_D1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadres.Reubicar_D2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadres.Sobrante].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadres.Peso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void FormatoHijos(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasHijos.Linea].Width = 70;
            dgv.Columns[(int)ColumnasHijos.Articulo].Width = 90;
            dgv.Columns[(int)ColumnasHijos.Descripcion].Width = 250;
            dgv.Columns[(int)ColumnasHijos.Clasificacion].Width = 70;
            dgv.Columns[(int)ColumnasHijos.HIJO_STOCK].Width = 90;
            dgv.Columns[(int)ColumnasHijos.HIJO_IDEAL].Width = 90;
            dgv.Columns[(int)ColumnasHijos.PADRE_STOCK].Width = 90;
            dgv.Columns[(int)ColumnasHijos.PADRE_IDEAL].Width = 90;
            dgv.Columns[(int)ColumnasHijos.REUBICAR_PADRE].Width = 90;
            dgv.Columns[(int)ColumnasHijos.Peso].Width = 90;
            dgv.Columns[(int)ColumnasHijos.Color].Visible = false;
            dgv.Columns[(int)ColumnasHijos.Directo].Visible = false;

            dgv.Columns[(int)ColumnasHijos.Clasificacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasHijos.HIJO_STOCK].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijos.HIJO_IDEAL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijos.PADRE_STOCK].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijos.PADRE_IDEAL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijos.REUBICAR_PADRE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijos.Peso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasHijos.HIJO_STOCK].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasHijos.HIJO_IDEAL].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasHijos.PADRE_STOCK].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasHijos.PADRE_IDEAL].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasHijos.REUBICAR_PADRE].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasHijos.Peso].DefaultCellStyle.Format = "N3";

            dgv.Columns[(int)ColumnasHijos.HIJO_STOCK].HeaderText = Almacen + " - " + this.GetName(Almacen, false) + " STOCK";
            dgv.Columns[(int)ColumnasHijos.HIJO_IDEAL].HeaderText = Almacen + " - " + this.GetName(Almacen, false) + " IDEAL";
            dgv.Columns[(int)ColumnasHijos.PADRE_STOCK].HeaderText = AlmacenPadre + " - " + this.GetName(AlmacenPadre, true) + " STOCK";
            dgv.Columns[(int)ColumnasHijos.PADRE_IDEAL].HeaderText = AlmacenPadre + " - " + this.GetName(AlmacenPadre, true) + " IDEAL";
            dgv.Columns[(int)ColumnasHijos.REUBICAR_PADRE].HeaderText = AlmacenPadre + " - " + this.GetName(AlmacenPadre, true) + " REUBICAR";
        }

        public void FormatoM(DataGridView dgv)
        {
            dgv.Columns[(int)ColumasPadresM.Linea].Width = 70;
            dgv.Columns[(int)ColumasPadresM.Articulo].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Descripcion].Width = 250;
            dgv.Columns[(int)ColumasPadresM.Clasificacion].Width = 70;
            dgv.Columns[(int)ColumasPadresM.Stock_O].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Ideal_O].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Exceso_O].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Reubicar_D1].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Reubicar_D2].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Sobrante].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Exceso_OM].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Reubicar_D1_M].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Reubicar_D2_M].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Sobrante_M].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Peso].Width = 90;
            dgv.Columns[(int)ColumasPadresM.Color].Visible = false;

            dgv.Columns[(int)ColumasPadresM.Stock_O].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadresM.Ideal_O].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadresM.Exceso_O].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadresM.Reubicar_D1].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadresM.Reubicar_D2].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadresM.Sobrante].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadresM.Respaldo].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadresM.Exceso_OM].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasPadresM.Reubicar_D1_M].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasPadresM.Reubicar_D2_M].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasPadresM.Sobrante_M].DefaultCellStyle.Format = "C2";
            dgv.Columns[(int)ColumasPadresM.Sobrante].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumasPadresM.Peso].DefaultCellStyle.Format = "N3";

            dgv.Columns[(int)ColumasPadresM.Clasificacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumasPadresM.Stock_O].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Ideal_O].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Exceso_O].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Respaldo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Reubicar_D1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Reubicar_D2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Sobrante].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Exceso_OM].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Reubicar_D1_M].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Reubicar_D2_M].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Sobrante_M].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumasPadresM.Peso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void FormatoHijosM(DataGridView dgv)
        {
            dgv.Columns[(int)ColumnasHijosM.Linea].Width = 70;
            dgv.Columns[(int)ColumnasHijosM.Articulo].Width = 90;
            dgv.Columns[(int)ColumnasHijosM.Descripcion].Width = 250;
            dgv.Columns[(int)ColumnasHijosM.Clasificacion].Width = 70;
            dgv.Columns[(int)ColumnasHijosM.HIJO_STOCK].Width = 90;
            dgv.Columns[(int)ColumnasHijosM.HIJO_IDEAL].Width = 90;
            dgv.Columns[(int)ColumnasHijosM.PADRE_STOCK].Width = 90;
            dgv.Columns[(int)ColumnasHijosM.PADRE_IDEAL].Width = 90;
            dgv.Columns[(int)ColumnasHijosM.REUBICAR_PADRE].Width = 90;
            dgv.Columns[(int)ColumnasHijosM.REUBICAR_PADRE_M].Width = 90;
            dgv.Columns[(int)ColumnasHijosM.Peso].Width = 90;
            dgv.Columns[(int)ColumnasHijosM.Color].Visible = false;
            dgv.Columns[(int)ColumnasHijosM.Directo].Visible = false;

            dgv.Columns[(int)ColumnasHijosM.Clasificacion].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgv.Columns[(int)ColumnasHijosM.HIJO_STOCK].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijosM.HIJO_IDEAL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijosM.PADRE_STOCK].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijosM.PADRE_IDEAL].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijosM.REUBICAR_PADRE].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijosM.REUBICAR_PADRE_M].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgv.Columns[(int)ColumnasHijosM.Peso].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgv.Columns[(int)ColumnasHijosM.HIJO_STOCK].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasHijosM.HIJO_IDEAL].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasHijosM.PADRE_STOCK].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasHijosM.PADRE_IDEAL].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasHijosM.REUBICAR_PADRE].DefaultCellStyle.Format = "N0";
            dgv.Columns[(int)ColumnasHijosM.Peso].DefaultCellStyle.Format = "N3";
            dgv.Columns[(int)ColumnasHijosM.REUBICAR_PADRE_M].DefaultCellStyle.Format = "C2";

            dgv.Columns[(int)ColumnasHijosM.HIJO_STOCK].HeaderText = Almacen + " - " + this.GetName(Almacen, false) + " STOCK";
            dgv.Columns[(int)ColumnasHijosM.HIJO_IDEAL].HeaderText = Almacen + " - " + this.GetName(Almacen, false) + " IDEAL";
            dgv.Columns[(int)ColumnasHijosM.PADRE_STOCK].HeaderText = Almacen + " - " + this.GetName(Almacen, true) + " STOCK";
            dgv.Columns[(int)ColumnasHijosM.PADRE_IDEAL].HeaderText = Almacen + " - " + this.GetName(Almacen, true) + " IDEAL";
            dgv.Columns[(int)ColumnasHijosM.REUBICAR_PADRE].HeaderText = Almacen + " - " + this.GetName(Almacen, true) + " REUBICAR";
            dgv.Columns[(int)ColumnasHijosM.REUBICAR_PADRE_M].HeaderText = Almacen + " - " + this.GetName(Almacen, true) + " REUBICAR ($)";
        }

        public DataTable ListaArticulos()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = ClasesSGUV.Propiedades.conectionSGUV;
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = "PJ_ReparticionStock";

                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Articulo", string.Empty);
                    command.Parameters.AddWithValue("@CantiadOK", decimal.Zero);
                    command.Parameters.AddWithValue("@Incremento", decimal.Zero);

                    command.CommandTimeout = 0;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.SelectCommand.CommandTimeout = 0;
                    adapter.Fill(Articul);

                    return Articul;
                }
            }
        }

        public static AutoCompleteStringCollection Autocomplete(DataTable _t, string _column)
        {
            DataTable dt = _t;

            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();

            foreach (DataRow row in dt.Rows)
            {
                coleccion.Add(Convert.ToString(row[_column]));
            }

            return coleccion;
        }

        public void CargarLinea(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 2);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public void CargarDetalle(string _articulo, DataGridView dg)
        {
            try
            {
                SqlCommand command = new SqlCommand("PJ_RemateDevolucion", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 2);
                command.Parameters.AddWithValue("@Lineas", string.Empty);
                command.Parameters.AddWithValue("@VecesIdeal", 0);
                command.Parameters.AddWithValue("@Articulo", _articulo);
                command.Parameters.AddWithValue("@Tipo", "Todos");
                command.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                dg.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CargarProveedores(ComboBox _cb, string _inicio)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 8);
            command.Parameters.AddWithValue("@Articulo", string.Empty);
            command.Parameters.AddWithValue("@Linea", 0);
            command.Parameters.AddWithValue("@AlmacenDestino", string.Empty);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            DataRow row = table.NewRow();
            row["Nombre"] = _inicio;
            row["Codigo"] = "0";
            table.Rows.InsertAt(row, 0);

            _cb.DataSource = table;
            _cb.DisplayMember = "Nombre";
            _cb.ValueMember = "Codigo";
        }

        public string GetCadena(CheckedListBox cb)
        {
            StringBuilder stb = new StringBuilder();
            foreach (DataRowView item in cb.CheckedItems)
            {
                if (item["Codigo"].ToString() != "0")
                {
                    if (!cb.ToString().Equals(string.Empty))
                    {
                        stb.Append(",");
                    }
                    stb.Append(item["Codigo"].ToString());
                }
            }
            if (cb.CheckedItems.Count == 0)
            {
                foreach (DataRowView item in cb.Items)
                {
                    if (item["Codigo"].ToString() != "0")
                    {
                        if (!cb.ToString().Equals(string.Empty))
                        {
                            stb.Append(",");
                        }
                        stb.Append(item["Codigo"].ToString());
                    }
                }
            }
            return stb.ToString();
        }

        public void CargarVentas(string _articulo, string _almacen, DataGridView _dg)
        {
            SqlCommand command = new SqlCommand("PJ_Compras", new SqlConnection(ClasesSGUV.Propiedades.conectionPJ));
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TipoConsulta", 4);
            command.Parameters.AddWithValue("@Articulo", _articulo);
            command.Parameters.AddWithValue("@Linea", string.Empty);
            command.Parameters.AddWithValue("@AlmacenDestino", _almacen);
            command.Parameters.AddWithValue("@AlmacenOrigen", string.Empty);
            command.Parameters.AddWithValue("@Proveedor", 0);

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.SelectCommand.CommandTimeout = 0;
            adapter.Fill(table);

            table.TableName = "Detalle";
            _dg.DataSource = table;
        }

        public DataTable Filtar(DataTable _t, string _articulo, int meses, decimal _veces)
        {
            DataTable table = new DataTable();
            if (string.IsNullOrEmpty(_articulo))
            {
                var query = from item in _t.AsEnumerable()
                            where item.Field<int>("Meses de ultima entrada") >= meses
                            && item.Field<decimal>("Veces ideal") >= _veces
                            select item;
                if (query.Count() > 0)
                {
                    table = query.CopyToDataTable();
                }
            }
            else if (!string.IsNullOrEmpty(_articulo))
            {
                var query = from item in _t.AsEnumerable()
                            where item.Field<string>("Artículo") == _articulo
                                  && item.Field<int>("Meses de ultima entrada") >= meses
                                  && item.Field<decimal>("Veces ideal") >= _veces
                            select item;
                if (query.Count() > 0)
                {
                    table = query.CopyToDataTable();
                }
            }

            return table;
        }

        private void Esperar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.WaitCursor;
            }
        }
        private void Continuar()
        {

            foreach (Control item in this.Controls)
            {
                item.Cursor = Cursors.Arrow;
            }
        }
        #endregion

        #region EVENTOS
        private void button2_Click(object sender, EventArgs e)
        {
            txtMeses.Text = "6.0";
            cbAlmacen.SelectedIndex = 0;
            gridReubicacion.DataSource = null;
            gridDetalle.DataSource = null;
            gridVentas.DataSource = null;
            gridReubicacion.Columns.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                isChild = string.Empty;
                isMoney = string.Empty;
                BidingComplete = string.Empty;

                this.Esperar();
                gridReubicacion.Columns.Clear();
                gridReubicacion.DataSource = null;
                gridDetalle.DataSource = null;
                gridVentas.DataSource = null;

                Almacen = cbAlmacen.SelectedItem.ToString().Substring(0, 2);
                Lineas = Convert.ToString(cbLinea.SelectedValue);
                Proveedores = Convert.ToString(cbProveedor.SelectedValue);

                string imp = string.Empty;
                if (rbTodo.Checked)
                    imp = "T";
                if (rbNacional.Checked)
                    imp = "N";
                if (rbImportacion.Checked)
                    imp = "I";

                SqlCommand command = new SqlCommand("PJ_ReubicacionesP", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                if (!checkBox1.Checked) command.Parameters.AddWithValue("@TipoConsulta", 5);
                else command.Parameters.AddWithValue("@TipoConsulta", 6);
                command.Parameters.AddWithValue("@Lineas", Lineas);
                command.Parameters.AddWithValue("@Proveedores", Proveedores);
                command.Parameters.AddWithValue("@Almacen", Almacen);
                command.Parameters.AddWithValue("@Importacion", imp);
                command.CommandTimeout = 0;

                if (!checkBox1.Checked) isMoney = "N";
                else isMoney = "Y";

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command1 = new SqlCommand("PJ_ReubicacionesP", connection))
                    {
                        command1.CommandType = CommandType.StoredProcedure;
                        command1.Parameters.AddWithValue("@TipoConsulta", 8);
                        command1.Parameters.AddWithValue("@Lineas", Lineas);
                        command1.Parameters.AddWithValue("@Proveedores", Proveedores);
                        command1.Parameters.AddWithValue("@Almacen", Almacen);
                        command1.Parameters.AddWithValue("@Importacion", rbImportacion.Checked ? "Y" : "N");
                        command1.CommandTimeout = 0;

                        connection.Open();

                        AlmacenPadre = Convert.ToString(command1.ExecuteScalar());
                    }
                }

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command1 = new SqlCommand("PJ_ReubicacionesP", connection))
                    {
                        command1.CommandType = CommandType.StoredProcedure;
                        command1.Parameters.AddWithValue("@TipoConsulta", 9);
                        command1.Parameters.AddWithValue("@Lineas", Lineas);
                        command1.Parameters.AddWithValue("@Proveedores", Proveedores);
                        command1.Parameters.AddWithValue("@Almacen", Almacen);
                        command1.Parameters.AddWithValue("@Importacion", rbImportacion.Checked ? "Y" : "N");
                        command1.CommandTimeout = 0;

                        connection.Open();

                        SqlDataReader reader = command1.ExecuteReader();
                        if (reader.Read())
                        {
                            BidingComplete = Convert.ToString(reader.GetString(0));
                            isChild = Convert.ToString(reader.GetString(1));
                        }
                    }
                }

                gridReubicacion.DataSource = table;

                if (!checkBox1.Checked)
                {
                    if (Almacen.Equals("01") || Almacen.Equals("02") || Almacen.Equals("18")) { this.Formato(gridReubicacion); dgvTotales.Visible = false; }
                    else this.FormatoHijos(gridReubicacion);
                }
                else
                {
                    if (Almacen.Equals("01") || Almacen.Equals("02") || Almacen.Equals("18"))
                    {
                        this.FormatoM(gridReubicacion);
                        decimal exceso;
                        decimal sobrante;

                        exceso = gridReubicacion.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells[(int)ColumasPadresM.Exceso_OM].Value));
                        sobrante = gridReubicacion.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells[(int)ColumasPadresM.Sobrante_M].Value));

                        lblTotal.Text = "Total Exeso: " + exceso.ToString("C2") + " \r\n" + "Total Sobrante: " + sobrante.ToString("C2");
                    }
                    else
                    {
                        this.FormatoHijosM(gridReubicacion);

                        decimal exceso;

                        exceso = gridReubicacion.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToInt32(x.Cells[(int)ColumnasHijosM.REUBICAR_PADRE_M].Value));

                        lblTotal.Text = "Total Reubicar: " + exceso.ToString("C2");
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Continuar();
            }
        }

        private void gridDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                int row = gridDetalle.CurrentCell.RowIndex;
                int column = gridDetalle.CurrentCell.ColumnIndex;

                gridVentas.DataSource = null;
                if (row >= 0 && column >= 1)
                {
                    string _articulo = gridDetalle.Rows[row].Cells[0].Value.ToString();
                    string _almacen = gridDetalle.Columns[column].HeaderText.Trim().Substring(0, 2);

                    //CargarVentas(_articulo, _almacen, gridVentas);

                    SqlCommand command = new SqlCommand("sp_HistorialVentas");
                    command.Parameters.AddWithValue("@TipoConsulta", 1);
                    command.Parameters.AddWithValue("@Almacen", _almacen);
                    command.Parameters.AddWithValue("@Articulo", _articulo);

                    Clases.Ventas vts = new Clases.Ventas();
                    gridVentas.DataSource = vts.GetVentas(command);


                    foreach (DataGridViewColumn item in gridVentas.Columns)
                    {
                        if (item.Index > 1)
                        {
                            item.Width = 60;
                            item.DefaultCellStyle.Format = "N0";
                            item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        else
                        {
                            item.Width = 75;
                            item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                        }

                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarAExcel exp = new ExportarAExcel();
                DialogResult dialogResult = MessageBox.Show("¿Exportar sin formato?\r\n Si elige 'No' el proceso puede durar varios minutos.", "HalcoNET", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //do something
                    if (exp.ExportarSinFormato(gridReubicacion))
                        MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something 
                    if (exp.Exportar(gridReubicacion))
                        MessageBox.Show("El archivo se creo con exito.", "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception)
            {
            }
            finally
            {
            }
        }

        private void txtArticulo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1_Click(sender, e);
        }

        private void Reubicaciones_Shown(object sender, EventArgs e)
        {
            try
            {
                log.ID = log.Inicio();
            }
            catch (Exception)
            {

            }
        }

        private void Reubicaciones_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                log.Fin();
            }
            catch (Exception)
            {

            }
        }

        private void gridReubicacion_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush((sender as DataGridView).RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void gridReubicacion_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = gridReubicacion.CurrentCell.RowIndex;
                int column = gridReubicacion.CurrentCell.ColumnIndex;
                gridDetalle.DataSource = null;

                string _articulo = gridReubicacion.Rows[row].Cells[1].Value.ToString();

                CargarDetalle(_articulo, gridDetalle);

                foreach (DataGridViewColumn item in gridDetalle.Columns)
                {
                    item.DefaultCellStyle.Format = "N0";
                    item.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                SqlCommand command = new SqlCommand("PJ_ReubicacionesP", new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV));
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TipoConsulta", 7);
                command.Parameters.AddWithValue("@Lineas", string.Empty);
                command.Parameters.AddWithValue("@Proveedores", _articulo);
                command.Parameters.AddWithValue("@Almacen", Almacen);
                command.Parameters.AddWithValue("@Importacion", rbImportacion.Checked ? "Y" : "N");

                command.CommandTimeout = 0;

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(table);

                dgvTraspasos.DataSource = table;

            }
            catch (Exception)
            {
            }
        }

        private void gridReubicacion_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                decimal total = decimal.Zero;
                decimal reubicar = decimal.Zero;
                decimal directo = decimal.Zero;

                foreach (DataGridViewRow item in (sender as DataGridView).Rows)
                {
                    if (item.Cells["COLOR"].Value.ToString() == "Y")
                        item.Cells[4].Style.BackColor = Color.Yellow;

                    //con columnas sin $$
                    if (BidingComplete == "Y" && isChild == "Y" && isMoney == "N")
                    {
                        if (item.Cells[(int)ColumnasHijos.Directo].Value.ToString() != "N")
                        {
                            item.Cells[(int)ColumnasHijos.Linea].Style.BackColor = Color.FromArgb(218, 150, 148);
                            item.Cells[(int)ColumnasHijos.Linea].Style.ForeColor = Color.Black;
                        }

                        else
                        {
                            item.Cells[(int)ColumnasHijos.Linea].Style.BackColor = Color.White;
                            item.Cells[(int)ColumnasHijos.Linea].Style.ForeColor = Color.Black;
                        }
                        
                    }
                    ///con columnas en $$
                    if (BidingComplete == "Y" && isChild == "Y" && isMoney == "Y")
                    {
                        if (item.Cells[(int)ColumnasHijosM.Directo].Value.ToString() != "N")
                        {
                            item.Cells[(int)ColumnasHijosM.Linea].Style.BackColor = Color.FromArgb(218, 150, 148);
                            item.Cells[(int)ColumnasHijosM.Linea].Style.ForeColor = Color.Black;
                            directo += Convert.ToDecimal(item.Cells[(int)ColumnasHijosM.REUBICAR_PADRE_M].Value);
                        }

                        else
                        {
                            item.Cells[(int)ColumnasHijosM.Linea].Style.BackColor = Color.White;
                            item.Cells[(int)ColumnasHijosM.Linea].Style.ForeColor = Color.Black;
                            reubicar += Convert.ToDecimal(item.Cells[(int)ColumnasHijosM.REUBICAR_PADRE_M].Value != null ? item.Cells[(int)ColumnasHijosM.REUBICAR_PADRE_M].Value : decimal.Zero);
                        }

                        total += Convert.ToDecimal(item.Cells[(int)ColumnasHijosM.REUBICAR_PADRE_M].Value);
                    }
                    else
                    {
                       // reubicar += Convert.ToDecimal(item.Cells[(int)ColumasPadresM.r].Value != null ? item.Cells[(int)ColumnasHijosM.REUBICAR_PADRE_M].Value : decimal.Zero);
                    }

                    if (isChild == "Y")
                    {
                        total += Convert.ToDecimal(item.Cells[(int)ColumnasHijos.REUBICAR_PADRE].Value);
                    }
                    if (isChild != "Y")
                    {
                        total += Convert.ToDecimal(item.Cells[(int)ColumnasHijosM.REUBICAR_PADRE_M].Value);
                    }
                }

                DataTable Totales = new DataTable();
                Totales.Columns.Add("Total a reubicar", typeof(decimal));
                Totales.Columns.Add("Entrega directa", typeof(decimal));
                Totales.Columns.Add("Diferencia", typeof(decimal));
                DataRow row = Totales.NewRow();

                row["Total a reubicar"] = total;
                row["Entrega directa"] = directo;
                row["Diferencia"] = reubicar;

                Totales.Rows.Add(row);

                dgvTotales.DataSource = Totales;

            }
            catch (Exception)
            {

            }
        }
        #endregion
    }
}
