using System.Data;
using CapaDeDatos.AccesoDatos;
using CapaNegocio.Entidades;

namespace CapaDeDatos.Interfaces
{
    public class ReservaInterface 
    {
        private readonly SQLManagement _dbQueryManager;

        public ReservaInterface(DBConnection dbConnection)
        {
            _dbQueryManager = new SQLManagement(dbConnection);
        }
        // id resreva, id docente, id lab, asunto, cantidad est, fecha reserva, hora inicio, hora fin, estado
        public int Guardar(Reserva reserva)
        {
            // la sentencia SELECT SCOPE_IDENTITY() permitirá obtener el id generado por el registro
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_id_docente", SqlDbType.Int, reserva.IdDocente),
               new Parametro("p_id_reserva", SqlDbType.Int, reserva.IdReserva),
               new Parametro("p_asunto", SqlDbType.VarChar, reserva.Asunto),
               new Parametro("p_cantidad_estudiantes", SqlDbType.Int, reserva.CantidadEstudiantes),
               new Parametro("p_fecha_reserva", SqlDbType.Date, reserva.FechaReserva),
               new Parametro("p_hora_inicio", SqlDbType.Time, reserva.Horario.HoraInicio),
               new Parametro("p_hora_fin", SqlDbType.Time, reserva.Horario.HoraFin)
            };

            int id_generado = _dbQueryManager.EjecutarSP_Scalar("sp_insertarReserva", parametros);

            return id_generado;
        }

        public List<Reserva> ObtenerTodos()
        {
            var resultado = _dbQueryManager.EjecutaSP_Query("fn_obtenerReservasActivos", new List<Parametro>());

            List<Reserva> reservas = new List<Reserva>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_reserva = Convert.ToInt32(fila["id_reserva"]);
                int id_docente = Convert.ToInt32(fila["id_docente"]);
                int id_laboratorio = Convert.ToInt32(fila["id_laboratorio"]);
                string asunto = fila["asunto"].ToString() ?? string.Empty;
                DateOnly fecha_reserva = DateOnly.FromDateTime(DateTime.Parse(fila["fecha_reserva"].ToString() ?? string.Empty));                
                int cantidad_estudiantes = Convert.ToInt32(fila["cantidad_estudiantes"]);
                TimeOnly hora_inicio = TimeOnly.Parse(fila["hora_inicio"].ToString() ?? string.Empty);
                TimeOnly hora_fin = TimeOnly.Parse(fila["hora_fin"].ToString() ?? string.Empty);
                int estado = Convert.ToInt32(fila["estado"].ToString());

                reservas.Add(
                    new Reserva(id_reserva, 
                        id_docente, 
                        id_laboratorio,
                        asunto, 
                        cantidad_estudiantes, 
                        fecha_reserva,
                        new BloqueHorario(hora_inicio, hora_fin), 
                        estado));
            }

            return reservas;
        }

        public bool Actualizar(int id, Reserva reserva)
        {
           List<Parametro> parametros = new List<Parametro>()
           {
               new Parametro("p_id_reserva", SqlDbType.Int, id),
               new Parametro("p_id_docente", SqlDbType.Int, reserva.IdDocente),
               new Parametro("p_id_reserva", SqlDbType.Int, reserva.IdReserva),
               new Parametro("p_asunto", SqlDbType.VarChar, reserva.Asunto),
               new Parametro("p_cantidad_estudiantes", SqlDbType.Int, reserva.CantidadEstudiantes),
               new Parametro("p_fecha_reserva", SqlDbType.Date, reserva.FechaReserva),
               new Parametro("p_hora_inicio", SqlDbType.Time, reserva.Horario.HoraInicio),
               new Parametro("p_hora_fin", SqlDbType.Time, reserva.Horario.HoraFin),
               new Parametro("p_estado", SqlDbType.Int, reserva.Estado)
           };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_actualizarReserva", parametros);

            return resultado;
        }

        public bool Eliminar(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_id_reserva", SqlDbType.Int, id)
            };

            var resultado = _dbQueryManager.EjecutaSP_NonQuery("sp_eliminarReservaPorId", parametros);

            return resultado;
        }

        public Reserva? ObtenerPorId(int id)
        {
            List<Parametro> parametros = new List<Parametro>()
            {
               new Parametro("p_id_reserva", SqlDbType.Int, id)
            };

            var resultado = _dbQueryManager.EjecutaSP_Query("fn_obtenerReservaPorId", parametros);

            List<Reserva> reservas = new List<Reserva>();

            foreach (DataRow fila in resultado.Rows)
            {
                int id_reserva = Convert.ToInt32(fila["id_reserva"]);
                int id_docente = Convert.ToInt32(fila["id_docente"]);
                int id_laboratorio = Convert.ToInt32(fila["id_laboratorio"]);
                string asunto = fila["asunto"].ToString() ?? string.Empty;
                DateOnly fecha_reserva = DateOnly.FromDateTime(DateTime.Parse(fila["fecha_reserva"].ToString() ?? string.Empty));
                int cantidad_estudiantes = Convert.ToInt32(fila["cantidad_estudiantes"]);
                TimeOnly hora_inicio = TimeOnly.Parse(fila["hora_inicio"].ToString() ?? string.Empty);
                TimeOnly hora_fin = TimeOnly.Parse(fila["hora_fin"].ToString() ?? string.Empty);
                int estado = Convert.ToInt32(fila["estado"].ToString());

                reservas.Add(
                    new Reserva(id_reserva,
                        id_docente,
                        id_laboratorio,
                        asunto,
                        cantidad_estudiantes,
                        fecha_reserva,
                        new BloqueHorario(hora_inicio, hora_fin),
                        estado));
            }

            return reservas.Count > 0 ? reservas.ElementAt(0) : null;
        }
    }
}