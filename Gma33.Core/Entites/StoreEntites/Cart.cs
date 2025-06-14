using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Entites.StoreEntites
{
    public class Cart : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; } = new HashSet<CartProduct>();
    }
}
