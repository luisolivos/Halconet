using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Presupuesto
{
    class Constantes
    {
        public enum TipoConsultaTendenciaGasto
        {
            GurpoDirecto = 1,
            GrupoCuentaDirecto = 2,
            SucursalDirecto = 3,
            SucursalDirectoCuenta = 4,
            SucursalDispersado = 5,
            SucursalDispersadoCuenta = 6
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
        //    Kluum = 13
        //}
    }
}
