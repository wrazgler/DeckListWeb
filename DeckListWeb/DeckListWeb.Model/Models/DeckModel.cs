using System.Collections.Generic;

namespace DeckListWeb.Model.Models
{
    public class DeckModel
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public List<CardModel> Cards { get; set; } 
    }
}
