using AutoMapper;
using GAM33.Dtos;
using Gma33.Core.Entites.OrderModule;
using Gma33.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GAM33.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(IMapper mapper,IOrderService orderService)
        {
            this._mapper = mapper;
            this._orderService = orderService;
        }
        [HttpPost("CreateOrder")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequest),StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var BuyerEmail=User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto, OrderAddress>(orderDto.ShippingAddress);
            var Order =await  _orderService.CreateOrderAsync(BuyerEmail, orderDto.DeliveryMethodId, MappedAddress);

            if(Order is null)
            {
                return BadRequest("There is Problem in Your Order");
            }
            return Ok(Order);


        }
    }
}
