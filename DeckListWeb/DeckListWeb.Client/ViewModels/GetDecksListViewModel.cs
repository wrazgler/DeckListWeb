using System.Collections.Generic;

using DeckListWeb.Model.Models;

namespace DeckListWeb.Client.ViewModels
{
    public class GetDecksListViewModel
    {
        public IEnumerable<DeckModel> Decks { get; set; }
        public PageModel PageViewModel { get; set; }
    }
}