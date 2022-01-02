using Microsoft.EntityFrameworkCore;

using DeckListWeb.Repository.Interfaces;

namespace DeckListWeb.Repository.Factories
{
    public class MSSQLContextFactory : IRepositoryContextFactory
    {
        public RepositoryContext CreateDbContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(connectionString);
           
            return new RepositoryContext(optionsBuilder.Options);
        }
    }
}
