using System.Collections.Generic;

namespace DeckListWeb.Model.Models
{
    public class DeckListModel
    {
        public IEnumerable<DeckModel> Decks { get; set; }
        public PageModel PageModel { get; set; }
    }
}
