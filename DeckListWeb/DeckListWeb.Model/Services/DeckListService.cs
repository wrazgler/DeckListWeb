using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DeckListWeb.Model.Models;
using DeckListWeb.Model.Interfaces;

using DeckListWeb.Repository.Interfaces;
using DeckListWeb.Repository.Models;

namespace DeckListWeb.Model.Services
{
    public class DeckListService : IDeckListService
    {
        private IBaseRepository Database { get; }

        public DeckListService(IBaseRepository baseRepository)
        {
            Database = baseRepository;
        }

        public async Task<AllDecksModel> GetAllDecks(int page)
        {
            IEnumerable<Deck> decksList = await Database.Decks.GetAllAsync();

            var newDeckList = ConvertToModel.ConvertDeckList(decksList);

            var count = newDeckList.Count();
            var items = newDeckList.Skip((page - 1) * PageModel.GetPageSize()).Take(PageModel.GetPageSize()).ToList();

            var allDecksModel = new AllDecksModel
            {
                PageModel = new PageModel(count, page),
                Decks = items
            };

            return allDecksModel;
        }

        public async Task<DeckModel> GetDeckAsync(int id)
        {
            var deck = await Database.Decks.GetById(id);

            return ConvertToModel.ConvertDeck(deck);
        }

        public async Task AddDeckAsync(string name)
        {
            var i = 0;
            Deck deck;

            do
            {
                i++;
                deck = await Database.Decks.GetByNumber(i);
            }
            while (deck != null);

            deck = new Deck { Number = i };

            if (name != null)
            {
                deck.Name = name;
            }

            Database.Decks.Add(deck);
            deck.CreateDeck(Database, deck);

            await Database.Save();
        }

        public async Task DeleteDeckAsync(int id)
        {
            await Database.Decks.Delete(id);
            await Database.Save();
        }

        public async Task<DeckModel> EditDeckAsync(int id, string name)
        {
            var deck = await Database.Decks.GetById(id);

            if (deck == null)
                return null;

            deck.Name = name;

            await Database.Save();

            return ConvertToModel.ConvertDeck(deck);
        }

        public async Task<DeckModel> ShuffleDeckAsync(IShuffleStrategy shuffleStrategy, int id)
        {
            var deck = await shuffleStrategy.ShuffleDeckAsync(Database, id);

            return deck;
        }
    }
}
