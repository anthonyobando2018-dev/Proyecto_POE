
using System.Data;

namespace CapaDatos.AccesoDatos
{
    internal class Parametro
    {
        public string Nombre { get; set; }
        public SqlDbType TipoDato { get; set; }
        public object Valor { get; set; }

        public Parametro(string nombre, SqlDbType tipoDato, object valor)
        {
            Nombre = nombre;
            TipoDato = tipoDato;
            Valor = valor;
        }
    }
}
