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

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecfication<T> specfication);

        Task<int> getCountAsync(ISpecfication<T> specifications);

    }
}
