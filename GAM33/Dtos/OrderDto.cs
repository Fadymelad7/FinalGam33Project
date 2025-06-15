using Gma33.Core.Entites.OrderModule;

namespace GAM33.Dtos
{
    public class OrderDto
    {
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
