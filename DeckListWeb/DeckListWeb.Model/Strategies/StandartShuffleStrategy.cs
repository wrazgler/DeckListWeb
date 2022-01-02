using System;
using System.Threading.Tasks;

using DeckListWeb.Model.Models;
using DeckListWeb.Model.Interfaces;

using DeckListWeb.Repository.Interfaces;

namespace DeckListWeb.Model.Strategies
{
    public class StandartShuffleStrategy : IShuffleStrategy
    {
        public async Task<DeckModel> ShuffleDeckAsync(IBaseRepository database, int id)
        {
            var deck = await database.Decks.GetById(id);

            if (deck == null)
                return null;

            var rand = new Random();

            foreach (var card in deck.Cards)
            {
                card.Position = rand.Next(deck.Cards.Count);
            }

            await database.Save();

            return ConvertToModel.ConvertDeck(deck);
        }
    }
}
