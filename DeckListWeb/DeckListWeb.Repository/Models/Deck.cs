using DeckListWeb.Repository.Interfaces;
using System.Collections.Generic;

namespace DeckListWeb.Repository.Models
{
    public class Deck
    {
        public int Id { get; set; }
        public int Number { get; set; } = 1;
        public string Name { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>(52);

        public void CreateDeck(RepositoryContext db, Deck deck)
        {
            var p = 0;
            for (var s = Suits.Бубны; s <= Suits.Черви; s++)
            {
                for (var v = CardValues.Два; v <= CardValues.Туз; v++)
                {
                    var card = new Card { Suit = s, Value = v, Position = ++p, Deck = deck };
                    db.Cards.Add(card);
                }
            }
        }

        public void CreateDeck(IBaseRepository Database, Deck deck)
        {
            var p = 0;
            for (var s = Suits.Бубны; s <= Suits.Черви; s++)
            {
                for (var v = CardValues.Два; v <= CardValues.Туз; v++)
                {
                    var card = new Card { Suit = s, Value = v, Position = ++p, Deck = deck };
                    Database.Cards.Add(card);
                }
            }
        }
    }
}
