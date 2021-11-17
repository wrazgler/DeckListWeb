using DeckListWeb.Model.Models;

namespace DeckListWeb.Client.ViewModels
{
    public class GetDeckViewModel
    {
        public int Page { get; set; }
        public DeckModel Deck { get; set; }
    }
}
