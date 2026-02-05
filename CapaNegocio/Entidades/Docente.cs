using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRLCProyectoPOE.Entidades
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
        public Docente(int idDocente, string cedula,string nombres, string apellidos, string especialidad)
        {
            this.idDocente = idDocente;
            this.cedula = cedula;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.especialidad = especialidad;
            this.estado = true;
        }

        public int IdDocente {  get { return idDocente; } set { idDocente = value; } }
        public string Cedula { get { return cedula; } set { cedula = value; } }
        public string Nombres { get { return nombres; } set { nombres = value; } }
        public string Apellidos { get { return apellidos; } set { apellidos = value; } }
        public string NombreCompleto { get { return $"{Nombres} {Apellidos}"; } }
        public string Especialidad { get { return especialidad; } set { especialidad = value; } }
        public bool Estado { get { return estado; } set { estado = value; } }
    }
}
