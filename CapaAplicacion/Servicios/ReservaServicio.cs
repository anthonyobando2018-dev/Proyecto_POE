using CapaDeDatos.Interfaces;
using CapaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion.Servicios
{
    public class ReservaServicio
    {
        private readonly ReservaInterface _reservaInterface;
        private readonly LaboratorioInterface _laboratorioInterface;
        private readonly DocenteInterface _docenteInterface;
        public ReservaServicio(
            ReservaInterface reservaInterface,
            LaboratorioInterface laboratorioInterface,
            DocenteInterface docenteInterface
            )
        {
            _reservaInterface = reservaInterface;
            _laboratorioInterface = laboratorioInterface;
            _docenteInterface = docenteInterface;
        }

        public void Reservar(int idDocente, int idLaboratorio, string asunto, int cantidadEstudiantes, DateOnly fechaReserva, TimeOnly horaInicio, TimeOnly horaFin)
        {
            ValidarSolapamientoDeReserva(idDocente, idLaboratorio, fechaReserva, horaInicio, horaFin);
            ValidarCapacidadDisponibleDeLaboratorio(idLaboratorio, cantidadEstudiantes);
            Reserva nuevoReserva = new Reserva(idDocente, idLaboratorio, asunto, cantidadEstudiantes, fechaReserva, new BloqueHorario(horaInicio, horaFin));
            _reservaInterface.Guardar(nuevoReserva);
        }

        public void ActualizarDatos(int idReserva, int idDocente, int idLaboratorio, string asunto, int cantidadEstudiantes, DateOnly fechaReserva, TimeOnly horaInicio, TimeOnly horaFin, int estado)
        {
            ValidarSolapamientoDeReserva(idDocente, idLaboratorio, fechaReserva, horaInicio, horaFin);
            ValidarCapacidadDisponibleDeLaboratorio(idLaboratorio, cantidadEstudiantes);
            Reserva reservaActualizada = new Reserva(idReserva, idDocente, idLaboratorio, asunto, cantidadEstudiantes, fechaReserva, new BloqueHorario(horaInicio, horaFin), estado);
            _reservaInterface.Actualizar(idReserva, reservaActualizada);
        }

        public Reserva BuscarPorId(int id)
        {
            return _reservaInterface.ObtenerPorId(id) ?? throw new ApplicationException("Reserva no encontrado con ese id.");
        }

        public List<Reserva> ListarTodos()
        {
            return _reservaInterface.ObtenerTodos();
        }

        private void ValidarCapacidadDisponibleDeLaboratorio(int idLaboratorio, int cantidadEstudiantes)
        {
            var labService = new LaboratorioServicio(_laboratorioInterface);
        }
    }
}
