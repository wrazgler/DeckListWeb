namespace DeckListWeb.Repository.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public Suits Suit { get; set; }
        public CardValues Value { get; set; }

        public int DeckId { get; set; }
        public Deck Deck { get; set; }
    }
}
