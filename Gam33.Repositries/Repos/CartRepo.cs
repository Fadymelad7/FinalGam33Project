using Gam33.Repositries.Data;
using Gma33.Core.Entites.StoreEntites;
using Gma33.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gam33.Repositries.Repos
{
    public class CartRepo : ICartRepo
    {
        private readonly StoreContext _storeContext;

        public CartRepo(StoreContext storeContext)
        {
            this._storeContext = storeContext;
        }
        public async Task<Cart> CreateOrUpdateCartAsync(Cart cart)
        {
            var existingCart = await _storeContext.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.UserEmail == cart.UserEmail);

            if (existingCart == null)
            {
                // Add new cart along with its CartProducts
                await _storeContext.Carts.AddAsync(cart);
            }
            else
            {
                // Remove existing CartProducts
                _storeContext.cartProducts.RemoveRange(existingCart.CartProducts);

                // Add new CartProducts individually
                existingCart.CartProducts = new List<CartProduct>();

                foreach (var cp in cart.CartProducts)
                {
                    existingCart.CartProducts.Add(new CartProduct
                    {
                        ProductId = cp.ProductId,
                        Quantity = cp.Quantity,
                        // Do NOT assign Product navigation property here
                    });
                }

                // Update other properties on existingCart if needed (e.g., UserEmail, date, etc.)
                existingCart.UserEmail = cart.UserEmail;

                _storeContext.Carts.Update(existingCart);
            }

            await _storeContext.SaveChangesAsync();

            // Return the updated cart loaded from DB with related data (optional but recommended)
            return await _storeContext.Carts
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                    
                .FirstOrDefaultAsync(c => c.UserEmail == cart.UserEmail);
        }


        public async Task<bool> DeleteCartAsync(string userEmail)
        {
            var cart = await _storeContext.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.UserEmail == userEmail);

            if (cart == null)
                return false;

            _storeContext.cartProducts.RemoveRange(cart.CartProducts); // Delete cart items
            _storeContext.Carts.Remove(cart);                          // Delete the cart itself

            await _storeContext.SaveChangesAsync();
            return true;
        }

        public async Task<Cart?> GetCartAsync(string userEmail)
        {
            return await _storeContext.Carts
           .Include(c => c.CartProducts)
               .ThenInclude(cp => cp.Product)
           .FirstOrDefaultAsync(c => c.UserEmail == userEmail);
        }
    }
}
