using System.Data;
using CapaDeDatos.AccesoDatos;
using CapaNegocio.Entidades;

namespace CapaDeDatos.Interfaces
{
    public class LaboratorioInterface 
    {
        private readonly SQLManagement _dbQueryManager;

        public LaboratorioInterface(DBConnection dbConnection)
        {
            _dbQueryManager = new SQLManagement(dbConnection);
        }

        public int Guardar(Laboratorio laboratorio)
        {
            // la sentencia SELECT SCOPE_IDENTITY() permitirá obtener el id generado por el registro
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_nombre", SqlDbType.VarChar, laboratorio.Nombre),
               new Parametro("p_capacidad_maxima", SqlDbType.Int, laboratorio.CapacidadMaxima)
            };

            int id_generado = _dbQueryManager.EjecutarSP_Scalar("sp_insertarLaboratorio", parametros);

            return id_generado;
        }

        public List<Laboratorio> ObtenerTodos()
        {
            var resultado = _dbQueryManager.EjecutaSP_Query("fn_obtenerLaboratoriosActivos", new List<Parametro>());

            List<Laboratorio> laboratorios = new List<Laboratorio>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_laboratorio = Convert.ToInt32(fila["id_laboratorio"]);
                string nombre = fila["nombre"].ToString() ?? string.Empty;
                int capacidad_maxima = Convert.ToInt32(fila["cantidad_estudiante"]);
                int estado = Convert.ToInt32(fila["estado"].ToString());

                laboratorios.Add(new Laboratorio(id_laboratorio, nombre, capacidad_maxima, estado));
            }

            return laboratorios;
        }

        public bool Actualizar(int id, Laboratorio laboratorio)
        {
           List<Parametro> parametros = new List<Parametro>()
           {
               new Parametro("p_id_laboratorio", SqlDbType.Int, id),
               new Parametro("p_nombre", SqlDbType.VarChar, laboratorio.Nombre),
               new Parametro("p_capacidad_maxima", SqlDbType.Int, laboratorio.CapacidadMaxima),
               new Parametro("p_estado", SqlDbType.Int, laboratorio.Estado)
           };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_actualizarLaboratorio", parametros);

            return resultado;
        }

        public bool Eliminar(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_id_laboratorio", SqlDbType.Int, id)
            };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_eliminarLaboratorioPorId", parametros);

            return resultado;
        }

        public Laboratorio? ObtenerPorId(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_id_laboratorio", SqlDbType.Int, id)
            };

            var resultado = _dbQueryManager.EjecutaSP_Query("fn_obtenerLaboratorioPorId", parametros);

            List<Laboratorio> laboratorios = new List<Laboratorio>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_laboratorio = Convert.ToInt32(fila["id_laboratorio"]);
                string nombre = fila["nombre"].ToString() ?? string.Empty;
                int capacidad_maxima = Convert.ToInt32(fila["cantidad_estudiante"]);
                int estado = Convert.ToInt32(fila["estado"].ToString());

                laboratorios.Add(new Laboratorio(id_laboratorio, nombre, capacidad_maxima, estado));
            }

            return laboratorios.Count > 0 ? laboratorios.ElementAt(0) : null;
        }


    }
}