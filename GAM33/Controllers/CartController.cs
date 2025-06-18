using AutoMapper;
using GAM33.Dtos;
using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GAM33.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepo _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartRepo cartService, IMapper mapper)
        {
            _cartService = cartService;
            this._mapper = mapper;
        }

        private string GetUserEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email);
        }

        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var email = GetUserEmail();
            var cart = await _cartService.GetCartAsync(email);
            if (cart == null) return NotFound();

            // Map to DTO if needed
            var cartDto = _mapper.Map<CartDto>(cart);

            return Ok(cartDto);
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> CreateOrUpdateCart(CartDto dto)
        {
            var email = GetUserEmail();
            if (email is null)
            {
                return Unauthorized("User is not authenticated");
            }

            var cart = new Cart
            {
                UserEmail = email,
                CartProducts = dto.CartProducts.Select(cp => new CartProduct
                {
                    ProductId = cp.ProductId,
                    Quantity = cp.Quantity
                }).ToList()
            };

            var result = await _cartService.CreateOrUpdateCartAsync(cart);

            var cartDto = _mapper.Map<CartDto>(result); // This is OK because Cart -> CartDto mapping is supported

            return Ok(cartDto);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart()
        {
            var email = GetUserEmail();
            var deleted = await _cartService.DeleteCartAsync(email);
            if (!deleted) return NotFound();
            return Ok("Cart Deleted Successfully");
        }
    }
}
