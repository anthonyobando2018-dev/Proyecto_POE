using System.Data;
using CapaDeDatos.AccesoDatos;
using CapaNegocio.Entidades;

namespace CapaDeDatos.Interfaces
{
    public class DocenteInterface 
    {
        private readonly SQLManagement _dbQueryManager;

        public DocenteInterface(SQLManagement sqlManagement)
        {
            _dbQueryManager = sqlManagement;
        }

        public int Guardar(Docente docente)
        {
            // la sentencia SELECT SCOPE_IDENTITY() permitirá obtener el id generado por el registro
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("@p_nombres", SqlDbType.VarChar, docente.Nombres),
               new Parametro("@p_apellidos", SqlDbType.VarChar, docente.Apellidos),
               new Parametro("@p_cedula", SqlDbType.VarChar, docente.Cedula),
               new Parametro("@p_especialidad", SqlDbType.VarChar, docente.Especialidad)
            };

            int id_generado = _dbQueryManager.EjecutarSP_Scalar("sp_insertar_docente", parametros);

            return id_generado;
        }

        public Docente? ObtenerPorCedula(string cedulaBuscada)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("@p_cedula", SqlDbType.VarChar, cedulaBuscada)
            };

            var resultado = _dbQueryManager.EjecutaSP_Query("sp_buscar_docente", parametros);

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

        public Docente? ObtenerPorId(int idDocente)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("@p_id_docente", SqlDbType.Int, idDocente)
            };

            var resultado = _dbQueryManager.EjecutaSP_Query("sp_buscar_docente", parametros);

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

        public List<Docente> ObtenerTodos(int? estado_docente = null)
        {
            var parametros = new List<Parametro>();
            if (estado_docente != null) parametros.Add(new Parametro("@p_estado", SqlDbType.Int, estado_docente));

            var resultado = _dbQueryManager.EjecutaSP_Query("sp_listar_docentes_activos", parametros);

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

        public bool Actualizar(int idDocente, Docente docente)
        {
           List<Parametro> parametros = new List<Parametro>()
           {
               new Parametro("@p_id_docente", SqlDbType.Int, idDocente),
               new Parametro("@p_nombres", SqlDbType.VarChar, docente.Nombres),
               new Parametro("@p_apellidos", SqlDbType.VarChar, docente.Apellidos),
               new Parametro("@p_cedula", SqlDbType.VarChar, docente.Cedula),
               new Parametro("@p_especialidad", SqlDbType.VarChar, docente.Especialidad),
               new Parametro("@p_estado", SqlDbType.Int, docente.Estado ? 1 : 0)
           };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_actualizar_docente", parametros);

            return resultado;
        }

        public bool ActualizarEstado(int idDocente, int estado_docente)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("@p_id_docente", SqlDbType.Int, idDocente),
               new Parametro("@p_estado",  SqlDbType.Int, estado_docente)
            };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_cambiar_estado_docente", parametros);

            return resultado;
        }


    }
}