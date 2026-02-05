using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace CapaDatos.SQL
{
    internal class Cl_Conecction
    {
        internal class Cl_Connection
        {
            private static string cadena_conexion = "server=XXXXXX; database=XXXXX; Integrated Security=true";
            private SqlConnection connection_db = new SqlConnection(cadena_conexion);

            //private static string cadena_conexion_sql = "server=ANTHONY; database=DB_POE1; Integrated Security=true";
            //establecer conexion con la base de datos
            internal SqlConnection openConnection()
            {
                if (connection_db.State == System.Data.ConnectionState.Closed)
                    connection_db.Open();

                return connection_db;
            }
            //cerrar la conexión con la base de datos
            internal SqlConnection closeConnection()
            {
                if (connection_db.State == System.Data.ConnectionState.Open)
                    connection_db.Close();

                return connection_db;
            }

        }
    }
}
