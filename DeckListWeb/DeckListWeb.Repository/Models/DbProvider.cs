using Microsoft.Extensions.Configuration;

namespace DeckListWeb.Repository.Models
{
    public class DbProvider
    {
        private readonly string _postgreSQL;
        private readonly string _msSQL;

        public DbProvider(IConfiguration configuration)
        {
            _postgreSQL = configuration.GetConnectionString("PostgreSQLConnection");
            _msSQL = configuration.GetConnectionString("MSSQLConnection");
        }

        public DbProviderState GetSQL(string connectionString)
        {
            var databaseState = DbProviderState.PostgeSQL;

            if (connectionString == _msSQL)
            {
                databaseState = DbProviderState.MSSQL;
            }

            return databaseState;
        }
    }
}
