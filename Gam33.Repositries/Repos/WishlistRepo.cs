using Gam33.Repositries.Data;
using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Interfaces;
using Gma33.Core.UnitOfWorkInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.Repos
{
    public class WishlistRepo : IWishlistRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly StoreContext _storeContext;

        public WishlistRepo(IUnitOfWork unitOfWork, StoreContext storeContext)
        {
            this._unitOfWork = unitOfWork;
            this._storeContext = storeContext;
        }
        public async Task<Wishlist> CreateOrUpdateWishlistAsync(Wishlist wishlist)
        {
            var existingWishlist = await _storeContext.Wishlists.Include(w => w.Items)
                                   .FirstOrDefaultAsync(w => w.BuyerEmail == wishlist.BuyerEmail);

            if (existingWishlist == null)
            {
                await _storeContext.Wishlists.AddAsync(wishlist);
            }
            else
            {
                // Remove existing CartProducts
                _storeContext.WishlistItems.RemoveRange(existingWishlist.Items);

                // Add new CartProducts individually
                existingWishlist.Items = new List<WishlistItem>();

                foreach (var cp in wishlist.Items)
                {
                    existingWishlist.Items.Add(new WishlistItem
                    {
                        ProductId = cp.ProductId,
                        Quantity = cp.Quantity,
                        // Do NOT assign Product navigation property here
                    });
                }

                // Update other properties on existingCart if needed (e.g., UserEmail, date, etc.)
                existingWishlist.BuyerEmail = wishlist.BuyerEmail;

                _storeContext.Wishlists.Update(existingWishlist);
            }

            await _unitOfWork.CompleteAsync();

            // Return the updated cart loaded from DB with related data (optional but recommended)
            return await _storeContext.Wishlists
                .Include(c => c.Items)
                    .ThenInclude(cp => cp.Product)

                .FirstOrDefaultAsync(c => c.BuyerEmail == wishlist.BuyerEmail);
        }


        public async Task<bool> DeleteWishlistAsync(string buyerEmail)
        {
            var Wishlist = await _storeContext.Wishlists
                 .Include(c => c.Items)
                 .FirstOrDefaultAsync(c => c.BuyerEmail == buyerEmail);

            if (Wishlist == null)
                return false;

            _storeContext.WishlistItems.RemoveRange(Wishlist.Items); // Delete cart items
            _storeContext.Wishlists.Remove(Wishlist);                          // Delete the cart itself

            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<Wishlist> GetWishlistAsync(string buyerEmail)
        {
            return await _storeContext.Wishlists
           .Include(c => c.Items)
               .ThenInclude(cp => cp.Product)
           .FirstOrDefaultAsync(c => c.BuyerEmail == buyerEmail);
        }
    }
}
