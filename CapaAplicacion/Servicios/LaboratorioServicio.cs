using CapaDeDatos.Interfaces;
using CapaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CapaAplicacion.Servicios
{
    public class LaboratorioServicio
    {
        private readonly LaboratorioInterface _laboratorioInterface;

        public LaboratorioServicio(LaboratorioInterface laboratorioInterface)
        {
            _laboratorioInterface = laboratorioInterface;
        }

        public int Registrar(string nombres, int capacidadMaxima)
        {
            Laboratorio nuevoLaboratorio = new Laboratorio(nombres, capacidadMaxima);
            return _laboratorioInterface.Guardar(nuevoLaboratorio);
        }

        public void ActualizarDatos(int idLaboratorio, string nombre, int capacidadMaxima, int estado)
        {
            Laboratorio laboratorioActualizado = new Laboratorio(idLaboratorio, nombre, capacidadMaxima, estado);
            _laboratorioInterface.Actualizar(idLaboratorio, laboratorioActualizado);
        }

        public Laboratorio BuscarPorId(int idLaboratorio)
        {
            return _laboratorioInterface.ObtenerPorId(idLaboratorio) ?? throw new ApplicationException("Laboratorio no encontrado con ese id.");
        }

        public bool Inactivar(int idLaboratorio)
        {
            return _laboratorioInterface.ActualizarEstado(idLaboratorio, 0);
        }

        public List<Laboratorio> ListarTodos()
        {
            return _laboratorioInterface.ObtenerTodos();
        }
    }
}
