using DeckListWeb.Repository.Interfaces;
using System.Collections.Generic;

namespace DeckListWeb.Repository.Models
{
    public class Deck
    {
        private const int deckSize = 52;
        public int Id { get; set; }
        public int Number { get; set; } = 1;
        public string Name { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>(deckSize);

        public void CreateDeck(RepositoryContext db, Deck deck)
        {
            var p = 0;
            for (var suitIndex = Suits.countFirst + 1; suitIndex < Suits.countLast; suitIndex++)
            {
                for (var valueIndex = CardValues.countFirst + 1; valueIndex < CardValues.countLast; valueIndex++)
                {
                    var card = new Card { Suit = suitIndex, Value = valueIndex, Position = ++p, Deck = deck };
                    db.Cards.Add(card);
                }
            }
        }

        public void CreateDeck(IBaseRepository Database, Deck deck)
        {
            var p = 0;
            for (var suitIndex = Suits.countFirst + 1; suitIndex < Suits.countLast; suitIndex++)
            {
                for (var valueIndex = CardValues.countFirst + 1; valueIndex < CardValues.countLast; valueIndex++)
                {
                    var card = new Card { Suit = suitIndex, Value = valueIndex, Position = ++p, Deck = deck };
                    Database.Cards.Add(card);
                }
            }
        }
    }
}
