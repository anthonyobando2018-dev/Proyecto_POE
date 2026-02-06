using SRLCProyectoPOE.Entidades;
using SRLCProyectoPOE.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Administradoras
{
    public static class DocenteAdministradora
    {
        private static List<Docente> _lista_docente = new List<Docente>();
        private static int _idActual = 1;

        //Agregar estudiantes a la listas
        public static void AgregarDocente(Docente docente)
        {
            try
            {
                //Validamos que la cedula no se repita y añadimo el docente a la lista
                ValidarDocenteDatos(docente);
                docente.IdDocente = _idActual;
                _lista_docente.Add(docente);
                _idActual++;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //Metodo para devolver una lista 
        public static List<Docente> ListarDocente()
        {
            return _lista_docente;
        }

        public static int GetIndiceCodigo(int idDocente)
        {
            for (int i = 0; i < _lista_docente.Count; i++)
            {
                if (_lista_docente[i].IdDocente == idDocente)
                    return i;
            }
            //si sale del for es porque no encontro el código
            return -1;
        }

        public static int GetNextCodigo()
        {
            return _idActual;
        }

        //Actualizar la lista de docentes
        public static void ActualizarDocente(Docente docente)
        {
            try
            {
                //obtener el indice del código
                ValidarDocenteDatos(docente);
                int indice = GetIndiceCodigo(docente.IdDocente);
                if (indice < 0)
                    throw new ArgumentException("El código del docente no se encuentra ");

                if (DocenteTieneReservasActivas(docente.IdDocente) && !docente.Estado)
                    throw new ArgumentException("El docente no se puede inactivar porque tiene reservas activas.");

                //actualizar la lista
                _lista_docente[indice] = docente;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        //Metodo que elimina un Docente
        public static void EliminarDocente(int idDocente)
        {
            try
            {
                //obtener el indice del código
                int indice = GetIndiceCodigo(idDocente);
                if (indice < 0)
                    throw new ArgumentException("El código del estudiante no se encuentra ");

                if (DocenteTieneReservasActivas(idDocente))
                    throw new ArgumentException("El docente no se puede inactivar porque tiene reservas activas.");

                //eliminar el elemento de la lista
                ///lista_docente.RemoveAt(indice);

                //eliminar logicamente el elemento de la lista
                _lista_docente[indice].Estado = false;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public static void ValidarCedulaRepetida(Docente docente)
        {
            for (int i = 0; i < _lista_docente.Count; i++)
            {
                // verificar cedula no repetida para todos menos el docente de id actual
                if (_lista_docente[i].IdDocente != docente.IdDocente)
                {
                    if (_lista_docente[i].Cedula.Equals(docente.Cedula))
                        throw new ArgumentException("Ya existe un docente con ese numero de cedula.");
                }
            }
        }

        private static void ValidarDocenteDatos(Docente docente)
        {
            Validaciones.ValidarCampoTexto(docente.Nombres, "nombres de docente", 3, 100);
            Validaciones.ValidarCampoTexto(docente.Apellidos, "apellidos de docente", 3, 100);
            Validaciones.ValidarCampoNumerico(docente.Cedula, "cedula");
            if (docente.Cedula.Trim().Length != 10)
                throw new ArgumentException("La cedula debe contener 10 numeros.");
            Validaciones.ValidarCampoTexto(docente.Especialidad, "especialidad", 5, 100);
            ValidarCedulaRepetida(docente);
        }

        private static bool DocenteTieneReservasActivas(int idDocente)
        {
            var docenteReservas = ReservaAdministradora.FiltrarPorDocente(idDocente, true);
            return docenteReservas.Count > 0;
        }

        //Metodo para buscar un Docente x Id
        public static Docente BuscarDocente(int idDocente)
        {
            return _lista_docente.Find(x => x.IdDocente == idDocente);
        }
    }
}
