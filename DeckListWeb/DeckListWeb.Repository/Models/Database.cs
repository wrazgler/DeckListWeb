using Microsoft.Extensions.Configuration;

namespace DeckListWeb.Repository.Models
{
    public class Database
    {
        private readonly string _postgreSQL;
        private readonly string _msSQL;

        public Database(IConfiguration configuration)
        {
            _postgreSQL = configuration.GetConnectionString("PostgreSQLConnection");
            _msSQL = configuration.GetConnectionString("MSSQLConnection");
        }

        public DatabaseState GetSQL(string connectionString)
        {
            var databaseState = DatabaseState.PostgeSQL;

            if (connectionString == _msSQL)
            {
                databaseState = DatabaseState.MSSQL;
            }

            return databaseState;
        }

    }
}
