using DeckListWeb.Repository.Interfaces;

namespace DeckListWeb.Repository
{
    public class ContextOptions : IContextOptions
    {
        public string ConnectionString { get; set; }
    }
}
