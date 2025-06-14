using Gma33.Core.Entites.StoreEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Specfication.ProductSpec
{
    public class ProductWithCategoryAndBrandPaginationCountSpec : BaseSpecfication<Product>
    {
        public ProductWithCategoryAndBrandPaginationCountSpec(ProductSpecPrams specPrams) : base(p =>

        (string.IsNullOrEmpty(specPrams.Product)) || specPrams.Product == p.ProductName
        &&
        (string.IsNullOrEmpty(specPrams.Category)) || specPrams.Category == p.Category.Name

        )
        {

        }
    }
}
