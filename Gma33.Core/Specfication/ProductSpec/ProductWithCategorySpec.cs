using Gma33.Core.Entites.StoreEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Specfication.ProductSpec
{
    public class ProductWithCategorySpec : BaseSpecfication<Product>
    {
        public ProductWithCategorySpec(ProductSpecPrams prams) : base(
            p=>
            (string.IsNullOrEmpty(prams.Product) ||p.ProductName.Contains(prams.Product))
            &&
            (string.IsNullOrEmpty(prams.Category)||prams.Category==p.Category.Name)
            )
        {
            Includes.Add(p => p.Category);
            if (!string.IsNullOrEmpty(prams.sort))
            {
                switch (prams.sort)
                {
                    case "PriceAsc":

                        AddOrderBy(p => p.Price);
                        break;

                    case "PriceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;

                    default:
                        AddOrderBy(p => p.ProductName);
                        break;
                }
            }
           IsPagination((prams.PageIndex-1)*prams.pagesize,prams.pagesize);
        }
    }
}
