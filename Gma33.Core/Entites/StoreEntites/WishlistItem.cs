using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Entites.StoreEntites
{
    public class WishlistItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int WishlistId { get; set; }
        public Wishlist Wishlist { get; set; }
        public int Quantity { get; set; }

    }
}
