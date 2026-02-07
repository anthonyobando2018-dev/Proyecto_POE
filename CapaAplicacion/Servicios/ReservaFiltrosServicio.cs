using CapaDeDatos.Interfaces;
using CapaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion.Servicios
{
    public class ReservaFiltrosServicio
    {
        private readonly ReservaInterface _reservaInterface;

        public ReservaFiltrosServicio(ReservaInterface reservaInterface)
        {
            _reservaInterface = reservaInterface;
        }

        /* Filtra las reservas dentro de un rango de fecha */
        public List<Reserva> FiltrarPorRangoDeFecha(DateOnly fechaMin, DateOnly fechaMax)
        {
            ValidarRangoValidoDeFecha(fechaMin, fechaMax);
            return _reservaInterface.FiltrarPorRangoDeFecha(fechaMin, fechaMax);
        }

        public List<Reserva> FiltrarPorParametros(int? idDocente = null, int? idLaboratorio = null, DateOnly? fechaReserva = null, int? estado = null)
        {
            return _reservaInterface.FiltrarPorParametros(idDocente: idDocente, idLaboratorio: idLaboratorio, fechaEspecifica: fechaReserva, estado_reserva: estado);
        }

        private void ValidarRangoValidoDeFecha(DateOnly fechaMin, DateOnly fechaMax)
        {
            if (fechaMin > fechaMax)
                throw new ApplicationException("La fecha minima no puede ser menor que la fecha maxima.");
        }
    }
}
