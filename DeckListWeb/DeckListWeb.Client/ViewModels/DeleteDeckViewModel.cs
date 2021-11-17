
using DeckListWeb.Model.Models;

namespace DeckListWeb.Client.ViewModels
{
    public class DeleteDeckViewModel
    {
        public int Page { get; set; }
        public string PreviousPage { get; set; }
        public DeckModel Deck { get; set; }
    }
}
