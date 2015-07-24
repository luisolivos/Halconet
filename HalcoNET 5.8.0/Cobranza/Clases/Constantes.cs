using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cobranza
{
    static class Constantes
    {
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
    }
}
