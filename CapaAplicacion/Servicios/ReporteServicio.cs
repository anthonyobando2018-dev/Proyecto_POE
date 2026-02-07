using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDeDatos.Interfaces;
using CapaNegocio.Entidades;

namespace CapaAplicacion.Servicios
{
    public class ReporteServicio
    {
        private readonly ReservaFiltrosServicio _reservaFiltroServicio;
        private readonly LaboratorioServicio _laboratorioServicio;
        private readonly DocenteServicio _docenteServicio;

        public ReporteServicio(
            ReservaFiltrosServicio reservaFiltroServicio, 
            LaboratorioServicio laboratorioServicio,
            DocenteServicio docentServicio
            )
        {
            _reservaFiltroServicio = reservaFiltroServicio;            
            _laboratorioServicio = laboratorioServicio;
            _docenteServicio = docentServicio;

        }

        public string GenerarReporte(DateOnly fechaInicio, DateOnly fechaFin)
        {
            // filtramos las reservas en el rango de fechas y que no esten canceladas
            var listaReservas = _reservaFiltroServicio.FiltrarPorRangoDeFecha(fechaInicio, fechaFin);
            listaReservas = listaReservas.FindAll((reserva) => reserva.Estado != 2);

            // instancia de StringBuilder para construir el reporte
            StringBuilder reporte = new StringBuilder();

            if (listaReservas.Count > 0)
            {
                // agrupamos por laboratorio
                var grupos = listaReservas.GroupBy((reserva) => reserva.IdLaboratorio);

                // recorremos los grupos de reservas
                foreach (var reservas in grupos)
                {
                    Laboratorio laboratorio = _laboratorioServicio.BuscarPorId(reservas.Key);
                    reporte.AppendLine($"Reservas del {laboratorio.Nombre}\n");
                    var tiempoReservado = new TimeSpan(0);
                    // recorremos las reservas
                    foreach (var reserva in reservas)
                    {

                        var duracionReserva = reserva.Horario.HoraFin - reserva.Horario.HoraInicio;
                        // mostramos los datos de cada reserva
                        Docente docente = _docenteServicio.BuscarPorId(reserva.IdDocente);
                        string reservaDatos = $"Reserva de ID: {reserva.IdReserva}\n" +
                            $"__Docente ocupante: {docente.Nombres}\n" +
                            $"__Asunto de reserva: {reserva.Asunto}\n" +
                            $"__Dia de reserva: {reserva.FechaReserva}\n" +
                            $"__Franja horaria: {reserva.Horario}\n";
                        reporte.AppendLine(reservaDatos);
                        tiempoReservado += duracionReserva;
                    }
                    reporte.AppendLine($"Tiempo total reservado: {tiempoReservado}");
                    reporte.AppendLine("------------------------------------------------");
                }

            }

            return reporte.ToString();
        }
    }
}