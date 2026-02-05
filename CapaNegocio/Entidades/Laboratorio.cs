using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SRLCProyectoPOE.Entidades
{
    public class Laboratorio
    {
        private int idLaboratorio;
        private string nombre;
        private int capacidadMax;
        private bool estado;

        public Laboratorio()
        {
            idLaboratorio = 0;
            nombre = string.Empty;
            capacidadMax = 0;
            estado = true;
        }

        public Laboratorio(int idLaboratorio, string nombre, int capacidadMax, bool estado)
        {
            this.idLaboratorio = idLaboratorio;
            this.nombre = nombre;
            this.capacidadMax = capacidadMax;
            this.estado = estado;
        }

        public int IdLaboratorio
        {
            get { return idLaboratorio; }
            set { idLaboratorio = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int CapacidadMaxima
        {
            get { return capacidadMax; }
            set { capacidadMax = value; }
        }

        public bool Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}

//HOLA ES DE GITHUB

