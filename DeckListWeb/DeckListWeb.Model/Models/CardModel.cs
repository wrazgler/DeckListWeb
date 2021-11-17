using DeckListWeb.Repository.Models;

namespace DeckListWeb.Model.Models
{
    public class CardModel
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public Suits Suit { get; set; }
        public CardValues Value { get; set; }
    }
}
