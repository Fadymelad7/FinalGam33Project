using Gam33.Repositries.Data;
using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Interfaces;
using Gma33.Core.Specfication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.Repos
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : BaseEntity
    {
        private readonly StoreContext _context;

        public GenaricRepo(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecfication<T> specfication)
        {
            return await SpecficationEvaluate.SpecficationEvaluation<T>.CreateQuery(_context.Set<T>(), specfication).ToListAsync();
        }


        public async Task<T?> GetAsync(int id)
        {
            /// we Delete it because we use Specification Design Pattern and we will build it Dynamic
            ///if (typeof(T) == typeof(Product))
            ///{
            ///    return (IEnumerable<T>)await _context.Set<Product>().Include(P => P.Brand).Include(p => p.Category).ToListAsync();
            ///}

            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<int> getCountAsync(ISpecfication<T> specifications)
        {

            return await SpecficationEvaluate.SpecficationEvaluation<T>.CreateQuery(_context.Set<T>(), specifications).CountAsync();
        }
        public async Task AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
        }

        public void Update(T item)
        {
            _context.Set<T>().Update(item);
        }

        public void Delete(T item)
        {
            _context.Set<T>().Remove(item);
        }
    }
}
