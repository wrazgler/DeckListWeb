using System;
using System.Threading.Tasks;

using DeckListWeb.Repository.Models;

namespace DeckListWeb.Repository.Interfaces
{
    public interface IBaseRepository : IDisposable
    {
        IRepository<Card> Cards { get; }
        IRepository<Deck> Decks { get; }
        Task Save();
    }
}
