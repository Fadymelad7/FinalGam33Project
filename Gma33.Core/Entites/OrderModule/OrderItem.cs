using Gma33.Core.Entites.StoreEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Entites.OrderModule
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(OrderItemOrdered product, int quantity, decimal price)
        {
            Product = product;
            Quantity = quantity;
            Price = price;
        }

        public OrderItemOrdered Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
