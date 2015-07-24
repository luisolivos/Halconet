using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Net;

namespace Ventas.Clases
{
    public class Logs
    {
        private string _Modulo;

        public string Modulo
        {
            get { return _Modulo; }
            set { _Modulo = value; }
        }
        private string _Usuario;

        public string Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Logs(string _usuario, string _modulo, int _id)
        {
            _Modulo = _modulo;
            _Usuario = _usuario;
            _ID = _id;

        }

        private string GetID()
        {
            try
            {
                string strHostName = string.Empty;

                strHostName = Dns.GetHostName();

                //System.Security.Principal.WindowsIdentity user;
                //user = System.Security.Principal.WindowsIdentity.GetCurrent();

                //IPHostEntry hostIPs = Dns.GetHostEntry(strHostName);

                // string ip = hostIPs.AddressList.ToString(); ;

                // strHostName += " [" + ip + "]";

                return strHostName;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public int Inicio()
        {
            string PC = this.GetID();

            string query = "Insert into Login Values(@Usuario, GetDate(), NULL, @Reporte, @PC) " +
                "Select SCOPE_IDENTITY()";

            using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.AddWithValue("@Usuario", _Usuario);
                    command.Parameters.AddWithValue("@Reporte", _Modulo);
                    command.Parameters.AddWithValue("@PC", PC);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }

        }


        public void Fin()
        {
            try
            {
                string query = "Update Login Set Fecha2 = GETDATE() Where ID = @ID";

                using (SqlConnection connection = new SqlConnection(ClasesSGUV.Propiedades.conectionSGUV))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        command.CommandType = System.Data.CommandType.Text;
                        command.Parameters.AddWithValue("@ID", _ID);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
