using Microsoft.EntityFrameworkCore;

using DeckListWeb.Repository.Interfaces;

namespace DeckListWeb.Repository.Factories
{
    public class PostreSQLContextFactory : IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
