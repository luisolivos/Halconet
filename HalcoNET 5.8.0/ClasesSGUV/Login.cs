using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesSGUV
{
    public static class Login
    {
        static string usuario;

        public static string Usuario
        {
            get { return Login.usuario; }
            set { Login.usuario = value; }
        }
        static string sucursal;

        public static string Sucursal
        {
            get { return Login.sucursal; }
            set { Login.sucursal = value; }
        }
        static string nombreUsuario;

        public static string NombreUsuario
        {
            get { return Login.nombreUsuario; }
            set { Login.nombreUsuario = value; }
        }
        static int rol;

        public static int Rol
        {
            get { return Login.rol; }
            set { Login.rol = value; }
        }

        static int Vendedor;

        public static int Vendedor1
        {
            get { return Login.Vendedor; }
            set { Login.Vendedor = value; }
        }

        static string nombreRol;

        public static string NombreRol
        {
            get { return Login.nombreRol; }
            set { Login.nombreRol = value; }
        }

        static int id_Usuario;

        public static int Id_Usuario
        {
            get { return Login.id_Usuario; }
            set { Login.id_Usuario = value; }
        }

        static string _edit;

        public static string Edit
        {
            get { return Login._edit; }
            set { Login._edit = value; }
        }

    }
}
