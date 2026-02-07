using CapaNegocio.Entidades;
using CapaDeDatos.AccesoDatos;
using CapaDeDatos.Interfaces;
using CapaAplicacion.Servicios;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string connst = @"Data Source=EMILIA-TOSCANO\SQLEXPRESS2025;Persist Security Info=False;User ID=sa;Password=genshin123;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=""SQL Server Management Studio"";Command Timeout=0";
            DBConnection dbconn = new DBConnection(connst);
            SQLManagement sqlMan = new SQLManagement(dbconn);

            LaboratorioInterface labInt = new LaboratorioInterface(sqlMan);
            LaboratorioServicio labSer = new LaboratorioServicio(labInt);

            DocenteInterface docInt = new DocenteInterface(sqlMan);
            DocenteServicio docSer = new DocenteServicio(docInt);

            ReservaInterface resInt = new ReservaInterface(sqlMan);
            ReservaServicio resSer = new ReservaServicio(resInt, labInt);

            try
            {
                //EjecutarPruebaDocente(docSer);
                EjecutarPruebaReserva(resSer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

            
        }

        private static void EjecutarPruebaLaboratorio(LaboratorioServicio labSer)
        {
            var encontrado = labSer.BuscarPorId(3);
            Console.WriteLine(encontrado);

            labSer.Registrar("Laboratorio de fisica", 60);

            labSer.ActualizarDatos(2, "Laboratorio de fisica 3", 80, 1);

            labSer.Inactivar(1);

            var labs = labSer.ListarTodos();
            labs.ForEach(lab => Console.WriteLine(lab));
        }

        private static void EjecutarPruebaDocente(DocenteServicio docSer)
        {
            // var encontrado = docSer.BuscarPorCedula("0912345678"); Console.WriteLine(encontrado);
            
            // docSer.Registrar("0912121212", "Victor", "Toscano", "Ing en sistema");

            // docSer.ActualizarDatos(2, "0912121212", "Victor", "Alvarez", "Ing en sistema", true);

            docSer.Inactivar(1);

            var docs = docSer.ListarTodos();
            docs.ForEach(doc => Console.WriteLine(doc));
            
        }


        private static void EjecutarPruebaReserva(ReservaServicio resSer)
        {
            //var encontrado = resSer.BuscarPorId(3); Console.WriteLine(encontrado);

            //resSer.Reservar(2, 3, "Para practicas profesionales", 20, new DateOnly(2026, 2, 8), new TimeOnly(10, 0), new TimeOnly(12,0));

            // resSer.ActualizarDatos(2, 2, 2, "POR QUIERO NMMS", 40, new DateOnly(2026, 2, 8), new TimeOnly(10, 0), new TimeOnly(11, 0), 1);

            //resSer.Cancelar(1);
            //resSer.Finalizar(2);



            var docs = resSer.ListarTodas();
            docs = resSer.FiltrarPorRangoDeFecha(new DateOnly(2026, 2, 7), new DateOnly(2026, 2, 8));
            docs.ForEach(doc => Console.WriteLine(doc));

        }
    }
}
