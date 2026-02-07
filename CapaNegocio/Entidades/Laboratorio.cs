using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace CapaNegocio.Entidades
{
    public class Laboratorio
    {
        private int idLaboratorio;
        private string nombre;
        private int capacidadMax;
        private int estado;

        public Laboratorio()
        {
            idLaboratorio = 0;
            nombre = string.Empty;
            capacidadMax = 0;
            estado = 1;
        }

        public Laboratorio(int idLaboratorio, string nombre, int capacidadMax, int estado)
        {
            this.idLaboratorio = idLaboratorio;
            this.nombre = nombre;
            this.capacidadMax = capacidadMax;
            this.estado = estado;
        }

        // constructor usado para las consultas insert
        public Laboratorio(string nombre, int capacidadMax)
        {
            ValidarDatosDeLaboratorio(nombre, capacidadMax);

            Nombre = nombre;
            CapacidadMaxima = capacidadMax;
        }

        private void ValidarDatosDeLaboratorio(string nombre, int capacidadMax)
        {
            ValidarNombre(nombre);
            ValidarCapacidadMaxima(capacidadMax);
        }


        private void ValidarNombre(string nombre)
        {
            string patronNombre = @"^[A-Za-zÁÉÍÓÚÜáéíóúüÑñ\s]{10,100}$";
            if (!Regex.IsMatch(nombre, patronNombre))
                throw new ArgumentException("El nombre del laboratorio no tienen un formato valido!");
        }

        private void ValidarCapacidadMaxima(int capacidadMaxima)
        {
            if (capacidadMaxima <= 10 || capacidadMaxima > 100)
                throw new ArgumentException("La capacidad del laboratorio debe estar en un rango entre 10 y 100!");
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

        public int Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        
        public override string ToString()
        {
            string estado = Estado == 1 ? "Activo" : "Inactivo";
            return $"{IdLaboratorio};{Nombre};{CapacidadMaxima};{estado}";
        }
    } 
}


