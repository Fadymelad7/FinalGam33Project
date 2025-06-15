using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Entites.StoreEntites
{
    public class Cart : BaseEntity
    {
        public string UserEmail { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; } = new HashSet<CartProduct>();
    }
}
