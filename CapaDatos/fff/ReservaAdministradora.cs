using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using SRLCProyectoPOE.Utilidades;
using SRLCProyectoPOE.Entidades;

namespace CapaDatos.Administradoras
{
    public static class ReservaAdministradora
    {
        private static List<Reserva> _listaReservas = new List<Reserva>();
        private static int _idActual = 1;

        public static void AgregarReserva(Reserva reserva)
        {
            reserva.IdReserva = _idActual;
            ValidarReservaDatos(reserva);
            _listaReservas.Add(reserva);
            _idActual++;
        }

        public static void EliminarReserva(int idReserva)
        {
            Reserva reservaEncontrada = BuscarReserva(idReserva);
            // _listaReservas.Remove(reservaEncontrada);
            reservaEncontrada.Estado = false;
        }

        public static List<Reserva> ListarReservas(bool? estado = null)
        {
            if (estado != null)
            {
                return _listaReservas.FindAll(reserva => reserva.Estado == estado);
            }
            return _listaReservas;
        }

        public static void ActualizarDatosReserva(Reserva reserva)
        {
            ValidarReservaDatos(reserva);
            Reserva reservaObsoleta = BuscarReserva(reserva.IdReserva);
            reservaObsoleta.IdDocente = reserva.IdDocente;
            reservaObsoleta.IdLaboratorio = reserva.IdLaboratorio;
            reservaObsoleta.CantidadEstudiantes = reserva.CantidadEstudiantes;
            reservaObsoleta.Horario = reserva.Horario;
            reservaObsoleta.Asunto = reserva.Asunto;
            reservaObsoleta.FechaReserva = reserva.FechaReserva;
            reservaObsoleta.Estado = reserva.Estado;
            /*
            EliminarReserva(reserva.IdReserva);
            _listaReservas.Add(reserva);
            */
        }

        public static Reserva BuscarReserva(int idReserva)
        {
            if (idReserva < 0)
                throw new ArgumentException("Valores negativos para Id no validos!");
            Reserva? reservaEncontrada = _listaReservas.Find((Reserva) => Reserva.IdReserva == idReserva);
            if (reservaEncontrada == null)
                throw new NullReferenceException($"El Reserva de id: {idReserva} no existe!");
            return reservaEncontrada;
        }

        // metodo para validar los datos de reserva
        private static void ValidarReservaDatos(Reserva reserva)
        {
            DocenteAdministradora.BuscarDocente(reserva.IdDocente);
            int labId = LaboratorioAdministradora.BuscarLaboratorio(reserva.IdLaboratorio);
            Laboratorio lab = LaboratorioAdministradora.GetListaLaboratorio()[labId];
            ValidarCantidadEstudiantes(reserva.CantidadEstudiantes, lab.CapacidadMaxima);
            Validaciones.ValidarCampoTexto(reserva.Asunto, "asunto de la reserva", 10, 100);
            ValidarFrajaHoraria(reserva.Horario);
            ValidarSolapamiento(reserva);
        }

        public static int GetIdActual()
        {
            return _idActual;
        }

        // filtrar reservas por fecha, laboratorio o ambos, si no se le pasan parametros, retorna la lista completa
        public static List<Reserva> FiltrarPorLaboratorio(int idLaboratorio, bool? estado = null)
        {
            List<Reserva> listaFiltrada = ListarReservas(estado);
            listaFiltrada = listaFiltrada.FindAll((reserva) => reserva.IdLaboratorio == idLaboratorio);
            return listaFiltrada;
        }

        public static List<Reserva> FiltrarPorDocente(int idDocente, bool? estado = null)
        {
            List<Reserva> listaFiltrada = ListarReservas(estado);
            listaFiltrada = listaFiltrada.FindAll((reserva) => reserva.IdDocente == idDocente);
            return listaFiltrada;
        }

        public static List<Reserva> FiltrarPorFecha(DateOnly fechaInicio, DateOnly? fechaFin = null, bool? estado = null)
        {
            List<Reserva> listaFiltrada = ListarReservas(estado);

            if (fechaFin != null)
                // filtrar por rango de fecha
                listaFiltrada = listaFiltrada.FindAll((reserva) => reserva.FechaReserva >= fechaInicio && reserva.FechaReserva <= fechaFin);
            else
                // filtra por fecha especifica
                listaFiltrada = listaFiltrada.FindAll((reserva) => reserva.FechaReserva == fechaInicio);
            return listaFiltrada;
        }

        private static void ValidarSolapamiento(Reserva reserva)
        {
            var horaInicio = reserva.Horario.HoraInicio;
            var horaFin = reserva.Horario.HoraFin;
            // filtrar reservas por el laboratorio seleccionado, de ellos en dicha fecha
            var listaFiltrada = _listaReservas.FindAll((res) => reserva.IdLaboratorio == res.IdLaboratorio && reserva.FechaReserva == res.FechaReserva && res.Estado);

            // para esas reservas, verificamos que la reserva a agregar no se solape con la franja horaria de las otras
            foreach (var res in listaFiltrada)
            {
                // exceptuamos la reserva actual
                if (res.IdReserva != reserva.IdReserva)
                {
                    // condiciones a cumplir
                    bool reservaDespues, reservaAntes;
                    // que la franja horaria sea posterior a la de las reservas existentes
                    reservaDespues = horaInicio >= res.Horario.HoraFin;
                    // que la franja horaria sea anterior a la de las existentes
                    reservaAntes = horaInicio < res.Horario.HoraInicio && horaFin <= res.Horario.HoraInicio;

                    // si no se cumplen, lanza una excepcion
                    if (!reservaDespues && !reservaAntes)
                    {
                        throw new Exception("Ya existe una reserva para ese laboratorio en ese dia y franja horaria!");
                    }
                }
            }
        }

        // metodo para verificar que la cantidad de estudiantes dispuesta en la reserva no se mayor a la dispuesta en el laboratorio
        private static void ValidarCantidadEstudiantes(int cantidadSolicitada = 0, int capacidadMaxima = 0)
        {
            if (cantidadSolicitada <= 0)
                throw new ArgumentException($"La cantidad solicitada no puede ser menor o igual que cero!");
            if (cantidadSolicitada > capacidadMaxima)
                throw new ArgumentException($"El laboratorio solo soporta una cantidad maxima de {capacidadMaxima} estudiante(s)!");
        }

        private static void ValidarFrajaHoraria(BloqueHorario horario)
        {
            if (horario.HoraInicio >= horario.HoraFin)
                throw new ArgumentException("La hora de inicio de la reserva no puede ser mayor o igual que la hora de fin!");
        }

        public static List<BloqueHorario> GenerarHorarios(int inicioJornada, int finJornada, double duracion = 60)
        {
            var bloquesHorarios = new List<BloqueHorario>();
            if (duracion < 10) throw new ArgumentException("La duracion del turno no puede ser menor a 10 minutos!");
            if (inicioJornada < 0 || inicioJornada >= 24) throw new ArgumentException("Valor no valido para inicio de jornada!");
            if (inicioJornada < 0 || inicioJornada >= 24) throw new ArgumentException("Valor no valido para fin de jornada!");
            if (inicioJornada >= finJornada) throw new ArgumentException("El inicio de jornada no puede ser mayor igual que el inicio de jornada!");
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
