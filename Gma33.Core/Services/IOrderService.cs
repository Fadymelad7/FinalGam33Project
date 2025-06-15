using Gma33.Core.Entites.IdentityEntites;
using Gma33.Core.Entites.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Core.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string BuyerEmail, int DeliveryMethodId, OrderAddress ShippingAddress);

        Task<IReadOnlyList<Order>> GetOrderForSpecificUser(string BuyerEmail);

        Task<Order> GetOrderById(int OrderId, string BuyerEmail);
    }
}
