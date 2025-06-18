using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Specfication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Interfaces
{
    public interface IGenaricRepo<T> where T : BaseEntity
    {

        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(int id);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecfication<T> specfication);

        Task<int> getCountAsync(ISpecfication<T> specifications);

        Task<T?> GetAsyncWithSpec(ISpecfication<T> specifications);

        Task AddAsync(T item);

        void Update(T item);

        void Delete(T item);
    }
}
