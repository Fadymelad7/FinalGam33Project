using Gam33.Repositries.Data;
using Gam33.Repositries.Repos;
using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Interfaces;
using Gma33.Core.UnitOfWorkInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable _Repostries;
        public UnitOfWork(StoreContext storeContext)
        {
            _Repostries = new Hashtable();
            this._storeContext = storeContext;
        }
        public async Task<int> CompleteAsync()
        {
            return await _storeContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _storeContext.DisposeAsync();
        }

        public IGenaricRepo<T> Repostries<T>() where T : BaseEntity
        {
            var type = typeof(T).Name;
            if (!_Repostries.ContainsKey(type))
            {
                var Repo = new GenaricRepo<T>(_storeContext);
                _Repostries.Add(type, Repo);
            }
            return _Repostries[type] as IGenaricRepo<T>;

        }
    }
}
