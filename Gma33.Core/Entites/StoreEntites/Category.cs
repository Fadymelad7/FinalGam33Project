using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Entites.StoreEntites
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
    }
}
