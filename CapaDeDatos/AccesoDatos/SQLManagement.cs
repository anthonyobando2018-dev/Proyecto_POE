using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace CapaDeDatos.AccesoDatos
{
    public class SQLManagement
    {
        private readonly DBConnection _dbConnection;

        public SQLManagement(DBConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // consulta que modifica la base datos y no retorna valores (delete, update)
        internal bool EjecutaSP_NonQuery(string nombre_sp, List<Parametro> parametros)
        {
            using (var conexion = _dbConnection.CrearConexion())
            {

                using (var comando = new SqlCommand(nombre_sp, conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    /* Asignacion dinamica de parametros */
                    parametros.ForEach((parametro) =>
                        comando.Parameters.Add(parametro.Nombre, parametro.TipoDato).Value = parametro.Valor
                    );

                    conexion.Open();

                    int numColumnasAfectadas = comando.ExecuteNonQuery();

                    conexion.Close();

                    return numColumnasAfectadas > 0;
                }
            }
        }

        // consulta que trae datos de la base de datos (SELECT)
        internal DataTable EjecutaSP_Query(string nombre_sp, List<Parametro> parametros)
        {
            using (var conexion = _dbConnection.CrearConexion())
            {
                Console.WriteLine("conexion exitos");
                using (var comando = new SqlCommand(nombre_sp, conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    /* Asignacion dinamica de parametros */
                    parametros.ForEach((parametro) =>
                        comando.Parameters.Add(parametro.Nombre, parametro.TipoDato).Value = parametro.Valor
                    );

                    using (var table = new DataTable())
                    {
                        conexion.Open();

                        using (var reader = comando.ExecuteReader())
                        {
                            table.Load(reader);
                        }

                        conexion.Close();
                        
                        return table;
                    }
                }
            }
        }

        // consulta que inserta datos y retorna id
        internal int EjecutarSP_Scalar(string nombre_sp, List<Parametro> parametros)
        {
            using (var conexion = _dbConnection.CrearConexion())
            {
                using (var comando = new SqlCommand(nombre_sp, conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;

                    /* Asignacion dinamica de parametros */
                    parametros.ForEach((parametro) =>
                        comando.Parameters.Add(parametro.Nombre, parametro.TipoDato).Value = parametro.Valor
                    );

                    conexion.Open();

                    var idNuevo = comando.ExecuteScalar();

                    conexion.Close();

                    return Convert.ToInt32(idNuevo);
                }
            }
        }
    }
}
