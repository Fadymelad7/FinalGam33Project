using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Specfication.ProductSpec
{
    public class ProductSpecPrams
    {
        public string? sort { get; set; }
        public string? Product { get; set; }
        public string? Category { get; set; }
        private int PageSize = 32;

        public int pagesize
        {
            get { return PageSize; }
            set { PageSize = value < 32 ? 32 : value; }
        }

        public int PageIndex { get; set; } = 1;



    }
}
