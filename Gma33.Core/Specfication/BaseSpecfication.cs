using Gma33.Core.Entites.StoreEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Specfication
{
    public class BaseSpecfication<T> : ISpecfication<T> where T : BaseEntity
    {
        #region Signatures
        public Expression<Func<T, bool>> Critria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool EnablePagination { get; set; }

        public bool IsRandomOrder { get; set; }
        #endregion



        #region constructors
        public BaseSpecfication()
        {
            Includes = new List<Expression<Func<T, object>>>();
        }
        public BaseSpecfication(Expression<Func<T, bool>> critria)
        {
            Critria = critria;

            Includes = new List<Expression<Func<T, object>>>();
        }
        #endregion

        #region Sort Methods
        public void AddOrderBy(Expression<Func<T, object>> orderby)
        {

            OrderBy = orderby;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> orderbydesc)
        {

            OrderByDesc = orderbydesc;
        }
        public void EnableRandomOrder()
        {
            IsRandomOrder = true;
        }
        #endregion

        #region Pagination Methods
        public void IsPagination(int skip, int take)
        {
            EnablePagination = true;
            Skip = skip;
            Take = take;
        }
        #endregion
    }
}
