using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeckListWeb.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
        Task<T> GetByName(string name);
        Task<T> GetByNumber(int number);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task Delete(int id);
        void Add(T item);
        void Update(T item);
    }
}
