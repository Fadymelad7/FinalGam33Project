using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Gma33.Core.Entites.StoreEntites
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string Details { get; set; }

        public string? ImageUrl { get; set; }
        // public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        //public int AdminId { get; set; } // I See the product Should belongs only to spec category not to spec Admin  
        //public Admin Admin { get; set; }


        [JsonIgnore]

        public ICollection<CartProduct> CartProducts { get; set; } = new HashSet<CartProduct>();
        [JsonIgnore]
        public ICollection<WishlistItem> WishlistItems { get; set; } = new HashSet<WishlistItem>();

    }
}
