using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio.Entidades
{
    public class BloqueHorario
    {
        public TimeOnly HoraInicio { get; private set; }
        public TimeOnly HoraFin {  get; private set; }

        public BloqueHorario(TimeOnly horaInicio, TimeOnly horaFin) 
        {
            ValidarFranjaHoraria(horaInicio, horaFin);

            HoraInicio = horaInicio;
            HoraFin = horaFin;
        }

        private void ValidarFranjaHoraria(TimeOnly horaInicio, TimeOnly horaFin)
        {
            if (horaInicio >= horaFin)
                throw new ArgumentException("La hora de inicio del rango horario no puede ser mayor o igual que la hora de fin!");
        }

        public override string ToString()
        {
            return $"{HoraFin}-{HoraFin}";
        }
    }
}
