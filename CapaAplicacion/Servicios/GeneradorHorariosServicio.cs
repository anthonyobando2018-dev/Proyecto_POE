using CapaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion.Servicios
{
    public static class GeneradorHorariosServicio
    {
        public static List<BloqueHorario> GenerarHorarios(int inicioJornada, int finJornada, double duracion = 60)
        {
            var bloquesHorarios = new List<BloqueHorario>();
            if (duracion < 10) throw new ApplicationException("La duracion del turno no puede ser menor a 10 minutos!");
            if (inicioJornada < 0 || inicioJornada >= 24) throw new ApplicationException("Valor no valido para inicio de jornada!");
            if (inicioJornada < 0 || inicioJornada >= 24) throw new ApplicationException("Valor no valido para fin de jornada!");
            if (inicioJornada >= finJornada) throw new ApplicationException("El inicio de jornada no puede ser mayor igual que el inicio de jornada!");
            // numero de horarios
            int numBloques = Convert.ToInt32((finJornada - inicioJornada) / (duracion / 60));

            TimeOnly horaInicio, horaFin;
            horaInicio = new TimeOnly(inicioJornada, 0);

            for (int i = 0; i < numBloques; i++)
            {
                horaFin = horaInicio.AddMinutes(duracion);
                bloquesHorarios.Add(new BloqueHorario(horaInicio, horaFin));
                horaInicio = horaFin;
            }

            return bloquesHorarios;
        }
    }
}
