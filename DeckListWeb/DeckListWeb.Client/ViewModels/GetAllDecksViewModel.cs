using System.Collections.Generic;

using DeckListWeb.Model.Models;

namespace DeckListWeb.Client.ViewModels
{
    public class GetAllDecksViewModel
    {
        public IEnumerable<DeckModel> Decks { get; set; }
        public PageModel PageViewModel { get; set; }
    }
}