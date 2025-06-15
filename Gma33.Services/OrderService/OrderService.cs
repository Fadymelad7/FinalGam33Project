using Gma33.Core.Entites.IdentityEntites;
using Gma33.Core.Entites.OrderModule;
using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Interfaces;
using Gma33.Core.Services;
using Gma33.Core.UnitOfWorkInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gma33.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepo _cartRepo;

        public OrderService(IUnitOfWork unitOfWork, ICartRepo cartRepo)
        {
            this._unitOfWork = unitOfWork;
            this._cartRepo = cartRepo;
        }
        public async Task<Order> CreateOrderAsync(string BuyerEmail, int DeliveryMethodId, OrderAddress ShippingAddress)
        {
            var Cart = await _cartRepo.GetCartAsync(BuyerEmail);
            if (Cart == null || !Cart.CartProducts.Any())
            {
                return null;
            }

            var OrderItems = new List<OrderItem>(); // عملت ليست من اوردر ايتم  بتاعي بحيث اني احط كل اللي جوا الباسكت جواها

            foreach (var item in Cart.CartProducts)
            {
                var product = await _unitOfWork.Repostries<Product>().GetAsync(item.ProductId);

                var OrderItemOrderd = new OrderItemOrdered(product.Id, product.ProductName, product.ImageUrl);

                var OrderItem = new OrderItem(OrderItemOrderd, item.Quantity, product.Price);

                OrderItems.Add(OrderItem);
            }
            var SubTotal = OrderItems.Sum(o => o.Quantity * o.Price);

            var DeliveryMethod = await _unitOfWork.Repostries<DeliveryMethod>().GetAsync(DeliveryMethodId);

            var order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItems, SubTotal);

            await _unitOfWork.Repostries<Order>().AddAsync(order); // Add Order Local

            var Result = await _unitOfWork.CompleteAsync();

            if (Result <= 0) return null;

            return order;
        }

        public Task<Order> CreateOrderAsync(string BuyerEmail, int DeliveryMethodId, Address ShippingAddress)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderById(int OrderId, string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrderForSpecificUser(string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
