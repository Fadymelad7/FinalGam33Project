using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Entites.StoreEntites
{
    public class Wishlist:BaseEntity
    {
        public string BuyerEmail { get; set; }
        public ICollection<WishlistItem> Items { get; set; }=new HashSet<WishlistItem>();
    }
}
