using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Specfication;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.SpecficationEvaluate
{
    public class SpecficationEvaluation<T> where T : BaseEntity
    {

        public static IQueryable<T> CreateQuery(IQueryable<T> InteryPoint, ISpecfication<T> specfication)
        {
            //_dbcontext.set<T> 

            var Query = InteryPoint;

            if (specfication.Critria is not null)
            {
                Query = Query.Where(specfication.Critria);
            }
            if (specfication.IsRandomOrder)
            {
                Query = Query.OrderBy(x => Guid.NewGuid());
            }
            else
            {
                if (specfication.OrderBy is not null)
                {
                    Query = Query.OrderBy(specfication.OrderBy);
                }

                if (specfication.OrderByDesc is not null)
                {
                    Query = Query.OrderByDescending(specfication.OrderByDesc);
                }
            }
            Query = Query.OrderBy(x => Guid.NewGuid());

            if (specfication.EnablePagination == true)
            {
                Query = Query.Skip(specfication.Skip).Take(specfication.Take);
            }



            Query = specfication.Includes.Aggregate(Query, (currentQuery, NextQuery) => currentQuery.Include(NextQuery));


            return Query;
        }
    }
}
