using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CapaNegocio.Entidades
{
    public class Docente
    {
        private int idDocente;
        private string cedula;
        private string nombres;
        private string apellidos;
        private string nombreCompleto;
        private string especialidad;
        private bool estado;
     

        public Docente()
        {
            idDocente = 0;
            cedula = string.Empty;
            nombres = string.Empty;
            apellidos = string.Empty;
            especialidad = string.Empty;
            estado = true;
        }
        public Docente(int idDocente, string cedula,string nombres, string apellidos, string especialidad, bool estado)
        {
            this.idDocente = idDocente;
            this.cedula = cedula;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.especialidad = especialidad;
            this.estado = estado;
        }

        public Docente(string cedula, string nombres, string apellidos, string especialidad)
        {
            ValidarDatosDeDocente(cedula, nombres, apellidos, especialidad);

            Cedula = cedula;
            Nombres = nombres;
            Apellidos = apellidos;
            Especialidad = especialidad;
        }

        private void ValidarDatosDeDocente(string cedula, string nombres, string apellidos, string especialidad)
        {
            ValidarCedula(cedula);
            ValidarNombres(nombres);
            ValidarApellidos(apellidos);
            ValidarEspecialidad(especialidad);
        }

        public void InactivarDocente()
        {
            Estado = false;
        }

        public void ActivarDocente()
        {
            Estado = true;
        }

        private void ValidarCedula(string cedula)
        {
            // verificar que la cedula sea de 10 caracteres numericos con expresiones regulares
            string patronCedula = @"^\d{10}$";
            if (!Regex.IsMatch(cedula, patronCedula))
                throw new ArgumentException("La cedula debe tener 10 caracteres numericos!");
        }

        private void ValidarNombres(string nombres)
        {
            string patronNombres = @"^[A-Za-zÁÉÍÓÚÜáéíóúüÑñ\s]{3,100}$";
            if (!Regex.IsMatch(nombres, patronNombres))
                throw new ArgumentException("Los nombres no tienen un formato valido!");
        }

        private void ValidarApellidos(string apellidos)
        {
            string patronApellidos = @"^[A-Za-zÁÉÍÓÚÜáéíóúüÑñ\s]{3,100}$";
            if (!Regex.IsMatch(apellidos, patronApellidos))
                throw new ArgumentException("Los apellidos no tienen un formato valido!");
        }

        private void ValidarEspecialidad(string especialidad)
        {
            string patronEspecialidad = @"^[A-Za-zÁÉÍÓÚÜáéíóúüÑñ\s]{3,100}$";
            if (!Regex.IsMatch(especialidad, patronEspecialidad))
                throw new ArgumentException("La especialidad del docente no tiene un formato valido!");
        }

        public int IdDocente {  get { return idDocente; } set { idDocente = value; } }
        public string Cedula { get { return cedula; } set { cedula = value; } }
        public string Nombres { get { return nombres; } set { nombres = value; } }
        public string Apellidos { get { return apellidos; } set { apellidos = value; } }
        public string NombreCompleto { get { return $"{Nombres} {Apellidos}"; } }
        public string Especialidad { get { return especialidad; } set { especialidad = value; } }
        public bool Estado { get { return estado; } set { estado = value; } }

        public override string ToString()
        {
            return $"{IdDocente};{Cedula};{Nombres};{Apellidos};{Especialidad};{Estado}";
        }
    }
}
