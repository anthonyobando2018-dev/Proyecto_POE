using CapaDeDatos.Interfaces;
using CapaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion.Servicios
{
    public class DocenteServicio
    {
        private readonly DocenteInterface _docenteInterface;

        public DocenteServicio(DocenteInterface docenteInterface)
        {
            _docenteInterface = docenteInterface;
        }

        public void Registrar(string cedula, string nombres, string apellidos, string especialidad)
        {
            ValidarCedulaDuplicada(cedula);
            Docente nuevoDocente = new Docente(cedula, nombres, apellidos, especialidad);
            _docenteInterface.Guardar(nuevoDocente);
        }

        public void ActualizarDatos(int idDocente, string cedula, string nombres, string apellidos, string especialidad, bool estado)
        {
            // ValidarCedulaDuplicada(cedula);
            Docente docenteActualizado = new Docente(idDocente, cedula, nombres, apellidos, especialidad, estado);
            _docenteInterface.Actualizar(idDocente, docenteActualizado);
        }

        public Docente BuscarPorCedula(string cedula)
        {
            return _docenteInterface.ObtenerPorCedula(cedula) ?? throw new ApplicationException("Docente no encontrado con esa cedula.");
        }

        public List<Docente> ListarTodos()
        {
            return _docenteInterface.ObtenerTodos();
        }

        public Docente BuscarPorId(int idDocente)
        {
            return _docenteInterface.ObtenerPorId(idDocente) ?? throw new ApplicationException("Docente no encontrado con ese id.");
        }

        public bool Inactivar(int idDocente)
        {
            return _docenteInterface.ActualizarEstado(idDocente, 0);
        }

        private void ValidarCedulaDuplicada(string cedula)
        {
            if (_docenteInterface.ObtenerPorCedula(cedula) != null)
                throw new ApplicationException("La cedula usada ya se encuentra registrada.");
        }
    }
}
