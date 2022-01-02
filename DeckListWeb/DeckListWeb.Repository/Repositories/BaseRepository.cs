using System;
using System.Threading.Tasks;

using DeckListWeb.Repository.Interfaces;
using DeckListWeb.Repository.Models;

namespace DeckListWeb.Repository.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public RepositoryContext Database { get; }
        protected IRepositoryContextFactory ContextFactory { get; }
        protected CardRepository CardRepository { get; set; }
        protected DeckRepository DeckRepository { get; set; }

        public BaseRepository(string connectionString, IRepositoryContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
            Database = ContextFactory.CreateDbContext(connectionString);
        }

        public IRepository<Card> Cards
        {
            get { return CardRepository ??= new CardRepository(Database); }
        }
        public IRepository<Deck> Decks
        {
            get { return DeckRepository ??= new DeckRepository(Database); }
        }

        public async Task Save()
        {
            await Database.SaveChangesAsync();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (this._disposed) 
                return;

            if (disposing)
            {
                Database.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}