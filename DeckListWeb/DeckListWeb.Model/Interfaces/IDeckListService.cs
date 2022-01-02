using System.Threading.Tasks;

using DeckListWeb.Model.Models;

namespace DeckListWeb.Model.Interfaces
{
    public interface IDeckListService
    {
        Task AddDeckAsync(string name);

        Task DeleteDeckAsync(int id);

        Task<DeckModel> GetDeckAsync(int id);

        Task<AllDecksModel> GetAllDecks(int page);

        Task<DeckModel> ShuffleDeckAsync(IShuffleStrategy shuffleStrategy, int id);

        Task<DeckModel> EditDeckAsync(int id, string name);
    }
}
