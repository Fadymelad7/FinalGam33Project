using AutoMapper;
using GAM33.Dtos;
using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GAM33.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistRepo _WishlistService;
        private readonly IMapper _mapper;

        public WishlistController(IWishlistRepo WishlistService, IMapper mapper)
        {
            _WishlistService = WishlistService;
            this._mapper = mapper;
        }
        private string GetUserEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email);
        }
        [HttpPost]
        public async Task<ActionResult<WishlistDto>> CreateOrUpdateWishlist(WishlistDto dto)
        {
            var email = GetUserEmail();
            if (email is null)
            {
                return Unauthorized("User is not authenticated");
            }

            var wishlist = new Wishlist
            {
                BuyerEmail = email,
                Items = dto.WishlistItems.Select(cp => new WishlistItem
                {
                    ProductId = cp.ProductId,
                    Quantity = cp.Quantity
                }).ToList()
            };

            var result = await _WishlistService.CreateOrUpdateWishlistAsync(wishlist);

            var wishlistDto = _mapper.Map<WishlistDto>(result); // This is OK because Cart -> CartDto mapping is supported

            return Ok(wishlistDto);
        }

        [HttpGet]
        public async Task<ActionResult<WishlistDto>> GetWishlist()
        {
            var email = GetUserEmail();
            var Wishlist = await _WishlistService.GetWishlistAsync(email);
            if (Wishlist == null) return NotFound();

            // Map to DTO if needed
            var WishlistDto = _mapper.Map<WishlistDto>(Wishlist);

            return Ok(WishlistDto);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart()
        {
            var email = GetUserEmail();
            var deleted = await _WishlistService.DeleteWishlistAsync(email);
            if (!deleted) return NotFound();
            return Ok("Wishlist Deleted");
        }


    }
}
