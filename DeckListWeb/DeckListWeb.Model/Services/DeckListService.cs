using System;
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
            const int pageSize = 15;

            IEnumerable<Deck> decksList = await Database.Decks.GetAllAsync();

            var decksListModel = ConvertDeckList(decksList);

            var count = decksListModel.Count();
            var items = decksListModel.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var allDecksModel = new AllDecksModel
            {
                PageModel = new PageModel(count, page, pageSize),
                Decks = items
            };

            return allDecksModel;
        }

        public async Task<DeckModel> GetDeckAsync(int id)
        {
            var deck = await Database.Decks.GetById(id);

            return ConvertDeck(deck);
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

            return ConvertDeck(deck);
        }

        public async Task<DeckModel> ShuffleDeckAsync(int id)
        {
            var deck = await Database.Decks.GetById(id);

            if (deck == null)
                return null;

            var rand = new Random();

            foreach (var card in deck.Cards)
            {
                card.Position = rand.Next(deck.Cards.Count);
            }

            await Database.Save();

            return ConvertDeck(deck);
        }

        public async Task<DeckModel> ManualShuffleDeckAsync(int id)
        {
            var deck = await Database.Decks.GetById(id);

            if (deck == null)
                return null;

            var rand = new Random();

            var deckShuffle = new List<Card>(deck.Cards.Count);
            var deckHalf1Shuffle = new List<Card>(deck.Cards.Count);
            var deckHalf2Shuffle = new List<Card>(deck.Cards.Count);

            deckShuffle.AddRange(deck.Cards);

            for (var i = 0; i < 25; i++)
            {
                deckHalf1Shuffle.AddRange(deckShuffle);
                deckHalf2Shuffle.AddRange(deckShuffle);

                var half = rand.Next(deck.Cards.Count / 2 - 10, deck.Cards.Count / 2 + 10);

                deckHalf1Shuffle.RemoveRange(0, half);
                deckHalf2Shuffle.RemoveRange(half, deck.Cards.Count - half);
                deckShuffle.Clear();

                var p = 1;
                foreach (var card in deckHalf1Shuffle)
                {
                    card.Position = p;
                    deckShuffle.Add(card);
                    p += 2;
                }
                p = 2;
                foreach (var card in deckHalf2Shuffle)
                {
                    card.Position = p;
                    deckShuffle.Add(card);
                    p += 2;
                }

                deckHalf1Shuffle.Clear();
                deckHalf2Shuffle.Clear();
            }

            deck.Cards = deckShuffle;

            await Database.Save();

            return ConvertDeck(deck);
        }

        public DeckModel ConvertDeck(Deck deck)
        {
            var cards = deck.Cards
                .Select(c => new CardModel
                {
                    Id = c.Id,
                    Position = c.Position,
                    Suit = c.Suit,
                    Value = c.Value
                })
                .ToList();
            var deckModel = new DeckModel
            {
                Id = deck.Id,
                Name = deck.Name,
                Number = deck.Number,
                Cards = cards
            };
          
            return deckModel;
        }

        public IEnumerable<DeckModel> ConvertDeckList(IEnumerable<Deck> decksList)
        {
            var decksListModel = new List<DeckModel>();

            foreach (var deck in decksList)
            {
                var cards = deck.Cards
                    .Select(c => new CardModel
                    {
                        Id = c.Id,
                        Position = c.Position,
                        Suit = c.Suit,
                        Value = c.Value
                    })
                    .ToList();

                var deckModel = new DeckModel
                {
                    Id = deck.Id,
                    Name = deck.Name,
                    Number = deck.Number,
                    Cards = cards
                };
                decksListModel.Add(deckModel);
            }

            return decksListModel;
        }
    }
}
