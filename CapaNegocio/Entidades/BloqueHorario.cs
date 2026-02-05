using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLCProyectoPOE.Entidades
{
    public class BloqueHorario
    {
        public TimeOnly HoraInicio { get; set; }
        public TimeOnly HoraFin {  get; set; }

        public BloqueHorario(TimeOnly horaInicio, TimeOnly horaFin) 
        {
            HoraInicio = horaInicio;
            HoraFin = horaFin;
        }

        public override string ToString() 
        {
            return $"{HoraInicio}-{HoraFin}";
        }
    }
}
