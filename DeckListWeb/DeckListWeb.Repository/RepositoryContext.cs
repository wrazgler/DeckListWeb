using Microsoft.EntityFrameworkCore;

using DeckListWeb.Repository.Models;

namespace DeckListWeb.Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }
    }
}
