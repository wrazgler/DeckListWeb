using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DeckListWeb.Repository.Interfaces;
using DeckListWeb.Repository.Models;

namespace DeckListWeb.Repository.Repositories
{
    public class DeckRepository : IRepository<Deck>
    {
        private readonly RepositoryContext _db;

        public DeckRepository(RepositoryContext context)
        {
            _db = context;
        }

        public IEnumerable<Deck> GetAll()
        {
            return _db.Decks.Include(d => d.Cards).OrderBy(d => d.Number);
        }

        public async Task<Deck> GetById(int id)
        {
            return await _db.Decks
                .Include(d => d.Cards)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Deck> GetByName(string name)
        {
            return await _db.Decks
                .Include(d => d.Cards)
                .FirstOrDefaultAsync(d => d.Name == name);
        }

        public async Task<Deck> GetByNumber(int number)
        {
            return await _db.Decks
                .Include(d => d.Cards)
                .FirstOrDefaultAsync(d => d.Number == number);
        }

        public void Add(Deck deck)
        {
            _db.Decks.Add(deck);
        }

        public void Update(Deck deck)
        {
            _db.Entry(deck).State = EntityState.Modified;
        }

        public IEnumerable<Deck> Find(Func<Deck, Boolean> predicate)
        {
            return _db.Decks
                .Include(d => d.Cards)
                .Where(predicate).ToList();
        }

        public async Task Delete(int id)
        {
            var deck = await _db.Decks.FindAsync(id);
            if (deck != null)
                _db.Decks.Remove(deck);
        }
    }
}