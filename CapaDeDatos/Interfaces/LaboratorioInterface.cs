using System.Data;
using CapaDeDatos.AccesoDatos;
using CapaNegocio.Entidades;

namespace CapaDeDatos.Interfaces
{
    public class LaboratorioInterface 
    {
        private readonly SQLManagement _dbQueryManager;

        public LaboratorioInterface(SQLManagement sqlManagement)
        {
            _dbQueryManager = sqlManagement;
        }

        public int Guardar(Laboratorio laboratorio)
        {
            // la sentencia SELECT SCOPE_IDENTITY() permitirá obtener el id generado por el registro
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("@p_nombre", SqlDbType.VarChar, laboratorio.Nombre),
               new Parametro("@p_capacidad_maxima", SqlDbType.Int, laboratorio.CapacidadMaxima)
            };

            int id_generado = _dbQueryManager.EjecutarSP_Scalar("sp_insertar_laboratorio", parametros);

            return id_generado;
        }

        public Laboratorio? ObtenerPorId(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("@p_id_laboratorio", SqlDbType.Int, id)
            };

            var resultado = _dbQueryManager.EjecutaSP_Query("sp_buscar_laboratorio", parametros);

            List<Laboratorio> laboratorios = new List<Laboratorio>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_laboratorio = Convert.ToInt32(fila["id_laboratorio"]);
                string nombre = fila["nombre"].ToString() ?? string.Empty;
                int capacidad_maxima = Convert.ToInt32(fila["capacidad_maxima"]);
                int estado = Convert.ToInt32(fila["estado"].ToString());

                laboratorios.Add(new Laboratorio(id_laboratorio, nombre, capacidad_maxima, estado));
            }

            return laboratorios.Count > 0 ? laboratorios.ElementAt(0) : null;
        }

        public List<Laboratorio> ObtenerTodos(int? estado_laboratorio = null)
        {
            var parametros = new List<Parametro>();
            if (estado_laboratorio != null) parametros.Add(new Parametro("@p_estado", SqlDbType.Int, estado_laboratorio));
           
            var resultado = _dbQueryManager.EjecutaSP_Query("sp_listar_laboratorios_activos", parametros);



            List<Laboratorio> laboratorios = new List<Laboratorio>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_laboratorio = Convert.ToInt32(fila["id_laboratorio"]);
                string nombre = fila["nombre"].ToString() ?? string.Empty;
                int capacidad_maxima = Convert.ToInt32(fila["capacidad_maxima"]);
                int estado = Convert.ToInt32(fila["estado"].ToString());

                laboratorios.Add(new Laboratorio(id_laboratorio, nombre, capacidad_maxima, estado));
            }

            return laboratorios;
        }

        public bool Actualizar(int id, Laboratorio laboratorio)
        {
           List<Parametro> parametros = new List<Parametro>()
           {
               new Parametro("@p_id_laboratorio", SqlDbType.Int, id),
               new Parametro("@p_nombre", SqlDbType.VarChar, laboratorio.Nombre),
               new Parametro("@p_capacidad_maxima", SqlDbType.Int, laboratorio.CapacidadMaxima),
               new Parametro("@p_estado", SqlDbType.Int, laboratorio.Estado)
           };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_actualizar_laboratorio", parametros);

            return resultado;
        }

        public bool ActualizarEstado(int idLaboratorio, int estado_laboratorio)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("@p_id_laboratorio", SqlDbType.Int, idLaboratorio),
               new Parametro("@p_estado", SqlDbType.Int, estado_laboratorio)
            };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_cambiar_estado_laboratorio", parametros);

            return resultado;
        }
    }
}