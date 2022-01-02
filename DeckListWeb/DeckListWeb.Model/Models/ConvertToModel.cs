using System.Collections.Generic;
using System.Linq;

using DeckListWeb.Repository.Models;

namespace DeckListWeb.Model.Models
{
    public class ConvertToModel
    {
        public static DeckModel ConvertDeck(Deck deck)
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
            var newDeck = new DeckModel
            {
                Id = deck.Id,
                Name = deck.Name,
                Number = deck.Number,
                Cards = cards
            };

            return newDeck;
        }

        public static IEnumerable<DeckModel> ConvertDeckList(IEnumerable<Deck> decksList)
        {
            var newDecksList = new List<DeckModel>();

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
                newDecksList.Add(deckModel);
            }

            return newDecksList;
        }
    }
}
