using System.Threading.Tasks;

using DeckListWeb.Model.Models;

using DeckListWeb.Repository.Interfaces;

namespace DeckListWeb.Model.Interfaces
{
    public interface IShuffleStrategy
    {
        Task<DeckModel> ShuffleDeckAsync(IBaseRepository database, int id);
    }
}
