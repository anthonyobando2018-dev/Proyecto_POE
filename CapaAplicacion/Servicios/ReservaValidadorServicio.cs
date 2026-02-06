using CapaDeDatos.Interfaces;
using CapaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion.Servicios
{
    internal class ReservaValidadorServicio
    {
        private readonly ReservaInterface _reservaInterface;
        private readonly LaboratorioInterface _laboratorioInterface;

        public ReservaValidadorServicio(
            ReservaInterface reservaInterface,
            LaboratorioInterface laboratorioInterface)
        {
            _reservaInterface = reservaInterface;
            _laboratorioInterface = laboratorioInterface;
        }

        /* Valida que la cantidad de estudiantes solicitada no exceda la capacidad del laboratorio */
        public void ValidarReservaNoExcedeCapacidadDeLaboratorio(int idLaboratorio, int cantidadEstudiantesSolicitada)
        {
            var labService = new LaboratorioServicio(_laboratorioInterface);
            Laboratorio laboratorioAReservar = labService.BuscarPorId(idLaboratorio);
            if (laboratorioAReservar.CapacidadMaxima < cantidadEstudiantesSolicitada)
                throw new ApplicationException("La cantidad de estudiantes de la reserva excede la capacidad del laboratorio.");
        }

        /* Validaciones varias que debe cumplir una nueva reserva */
        public void ValidarReservaNoSolapada(int idDocente, int idLaboratorio, DateOnly fechaReserva, TimeOnly horaInicio, TimeOnly horaFin)
        {
            ValidarLaboratorioNoOcupado(idLaboratorio, fechaReserva, horaInicio, horaFin);
            ValidarDocenteNoOcupado(idDocente, fechaReserva, horaInicio, horaFin);
            ValidarDocenteNoReservaMismoLaboratorioMismaFecha(idDocente, idLaboratorio, fechaReserva);
        }

        /* Valida que el laboratorio no este siendo reservado ya en la franja horaria de la nueva reserva */
        private void ValidarLaboratorioNoOcupado(int idLaboratorio, DateOnly fechaReserva, TimeOnly horaInicio, TimeOnly horaFin)
        {
            var reservas = _reservaInterface.FiltrarPorParametros(idLaboratorio: idLaboratorio, fechaEspecifica: fechaReserva);
            if (!ReservaEstaAntesODespues(horaInicio, horaFin, reservas))
                throw new ApplicationException("El laboratorio se encuentra ocupado en esa franja horaria.");
        }

        /* Valida que la nueva reserva no solape las reservas actuales del docente */
        private void ValidarDocenteNoOcupado(int idDocente, DateOnly fechaReserva, TimeOnly horaInicio, TimeOnly horaFin)
        {
            var reservas = _reservaInterface.FiltrarPorParametros(idDocente: idDocente, fechaEspecifica: fechaReserva);
            if (!ReservaEstaAntesODespues(horaInicio, horaFin, reservas))
                throw new ApplicationException("El docente tiene reservas en esa franja horaria.");
        }

        /* Valida que el docente no reserve un mismo laboratorio en una misma fecha mas de una vez */
        private void ValidarDocenteNoReservaMismoLaboratorioMismaFecha(int idDocente, int idLaboratorio, DateOnly fechaReserva)
        {
            var reservas = _reservaInterface.FiltrarPorParametros(idDocente, idLaboratorio, fechaReserva);
            if (reservas.Count > 0)
                throw new ApplicationException("El docente no puede reservar un laboratorio mas de una vez el mismo dia.");
        }

        /* Verifica que la reserva entrante este antes o despues de las actuales */
        private bool ReservaEstaAntesODespues(TimeOnly horaInicio, TimeOnly horaFin, List<Reserva> reservasActuales)
        {
            return reservasActuales.All((reserva) =>
                (horaInicio < reserva.Horario.HoraInicio
                && horaFin <= reserva.Horario.HoraInicio)
                || horaInicio >= reserva.Horario.HoraFin);
        }
    }
}
