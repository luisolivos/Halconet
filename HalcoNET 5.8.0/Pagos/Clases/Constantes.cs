using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Pagos
{
    static class Constantes
    {


        /// <summary>  
        /// Enumerador para los tipos de consulta 
        /// </summary>
         public enum TipoConsultaSGUV
        {
            Agregar = 1,
            Modificar = 2,
            Eliminar = 3,
            Verificar = 4,
            ConsultarTodos = 5,
            ConsultarPorUsuario = 6,
            ConsultarSiExisteNombreUsuario = 7,
            ConsultarSiExisteNombreUsuarioYClave = 8
        }

        ///// <summary>
        ///// Roles Nuevos del sistema
        ///// </summary>
        //public enum RolesSistemaSGUV
        //{
        //    Manager = 0,
        //    Administrador = 1,
        //    GerenteVentas = 2,
        //    GerenteSucursal = 3,
        //    VentasEspecial = 4,
        //    Ventas = 5,
        //    GerenteCobranza = 6,
        //    JefasCobranza = 7,
        //    Descargas = 8,
        //    Presupuesto = 9,
        //    Compras = 10,
        //    Telemarketing = 11,
        //    Proveedores = 12,
        //    Kluum = 13,
        //    Caja = 14,
        //    PagosGastos = 15
        //}


        /// <summary>
        /// Indica el resultado de la verificación si ya existe un usuario con el 
        /// mismo nombre en la BD
        /// </summary>
        public enum ResultadoVerficacionUsuario
        {
            NoExisteElUsuario = 0,
            SiExisteElUsuario = 1
        }

        /// <summary>
        /// Enumerador para los tipos de consulta
        /// </summary>
        public enum TipoConsultaPJ
        {
            ConsultaDeFacturas = 1,
            ConsultaDeCobranza = 2,
            ConsultaDetallesFactura = 3,
            ConsultaDetallesCobranza = 4,
            ConsultaReporte = 5,
            CargarArticulo = 6,
            ModificarListaPrecios = 7,
            ActualizarPrecioEspecial = 8,
            NoPrecioEspecial = 9,
            ConsultaMoneda = 10,
            ActualizaPCB = 11,

            //Gran canal
            Trasporte = 60,
            Mayoreo = 61,
            CuentasClave = 51,
            CuentasClaveTransporte = 5160,
            CuentasClaveMayoreo = 5161,
            TransporteMayoreo = 6061,
            Todos = 0
        }

        private static string fechaDesde;

        public static string FechaDesde
        {
            get { return Constantes.fechaDesde; }
            set { Constantes.fechaDesde = value; }
        }
        private static string fechaHasta;

        public static string FechaHasta
        {
            get { return Constantes.fechaHasta; }
            set { Constantes.fechaHasta = value; }
        }

        /// <summary>
        /// Enumerador para los tipos de consulta Varias
        /// </summary>
        public enum ConsultasVariasPJ
        {
            Vendedores = 1,
            GrupoDeClientes = 2,
            Canal = 3,
            Linea = 4,
            GranCanal = 5,
            Sucursales = 6,
            JefesCobranza = 7
        }

        /// <summary>
        /// Enumerador para los meses
        /// </summary>
        public enum Meses
        {
            Enero = 1,
            Febrero = 2,
            Marzo = 3,
            Abril = 4,
            Mayo = 5,
            Junio = 6,
            Julio = 7,
            Agosto = 8,
            Septiembre = 9,
            Octubre = 10,
            Noviembre = 11,
            Diciembre = 12
        }

        public enum TipoConsultaObjetivos
        {
            Insertar = 1,
            Consultar = 2
        }


        public static bool IsBetween<T>(this T item, T start, T end)
        {
            return Comparer<T>.Default.Compare(item, start) >= 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }
    }
}
