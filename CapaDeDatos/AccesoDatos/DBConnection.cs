using Microsoft.Data.SqlClient;


namespace CapaDeDatos.AccesoDatos
{
    public class DBConnection
    {
        private readonly string _dbConnectionString;

        public DBConnection(string dbConnectionString)
        {
            // string dbConnectionString = "Server=ALAN\\SQLEXPRESS2;DATABASE=DB_POE;Trusted_Connection=True;TrustServerCertificate=True;";
            // "Data Source=ALAN\\SQLEXPRESS2;Persist Security Info=False;User ID=user_poe;Password=genshin456;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=\"SQL Server Management Studio\";Command Timeout=0"
            _dbConnectionString = dbConnectionString;
        }

        internal SqlConnection CrearConexion()
        {
            return new SqlConnection(_dbConnectionString);
        }
    }
}
