using System.Data;
using CapaDeDatos.AccesoDatos;
using CapaNegocio.Entidades;

namespace CapaDeDatos.Interfaces
{
    public class DocenteInterface 
    {
        private readonly SQLManagement _dbQueryManager;

        public DocenteInterface(DBConnection dbConnection)
        {
            _dbQueryManager = new SQLManagement(dbConnection);
        }

        public int Guardar(Docente docente)
        {
            // la sentencia SELECT SCOPE_IDENTITY() permitirá obtener el id generado por el registro
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_nombres", SqlDbType.VarChar, docente.Nombres),
               new Parametro("p_apellidos", SqlDbType.VarChar, docente.Apellidos),
               new Parametro("p_cedula", SqlDbType.VarChar, docente.Cedula),
               new Parametro("p_especialidad", SqlDbType.VarChar, docente.Especialidad)
            };

            int id_generado = _dbQueryManager.EjecutarSP_Scalar("sp_insertarDocente", parametros);

            return id_generado;
        }

        public List<Docente> ObtenerTodos()
        {
            var resultado = _dbQueryManager.EjecutaSP_Query("fn_obtenerDocentesActivos", new List<Parametro>());

            List<Docente> docentes = new List<Docente>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_docente = Convert.ToInt32(fila["id_docente"]);
                string cedula = fila["cedula"].ToString() ?? string.Empty;
                string nombres = fila["nombres"].ToString() ?? string.Empty;
                string apellidos = fila["apellidos"].ToString() ?? string.Empty;
                string especialidad = fila["especialidad"].ToString() ?? string.Empty;
                int estado = Convert.ToInt32(fila["estado"].ToString());

                docentes.Add(new Docente(id_docente, cedula, nombres, apellidos, especialidad, estado == 1 ? true : false));
            }

            return docentes;
        }

        public bool Actualizar(int id, Docente docente)
        {
           List<Parametro> parametros = new List<Parametro>()
           {
               new Parametro("p_id_docente", SqlDbType.Int, id),
               new Parametro("p_nombres", SqlDbType.VarChar, docente.Nombres),
               new Parametro("p_apellidos", SqlDbType.VarChar, docente.Apellidos),
               new Parametro("p_cedula", SqlDbType.VarChar, docente.Cedula),
               new Parametro("p_especialidad", SqlDbType.VarChar, docente.Especialidad),
               new Parametro("p_estado", SqlDbType.Int, docente.Estado ? 1 : 0)
           };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_actualizarDocente", parametros);

            return resultado;
        }

        public bool Eliminar(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_id_docente", SqlDbType.Int, id)
            };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_eliminarDocentePorId", parametros);

            return resultado;
        }

        public Docente? ObtenerPorCedula(string cedulaBuscada)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_cedula", SqlDbType.VarChar, cedulaBuscada)
            };

            var resultado = _dbQueryManager.EjecutaSP_Query("fn_obtenerDocentePorCedula", parametros);

            List<Docente> docentes = new List<Docente>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_docente = Convert.ToInt32(fila["id_docente"]);
                string cedula = fila["cedula"].ToString() ?? string.Empty;
                string nombres = fila["nombres"].ToString() ?? string.Empty;
                string apellidos = fila["apellidos"].ToString() ?? string.Empty;
                string especialidad = fila["especialidad"].ToString() ?? string.Empty;
                int estado = Convert.ToInt32(fila["estado"].ToString());

                docentes.Add(new Docente(id_docente, cedula, nombres, apellidos, especialidad, estado == 1 ? true : false));
            }

            return docentes.Count > 0 ? docentes.ElementAt(0) : null;
        }


    }
}