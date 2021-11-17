using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DeckListWeb.Repository.Interfaces;
using DeckListWeb.Repository.Models;

namespace DeckListWeb.Repository.Repositories
{
    public class CardRepository : IRepository<Card>
    {
        private readonly RepositoryContext _db;

        public CardRepository(RepositoryContext context)
        {
            _db = context;
        }

        public IEnumerable<Card> GetAll()
        {
            return _db.Cards;
        }

        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public async Task<Card> GetById(int id)
        {
            return await _db.Cards.FindAsync(id);
        }

        public Task<Card> GetByName(string name)
        {
            throw new Exception("Метод GetByName не реализован");
        }

        public Task<Card> GetByNumber(int number)
        {
            throw new Exception("Метод GetByNumber не реализован");
        }

        public void Add(Card card)
        {
            _db.Cards.Add(card);
        }

        public void Update(Card card)
        {
            _db.Entry(card).State = EntityState.Modified;
        }

        public IEnumerable<Card> Find(Func<Card, Boolean> predicate)
        {
            return _db.Cards.Where(predicate).ToList();
        }

        public async Task Delete(int id)
        {
            var card = await _db.Cards.FindAsync(id);
            if (card != null)
                _db.Cards.Remove(card);
        }
    }
}
