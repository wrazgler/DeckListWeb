using Microsoft.EntityFrameworkCore;

using DeckListWeb.Repository.Interfaces;

namespace DeckListWeb.Repository.Factories
{
    public class RepositoryContextFactory: IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString, bool isPostgreSql)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();

            if (isPostgreSql)
            {
                optionsBuilder.UseNpgsql(connectionString);
            }
            else
            {
                optionsBuilder.UseSqlServer(connectionString);
            }

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
