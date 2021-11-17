using System.Collections.Generic;

namespace DeckListWeb.Model.Models
{
    public class AllDecksModel
    {
        public IEnumerable<DeckModel> Decks { get; set; }
        public PageModel PageModel { get; set; }
    }
}
