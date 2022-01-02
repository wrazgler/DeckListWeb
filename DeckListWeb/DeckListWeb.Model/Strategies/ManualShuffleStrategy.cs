using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DeckListWeb.Model.Models;
using DeckListWeb.Model.Interfaces;

using DeckListWeb.Repository.Interfaces;
using DeckListWeb.Repository.Models;

namespace DeckListWeb.Model.Strategies
{
    public class ManualShuffleStrategy : IShuffleStrategy
    {
        public async Task<DeckModel> ShuffleDeckAsync(IBaseRepository database, int id)
        {
            var deck = await database.Decks.GetById(id);

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

            await database.Save();

            return ConvertToModel.ConvertDeck(deck);
        }
    }
}
