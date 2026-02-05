using System;

namespace SRLCProyectoPOE.Entidades
{
    public class Reserva
    {
        // Propiedades de la clase Reserva
        public int IdReserva { get; set; }
        public int IdDocente { get; set; }
        public int IdLaboratorio { get; set; }
        public string Asunto {  get; set; }
        public int CantidadEstudiantes { get; set; }
        public DateOnly FechaReserva { get; set; } // DateOnly para guardar fecha formato yyyy-mm-dd
        public BloqueHorario Horario { get; set; } // Guardar hora de inicio y hora de fin
        public int Estado { get; set; }

        // constructor parametrizado
        public Reserva(int idDocente, int idLaboratorio, string asunto,
            int cantidadEstudiantes, DateOnly fechaReserva, BloqueHorario bloqueHorario)
        {
            IdDocente = idDocente;
            IdLaboratorio = idLaboratorio;
            Asunto = asunto;
            CantidadEstudiantes = cantidadEstudiantes;
            FechaReserva = fechaReserva;
            Horario = bloqueHorario;
            Estado = 0;
        }

        // ToString retornando los valores de instancia
        public override string ToString()
        {
            return $"{IdReserva};{IdDocente};{IdLaboratorio};{Asunto};{CantidadEstudiantes};{FechaReserva};{Horario.HoraInicio};{Horario.HoraFin}";
        }
    }
}
