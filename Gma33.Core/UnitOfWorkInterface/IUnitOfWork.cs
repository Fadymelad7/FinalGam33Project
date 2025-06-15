using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.UnitOfWorkInterface
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task<int> CompleteAsync();

        IGenaricRepo<T> Repostries<T>() where T : BaseEntity;

    }
}
