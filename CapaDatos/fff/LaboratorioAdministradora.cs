using SRLCProyectoPOE.Entidades;
using SRLCProyectoPOE.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Administradoras
{
    public static class LaboratorioAdministradora
    {

        private static List<Laboratorio> _lista_laboratorio = new List<Laboratorio>();
        private static int _idActual = 1;

        public static List<Laboratorio> GetListaLaboratorio()
        {
            return _lista_laboratorio;
        }

        // verificar los datos de laboratorio y agregarlo
        public static void CrearLaboratorio(Laboratorio laboratorio)
        {
            try
            {
                ValidarLaboratorioDatos(laboratorio);
                laboratorio.IdLaboratorio = _idActual;
                _lista_laboratorio.Add(laboratorio);
                _idActual++;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        // Obtenemos el codigo (Id)
        public static int GetNextCodigo()
        {
            return _idActual;
        }

        // Buscar indice por codigo
        public static int BuscarLaboratorio(int codigo)
        {
            for (int i = 0; i < _lista_laboratorio.Count; i++)
            {
                if (_lista_laboratorio[i].IdLaboratorio == codigo)
                    return i;
            }
            return -1;
        }


        public static void ActualizarLaboratorio(Laboratorio laboratorio)
        {
            try
            {
                ValidarLaboratorioDatos(laboratorio);
                int indice = BuscarLaboratorio(laboratorio.IdLaboratorio);
                if (indice < 0)
                    throw new ArgumentException("El codigo del laboratorio no se encuentra");

                if (LaboratorioTieneReservasActivas(laboratorio.IdLaboratorio) && !laboratorio.Estado)
                    throw new ArgumentException("El laboratorio no se puede inactivar porque tiene reservas activas.");

                _lista_laboratorio[indice] = laboratorio;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        // Eliminar laboratorio
        public static void EliminarLaboratorio(int codigo)
        {
            try
            {
                int indice = BuscarLaboratorio(codigo);
                if (indice < 0)
                    throw new ArgumentException("El codigo del laboratorio no se encuentra");

                if (LaboratorioTieneReservasActivas(codigo))
                    throw new ArgumentException("El laboratorio no se puede inactivar porque tiene reservas activas.");
                // lista_laboratorio.RemoveAt(indice);
                // inactivamos al Laboratorio
                _lista_laboratorio[indice].Estado = false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        private static bool LaboratorioTieneReservasActivas(int codigo)
        {
            var laboratorioReservas = ReservaAdministradora.FiltrarPorLaboratorio(codigo, true);
            return laboratorioReservas.Count > 0;
        }

        private static void ValidarLaboratorioDatos(Laboratorio laboratorio)
        {
            // un laboratorio debe cumpllir con los siguientes criterios
            Validaciones.ValidarCampoTexto(laboratorio.Nombre, "nombre del laboratorio", 10, 100);
            Validaciones.ValidarRangoNumerico(laboratorio.CapacidadMaxima, "capacidad de estudiantes", 1, 100);
        }
    }
}

