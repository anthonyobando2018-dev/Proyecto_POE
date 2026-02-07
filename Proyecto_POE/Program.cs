using CapaAplicacion.Servicios;
using CapaDeDatos.AccesoDatos;
using CapaDeDatos.Interfaces;
using CapaPresentacion.Formularios;

namespace Proyecto_POE
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            /* Inyeccion de dependencias */
            string cadena_conexion = @"Data Source=EMILIA-TOSCANO\SQLEXPRESS2025;Persist Security Info=False;User ID=sa;Password=genshin123;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=""SQL Server Management Studio"";Command Timeout=0";
            
            
            DBConnection dbConexion = new DBConnection(cadena_conexion);
            SQLManagement sqlManagement = new SQLManagement(dbConexion);

            DocenteInterface docenteInterface = new DocenteInterface(sqlManagement);
            LaboratorioInterface laboratorioInterface = new LaboratorioInterface(sqlManagement);
            ReservaInterface reservaInterface = new ReservaInterface(sqlManagement);
            
            DocenteServicio docenteServicio = new DocenteServicio(docenteInterface);
            LaboratorioServicio laboratorioServicio = new LaboratorioServicio(laboratorioInterface);
            ReservaServicio reservaServicio = new ReservaServicio(reservaInterface, laboratorioInterface);
            ReservaFiltrosServicio reservaFiltroServicio = new ReservaFiltrosServicio(reservaInterface);
            ReporteServicio reporteServicio = new ReporteServicio(reservaFiltroServicio, laboratorioServicio, docenteServicio);

            Application.Run(new Form1(
                docenteServicio, 
                laboratorioServicio, 
                reservaServicio, 
                reservaFiltroServicio, 
                reporteServicio));
            //Application.Run(new FrmLogincs());
        }
    }
}