using System;
using System.Text.RegularExpressions;

namespace CapaNegocio.Entidades
{
    public class Reserva
    {
        // Propiedades de la clase Reserva
        public int IdReserva { get; set; }
        public int IdDocente { get; set; }
        public int IdLaboratorio { get; set; }
        public string Asunto { get; set; }
        public int CantidadEstudiantes { get; set; }
        public DateOnly FechaReserva { get; set; } // DateOnly para guardar fecha formato yyyy-mm-dd
        public BloqueHorario Horario { get; set; } // Guardar hora de inicio y hora de fin
        public int Estado { get; set; }

        // constructor parametrizado
        public Reserva(
            int idDocente,
            int idLaboratorio,
            string asunto,
            int cantidadEstudiantes,
            DateOnly fechaReserva,
            BloqueHorario bloqueHorario)
        {
            ValidarDatosDeReserva(asunto, cantidadEstudiantes, fechaReserva, bloqueHorario);

            IdDocente = idDocente;
            IdLaboratorio = idLaboratorio;
            Asunto = asunto;
            CantidadEstudiantes = cantidadEstudiantes;
            FechaReserva = fechaReserva;
            Horario = bloqueHorario;
            Estado = 1;
        }

        // constructor parametrizado
        public Reserva(
            int idReserva,
            int idDocente,
            int idLaboratorio,
            string asunto,
            int cantidadEstudiantes,
            DateOnly fechaReserva,
            BloqueHorario bloqueHorario,
            int estado)
        {
            IdReserva = idReserva;
            IdDocente = idDocente;
            IdLaboratorio = idLaboratorio;
            Asunto = asunto;
            CantidadEstudiantes = cantidadEstudiantes;
            FechaReserva = fechaReserva;
            Horario = bloqueHorario;
            Estado = estado;
        }

        public void ValidarDatosDeReserva(string asunto, int cantidadEstudiantes, DateOnly fechaReserva, BloqueHorario bloqueHorario)
        {
            ValidarAsuntoDeReserva(asunto);
            ValidarCantidadDeEstudiantes(cantidadEstudiantes);
            ValidarFechaDeReserva(fechaReserva);
            ValidarBloqueHorario(bloqueHorario);
        }

        private void ValidarBloqueHorario(BloqueHorario horario)
        {
            if (horario.HoraInicio.AddMinutes(30) > horario.HoraFin || horario.HoraInicio.AddHours(2) < horario.HoraFin)
                throw new ArgumentException("La sesion minima de una reserva es de minimo 30 minutos y maximo 2 horas");
        }

        private void ValidarFechaDeReserva(DateOnly fechaReserva)
        {
            if (fechaReserva < DateOnly.FromDateTime(DateTime.Now))
                throw new ArgumentException("No se puede reservar una reserva antes de la fecha actual.");
        }

        private void ValidarAsuntoDeReserva(string asunto)
        {
            string patronAsunto = @"^[A-Za-zÁÉÍÓÚÜáéíóúüÑñ\s]{10,100}$";
            if (!Regex.IsMatch(asunto, patronAsunto))
                throw new ArgumentException("El asunto de la reserva no tienen un formato valido!");
        }

        private void ValidarCantidadDeEstudiantes(int cantidadEstudiantes)
        {
            if (cantidadEstudiantes <= 10 || cantidadEstudiantes > 100)
                throw new ArgumentException("La cantidad de estudiantes de la reserva debe ser entre 10 minimo y maximo 100!");
        }

        public override string ToString()
        {
            string estado = Estado == 1 ? "Activa" : (Estado == 2 ? "Cancelada" : "Finalizada");
            return $"{IdReserva};{IdDocente};{IdLaboratorio};{Asunto};{CantidadEstudiantes};{FechaReserva};{Horario};{Estado}";
        }
    }
}
