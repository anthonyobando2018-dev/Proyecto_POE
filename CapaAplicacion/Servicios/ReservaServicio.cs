using CapaDeDatos.Interfaces;
using CapaNegocio.Entidades;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion.Servicios
{
    public class ReservaServicio
    {
        private readonly ReservaInterface _reservaInterface;
        private readonly ReservaValidadorServicio _reservaValidador;

        public ReservaServicio(
            ReservaInterface reservaInterface,
            LaboratorioInterface laboratorioInterface
            )
        {
            _reservaInterface = reservaInterface;
            _reservaValidador = new ReservaValidadorServicio(reservaInterface, laboratorioInterface);
        }

        public int Reservar(int idDocente, int idLaboratorio, string asunto, int cantidadEstudiantes, DateOnly fechaReserva, TimeOnly horaInicio, TimeOnly horaFin)
        {
            _reservaValidador.ValidarReservaNoSolapada(idDocente, idLaboratorio, fechaReserva, horaInicio, horaFin);
            _reservaValidador.ValidarReservaNoExcedeCapacidadDeLaboratorio(idLaboratorio, cantidadEstudiantes);
            Reserva nuevoReserva = new Reserva(idDocente, idLaboratorio, asunto, cantidadEstudiantes, fechaReserva, new BloqueHorario(horaInicio, horaFin));
            return _reservaInterface.Guardar(nuevoReserva);
        }

        public void ActualizarDatos(int idReserva, int idDocente, int idLaboratorio, string asunto, int cantidadEstudiantes, DateOnly fechaReserva, TimeOnly horaInicio, TimeOnly horaFin, int estado)
        {
            _reservaValidador.ValidarReservaNoSolapada(idDocente, idLaboratorio, fechaReserva, horaInicio, horaFin, idReserva);
            _reservaValidador.ValidarReservaNoExcedeCapacidadDeLaboratorio(idLaboratorio, cantidadEstudiantes);
            Reserva reservaActualizada = new Reserva(idReserva, idDocente, idLaboratorio, asunto, cantidadEstudiantes, fechaReserva, new BloqueHorario(horaInicio, horaFin), estado);
            _reservaInterface.Actualizar(idReserva, reservaActualizada);
        }

        public Reserva BuscarPorId(int id)
        {
            return _reservaInterface.ObtenerPorId(id) ?? throw new ApplicationException("Reserva no encontrado con ese id.");
        }

        public List<Reserva> ListarTodas(int? estado = null)
        {
            return _reservaInterface.ObtenerTodos(estado);
        }

        /* Actualiza a estado cancelado (solo reservas activas) */
        public void Cancelar(int idReserva)
        {
            _reservaInterface.FiltrarPorParametros(estado_reserva: 1);
            if (!_reservaInterface.ActualizarEstado(idReserva, estado: 2))
                throw new ApplicationException("La reserva no se puede cancelar porque: no existe, esta cancelada o finalizada actualmente.");
        }

        /* Actualiza a estado finalizado (solo reservas activas) */
        public void Finalizar(int idReserva)
        {
            _reservaInterface.FiltrarPorParametros(estado_reserva: 1);
            if (!_reservaInterface.ActualizarEstado(idReserva, estado: 3))
                throw new ApplicationException("La reserva no se puede finalizar porque: no existe, esta cancelada o finalizada actualmente.");
        }
    }
}
