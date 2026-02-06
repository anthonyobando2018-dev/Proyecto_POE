using System.Data;
using CapaDatos.AccesoDatos;
using 

namespace CapaDatos.Interfaces
{
    public class EstudianteInterface 
    {
        private readonly DBQueryManager _dbQueryManager;

        public EstudianteInterface(DBConnection dbConnection)
        {
            _dbQueryManager = new DBQueryManager(dbConnection);
        }

        public bool Actualizar(int id, Docente docente)
        {
           List<Parametro> parametros = new List<Parametro>()
           {
               new Parametro("p_id_estudiante", SqlDbType.Int, estudiante.Id),
               new Parametro("p_nombres", SqlDbType.VarChar, estudiante.Nombres),
               new Parametro("p_apellidos", SqlDbType.VarChar, estudiante.Apellidos),
               new Parametro("p_cedula", SqlDbType.VarChar, estudiante.Cedula),
               new Parametro("p_direccion", SqlDbType.VarChar, estudiante.Direccion),
               new Parametro("p_genero", SqlDbType.Char, estudiante.Genero),
               new Parametro("p_fecha_nacimiento", SqlDbType.Date, DateTime.Parse(estudiante.FechaNacimiento.ToString()))
           };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_actualizarEstudiante", parametros);

            return resultado;
        }

        public bool Eliminar(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_id_estudiante", SqlDbType.Int, id)
            };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_eliminarEstudiantePorId", parametros);

            return resultado;
        }

        public Estudiante? ObtenerPorCedula(string cedulaBuscada)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_cedula", SqlDbType.VarChar, cedulaBuscada)
            };

            var resultado = _dbQueryManager.EjecutaSP_Query("fn_obtenerEstudiantePorCedula", parametros);

            List<Estudiante> estudiantes = new List<Estudiante>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_estudiante = Convert.ToInt32(fila["id_estudiante"]);
                string cedula = fila["cedula"].ToString() ?? string.Empty;
                string nombres = fila["nombres"].ToString() ?? string.Empty;
                string apellidos = fila["apellidos"].ToString() ?? string.Empty;
                string direccion = fila["direccion"].ToString() ?? string.Empty;
                char genero = (fila["genero"].ToString() ?? string.Empty)[0];
                DateOnly fecha_nacimiento = DateOnly.Parse(fila["fecha_nacimiento"].ToString() ?? string.Empty);
                bool activo = Convert.ToInt32(fila["estado"].ToString()) == 1 ? true : false;

                estudiantes.Add(new Estudiante(id_estudiante, nombres, apellidos, cedula, direccion, genero, fecha_nacimiento, activo));
            }

            return estudiantes.Count > 0 ? estudiantes.ElementAt(0) : null;
        }

        public int Guardar(Estudiante estudiante)
        {
            // la sentencia SELECT SCOPE_IDENTITY() permitirá obtener el id generado por el registro
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_nombres", SqlDbType.VarChar, estudiante.Nombres),
               new Parametro("p_apellidos", SqlDbType.VarChar, estudiante.Apellidos),
               new Parametro("p_cedula", SqlDbType.VarChar, estudiante.Cedula),
               new Parametro("p_direccion", SqlDbType.VarChar, estudiante.Direccion),
               new Parametro("p_genero", SqlDbType.Char, estudiante.Genero),
               new Parametro("p_fecha_nacimiento", SqlDbType.Date, DateTime.Parse(estudiante.FechaNacimiento.ToString()))
            };

            int id_generado = _dbQueryManager.EjecutarSPScalar("sp_insertarEstudiante", parametros);

            return id_generado;
        }

        public Estudiante? ObtenerPorId(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_id", SqlDbType.Int, id)
            };

            var resultado = _dbQueryManager.EjecutaSP_Query("fn_obtenerEstudiantePorId", parametros);

            List<Estudiante> estudiantes = new List<Estudiante>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_estudiante = Convert.ToInt32(fila["id_estudiante"]);
                string cedula = fila["cedula"].ToString() ?? string.Empty;
                string nombres = fila["nombres"].ToString() ?? string.Empty;
                string apellidos = fila["apellidos"].ToString() ?? string.Empty;
                string direccion = fila["direccion"].ToString() ?? string.Empty;
                char genero = (fila["genero"].ToString() ?? string.Empty)[0];
                DateOnly fecha_nacimiento = DateOnly.FromDateTime(DateTime.Parse(fila["fecha_nacimiento"].ToString() ?? string.Empty));
                bool activo = Convert.ToInt32(fila["estado"].ToString()) == 1 ? true : false;

                estudiantes.Add(new Estudiante(id_estudiante, nombres, apellidos, cedula, direccion, genero, fecha_nacimiento, activo));
            }

            return estudiantes.Count > 0 ? estudiantes.ElementAt(0) : null;
        }

        public IReadOnlyList<Estudiante> ObtenerTodos()
        {
            var resultado = _dbQueryManager.EjecutaSP_Query("fn_obtenerEstudiantesActivos", new List<Parametro>());

            List<Estudiante> estudiantes = new List<Estudiante>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_estudiante = Convert.ToInt32(fila["id_estudiante"]);
                string cedula = fila["cedula"].ToString() ?? string.Empty;
                string nombres = fila["nombres"].ToString() ?? string.Empty;
                string apellidos = fila["apellidos"].ToString() ?? string.Empty;
                string direccion = fila["direccion"].ToString() ?? string.Empty;
                char genero = (fila["genero"].ToString() ?? string.Empty)[0];
                DateOnly fecha_nacimiento = DateOnly.FromDateTime(DateTime.Parse(fila["fecha_nacimiento"].ToString() ?? string.Empty));
                bool activo = Convert.ToInt32(fila["estado"].ToString()) == 1 ? true : false;

                estudiantes.Add(new Estudiante(id_estudiante, cedula, nombres, apellidos, direccion, genero, fecha_nacimiento, activo));
            }

            return estudiantes.AsReadOnly();
        }
    }
}